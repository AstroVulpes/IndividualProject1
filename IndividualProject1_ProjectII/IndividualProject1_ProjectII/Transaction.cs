using System.Globalization;

namespace IndividualProject1_ProjectII
{
    public class Transaction()
    {
        public int Id { get; set; } // Randomly generated
        public TransactionType Type { get; set; } // Income or Expense
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }

        public Transaction(TransactionType type, double value, DateTime date, string note = "") : this()
        {
            Random random = new Random();
            int[] sequence = new int[6];

            sequence[0] = 1 + random.Next(8);
            for (int i = 1; i < 6; i++)
            {
                sequence[i] = 1 + random.Next(9);
            }
            Id = int.Parse(string.Join("", sequence));

            Type = type;
            Value = value;
            Date = date;
            Note = note;
        }

        private int RandomId()
        {
            int id;
            Random random = new Random();
            int[] sequence = new int[6];

            sequence[0] = 1 + random.Next(8); // Ensure that the first digit is not 0
            for (int i = 1; i < 6; i++)
            {
                sequence[i] = 1 + random.Next(9);
            }
            id = int.Parse(string.Join("", sequence));
            if (!IsIdDuplicate(id)) // Rerandomize if the ID already exists in the transactions list
            {
                return RandomId();
            }
            return id;
        }

