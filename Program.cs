using System;
using System.IO;

namespace lab_1
{
    class Program
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        static void Main(string[] args)
        {
            do
            {
                int point = readInt("Which point should I run? Regular array (1), 2D array (2), step array (3) or exit (4)?");
                switch (point)
                {
                    case 1:
                        point1();
                        break;
                    case 2:
                        point2();
                        break;
                    case 3:
                        point3();
                        break;
                    case 4:
                        return;
                    default:
                        writeErrorLine("Wrong input! Try again");
                        break;
                }
            } while (true);
        }

        static void point1()
        {
            int[] arr;
            int n;
            int evenAmount = 0;

            // Checking input method + filling up array
            int inputWay;
            do { 
                inputWay = readInt("Should I get numbers from console (1) or from file (2)?");
                if(inputWay == 1 || inputWay == 2){
                    break;
                }
            } while(true);

            if (inputWay == 1) 
            {
                    n = readInt();
                    arr = new int[n];
                    Console.WriteLine("Fill up the array (10 numbers): ");
                    for (int i = 0; i < n; i++)
                    {
                        arr[i] = readInt();
                    }
            } else {
                try{
                    string filePath = Directory.GetCurrentDirectory() + @"\fileArray.txt";
                    arr = Array.ConvertAll(File.ReadAllLines(filePath), int.Parse);
                } catch(System.FormatException){
                    writeErrorLine("Invalid file");
                    return;
                } catch(FileNotFoundException){
                    writeErrorLine("File not found");
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

        static void point2()
        {
            // Get paraments of array
            int rows = readInt("Insert amount of rows:");
            int elementsInRow = readInt("Insert amount of elements in row:");
            int[,] doubleArr = new int[rows, elementsInRow];
            writeQuestionLine("Fill the array:");

            // Filling the array
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < elementsInRow; j++)
                {
                    doubleArr[i, j] = readInt();
                }
            }
            int max = 0;
            int indexOfMax = 0;
            int min = doubleArr[0, 0];
            int indexOfMin = 0;

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
                        indexOfMin = i * rows + j; // Amount of rows * Serial number of current row + Serial number of element
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
            Console.WriteLine("Max: " + max + " (№" + indexOfMax + ")");
            Console.WriteLine("Min: " + min + " (№" + indexOfMin + ")");

            // Changing element
            bool anoutherChange = true;
            string wantToChangeQ = "Want to change element of array? Yes (1), no (2)?";
            do
            {
                int wantToChange = readInt(wantToChangeQ);
                switch (wantToChange)
                {
                    case 1:
                        // Getting row of element
                        int rowToChange;
                        while (true)
                        {
                            rowToChange = readInt("Write row:");
                            if (rowToChange < rows)
                            {
                                break;
                            }
                            writeErrorLine("Index was outside the bounds of the array. Try again");
                        }

                        // Getting column of element
                        int columnToChange;
                        while (true)
                        {
                            columnToChange = readInt("Write column:");
                            if (columnToChange < elementsInRow)
                            {
                                break;
                            }
                            writeErrorLine("Index was outside the bounds of the array. Try again");
                        }

                        // Changing element
                        doubleArr[rowToChange, columnToChange] = readInt("What this element should be?");
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
                        writeErrorLine("Wrong input! Try again");
                        break;
                }
            } while (anoutherChange == true);
        }

        static void point3()
        {

        }

        static int readInt(string msg = "")
        {
            do
            {
                if (msg != "")
                {
                    writeQuestionLine(msg);
                }
                try
                {
                    return Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    writeErrorLine("Wrong input! Try again");
                }
                catch (OverflowException)
                {
                    writeErrorLine("Your number's too big! Try again");
                }
            } while (true);
        }
        static void writeErrorLine(string msg){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
        static void writeQuestionLine(string msg){
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}
