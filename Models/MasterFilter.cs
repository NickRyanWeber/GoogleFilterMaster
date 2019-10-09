using System.Collections.Generic;
using GoogleFilterMaster.Models;

namespace googlefiltermaster.Models
{
  public class MasterFilter
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string FilterValue { get; set; }
    public int? UserId { get; set; }
    public User User { get; set; }
    public List<SelectedFilter> SelectedFilter { get; set; } = new List<SelectedFilter>();
  }
}