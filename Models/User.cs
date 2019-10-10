using System.Collections.Generic;
using googlefiltermaster.ViewModels;
using GoogleFilterMaster.Models;

namespace googlefiltermaster.Models
{
  public class User
  {
    public int Id { get; set; }
    public string GoogleId { get; set; }
    public string Email { get; set; }
    public bool VerifiedEmail { get; set; }
    public string Name { get; set; }
    public string GivenName { get; set; }
    public string FamilyName { get; set; }
    public string Picture { get; set; }
    public string Locale { get; set; }
    public string Token { get; set; }
    public List<MasterFilter> MasterFilters { get; set; } = new List<MasterFilter>();
    public List<AccountsCache> AccountsCache { get; set; } = new List<AccountsCache>();

    // constructors
    public User() { }

    public User(GoogleUser _user)
    {
      this.Email = _user.email;
      this.FamilyName = _user.family_name;
      this.GivenName = _user.given_name;
      this.GoogleId = _user.id;
      this.Locale = _user.locale;
      this.Name = _user.name;
      this.Picture = _user.picture;
      this.VerifiedEmail = _user.verified_email;
    }

  }
}