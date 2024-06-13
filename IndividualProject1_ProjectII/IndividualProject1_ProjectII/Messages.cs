namespace IndividualProject1_ProjectII
{
    public static class Messages
    {
        public static void PrintCancelMessage()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(LanguageData.cancelMessage);
            Console.ResetColor();
        }
        
        public static void PrintBalanceMessage()
        {
            for (int i = 0; i < LanguageData.balanceMessage.Length; i++)
            {
                if (char.IsDigit(LanguageData.balanceMessage[i]) || LanguageData.balanceMessage[i] == '-' || LanguageData.balanceMessage[i] == ',')
                {
                    if (Program.balance < 0)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (Program.balance == 0)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(LanguageData.balanceMessage[i]);
                    Console.ResetColor();
                }
                else if (i < LanguageData.balanceMessage.Length - 1)
                {
                    if ((LanguageData.balanceMessage[i] == '.' && char.IsDigit(LanguageData.balanceMessage[i + 1])) || LanguageData.balanceMessage.Substring(i, 2) == "kr")
                    {
                        if (Program.balance < 0)
                            Console.ForegroundColor = ConsoleColor.Red;
                        else if (Program.balance == 0)
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        else
                            Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(LanguageData.balanceMessage.Substring(i, 2));
                        Console.ResetColor();
                        if (LanguageData.balanceMessage.Substring(i, 2) == "kr")
                            i++;
                    }
                    else
                        Console.Write(LanguageData.balanceMessage[i]);
                }
                else
                    Console.Write(LanguageData.balanceMessage[i]);
            }
        }

        public static void PrintOptions()
        {
            for (int i = 0; i < LanguageData.optionsMessage.Length; i++)
            {
                if (char.IsDigit(LanguageData.optionsMessage[i]))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(LanguageData.optionsMessage[i]);
                    Console.ResetColor();
                }
                else
                    Console.Write(LanguageData.optionsMessage[i]);
            }
        }
    }
}