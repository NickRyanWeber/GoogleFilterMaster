/*
Copyright 2018 Google Inc

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    https://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using Google.Apis.Services;
using Google.Apis.Analytics.v3;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.AspNetCore;
using Google.Apis.Auth;
using Google.Apis.Analytics.v3.Data;

namespace googlefiltermaster.Controllers
{
  public class HomeController : Controller
  {
    // No authorization required. User doesn't need to login to see this.
    public IActionResult Index()
    {
      return View();
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      // Must not redirect after sign-out; otherwise user is not signed-out.
      // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-2.2&tabs=visual-studio#scaffold-register-login-and-logout
      return View();
    }

    // Authentication required, but no specific scopes
    [Authorize]
    public async Task<IActionResult> ScopeListing([FromServices] IGoogleAuthProvider auth)
    {
      return View(await auth.GetCurrentScopesAsync());
    }

    // Test showing use of incremental auth.
    // This attribute states that the listed scope(s) must be authorized in the handler.
    [GoogleScopedAuthorize(AnalyticsService.ScopeConstants.Analytics)]
    [GoogleScopedAuthorize(AnalyticsService.ScopeConstants.AnalyticsEdit)]
    public async Task<IActionResult> AnalyticsListing([FromServices] IGoogleAuthProvider auth)
    {
      var cred = await auth.GetCredentialAsync();
      var service = new AnalyticsService(new BaseClientService.Initializer
      {
        HttpClientInitializer = cred
      });

      var thing = await auth.GetCurrentScopesAsync();

      var accounts = await service.Management.Accounts.List().ExecuteAsync();

      var account = accounts.Items.First();


      var accountId = "118875512";

      // var properties = await service.Management.Webproperties.List(accountId).ExecuteAsync();

      //var propertyId = "UA-118875512-1";


      var filters = await service.Management.Filters.List(accountId).ExecuteAsync();

      var matchingFilter = filters.Items.First();

      matchingFilter.ExcludeDetails.ExpressionValue = "8.8.8.8";

      // service.Management.Filters.Update(matchingFilter, accountId, matchingFilter.Id).Execute();

      //filter.ExcludeDetails.Field = "GEO_IP_ADDRESS";
      //filter.

      var accountNames = accounts.Items.Select(x => x.Name).ToList();
      return View(accountNames);
    }






    // The below one will be my endpoint for verifying a use

    [Authorize]
    public async Task<IActionResult> ShowTokens()
    {
      var auth = await HttpContext.AuthenticateAsync();
      var idToken = auth.Properties.GetTokenValue(OpenIdConnectParameterNames.IdToken);
      string idTokenValid, idTokenIssued, idTokenExpires;
      try
      {
        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
        idTokenValid = "true";
        idTokenIssued = new DateTime(1970, 1, 1).AddSeconds(payload.IssuedAtTimeSeconds.Value).ToString();
        idTokenExpires = new DateTime(1970, 1, 1).AddSeconds(payload.ExpirationTimeSeconds.Value).ToString();
      }
      catch (Exception e)
      {
        idTokenValid = $"false: {e.Message}";
        idTokenIssued = "invalid";
        idTokenExpires = "invalid";
      }
      var accessToken = auth.Properties.GetTokenValue(OpenIdConnectParameterNames.AccessToken);
      var refreshToken = auth.Properties.GetTokenValue(OpenIdConnectParameterNames.RefreshToken);
      var accessTokenExpiresAt = auth.Properties.GetTokenValue("expires_at");
      var cookieIssuedUtc = auth.Properties.IssuedUtc?.ToString() ?? "<missing>";
      var cookieExpiresUtc = auth.Properties.ExpiresUtc?.ToString() ?? "<missing>";

      return View(new[]
      {
                $"Id Token: '{idToken}'",
                $"Id Token valid: {idTokenValid}",
                $"Id Token Issued UTC: '{idTokenIssued}'",
                $"Id Token Expires UTC: '{idTokenExpires}'",
                $"Access Token: '{accessToken}'",
                $"Refresh Token: '{refreshToken}'",
                $"Access token expires at: '{accessTokenExpiresAt}'",
                $"Cookie Issued UTC: '{cookieIssuedUtc}'",
                $"Cookie Expires UTC: '{cookieExpiresUtc}'",
            });
    }

    public class ForceTokenRefreshModel
    {
      public IReadOnlyList<string> Results;
      public string AccessToken;
    }

    [Authorize]
    public async Task<IActionResult> ForceTokenRefresh([FromServices] IGoogleAuthProvider auth)
    {
      var authResult0 = await HttpContext.AuthenticateAsync();
      var accessToken0 = authResult0.Properties.GetTokenValue(OpenIdConnectParameterNames.AccessToken);
      var refreshToken0 = authResult0.Properties.GetTokenValue(OpenIdConnectParameterNames.RefreshToken);
      var issuedUtc0 = authResult0.Properties.IssuedUtc?.ToString() ?? "<missing>";
      var expiresUtc0 = authResult0.Properties.ExpiresUtc?.ToString() ?? "<missing>";

      // Force token refresh by specifying a too-long refresh window.
      var cred = await auth.GetCredentialAsync(TimeSpan.FromHours(24));

      var authResult1 = await HttpContext.AuthenticateAsync();
      var accessToken1 = authResult1.Properties.GetTokenValue(OpenIdConnectParameterNames.AccessToken);
      var refreshToken1 = authResult1.Properties.GetTokenValue(OpenIdConnectParameterNames.RefreshToken);
      var issuedUtc1 = authResult1.Properties.IssuedUtc?.ToString() ?? "<missing>";
      var expiresUtc1 = authResult1.Properties.ExpiresUtc?.ToString() ?? "<missing>";

      var credAccessToken = await cred.UnderlyingCredential.GetAccessTokenForRequestAsync();

      var accessTokenChanged = accessToken0 != accessToken1;
      var credHasCorrectAccessToken = credAccessToken == accessToken1;

      var pass = accessTokenChanged && credHasCorrectAccessToken;

      var model = new ForceTokenRefreshModel
      {
        Results = new[]
          {
                    $"Before Access Token: '{accessToken0}'",
                    $"Before Refresh Token: '{refreshToken0}'",
                    $"Before Issued UTC: '{issuedUtc0}'",
                    $"Before Expires UTC: '{expiresUtc0}'",
                    $"After Access Token: '{accessToken1}'",
                    $"After Refresh Token: '{refreshToken1}'",
                    $"After Issued UTC: '{issuedUtc1}'",
                    $"After Expires UTC: '{expiresUtc1}'",
                    $"Access token changed: {accessTokenChanged}",
                    $"Credential has correct access token: {credHasCorrectAccessToken}",
                    $"Pass: {pass}"
                },
        AccessToken = accessToken1
      };
      return View(model);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ForceTokenRefreshCheckPersisted([FromServices] IGoogleAuthProvider auth, [FromForm] string expectedAccessToken)
    {
      // Check that the refreshed access token is correctly persisted across requests.
      var cred = await auth.GetCredentialAsync();
      var credAccessToken = await cred.UnderlyingCredential.GetAccessTokenForRequestAsync();
      var pass = credAccessToken == expectedAccessToken;
      return View(new[]
      {
                $"Expected access token: '{expectedAccessToken}'",
                $"Credential access token: '{credAccessToken}'",
                $"Pass: {pass}"
            });
    }
  }
}
