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
using System.Net;
using System.IO;
using Newtonsoft.Json;
using googlefiltermaster.Models;
using googlefiltermaster;
using Microsoft.EntityFrameworkCore;
using googlefiltermaster.ViewModels;
using GoogleFilterMaster.Models;
using System.Net.Http;
using System.Text;

namespace GoogleFilterMaster.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TestController : Controller
  {
    private DatabaseContext context;

    public TestController(DatabaseContext _context)
    {
      this.context = _context;
    }

    // [HttpGet]
    // [Authorize]
    // public async Task<ActionResult> GetMasterFilters()
    // {
    //   var googleId = User.Claims.First(f => f.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
    //   // Will need to include Master Filter and Selected Filter Data
    //   // EX - ...User.Include(i => i.AccountsCache).ThenInclude(t => t.FiltersCache)Include(MASTER FILTER INFO).ThenInclude(SELECTED FILTER INFO).FirstOrDefaultAsync(...
    //   var user = await context.User.Include(i => i.AccountsCache).ThenInclude(t => t.FiltersCache).FirstOrDefaultAsync(u => u.GoogleId == googleId);

    //   if (user == null)
    //   {
    //     return BadRequest();
    //   }
    //   else
    //   {
    //     return Ok(user);
    //   }
    // }

    //   [HttpPut("{id}")]
    //   public async Task<ActionResult> UpdateMasterFilter(int id, MasterFilter masterFilter)
    //   {
    //     var foundFilter = context.MasterFilter.FirstOrDefault(m => m.Id == id);
    //     List<SelectedFilter> selectedFilter;
    //     if (foundFilter == null)
    //     {
    //       selectedFilter = masterFilter.SelectedFilter;
    //       masterFilter.SelectedFilter = null;
    //       context.MasterFilter.Add(masterFilter);
    //       await context.SaveChangesAsync();
    //       foundFilter = masterFilter;

    //     }
    //     else
    //     {
    //       // Update Database
    //       // Name
    //       foundFilter.Name = masterFilter.Name;
    //       // Value
    //       foundFilter.FilterValue = masterFilter.FilterValue;
    //       await context.SaveChangesAsync();
    //       selectedFilter = foundFilter.SelectedFilter;
    //       //   // Delete All Selected Filters for the MasterFilter, and create new ones from the object
    //       var selectedFiltersToBeDeleted = context.SelectedFilter.Where(w => w.MasterFilterId == foundFilter.Id);
    //       context.SelectedFilter.RemoveRange(selectedFiltersToBeDeleted);
    //       await context.SaveChangesAsync();

    //     }
    //     var user = context.User.FirstOrDefault(f => f.Id == masterFilter.UserId);
    //     var accessToken = user.Token;
    //     await context.SaveChangesAsync();

    //     foreach (var filter in selectedFilter)
    //     {

    //       // update database
    //       var _filter = new SelectedFilter
    //       {
    //         GoogleAccountId = filter.GoogleAccountId,
    //         GoogleFilterId = filter.GoogleFilterId,
    //         GoogleAccountName = filter.GoogleAccountName,
    //         GoogleFilterName = filter.GoogleFilterName,
    //         MasterFilterId = foundFilter.Id
    //       };
    //       await context.SelectedFilter.AddAsync(_filter);
    //       await context.SaveChangesAsync();

    //       // get filter object from Google
    //       var accountId = filter.GoogleAccountId;
    //       var filterId = filter.GoogleFilterId;
    //       var API = $"https://www.googleapis.com/analytics/v3/management/accounts/{accountId}/filters/{filterId}";
    //       HttpClient getFilterClient = new HttpClient();
    //       getFilterClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
    //       getFilterClient.DefaultRequestHeaders.Add("Accept", "application/json");
    //       HttpResponseMessage response = await getFilterClient.GetAsync(API);
    //       var content = await response.Content.ReadAsStringAsync();
    //       var data = JsonConvert.DeserializeObject<RootObject>(content);

    //       // Update Filter Value
    //       data.excludeDetails.expressionValue = masterFilter.FilterValue;

    //       // Update Google
    //       HttpClient postFilterClient = new HttpClient();
    //       postFilterClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
    //       postFilterClient.DefaultRequestHeaders.Add("Accept", "application/json");
    //       // postFilterClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
    //       var postContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

    //       HttpResponseMessage putResponse = await postFilterClient.PutAsync(API, postContent);
    //     }
    //     return Ok(masterFilter);
    //   }
    // }

  }
}





// CODE TO PULL FROM

// Stuff

// this is the path that is copied when trying to find the credential token
//   var nwThing = ((Google.Apis.Auth.OAuth2.GoogleCredential.AccessTokenCredential)cred.credential)._accessToken

//   var thing = await auth.GetCurrentScopesAsync();

//   var accounts = await service.Management.Accounts.List().ExecuteAsync();

//   var account = accounts.Items.First();

// Delete ultimately
//   var accountId = "118875512";

//   var properties = await service.Management.Webproperties.List(accountId).ExecuteAsync();

// Delete ultimately
// var propertyId = "UA-118875512-1";


//   var filters = await service.Management.Filters.List(accountId).ExecuteAsync();

//   var matchingFilter = filters.Items.First();

//   matchingFilter.ExcludeDetails.ExpressionValue = "8.8.8.8";

// service.Management.Filters.Update(matchingFilter, accountId, matchingFilter.Id).Execute();

//filter.ExcludeDetails.Field = "GEO_IP_ADDRESS";
//filter.

//   var accountNames = accounts.Items.Select(x => x.Name).ToList();
//   return View("Index");
// return Redirect("/app");