namespace FileIOExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "..\\..\\..\\TestFile.txt";
            string[,] customerData = ReadFileIntoArray("..\\..\\..\\UserData.txt");
            //WriteToFile(filePath);
            //AppendToFile(filePath);
            //ReadFromFile(filePath);
            //ReadWholeFile("..\\..\\..\\UserData.txt");
            DisplayCustomerData(customerData);
            //pause
            Console.Read();
        }

        static void WriteToFile(string path)
        {
            using (StreamWriter currentFile = File.CreateText(path))
            {
                currentFile.WriteLine("Wake up Neo...");
            }
        }

        static void AppendToFile(string path)
        {
            using (StreamWriter currentFile = File.AppendText(path))
            {
                currentFile.WriteLine("Follow the white rabbit...");
            }
        }

        static void ReadFromFile(string path)
        {
            using (StreamReader currentFile = new StreamReader(path))
            {
                Console.WriteLine(currentFile.ReadLine());
                Console.WriteLine(currentFile.ReadLine());
                Console.WriteLine(currentFile.ReadLine());
                Console.WriteLine(currentFile.ReadLine());//pay read past end of file
                Console.WriteLine(currentFile.ReadLine());
                Console.WriteLine(currentFile.ReadLine());
            }
        }

        static void ReadWholeFile(string path)
        {
            using (StreamReader currentFile = new StreamReader(path))
            {
                while (!currentFile.EndOfStream) 
                {
                    Console.WriteLine(currentFile.ReadLine());
                }
            }
        }

        static int CountOfLinesIn(string path)
        {
            int count = 0;
            using (StreamReader currentFile = new StreamReader(path))
            {
                while (!currentFile.EndOfStream)
                {
                    currentFile.ReadLine();
                    count++;
                }
            }
            return count;
        }

        static string[,] ReadFileIntoArray(string path)
        {
            string[,] customerData = new string[4,CountOfLinesIn(path)];
            string[] temp;
            int customerNumber = 0;

            using (StreamReader currentFile = new StreamReader(path))
            {
                while (!currentFile.EndOfStream)
                {
                    temp = currentFile.ReadLine().Split(",");
                    customerData[0,customerNumber] = temp[0].Replace("\"$$","");
                    customerData[1, customerNumber] = temp[1];
                    customerData[2, customerNumber] = temp[2];
                    customerData[3, customerNumber] = temp[3].Replace("\"","");
                    customerNumber++;
                }
            }
            return customerData;
        }

        static void DisplayCustomerData(string[,] customerData)
        {
            string formattedRow = "";
            for (int row = 0; row < customerData.GetLength(1); row++)
            {
                for (int column = 0; column < customerData.GetLength(0); column++)
                {
                    formattedRow += customerData[column, row].PadRight(12);
                    //TODO method to get the max length of each column
                    //then dynamically pad to column max + 2 or so.
                    Console.Write(formattedRow);
                }
                Console.WriteLine();
            } 

             
        }

    }
}
