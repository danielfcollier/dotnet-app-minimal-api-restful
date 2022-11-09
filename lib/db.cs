using System.Text.Json;

using Model;

namespace Db
{
  public static class Handler
  {
    private readonly static string DB_FILE = "data.json";
    private readonly static string filepath = Path.Join(".", "db", DB_FILE);

    private static async Task<List<Account>?> ReadAll()
    {
      var content = await File.ReadAllTextAsync(filepath);

      if (content is not null)
      {
        return JsonSerializer.Deserialize<List<Account>>(content);
      }

      return null;
    }

    public static async Task<Account?> Read(string id)
    {
      var data = await ReadAll();

      if (data is not null)
      {
        foreach (var element in data)
        {
          if (element.Id == id)
          {
            return element;
          }
        }
      }

      return null;
    }
  }
}