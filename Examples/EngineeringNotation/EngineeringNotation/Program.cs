namespace EngineeringNotation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(EngineeringNotation(12345.6789345634563456345634563456345745680m, 6));
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


            return $"{mantessa.ToString().Remove(fix+1)}E{exponant.ToString()}";
        }
    }
}
