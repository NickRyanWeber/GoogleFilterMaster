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

    [HttpGet]
    [Authorize]
    public async Task<ActionResult> GetMasterFilters()
    {
      var googleId = User.Claims.First(f => f.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
      // Will need to include Master Filter and Selected Filter Data
      // EX - ...User.Include(i => i.AccountsCache).ThenInclude(t => t.FiltersCache)Include(MASTER FILTER INFO).ThenInclude(SELECTED FILTER INFO).FirstOrDefaultAsync(...
      var user = await context.User.Include(i => i.AccountsCache).ThenInclude(t => t.FiltersCache).FirstOrDefaultAsync(u => u.GoogleId == googleId);

      if (user == null)
      {
        return BadRequest();
      }
      else
      {
        return Ok(user);
      }
    }
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