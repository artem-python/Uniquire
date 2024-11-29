using System.Xml;

namespace Uniquire
{
    public class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Paste file name with CSV code or /exit:");
                string? path = Console.ReadLine();
                if (path != null)
                {
                    if (path == "/exit") { Environment.Exit(0); }
                    if (!new FileInfo(path).Exists) { continue; }
                    using StreamReader reader = new(path);
                    Process(reader.ReadToEnd());
                    Console.WriteLine("Done");
                }
            }
        }

        static void Process(string? data)
        {
            if (data != null)
            {
                string[] rows = data.Split(Environment.NewLine);
                int maxColums = rows.Max(x => x.Split(';').Length);
                string[,] table = new string[rows.Length, maxColums];
                for (int x = 0; x < rows.Length; x++)
                {
                    string[] colums = rows[x].Split(";");
                    for (int y = 0; y < maxColums; y++) { table[x, y] = colums[y]; }
                }
                List<string> unique = [];
                foreach (string str in table) { if (!unique.Contains(str)) { unique.Add(str); } }
                string result = "";
                foreach (string str in unique) { result += $"{str}\n"; }
                using StreamWriter sw = new("output.txt");
                sw.WriteLine(result);
            }
        }
    }
}
