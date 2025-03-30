using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;

namespace ASC_Testing;

public class Constants
{
  static readonly public string BASE_URL = "https://anphashop-clone.vercel.app";
  // static readonly public string BASE_URL = "http://localhost:3000";
}

static public class Files
{
  static public Dictionary<string, string> Read(string fileName, int index = 0, [CallerMemberName] string testCase = "")
  {
    string basePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
    string filePath = Path.Combine(basePath, "Data", fileName);

    if (!File.Exists(filePath))
      throw new FileNotFoundException($"File not found at: {filePath}");

    string jsonContent = File.ReadAllText(filePath);
    var data = JsonConvert.DeserializeObject<dynamic>(jsonContent) ?? throw new Exception("Failed to deserialize JSON data.");
    var testCaseData = data[testCase][index] ?? throw new Exception("Data not found");

    JObject jObject = (JObject)testCaseData;

    var result = new Dictionary<string, string>();

    foreach (var property in jObject.Properties())
    {
      string key = property.Name;
      string value = property.Value?.ToString() ?? string.Empty;
      result[key] = value;
    }

    return result;
  }
}