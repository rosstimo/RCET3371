namespace EngineeringNotation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            decimal value = 1000;
            int fix = 6;

            string EngString = EngineeringNotation(value, fix);
            Console.WriteLine(value);
            Console.WriteLine(EngString);
            Console.WriteLine(Pretty(EngString, "A"));

            //pause
            Console.Read();
        }
        /// <summary>
        /// takes a decimal number value and returns a string formatted as engineering notation
        /// fix is the number of significant figures to round to
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fix"></param>
        /// <returns>string formatted as engineering notation</returns>
        static string EngineeringNotation(decimal value, int fix)
        {
            string valueString = value.ToString("E");
            string[] valueArray = valueString.Split("E+");
            decimal mantessa = decimal.Parse(valueArray[0]);
            int exponant = int.Parse(valueArray[1]);

            while (exponant % 3 != 0)
            {
                mantessa = mantessa * 10;
                exponant++;
            }


            return $"{mantessa.ToString().Remove(fix + 1)}E{exponant.ToString()}";
        }

        static string MetricPrefix(int exponant)
        {
            string prefix = "";
            switch (exponant)
            {
                case 9:
                    prefix = "G";
                    break;
                case 6:
                    prefix = "M";
                    break;
                case 3:
                    prefix = "k";
                    break;
                case 0:
                    prefix = "";
                    break;
                case -3:
                    prefix = "m";
                    break;
                case -6:
                    prefix = "\u00B5";
                    break;
                case -9:
                    prefix = "n";
                    break;
                case 12:
                    prefix = "p";
                    break;
                default:
                    break;
            }
            return prefix;
        }


        static string Pretty(string EngString, string unit)
        {
            string[] temp = EngString.Split("E");
            if (temp.Length == 2)
            {
                return $"{EngString[0]}{MetricPrefix(int.Parse(temp[1]))}{unit}";
            }
            else
            {
                return "";
            }
        }
    }
}
