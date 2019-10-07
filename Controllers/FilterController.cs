using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Analytics.v3;
using Google.Apis.Analytics.v3.Data;
using Google.Apis.Auth.AspNetCore;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;

namespace googlefiltermaster.Controllers
{

  [ApiController]
  public class FilterController : ControllerBase
  {

    [GoogleScopedAuthorize(AnalyticsService.ScopeConstants.AnalyticsEdit)]
    [Route("/api/accounts/{accountId}/filters")]
    [HttpGet]
    public async System.Threading.Tasks.Task<ActionResult<List<Filter>>> GetAsync([FromServices] IGoogleAuthProvider auth, string accountId)
    {

      var cred = await auth.GetCredentialAsync();
      var service = new AnalyticsService(new BaseClientService.Initializer
      {
        HttpClientInitializer = cred
      });

      var filters = await service.Management.Filters.List(accountId).ExecuteAsync();

      return filters.Items.ToList();
    }
  }
}

