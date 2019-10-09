using googlefiltermaster.Models;

namespace GoogleFilterMaster.Models
{
  public class SelectedFilter
  {
    public int Id { get; set; }
    public string GoogleAccountId { get; set; }
    public string GoogleFilterId { get; set; }
    public string GoogleAccountName { get; set; }
    public string GoogleFilterName { get; set; }
    public int? MasterFilterId { get; set; }
    public MasterFilter MasterFilter { get; set; }
  }
}