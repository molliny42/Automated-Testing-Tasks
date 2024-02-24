using System.Text.Json;

namespace BuycyclePagesTests
{
    internal class JsonParser
    {
        public static string GetString(string fileName, string key)
        {
            string str = Environment.CurrentDirectory + "/Resources/" + fileName;

            if (!File.Exists(str))
            {
                throw new FileNotFoundException("File not found", str);
            }

            using var jDoc = JsonDocument.Parse(File.ReadAllText(str));
            var myClass = jDoc.RootElement.GetProperty(key);
            return myClass.ToString();

        }
    }
}