namespace GoogleFilterMaster.Models
{
  public class ExcludeDetails
  {
    public string kind { get; set; }
    public string field { get; set; }
    public string matchType { get; set; }
    public string expressionValue { get; set; }
    public bool caseSensitive { get; set; }
  }
}