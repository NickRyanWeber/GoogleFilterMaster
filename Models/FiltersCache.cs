namespace GoogleFilterMaster.Models
{
  public class FiltersCache
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string GoogleFilterId { get; set; }
    public string FilterValue { get; set; }
    public int? AccountsCacheId { get; set; }
    public AccountsCache AccountsCache { get; set; }

    public FiltersCache() { }
  }
}