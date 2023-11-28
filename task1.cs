using System;
using System.Text.RegularExpressions;

namespace ConsoleApp29
{
    class Program
    {
       
        // Метод  поиска минимального значения 
        static void FindMinValue(/*ref int[,] array, int n*/in int[,] array, int n, out int min)//out передает параметр по ссылке(переменную можно не инициализировать)
        {
            min = 1000000000;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (array[i, j] < min)
                    {
                        min = array[i, j];
                    }
                }
            }
            Console.WriteLine("Минимальное значение в массиве: " + min);
        }
        static void FindEACHMinValue(/*ref int[,] array, int n*/in int[,] array, int n,int i)// in передает параметр по ссылке(но параметр внутри метода менять нельзя)
        {
            

              int min = 1000000000;
                for (int j = 0; j < n; j++)
                {
                    if (array[i, j] < min)
                    {
                        min = array[i, j];
                    }
                }
                Console.WriteLine("Минимальное  значение в строке" + (i + 1) + ": " + min);
            
           
        }

        // Метод поиска максимального значения 
        static void FindMaxValue(ref int[,] array,int n) //ref позволяет передавать параметр по ссылке, а не по значению(т.е. этот параметр  можно изменить внутри метода)(переменную нужно инициализировать)
        {
            int max = -1000000000;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (array[i, j] > max)
                    {
                        max = array[i, j];
                    }
                }
            }
            
            Console.WriteLine("Максимальное значение в массиве: " + max);
        }

        static void FindEACHMaxValue(ref int[,] array, int n,int i) //При передаче параметров по ссылке перед параметрами используется модификатор ref
        {
            
           int max = -1000000000;
                for (int j = 0; j < n; j++)
                {
                    if (array[i, j] > max)
                    {
                        max = array[i, j];
                    }

                }
                Console.WriteLine("Максимальное значение в строке" + (i+1) + ": " + max);
            
            
        }

        // Метод для вычисления суммы каждой строки массива
        static void FindSumOfEachRow(/*ref int[,] array, int n*/in int[,] array, int n)//Модификатор in указывает, что данный параметр будет передаваться в метод по ссылке, однако внутри метода его значение параметра нельзя будет изменить.
        {
            for (int i = 0; i < n; i++)
            {
                int sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum += array[i, j];
                }
                Console.WriteLine("Сумма элементов в строке " +  (i+1) + ": "  + sum);
            }
        }

        //проверяем является ли j элемент массива числом
        static bool chislo(string[] input,int j)
        {
            
            if (int.TryParse(input[j],out int value))// преобразовать строку к типу int и, если преобразование прошло успешно, то возвращает true(в переменную)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Введите количество строк массива: ");
            int n = Convert.ToInt32(Console.ReadLine());

            int[,] array = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                Console.Write("Введите " +  n  + " значений для строки " + (i + 1) + ": ");
                
                string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries); 
                //Метод "Split" разбивает строку на подстроки на основе пробела
                //Метод "StringSplitOptions.RemoveEmptyEntries" говорит о том, что необходимо удалить пустые подстроки из результирующего массива.
                
                //для каждого элемента массива строки проверяем является ли элемент числом
                for (int j = 0; j < n; j++)
                {
                    // проверяем является символ числом  ( !int.TryParse(input[j], out array[i,j]) array[i,j] = 0)
                    if (chislo(input, j))
                        {
                            array[i, j] = int.Parse(input[j]);
                        }
                        else
                        {
                            array[i, j] = 0;
                        }
                    
                }
            }


            Console.WriteLine("Измененный массив:");
            for (int i = 0; i < n; i++)
            {
                //выводим строку
                for (int j = 0; j < n; j++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine();
            }

            
            /*int min;
            FindMinValue(in array, n, out min);
            FindMaxValue(ref array, n);
            FindSumOfEachRow(in array, n);
            Console.WriteLine();*/

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Для " + (i + 1) + " cтроки ");
                FindEACHMinValue(in array,n, i);
                FindEACHMaxValue(ref array,n, i);
                
                Console.WriteLine();
            }
            FindSumOfEachRow(in array, n);
        }
    }
}











/*
 * //string[] input = Console.ReadLine().Split(' ');
 Console.Write("Введите количество строк массива: ");
        int n = int.Parse(Console.ReadLine());

        int[,] array = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            Console.Write($"Введите значения для строки {i + 1}: ");
            string[] inputValues = Console.ReadLine().Split(' ');

            for (int j = 0; j < n; j++)
            {
                array[i, j] = int.TryParse(inputValues[j], out int value) ? value : 0;
            }
        }

        int minValue, maxValue;
        int[,] arrayCopy = new int[n, n];
        arrayCopy = (int[,])array.Clone(); // Создаем копию массива для использования в методах

        FindMinValue(in arrayCopy, n, out minValue);
        FindMaxValue(in arrayCopy, n, out maxValue);
        FindSumOfEachRow(in arrayCopy, n);

        Console.WriteLine("Измененный массив:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write($"{arrayCopy[i, j]} ");
            }
            Console.WriteLine();
        }

        Console.WriteLine($"Минимальное значение в массиве: {minValue}");
        Console.WriteLine($"Максимальное значение в массиве: {maxValue}");
    }

    static void FindMinValue(in int[,] array, int n, out int minValue)
    {
        minValue = int.MaxValue;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (array[i, j] < minValue)
                {
                    minValue = array[i, j];
                }
            }
        }
    }

    static void FindMaxValue(in int[,] array, int n, out int maxValue)
    {
        maxValue = int.MinValue;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (array[i, j] > maxValue)
                {
                    maxValue = array[i, j];
                }
            }
        }
    }

    static void FindSumOfEachRow(in int[,] array, int n)
    {
        for (int i = 0; i < n; i++)
        {
            int rowSum = 0;
            for (int j = 0; j < n; j++)
            {
                rowSum += array[i, j];
            }
            Console.WriteLine($"Сумма элементов в строке {i + 1}: {rowSum}");
        }
 */