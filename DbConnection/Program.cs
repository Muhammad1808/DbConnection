
namespace DbConnection
{
    public class Program
    {
        public static string connectionString;

        static void Main(string[] args)
        {
            Console.Write("Host= ");
            var host = Console.ReadLine();
            Console.Write("Port= ");
            var port = int.Parse(Console.ReadLine());
            Console.Write("Database= ");
            var database = Console.ReadLine();
            Console.Write("UserName= ");
            var userName = Console.ReadLine();
            Console.Write("Password= ");
            var password = Console.ReadLine();
            Console.Clear();
            connectionString = $"Host={host};Port={port};Database={database};Username={userName};Password={password}";

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("1.Table");
                Console.WriteLine("2.Columns");;
                Console.WriteLine("3.Exit");
                Console.Write("Choose option: ");
                var choise = Console.ReadLine();
                Console.Clear();

                switch (choise)
                {
                    case "1":
                        TableOperations();
                        Console.Clear();
                        break;
                    case "2":
                        ColumnOperations();
                        Console.Clear();
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Incorrect, please try again");
                        break;
                }
            }

            static void TableOperations()
            {
                bool back = false;
                while (!back)
                {
                    Console.WriteLine("1.Add table");
                    Console.WriteLine("2.Update table");
                    Console.WriteLine("3.Delete table");
                    Console.WriteLine("4.List tables");
                    Console.WriteLine("5.Back");
                    Console.Write("Choose an option: ");
                    var choise1 = Console.ReadLine();
                    Console.Clear();

                    switch (choise1)
                    {
                        case "1":
                            Console.Write("Enter table name: ");
                            var tName = Console.ReadLine();
                            var columns = new Dictionary<string, string>();
                            bool again = true;
                            while (again)
                            {
                                Console.Write("Enter column name: ");
                                var cName = Console.ReadLine();
                                Console.Write("Enter column data type: ");
                                var type = Console.ReadLine();
                                columns[cName] = type;
                                Console.Write("Do you want to add another column? (yes/no): ");
                                var response = Console.ReadLine()?.ToLower();

                                if (response == "no")
                                {
                                    again = false;
                                }
                            }
                            Table.AddTable(tName, columns);
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case "2":
                            Console.Write("Enter table name to update: ");
                            string utName = Console.ReadLine();
                            Console.Write("Enter new table name: ");
                            string ntName = Console.ReadLine();
                            Table.UpdateTable(utName, ntName);
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case "3":
                            Console.Write("Enter table name to delete: ");
                            var dtName = Console.ReadLine();
                            Table.DeleteTable(dtName);
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case "4":
                            Table.ListTables();
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case "5":
                            back = true;
                            break;
                        default:
                            Console.WriteLine("Incorrect, please try again");
                            break;
                    }
                }

            }

            static void ColumnOperations()
            {
                bool back = false;
                while (!back)
                {
                    Console.WriteLine("1.Add column");
                    Console.WriteLine("2.Update column");
                    Console.WriteLine("3.Delete column");
                    Console.WriteLine("4.List column");
                    Console.WriteLine("5.Back");
                    Console.Write("Choose an option: ");
                    var choise1 = Console.ReadLine();
                    Console.Clear();

                    switch (choise1)
                    {
                        case "1":
                            Console.Write("Enter table name: ");
                            var tName = Console.ReadLine();
                            Console.Write("Enter column name: ");
                            var cName = Console.ReadLine();
                            Console.Write("Enter column type: ");
                            var type = Console.ReadLine();
                            Column.AddColumn(tName, cName, type);
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case "2":
                            Console.Write("Enter table name: ");
                            var tName1 = Console.ReadLine();
                            Console.Write("Enter column name: ");
                            var cName1 = Console.ReadLine();
                            Console.Write("Enter new column name: ");
                            var cName2 = Console.ReadLine();
                            Console.Write("Enter new data type: ");
                            var type1 = Console.ReadLine();
                            Column.UpdateColumn(tName1, cName1, cName2, type1);
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case "3":
                            Console.Write("Enter table name: ");
                            var dtName = Console.ReadLine();
                            Console.Write("Enter column name: ");
                            var dcName = Console.ReadLine();
                            Column.DeleteColumn(dtName, dcName);
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case "4":
                            Console.Write("Enter table name: ");
                            var stName = Console.ReadLine();
                            foreach (var col in Column.ListColumn(stName))
                            {
                                Console.WriteLine(col);
                            }
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case "5":
                            back = true;
                            break;
                        default:
                            Console.WriteLine("Inncorrect, please try again");
                            break;
                    }
                }
            }

        }
    }
}
