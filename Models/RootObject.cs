using System;

namespace GoogleFilterMaster.Models
{
  public class RootObject
  {
    public string id { get; set; }
    public string kind { get; set; }
    public string selfLink { get; set; }
    public string accountId { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public DateTime created { get; set; }
    public DateTime updated { get; set; }
    public ParentLink parentLink { get; set; }
    public ExcludeDetails excludeDetails { get; set; }
  }
}