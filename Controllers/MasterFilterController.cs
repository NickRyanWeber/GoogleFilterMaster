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
    public async Task<IActionResult> PutMasterFilter(int id, MasterFilter masterFilter)
    {
      if (id != masterFilter.Id)
      {
        return BadRequest();
      }

      _context.Entry(masterFilter).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!MasterFilterExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/MasterFilter
    [HttpPost]
    public async Task<ActionResult<MasterFilter>> PostMasterFilter(MasterFilter masterFilter)
    {
      _context.MasterFilter.Add(masterFilter);
      await _context.SaveChangesAsync();

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
