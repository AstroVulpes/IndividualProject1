using System.Globalization;

namespace IndividualProject1_ProjectII
{
    public static class LanguageData // Class that stores every string to be translated
    {
        public static string introMessage { get; set; } = "Welcome to TrackMoney. ";
        public static string balanceMessagePlain { get; set; } = "You currently have {amount} in your account. \n";
        public static string balanceMessage => balanceMessagePlain.Replace("{amount}",
            Program.balance.ToString("c2", new CultureInfo("sv-SE"))); // Replaces {amount} with the current balance in the format "1 234,56 kr"
        public static string optionsMessage { get; set; } = "\nChoose an option: \n(0) <- \n(1) Show items (All/Expenses/Incomes) \n(2) Add new Expense/Income \n(3) Edit Item (edit, remove) \n(4) Save and Quit \nInput \"cancel\" any time to stop a process. \n";
        public static string optionsError { get; set; } = "Input needs to be 1, 2, 3, or 4. ";
        public static string cancelMessage { get; set; } = "Transaction / Operation cancelled. ";
        public static string listMessage { get; set; } = "All (1), Incomes (2), or Expenses (3)? ";
        public static string listError { get; set; } = "Input needs to be 1, 2, or 3. ";
        public static string tableHeaders { get; set; } = "Tr. ID,Type,Value,Date,Note";
        public static string income { get; set; } = "Income";
        public static string expense { get; set; } = "Expense";
        public static string transactionTypeMessage { get; set; } = "Income (1) or Expense (2)? ";
        public static string valueMessage { get; set; } = "Enter the value of the transaction: ";
        public static string dateMessage { get; set; } = "Enter the date of the transaction (format YYYY-MM-DD): ";
        public static string noteMessage { get; set; } = "Enter a note about the transaction (optional): ";
        public static string transactionTypeError { get; set; } = "Input needs to be 1 or 2. ";
        public static string valueError { get; set; } = "Invalid value. Value must be a number that is not 0. ";
        public static string dateError { get; set; } = "Invalid date. ";
        public static string transactionSuccess { get; set; } = "Transaction successful. ";
        public static string editOrRemoveMessage { get; set; } = "Edit (1) or Remove (2) transaction? ";
        public static string editOrRemoveError { get; set; } = transactionTypeError; // The two are the same.
        public static string editMessage { get; set; } = "Enter the ID of the transaction: ";
        public static string editError { get; set; } = "ID not found / invalid ID. ";
        public static string removeMessage { get; set; } = editMessage;
        public static string removeError { get; set; } = editError;
        public static string operationSuccess { get; set; } = "Operation successful. ";
        public static string endMessage { get; set; } = "Thank you for using TrackMoney. Press any key to end program... ";
    }
}