namespace EngineeringNotation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //test the engineering notation function with a sample value and fix
            decimal value = 0.001555555555m;
            int fix = 5;

            // get the engineering notation string for the value and fix
            string engString = EngineeringNotation(value, fix);
            // print the original value, the engineering notation string, and the pretty string with metric prefix and unit
            Console.WriteLine(value);
            Console.WriteLine(engString);
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
            decimal mantissa = value;
            int exponent = 0;

            // loop until the exponent is a multiple of 3 and the mantissa is between 1 and 999
            while (exponent % 3 != 0 || mantissa >= 1000 || mantissa < 1)
            {                    
                if (value >= 1000) // if the value is greater than or equal to 1000, divide by 10 and increment the exponent
                {
                    mantissa /= 10;
                    exponent++;
                }
                else if (value < 1) // if the value is less than 1, multiply by 10 and decrement the exponent
                {
                    mantissa *= 10;
                    exponent--;
                }
                else // if the value is between 1 and 999, we are done
                {
                    break;
                }
            }

            // round the mantissa to the specified number of significant figures
            int temp = mantissa.ToString().Length;
            while (mantissa.ToString().Length > fix+1)
            {
                mantissa = Math.Round(mantissa, temp);
                temp--;
            }

            // return the mantissa and exponent as a string in engineering notation format
            return $"{mantissa.ToString()}E{exponent.ToString()}";
        }


        
/// <summary>
/// takes an exponent and returns the corresponding metric prefix
/// </summary>
/// <param name="exponent"></param>
/// <returns>string representing the metric prefix for the given exponent</returns>
        static string MetricPrefix(int exponent)
        {
            string prefix = "";
            switch (exponent)

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
                    prefix = $"E{exponent}";
                    break;
            }
            return prefix;
        }

/// <summary>
/// takes a decimal number value, a unit string, and an integer fix for significant figures, and returns a string formatted as engineering notation with the appropriate metric prefix and unit
/// </summary>
/// <param name="value"></param>
/// <param name="unit"></param>
/// <param name="fix"></param>
/// <returns>string formatted as engineering notation with the appropriate metric prefix and unit</returns>
        static string Pretty(decimal value, string unit, int fix)
        {
            string engString = EngineeringNotation(value, fix);
            string[] temp = engString.Split("E");
            if (temp.Length == 2)
            {
                return $"{temp[0]}{MetricPrefix(int.Parse(temp[1]))}{unit}";
            }
            else
            {
                return engString + unit;
            }
        }
    }
}
