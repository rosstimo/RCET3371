namespace FileIOExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "..\\..\\..\\TestFile.txt";

            //WriteToFile(filePath);
            //AppendToFile(filePath);
            ReadFromFile(filePath);
            
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

    }
}
