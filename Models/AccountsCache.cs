using System.Collections.Generic;
using googlefiltermaster.Models;

namespace GoogleFilterMaster.Models
{
  public class AccountsCache
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string GoogleAccountId { get; set; }
    public int? UserId { get; set; }
    public User User { get; set; }
    public List<FiltersCache> FiltersCache { get; set; } = new List<FiltersCache>();

    public AccountsCache() { }

  }
}