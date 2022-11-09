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

            if (content is null || content == String.Empty)
            {
                return null;
            }

            return JsonSerializer.Deserialize<List<Account>>(content);
        }

        public static async Task Reset()
        {
            await File.WriteAllTextAsync(filepath, String.Empty);
        }

        public static async Task Create(Account account)
        {
            List<Account>? data = await ReadAll();

            if (data is null)
            {
                data = new() { account };
            }
            else
            {
                data.Add(account);
            }

            await File.WriteAllTextAsync(filepath, JsonSerializer.Serialize(data));
        }

        public static async Task<Account?> Read(string id)
        {
            var data = await ReadAll();

            if (data is null)
            {
                return null;
            }

            foreach (var element in data)
            {
                if (element.Id == id)
                {
                    return element;
                }
            }

            return null;
        }

        public static Account Update(Account account)
        {
            return account;
        }
    }
}