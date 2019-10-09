using System;
using System.Text.RegularExpressions;
using googlefiltermaster.Models;
using GoogleFilterMaster.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace googlefiltermaster
{
  public partial class DatabaseContext : DbContext
  {
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    private string ConvertPostConnectionToConnectionString(string connection)
    {
      var _connection = connection.Replace("postgres://", String.Empty);
      var output = Regex.Split(_connection, ":|@|/");
      return $"server={output[2]};database={output[4]};User Id={output[0]}; password={output[1]}; port={output[3]}";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        var envConn = Environment.GetEnvironmentVariable("DATABASE_URL");
        var conn = "server=localhost;database=GoogleFilterMasterDatabase";
        if (envConn != null)
        {
          conn = ConvertPostConnectionToConnectionString(envConn);
        }
        optionsBuilder.UseNpgsql(conn);
      }
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");
    }
    public DbSet<User> User { get; set; }
    public DbSet<MasterFilter> MasterFilter { get; set; }
    public DbSet<AccountsCache> AccountsCache { get; set; }
    public DbSet<FiltersCache> FiltersCache { get; set; }
    public DbSet<SelectedFilter> SelectedFilter { get; set; }
  }
}
