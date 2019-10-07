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

    public AccountsCache() { }

  }
}