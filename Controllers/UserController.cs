using Google.Apis.Services;
using Google.Apis.Analytics.v3;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Google.Apis.Auth.AspNetCore;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using googlefiltermaster.Models;
using googlefiltermaster;
using Microsoft.EntityFrameworkCore;
using googlefiltermaster.ViewModels;
using System.Linq;
using GoogleFilterMaster.Models;

namespace GoogleFilterMaster.Controllers
{
  public class UserController : Controller
  {
    // Defines the thing (lol) so I don't have to do it for each endpoint
    private DatabaseContext context;

    public UserController(DatabaseContext _context)
    {
      this.context = _context;
    }
    [GoogleScopedAuthorize(AnalyticsService.ScopeConstants.AnalyticsEdit, AnalyticsService.ScopeConstants.Analytics)]
    public async Task<IActionResult> Login([FromServices] IGoogleAuthProvider auth)
    {
      var cred = await auth.GetCredentialAsync();
      var service = new AnalyticsService(new BaseClientService.Initializer
      {
        HttpClientInitializer = cred
      });

      var accessToken = await cred.UnderlyingCredential.GetAccessTokenForRequestAsync();
      WebRequest request = WebRequest.Create($"https://www.googleapis.com/oauth2/v1/userinfo?access_token={accessToken}");
      HttpWebResponse response = (HttpWebResponse)request.GetResponse();
      Stream dataStream = response.GetResponseStream();
      StreamReader reader = new StreamReader(dataStream);
      string responseFromServer = reader.ReadToEnd();
      var data = JsonConvert.DeserializeObject<GoogleUser>(responseFromServer);

      var user = await context.User.FirstOrDefaultAsync(u => u.GoogleId == data.id);
      if (user == null)
      {
        user = new User(data);
        await context.User.AddAsync(user);
        await context.SaveChangesAsync();
      }

      // clear the "cache"
      // MATCHING USER ID && ACCOUNT ID
      var accountsToDelete = context.AccountsCache.Where(w => w.UserId == user.Id);
      foreach (var account in accountsToDelete)
      {
        var filtersToDelete = context.FiltersCache.Where(w => w.AccountsCacheId == account.Id);
        context.FiltersCache.RemoveRange(filtersToDelete);
      }
      context.AccountsCache.RemoveRange(accountsToDelete);
      await context.SaveChangesAsync();

      var accounts = await service.Management.Accounts.List().ExecuteAsync();
      foreach (var account in accounts.Items)
      {
        var _account = new AccountsCache { GoogleAccountId = account.Id, Name = account.Name, UserId = user.Id };
        // save the account
        await context.AccountsCache.AddAsync(_account);
        // get the filters
        var filters = await service.Management.Filters.List(account.Id).ExecuteAsync();
        // save the filters
        var excludeFilters = filters.Items.Where(w => w.ExcludeDetails != null);
        foreach (var filter in excludeFilters)
        {
          var _filter = new FiltersCache { Name = filter.Name, GoogleFilterId = filter.Id, FilterValue = filter.ExcludeDetails.ExpressionValue, AccountsCacheId = _account.Id };
          await context.FiltersCache.AddAsync(_filter);
        }
      }
      await context.SaveChangesAsync();


      return Redirect("/app");
    }
  }
}