        private bool IsIdDuplicate(int id) // Double check if the ID already exists
        {
            foreach (Transaction transaction in Program.transactions)
            {
                if (transaction.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public static void ListTransactions()
        {
            string input = "";
            int value = 0;

            while (!int.TryParse(input, out value))
            {
                Console.Write(LanguageData.listMessage);

                Console.ForegroundColor = ConsoleColor.Yellow;
                input = Console.ReadLine();
                Console.ResetColor();
                Console.WriteLine(input);
                if (input.ToLower() == "cancel")
                {
                    Messages.PrintCancelMessage();
                    return;
                }
                else if (int.TryParse(input, out value)) // Checks whether the input is 1 (All), 2 (Incomes) or 3 (Expenses)
                {
                    if (int.Parse(input) < 1 || value > 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(LanguageData.listError);
                        Console.ResetColor();
                    }
                    else
                    {
                        List<Transaction> transactionsCopy = new List<Transaction>();
                        if (int.Parse(input) == 1)
                        {
                            transactionsCopy = Program.transactions;
                        }
                        else if (int.Parse(input) == 2) // This could have been done with LINQ but it was written before that lesson
                        {
                            foreach (Transaction transaction in Program.transactions)
                            {
                                if (transaction.Type == TransactionType.Income)
                                {
                                    transactionsCopy.Add(transaction);
                                }
                            }
                        }
                        else if (int.Parse(input) == 3)
                        {
                            foreach (Transaction transaction in Program.transactions)
                            {
                                if (transaction.Type == TransactionType.Expense)
                                {
                                    transactionsCopy.Add(transaction);
                                }
                            }
                        }
                        List<string> headerList = new List<string>();

                        string[] arr = LanguageData.tableHeaders.Split(',');
                        foreach (string s in arr)
                        {
                            headerList.Add(s.Trim());
                        }
                        Console.WriteLine(" " + new string('_', 90) + " ");
                        Console.WriteLine("| {0,-7} | {1,-8} | {2,-20} | {3,-11} | {4,-30} |",
                        headerList[0], headerList[1], headerList[2], headerList[3], headerList[4]);
                        Console.WriteLine("|{0,-9}|{1,-10}|{2,-22}|{3,-13}|{4,-32}|",
                        new string('_', 9), new string('_', 10), new string('_', 22), new string('_', 13), new string('_', 32));
                        foreach (Transaction transaction in transactionsCopy)
                        {
                            string type = ((transaction.Type == TransactionType.Income) ? LanguageData.income : LanguageData.expense);
                            Console.WriteLine("| {0,7} | {1,-8} | {2,20} | {3,11} | {4,-30} |",
                            transaction.Id, type, transaction.Value.ToString("c2", new CultureInfo("sv-SE")), transaction.Date.ToString("yyyy-MM-dd"), transaction.Note);// Transaction row
                            Console.WriteLine("|{0,-9}|{1,-10}|{2,-22}|{3,-13}|{4,-32}|",
                            new string('_', 9), new string('_', 10), new string('_', 22), new string('_', 13), new string('_', 32));
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(LanguageData.listError);
                    Console.ResetColor();
                }
            }
        }

        public static void InputTransaction()
        {
            string input1 = "";
            TransactionType transactionType = 0;
            while (transactionType != TransactionType.Income && transactionType != TransactionType.Expense) // Check whether the user inputs an Income or an Expense
            {
                Console.Write(LanguageData.transactionTypeMessage);

                Console.ForegroundColor = ConsoleColor.Yellow;
                input1 = Console.ReadLine();
                Console.ResetColor();

                if (input1 == "1")
                {
                    transactionType = TransactionType.Income;
                }
                else if (input1 == "2")
                {
                    transactionType = TransactionType.Expense;
                }
                else if (input1.ToLower() == "cancel") // Stops the process and returns to Options menu
                {
                    Messages.PrintCancelMessage();
                    return;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(LanguageData.transactionTypeError);
                    Console.ResetColor();
                }
            }

            double value = 0;
            string input2 = "";
            while (!double.TryParse(input2, out value)) // Checks whether the input is a valid value
            {
                Console.Write(LanguageData.valueMessage);

                Console.ForegroundColor = ConsoleColor.Yellow;
                input2 = Console.ReadLine();
                Console.ResetColor();

                if (input2.ToLower() == "cancel")
                {
                    Messages.PrintCancelMessage();
                    return;
                }
                else if (!double.TryParse(input2, out value))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(LanguageData.valueError);
                    Console.ResetColor();
                }
            }

            DateTime date;
            string input3 = "";
            while (!DateTime.TryParse(input3, out date)) // Checks whether the input is a valid date
            {
                Console.Write(LanguageData.dateMessage);

                Console.ForegroundColor = ConsoleColor.Yellow;
                input3 = Console.ReadLine();
                Console.ResetColor();

                if (input1.ToLower() == "cancel")
                {
                    Messages.PrintCancelMessage();
                    return;
                }
                else if (!DateTime.TryParse(input3, out date))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(LanguageData.dateError);
                    Console.ResetColor();
                }
            }

            string note = "";
            string input4 = "";
            Console.Write(LanguageData.noteMessage);

            Console.ForegroundColor = ConsoleColor.Yellow;
            input4 = Console.ReadLine(); // An optional note
            Console.ResetColor();

            if (input1.ToLower() == "cancel")
            {
                Messages.PrintCancelMessage();
                return;
            }

            else if (input4 != null)
                note = input4;

            MakeTransaction(transactionType, value, date, note);
        }

        public static void MakeTransaction(TransactionType type, double value, DateTime date, string note = "") // The method to actually add the transaction to the list
        {
            if (type == TransactionType.Expense)
                value = -Math.Abs(value);
            else if (type == TransactionType.Income)
                value = Math.Abs(value);

            value = (double)Math.Round(value, 2);
            Program.balance += value;

            Program.transactions.Add(new Transaction(type, value, date, note));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(LanguageData.transactionSuccess);
            Console.ResetColor();

            Messages.PrintBalanceMessage();
        }

        public static void EditOrRemoveTransaction() // Method to let a user choose to edit or remove
        {
            string input1 = "";
            bool inputCorrect = false;
            while (!inputCorrect)
            {
                Console.Write(LanguageData.editOrRemoveMessage);

                Console.ForegroundColor = ConsoleColor.Yellow;
                input1 = Console.ReadLine();
                Console.ResetColor();

                if (input1.ToLower() == "cancel")
                {
                    Messages.PrintCancelMessage();
                    return;
                }
                else if (input1 == "1")
                {
                    EditTransaction();
                    inputCorrect = true;
                }
                else if (input1 == "2")
                {
                    RemoveTransaction();
                    inputCorrect = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(LanguageData.editOrRemoveError);
                    Console.ResetColor();
                }
            }
        }

        public static void EditTransaction() // Works similarly to adding a transaction but only the fields inputed are changed
        {
            string input0 = "";
            bool inputCorrect = false;
            while (!inputCorrect)
            {
                Console.Write(LanguageData.editMessage);

                Console.ForegroundColor = ConsoleColor.Yellow;
                input0 = Console.ReadLine();
                Console.ResetColor();

                foreach (Transaction transaction in Program.transactions)
                {
                    if (input0 == transaction.Id.ToString())
                    {
                        inputCorrect = true;

                        string input1 = "";
                        TransactionType transactionType = 0;
                        while (transactionType != TransactionType.Income && transactionType != TransactionType.Expense)
                        {
                            Console.Write(LanguageData.transactionTypeMessage);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            input1 = Console.ReadLine();
                            Console.ResetColor();

                            if (input1.ToLower() == "cancel")
                            {
                                Messages.PrintCancelMessage();
                                return;
                            }
                            else if (input1 == "1")
                            {
                                transactionType = TransactionType.Income;
                            }
                            else if (input1 == "2")
                            {
                                transactionType = TransactionType.Expense;
                            }
                            else if (input1 == "")
                            {
                                break;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(LanguageData.transactionTypeError);
                                Console.ResetColor();
                            }
                        }

                        double value = 0;
                        string input2 = "";
                        while (!double.TryParse(input2, out value))
                        {
                            Console.Write(LanguageData.valueMessage);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            input2 = Console.ReadLine();
                            Console.ResetColor();

                            if (input2.ToLower() == "cancel")
                            {
                                Messages.PrintCancelMessage();
                                return;
                            }
                            else if (input2 == "")
                            {
                                break;
                            }
                            else if (!double.TryParse(input2, out value))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(LanguageData.valueError);
                                Console.ResetColor();
                            }
                        }

                        DateTime date;
                        string input3 = "";
                        while (!DateTime.TryParse(input3, out date))
                        {
                            Console.Write(LanguageData.dateMessage);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            input3 = Console.ReadLine();
                            Console.ResetColor();

                            if (input3.ToLower() == "cancel")
                            {
                                Messages.PrintCancelMessage();
                                return;
                            }
                            else if (input3 == "")
                            {
                                break;
                            }
                            else if (!DateTime.TryParse(input3, out date))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(LanguageData.dateError);
                                Console.ResetColor();
                            }
                        }

                        string input4 = "";
                        Console.Write(LanguageData.noteMessage);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        input4 = Console.ReadLine();
                        if (input4.ToLower() == "cancel")
                        {
                            Messages.PrintCancelMessage();
                            return;
                        }
                        Console.ResetColor();

                        if (transactionType == TransactionType.Income || transactionType == TransactionType.Expense)
                        {
                            transaction.Type = transactionType;
                        }
                        if (value != 0)
                        {
                            transaction.Value = value;
                        }
                        if (date != DateTime.MinValue)
                        {
                            transaction.Date = date;
                        }
                        if (input4 != null && input4 != "")
                        {
                            transaction.Note = input4;
                        }

                        break;
                    }
                }
                if (!inputCorrect)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(LanguageData.editError);
                    Console.ResetColor();
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(LanguageData.operationSuccess);
            Console.ResetColor();
        }

        public static void RemoveTransaction() // Simple iteration through the list based on ID
        {
            string input1 = "";
            bool inputCorrect = false;
            while (!inputCorrect)
            {
                Console.Write(LanguageData.removeMessage);

                Console.ForegroundColor = ConsoleColor.Yellow;
                input1 = Console.ReadLine();
                Console.ResetColor();

                foreach (Transaction transaction in Program.transactions)
                {
                    if (input1 == transaction.Id.ToString())
                    {
                        Program.transactions.Remove(transaction);
                        inputCorrect = true;
                        break;
                    }
                }
                if (!inputCorrect) // The ID does not match any of the transactions in the list
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(LanguageData.removeError);
                    Console.ResetColor();
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(LanguageData.operationSuccess);
            Console.ResetColor();
        }
    }
}