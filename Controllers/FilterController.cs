using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using googlefiltermaster;
using Microsoft.EntityFrameworkCore;

namespace GoogleFilterMaster.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FilterController : Controller
  {
    private DatabaseContext context;

    public FilterController(DatabaseContext _context)
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
      var user = await context.User.Include(i => i.AccountsCache).ThenInclude(t => t.FiltersCache).Include(m => m.MasterFilters).ThenInclude(s => s.SelectedFilter).FirstOrDefaultAsync(u => u.GoogleId == googleId);

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
