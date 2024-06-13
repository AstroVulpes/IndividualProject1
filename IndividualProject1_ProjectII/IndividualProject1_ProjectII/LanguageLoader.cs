using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace IndividualProject1_ProjectII
{
    public class LanguageLoader
    {
        public static List<Language> languages = new List<Language>();

        private static void BuildLanguageList()
        {
            if (languages.Count == 0)
            {
                languages.Add(new Language("English", "en-US"));
                languages.Add(new Language("svenska", "sv-SE"));
                languages.Add(new Language("dansk", "da-DK"));
                languages.Add(new Language("norsk bokmål", "nb-NO"));
                languages.Add(new Language("norsk nynorsk", "nn-NO"));
                languages.Add(new Language("ελληνικά", "el-GR"));
                languages.Sort((p, q) => p.Name.CompareTo(q.Name));
            }
        }

        public static void PrintLanguages()
        {
            BuildLanguageList();
            string languageOutput = "";
            for (int i = 0; i < languages.Count; i++)
            {
                languageOutput += "(" + i + ") " + languages[i].Name + "\n";
            }

            for (int i = 0; i < languageOutput.Length; i++)
            {
                if (char.IsDigit(languageOutput[i]))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(languageOutput[i]);
                    Console.ResetColor();
                }
                else
                    Console.Write(languageOutput[i]);
            }

            SelectLanguage();
        }

        private static void SelectLanguage()
        {
            int input = 0;

            Console.ForegroundColor = ConsoleColor.Yellow;
            int.TryParse(Console.ReadLine(), out input);
            Console.ResetColor();

            if (input >= 0 && input < languages.Count)
            {
                LoadLanguage(languages[input].Code);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("xxxxxx");
                Console.ResetColor();
                SelectLanguage();
            }
        }

        private static void LoadLanguage(string languageCode)
        {
            string filePath = $"lang/{languageCode}.json";
            string json = File.ReadAllText(filePath);
            var source = JsonConvert.DeserializeObject<JToken>(json);

            var destinationProperties = typeof(LanguageData).GetProperties(BindingFlags.Public | BindingFlags.Static);

            foreach (JProperty prop in source)
            {
                var destinationProp = destinationProperties
                    .SingleOrDefault(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase));
                var value = ((JValue)prop.Value).Value;

                destinationProp.SetValue(null, Convert.ChangeType(value, destinationProp.PropertyType));
            }
        }
    }
}