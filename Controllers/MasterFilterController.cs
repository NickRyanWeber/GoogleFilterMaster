using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using googlefiltermaster;
using googlefiltermaster.Models;
using GoogleFilterMaster.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace sdg_react_template.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MasterFilterController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public MasterFilterController(DatabaseContext context)
    {
      _context = context;
    }

    // GET: api/MasterFilter
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MasterFilter>>> GetMasterFilter()
    {
      return await _context.MasterFilter.ToListAsync();
    }

    // GET: api/MasterFilter/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MasterFilter>> GetMasterFilter(int id)
    {
      var masterFilter = await _context.MasterFilter.FindAsync(id);

      if (masterFilter == null)
      {
        return NotFound();
      }

      return masterFilter;
    }

    // PUT: api/MasterFilter/5
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMasterFilter(int id, MasterFilter masterFilter)
    {
      var foundFilter = _context.MasterFilter.FirstOrDefault(m => m.Id == id);
      List<SelectedFilter> selectedFilter;
      if (foundFilter == null)
      {
        selectedFilter = masterFilter.SelectedFilter;
        masterFilter.SelectedFilter = null;
        _context.MasterFilter.Add(masterFilter);
        await _context.SaveChangesAsync();
        foundFilter = masterFilter;

      }
      else
      {
        // Update Database
        // Name
        foundFilter.Name = masterFilter.Name;
        // Value
        foundFilter.FilterValue = masterFilter.FilterValue;
        await _context.SaveChangesAsync();
        selectedFilter = foundFilter.SelectedFilter;
        //   // Delete All Selected Filters for the MasterFilter, and create new ones from the object
        var selectedFiltersToBeDeleted = _context.SelectedFilter.Where(w => w.MasterFilterId == foundFilter.Id);
        _context.SelectedFilter.RemoveRange(selectedFiltersToBeDeleted);
        await _context.SaveChangesAsync();

      }
      var user = _context.User.FirstOrDefault(f => f.Id == masterFilter.UserId);
      var accessToken = user.Token;
      await _context.SaveChangesAsync();

      foreach (var filter in selectedFilter)
      {

        // update database
        var _filter = new SelectedFilter
        {
          GoogleAccountId = filter.GoogleAccountId,
          GoogleFilterId = filter.GoogleFilterId,
          GoogleAccountName = filter.GoogleAccountName,
          GoogleFilterName = filter.GoogleFilterName,
          MasterFilterId = foundFilter.Id
        };
        await _context.SelectedFilter.AddAsync(_filter);
        await _context.SaveChangesAsync();

        // get filter object from Google
        var accountId = filter.GoogleAccountId;
        var filterId = filter.GoogleFilterId;
        var API = $"https://www.googleapis.com/analytics/v3/management/accounts/{accountId}/filters/{filterId}";
        HttpClient getFilterClient = new HttpClient();
        getFilterClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
        getFilterClient.DefaultRequestHeaders.Add("Accept", "application/json");
        HttpResponseMessage response = await getFilterClient.GetAsync(API);
        var content = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<RootObject>(content);

        // Update Filter Value
        data.excludeDetails.expressionValue = masterFilter.FilterValue;

        // Update Google
        HttpClient postFilterClient = new HttpClient();
        postFilterClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
        postFilterClient.DefaultRequestHeaders.Add("Accept", "application/json");
        // postFilterClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
        var postContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        HttpResponseMessage putResponse = await postFilterClient.PutAsync(API, postContent);
      }
      return Ok(masterFilter);
    }


    // POST: api/MasterFilter
    [HttpPost]
    public async Task<ActionResult<MasterFilter>> PostMasterFilter(MasterFilter masterFilter)
    {
      _context.MasterFilter.Add(masterFilter);
      await _context.SaveChangesAsync();

      // Update Google
      var user = _context.User.FirstOrDefault(f => f.Id == masterFilter.UserId);
      var accessToken = user.Token;
      foreach (var filter in masterFilter.SelectedFilter)
      {
        // get filter object from Google
        var accountId = filter.GoogleAccountId;
        var filterId = filter.GoogleFilterId;
        var API = $"https://www.googleapis.com/analytics/v3/management/accounts/{accountId}/filters/{filterId}";
        HttpClient getFilterClient = new HttpClient();
        getFilterClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
        getFilterClient.DefaultRequestHeaders.Add("Accept", "application/json");
        HttpResponseMessage response = await getFilterClient.GetAsync(API);
        var content = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<RootObject>(content);

        // Update Filter Value
        data.excludeDetails.expressionValue = masterFilter.FilterValue;

        // Update Google
        HttpClient postFilterClient = new HttpClient();
        postFilterClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
        postFilterClient.DefaultRequestHeaders.Add("Accept", "application/json");
        // postFilterClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
        var postContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        HttpResponseMessage putResponse = await postFilterClient.PutAsync(API, postContent);
      }

      return CreatedAtAction("GetMasterFilter", new { id = masterFilter.Id }, masterFilter);
    }

    [HttpPost("{id}/SelectedFilter")]
    public async Task<ActionResult<MasterFilter>> PostSelectedFilter(int id, SelectedFilter selectedFilter)
    {
      var masterFilter = _context.MasterFilter.FirstOrDefault(m => m.Id == id);
      if (masterFilter == null)
      {
        return NotFound();
      }
      else
      {
        selectedFilter.MasterFilterId = id;
        _context.SelectedFilter.Add(selectedFilter);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMasterFilter", new { id = selectedFilter.Id }, selectedFilter);
      }
    }

    // DELETE: api/MasterFilter/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<MasterFilter>> DeleteMasterFilter(int id)
    {
      var masterFilter = await _context.MasterFilter.FindAsync(id);
      if (masterFilter == null)
      {
        return NotFound();
      }
      var selectedFiltersToBeDeleted = _context.SelectedFilter.Where(w => w.MasterFilterId == masterFilter.Id);
      _context.SelectedFilter.RemoveRange(selectedFiltersToBeDeleted);
      _context.MasterFilter.Remove(masterFilter);
      await _context.SaveChangesAsync();

      return masterFilter;
    }

    private bool MasterFilterExists(int id)
    {
      return _context.MasterFilter.Any(e => e.Id == id);
    }
  }
}