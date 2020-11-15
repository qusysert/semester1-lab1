using System;
using System.IO;

namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                var point = ReadInt(
                    "Which point should I run? Regular array (1), 2D array (2), step array (3) or exit (4)?");
                switch (point)
                {
                    case 1:
                        Point1();
                        break;
                    case 2:
                        Point2();
                        break;
                    case 3:
                        Point3();
                        break;
                    case 4:
                        return;
                    default:
                        WriteErrorLine("Wrong input! Try again");
                        break;
                }
            } while (true);
        }

        private static void Point1()
        {
            int[] arr;
            int n;
            int evenAmount = 0;

            // Checking input method + filling up array
            int inputWay;
            do
            {
                inputWay = ReadInt("Should I get numbers from console (1) or from file (2)?");
                if (inputWay == 1 || inputWay == 2)
                {
                    break;
                }

                WriteErrorLine("Wrong input! Try again");
            } while (true);

            if (inputWay == 1)
            {
                n = ReadInt("Enter size of the array: ");
                arr = new int[n];
                Console.WriteLine("Fill up the array: ");
                for (int i = 0; i < n; i++)
                {
                    arr[i] = ReadInt();
                }
            }
            else
            {
                try
                {
                    string filePath = Directory.GetCurrentDirectory() + @"\fileArray.txt";
                    arr = Array.ConvertAll(File.ReadAllLines(filePath), int.Parse);
                }
                catch (System.FormatException)
                {
                    WriteErrorLine("Invalid file");
                    return;
                }
                catch (FileNotFoundException)
                {
                    WriteErrorLine("File not found");
                    return;
                }
            }

            Console.WriteLine("Your array: " + string.Join(", ", arr));

            // Get max and min + counting amount of even elements
            int max = arr[0];
            int min = arr[0];
            int maxIndex = 0;
            int minIndex = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                    maxIndex = i;
                }

                if (arr[i] < min)
                {
                    min = arr[i];
                    minIndex = i;
                }

                if (arr[i] % 2 == 0)
                {
                    evenAmount++;
                }
            }

            Console.WriteLine("Max: {0} (index {1}); Min: {2} (index {3})", max, maxIndex, min, minIndex);

            // Sort
            bool isSorted = false;
            do
            {
                isSorted = true;
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        int buffer = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = buffer;
                        isSorted = false;
                    }
                }
            } while (!isSorted);

            Console.WriteLine("Sorted: " + string.Join(", ", arr));

            // Rev sort
            int[] revSortArr = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                revSortArr[i] = arr[arr.Length - 1 - i];
            }

            Console.WriteLine("Rev sorted: " + string.Join(", ", revSortArr));

            // Only evens array
            int[] onlyEvenArr = new int[evenAmount];
            int counterForEvens = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] % 2 == 0)
                {
                    onlyEvenArr[counterForEvens] = arr[i];
                    counterForEvens++;
                }
            }

            Console.WriteLine("Evens: " + string.Join(", ", onlyEvenArr));
        }
        
        private static void Point2()
        {
            int inputWay;
            do
            {
                inputWay = ReadInt("Should I get numbers from console (1) or from file (2)?");
                if (inputWay == 1 || inputWay == 2)
                {
                    break;
                }

                WriteErrorLine("Wrong input! Try again");
            } while (true);

            int[,] doubleArr;
           

            if (inputWay == 1)
            {
                doubleArr = Point2Get2dArrayFromConsole();
            }
            else
            {
                var filePath = Directory.GetCurrentDirectory() + @"\2dArray.txt";
                try
                {
                    doubleArr = Point2Get2dArrayFromFile(filePath);
                }
                catch (FormatException)
                {
                    WriteErrorLine("Invalid file");
                    return;
                }
                catch (FileNotFoundException)
                {
                    WriteErrorLine("File not found " + filePath);
                    return;
                }
            }
            int max = 0;
            int indexOfMax = 0;
            int min = doubleArr[0, 0];
            int indexOfMin = 0;
            int rows = doubleArr.GetLength(0);
            int elementsInRow = doubleArr.GetLength(1);
            // Write array and get mix and max
            Console.WriteLine("Your array:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < elementsInRow; j++)
                {
                    int element = doubleArr[i, j];
                    Console.Write(element + " ");
                    if (element < min)
                    {
                        min = element;
                        // Amount of rows * Serial number of current row + Serial number of element
                        indexOfMin = i * rows + j;
                    }

                    if (element > min)
                    {
                        max = element;
                        indexOfMax = i * rows + j;
                    }
                }

                Console.WriteLine();
            }

            // Write min and max
            Console.WriteLine("Max: {0} (index {1})", max, indexOfMax);
            Console.WriteLine("Min: {0} (index {1})", min, indexOfMin);

            // Changing element
            bool anoutherChange = true;
            string wantToChangeQ = "Want to change element of array? Yes (1), no (2)?";
            do
            {
                int wantToChange = ReadInt(wantToChangeQ);
                switch (wantToChange)
                {
                    case 1:
                        // Getting row of element
                        int rowToChange;
                        while (true)
                        {
                            rowToChange = ReadInt("Write row:");
                            if (rowToChange < rows && rowToChange >= 0)
                            {
                                break;
                            }

                            WriteErrorLine("Index was outside the bounds of the array. Try again");
                        }

                        // Getting column of element
                        int columnToChange;
                        while (true)
                        {
                            columnToChange = ReadInt("Write column:");
                            if (columnToChange < elementsInRow && columnToChange >= 0)
                            {
                                break;
                            }

                            WriteErrorLine("Index was outside the bounds of the array. Try again");
                        }

                        // Changing element
                        doubleArr[rowToChange, columnToChange] = ReadInt("What this element should be?");
                        // Writing changed array
                        for (int i = 0; i < rows; i++)
                        {
                            for (int j = 0; j < elementsInRow; j++)
                            {
                                Console.Write(doubleArr[i, j] + " ");
                            }

                            Console.WriteLine();
                        }

                        wantToChangeQ = "Want to change another element of array? Yes (1), no (2)?";
                        break;
                    case 2:
                        anoutherChange = false;
                        break;
                    default:
                        WriteErrorLine("Wrong input! Try again");
                        break;
                }
            } while (anoutherChange);
        }

        private static void Point3()
        {
            
            int min = Int32.MaxValue;
            int max = 0;


            int inputWay;
            do
            {
                inputWay = ReadInt("Should I get numbers from console (1) or from file (2)?");
                if (inputWay == 1 || inputWay == 2)
                {
                    break;
                }

                WriteErrorLine("Wrong input! Try again");
            } while (true);

            int[][] jagArr;
            
            // Inputing jagged array
            if (inputWay == 1)
            {
                jagArr = Point3GetJagArrFromConsole();
            }
            else
            {
                var filePath = Directory.GetCurrentDirectory() + @"\fileJaggedArray.txt";
                try
                {
                    jagArr = Point3GetJagArrFromFile(filePath);
                }
                catch (FormatException)
                {
                    WriteErrorLine("Invalid file");
                    return;
                }
                catch (FileNotFoundException)
                {
                    WriteErrorLine("File not found " + filePath);
                    return;
                }
           
            }
        

            // write jagged array
            Console.WriteLine("Your jagged array: ");
            foreach (var row in jagArr)
            {
                foreach (var item in row)
                {
                    Console.Write(item + " ");
                    if (item > max)
                    {
                        max = item;
                    }

                    if (item < min)
                    {
                        min = item;
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("Min: {0}; Max {1}", min, max);

            // Change element of jagged array
            int rowToChange;
            int elementToChange;
            bool anotherChange = true;
            string wantToChangeQ = "Do you want to change element of array? Yes (1), no (2)";
            do
            {
                int wantToChange = ReadInt(wantToChangeQ);
                switch (wantToChange)
                {
                    case 1:
                        while (true)
                        {
                            rowToChange = ReadInt("Write row number:");
                            if (rowToChange < jagArr.Length && rowToChange >= 0)
                            {
                                break;
                            }

                            WriteErrorLine("There is no such row. Try again");
                        }

                        // Get element in row + check
                        while (true)
                        {
                            elementToChange = ReadInt("Write element number:");
                            if (elementToChange < jagArr[rowToChange].Length && elementToChange >= 0)
                            {
                                break;
                            }

                            WriteErrorLine("Index was outside the bounds of the array. Try again");
                        }

                        jagArr[rowToChange][elementToChange] = ReadInt("Write new element: ");
                        wantToChangeQ = "Do you want to change another element of array? Yes (1), no (2)";
                        // Write changed array
                        Console.WriteLine("Changed array:");
                        foreach (var row in jagArr)
                        {
                            foreach (var item in row)
                            {
                                Console.Write(item + " ");
                            }

                            Console.WriteLine();
                        }

                        break;
                    case 2:
                        anotherChange = false;
                        break;
                    default:
                        WriteErrorLine("Wrong input! Try again");
                        break;
                }
            } while (anotherChange);
        }

        private static int[][] Point3GetJagArrFromConsole()
        {
            int amountOfRows = ReadInt("Write amount of rows: ");
            int[][] jagArr = new int[amountOfRows][];
            for (int i = 0; i < amountOfRows; i++)
            {
                while (true)
                {
                    try
                    {
                                
                        Console.WriteLine("Write row №{0} (elements divided by space)", i);
                        var inputString = Console.ReadLine().Trim(); // string 
                        var splittedString = inputString.Split(' '); // split string into string array
                        jagArr[i] = Array.ConvertAll(splittedString, int.Parse);
                        break;
                    }
                    catch (FormatException)
                    {
                        WriteErrorLine("Invalid string! Try again");
                    }
                }
            }
            return jagArr;
        }
        
        private static int[][] Point3GetJagArrFromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int[][] jagArr = new int[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                jagArr[i] = Array.ConvertAll(lines[i].Split(' '), Convert.ToInt32);
            }
            return jagArr;
       }

        private static int[,] Point2Get2dArrayFromConsole()
        {
            // Get paraments of array
            int rows = ReadInt("Insert amount of rows:");
            int elementsInRow = ReadInt("Insert amount of elements in row:");
            int[,] doubleArr = new int[rows, elementsInRow];
            WriteQuestionLine("Fill the array:");

            // Filling the array
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < elementsInRow; j++)
                {
                    doubleArr[i, j] = ReadInt();
                }
            }

            return doubleArr;
        }
        
        private static int[,] Point2Get2dArrayFromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int[,] doubleArr = new int[lines.Length, lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                int[] currentRow = Array.ConvertAll(lines[i].Split(' '), Convert.ToInt32);
                for (int j = 0; j < lines.Length; j++)
                {
                    doubleArr[i, j] = currentRow[j];
                }
            }

            return doubleArr;
        }

        static int ReadInt(string msg = "")
        {
            do
            {
                if (msg != "")
                {
                    WriteQuestionLine(msg);
                }

                try
                {
                    return Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    WriteErrorLine("Wrong input! Try again");
                }
                catch (OverflowException)
                {
                    WriteErrorLine("Your number's too big! Try again");
                }
            } while (true);
        }

        static void WriteErrorLine(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        static void WriteQuestionLine(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}