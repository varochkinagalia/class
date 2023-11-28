using System;
using System.Collections.Generic;

namespace ConsoleApp31
{
    class MyArray
    {
        private int[] data;//поле класса

        public MyArray(int size)
        {
            data = new int[size];
        }

        public void InputData()
        {
            Console.WriteLine("Введите элементы массива:");
            for (int i = 0; i < data.Length; i++)
            {
                Console.Write("Элемент " + (i + 1) + ": ");
                data[i] = Convert.ToInt32(Console.ReadLine());
            }
        }

        public void InputDataRandom()
        {
            Random random = new Random();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = random.Next(100);
            }
        }

        public void Print(int startIndex, int endIndex)
        {
            if (startIndex >= 0 && endIndex < data.Length && startIndex <= endIndex)
            {
                Console.WriteLine("Элементы массива:");
                for (int i = startIndex; i <= endIndex; i++)
                {
                    Console.WriteLine("Элемент " + i + ": " + data[i]);
                }
            }
            else
            {
                Console.WriteLine("неправильные индексы массива");
            }
        }

        public List<int> FindValue(int value)
        {
            List<int> indices = new List<int>();
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == value)
                {
                    indices.Add(i);
                }
            }
            return indices;
        }

        public void DelValue(ref int value)//передача параметра по ссылке
        {
            List<int> indicesToRemove = new List<int>();
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == value)
                {
                    indicesToRemove.Add(i);
                }
            }
            if (indicesToRemove.Count > 0)
            {
                int[] newData = new int[data.Length - indicesToRemove.Count];
                int newIndex = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    if (!indicesToRemove.Contains(i))//если номер не соответствует номеру элемента, который надо удалить, то заменяем
                    {
                        newData[newIndex] = data[i];
                        newIndex++;
                    }
                }
                data = newData;
            }
        }

        public int FindMax(out int index)
        {
            int max = data[0];
            index = 0;
            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] > max)
                {
                    max = data[i];
                    index = i;
                }
            }
            return max;
        }

        public void Add( int[] array2)
        {
            int[] m = new int[data.Length];

            for (int j = 0;j<array2.Length;j++)
            {
                Console.Write("Элемент " + j + ": ");
                array2[j] = Convert.ToInt32(Console.ReadLine());
            }
            if (data.Length == array2.Length)
            {
                //Console.WriteLine("Элементы массива:");
                for (int i = 0; i < data.Length; i++)
                {
                   // data[i] += array2[i];// для сложения массива 
                    m[i] = data[i] + array2[i];
                }
                Console.WriteLine("Элементы массива:");
                for (int j = 0; j < m.Length; j++)
                {
                    Console.WriteLine("Элемент " + j + ": " + m[j]);
                    
                }
            }
            else
            {
                Console.WriteLine("Длина не совпадает");
            }
        }

        public void Sort()
        {
            
            for (int i = 0; i < data.Length - 1; i++)
            {
                for (int j = i+1;j<data.Length;j++)
                {
                    if (data[i]>data[j])
                    {
                        int temp = data[j];
                        data[j] = data[i];
                        data[i] = temp;
                    }
                }
                
            }
        }

        public int length()
        {
            return data.Length;
            /*int count = 0;
            foreach(int i in data)
            {
                count+=1;
            }
            return count;*/
        }
    }
    class Program
    {
        static void Main()
        {
            Console.Write("Введите размер массива: ");
            int size = Convert.ToInt32(Console.ReadLine());
            MyArray myArray = new MyArray(size);//создали обьект класса массив

            Console.WriteLine();

            myArray.InputData();

            Console.WriteLine();

            Console.WriteLine("Вывод массива: ");
            myArray.Print(0, size - 1);

            Console.WriteLine();

            Console.Write("Введите значение для поиска: ");
            int searchValue = Convert.ToInt32(Console.ReadLine());
            List<int> indices = myArray.FindValue(searchValue);
            if (indices.Count > 0)
            {
                Console.WriteLine("Значение " + searchValue + " было найдено под индексом: " + string.Join(", ", indices));
            }
            else
            {
                Console.WriteLine("Значение " + searchValue + " не найдено.");
            }

            Console.WriteLine();


            Console.Write("Введите значение для удаления: ");
            int deleteValue = Convert.ToInt32(Console.ReadLine());
            myArray.DelValue(ref deleteValue);
            Console.WriteLine();

            Console.WriteLine("Массив после удаления:");
            int l = myArray.length();
            myArray.Print(0,l-1);
            Console.WriteLine();

            Console.WriteLine("Максимальное значение:");
            int maxIndex;
            int max = myArray.FindMax(out maxIndex);
            Console.WriteLine("Максимальное значение: " + max + ", Индекс:  " + maxIndex);
            Console.WriteLine();


            Console.WriteLine("Сложение: ");
            Console.Write("Введите размера массива для сложения: ");
            int size2 = Convert.ToInt32(Console.ReadLine());
            int[] array2 = new int[size2];
            myArray.Add(array2);
            int l0 = myArray.length();
            //myArray.Print(0, l0 - 1);// для сложения массива
            Console.WriteLine();


            Console.WriteLine("Сортировка: ");
            myArray.Sort();
            int l1 = myArray.length();
            myArray.Print(0, l1 - 1);
        }
    }
    
}
