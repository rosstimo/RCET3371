namespace EngineeringNotation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            decimal value = 100000m;
            int fix = 6;

            //string engString = EngineeringNotation(value, fix);
            //Console.WriteLine(value);
            //Console.WriteLine(engString);
            Console.WriteLine(Pretty(value,"V",fix));

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

            if (value >= 1 & value <= 999)
            {
                return value.ToString();//.Remove(fix +1);
            }
            else
            {
                string valueString = value.ToString("E");
                string[] valueArray = valueString.Split("E");
                decimal mantessa = decimal.Parse(valueArray[0]);
                int exponant = int.Parse(valueArray[1]);
                while (exponant % 3 != 0 & exponant > 3)
                {
                    mantessa = mantessa * 10;
                    exponant++;
                }
                    return $"{mantessa.ToString().Remove(fix + 1)}E{exponant.ToString()}";
            }


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


        static string Pretty(decimal value, string unit, int fix)
        {
            string engString = EngineeringNotation(value, fix);
            string[] temp = engString.Split("E");
            if (temp.Length == 2)
            {
                return $"{engString[0]}{MetricPrefix(int.Parse(temp[1]))}{unit}";
            }
            else
            {
                return engString + unit;
            }
        }
    }
}
