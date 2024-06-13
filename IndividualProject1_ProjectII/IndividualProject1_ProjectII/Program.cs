using System.Globalization;
using CsvHelper;
using IndividualProject1_ProjectII;
using System.Text;

namespace IndividualProject1_ProjectII
{
    internal partial class Program
    {
        public static double balance { get; set; } = 0; // The amount of money in the account, updated with each transaction.

        public static List<Transaction> transactions = new List<Transaction>(); // The list of all transactions made.

        private static bool exit = false;
        public static void Options()
        {
            Messages.PrintOptions();

            int input = 0;

            Console.ForegroundColor = ConsoleColor.Yellow;
            int.TryParse(Console.ReadLine(), out input); // Interprets the input to match it with the existing options
            Console.ResetColor();

            switch (input)
            {
                case 0:
                    Console.Clear();
                    LanguageLoader.PrintLanguages();
                    break;
                case 1:
                    Transaction.ListTransactions();
                    break;
                case 2:
                    Transaction.InputTransaction();
                    break;
                case 3:
                    Transaction.EditOrRemoveTransaction();
                    break;
                case 4:
                    SaveToFile();
                    exit = true;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(LanguageData.optionsError);
                    Console.ResetColor();
                    return; 
            }
        }

        public static void SaveToFile() // Saves all the transactions made before closing the application
        {
            File.WriteAllText("balance.csv", balance.ToString());

            var filePath = "transactions.csv";
            var csv = new StringBuilder();
            csv.AppendLine("Id,Type,Value,Date,Note");

            foreach (var transaction in transactions)
            {
                var first = transaction.Id.ToString();
                var second = transaction.Type.ToString();
                var third = transaction.Value.ToString();
                var fourth = transaction.Date.ToString();
                var fifth = transaction.Note.ToString();
                var newLine = string.Format("{0},{1},{2},{3},{4}", first, second, third, fourth, fifth);
                csv.AppendLine(newLine);
            }
            File.WriteAllText(filePath, csv.ToString());
        }

        public static void LoadFromFile()
        {
            try
            {
                using (var reader = new StreamReader("balance.csv")) // Loads the balance
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Read();
                        var record = csv.GetField(0);
                        balance = double.Parse(record);                        
                    }
                }

                using (var reader = new StreamReader("transactions.csv")) // Loads all the data into the transactions list
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Read();
                        csv.ReadHeader();
                        while (csv.Read())
                        {
                            var record = csv.GetRecord<Transaction>();
                            transactions.Add(record);
                        }
                    }
                }
            }
            catch
            {
                // In case a document does not exist, do not load.
            }
        }

        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            LanguageLoader.PrintLanguages(); // Language select
            LoadFromFile(); // Loads the values already stored
            Console.WriteLine(LanguageData.introMessage);

            Messages.PrintBalanceMessage(); // Prints the amount of money in the account

            while (!exit) // Loops as long as the program should not exit (option 4)
            {
                Options();
            }

            Console.WriteLine(LanguageData.endMessage);
            Console.ReadKey();
        }
    }
}