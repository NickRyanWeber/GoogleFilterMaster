using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace googlefiltermaster
{
  /// <summary>
  /// Client auth information, loaded from a Google user credential json file.
  /// Set the TEST_CLIENT_SECRET_FILENAME environment variable to point to the credential file.
  /// </summary>
  public class ClientInfo
  {
    public static ClientInfo Load()
    {
      string clientSecretFilename = "oauth_config.json";

      var secrets = JObject.Parse(Encoding.UTF8.GetString(File.ReadAllBytes(clientSecretFilename)))["installed"];
      var projectId = secrets["project_id"].Value<string>();
      var clientId = secrets["client_id"].Value<string>();
      var clientSecret = secrets["client_secret"].Value<string>();
      return new ClientInfo(projectId, clientId, clientSecret);
    }

    private ClientInfo(string projectId, string clientId, string clientSecret)
    {
      ProjectId = projectId;
      ClientId = clientId;
      ClientSecret = clientSecret;
    }

    public string ProjectId { get; }
    public string ClientId { get; }
    public string ClientSecret { get; }
  }
}
