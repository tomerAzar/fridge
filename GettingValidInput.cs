using System;

namespace Fridge
{
    public static class GettingValidInput
    {
        public static int GettingPositiveInt (string msg)
        {
            Console.Write(msg);
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int intInput) || intInput <0 ) return GettingPositiveInt(msg);
            else
                return intInput;
        }
        public static string GettingString(string msg)
        {
            Console.Write(msg);
            string input = Console.ReadLine();
            if (input.Trim().Length == 0) return GettingString(msg);
            else
                return input;
        }
        public static bool GettingYesNo(string msg)
        {
            Console.Write(msg);
            string input = Console.ReadLine();
            if (input == "yes") return true;
            else if (input == "no") return false;
            else
                return GettingYesNo(msg);
        }

        public static DateTime GettingDate (string msg)
        {
            Console.Write(msg);
            string input = Console.ReadLine();
            if (input.Length == 10)
            {
                bool isDays = int.TryParse(input.Substring(0, 2), out int days);
                bool isMonth = int.TryParse(input.Substring(3, 2), out int month);
                bool isYear = int.TryParse(input.Substring(6, 4), out int year);
                if (input[2] != '/' || input[5] != '/' || ! isDays || ! isMonth || ! isYear || days > 31 || days < 1 || month > 12 || month < 1)
                    return GettingDate(msg);
                else
                    return new DateTime(year, month, days);
            }
            else 
                return GettingDate(msg);              
        }
        public static T ParseEnum<T>()
        where T : struct
        {
            try
            {
                Console.WriteLine("\nyou have to choose: " + typeof(T));
                foreach (T option in Enum.GetValues(typeof(T)))
                    Console.WriteLine(" " + option);
                Console.WriteLine("enter one option \nwrite the string value or enter index (starts from 0): ");
                string enumString = Console.ReadLine();
                if (int.TryParse(enumString, out int result) && result > Enum.GetValues(typeof(T)).Length - 1)
                    throw new Exception();
                else return (T)Enum.Parse(typeof(T), enumString, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("you have not entered value from options \n ");
                return ParseEnum<T>();
            }
        }
    }
}
