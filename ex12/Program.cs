using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ex12
{
    delegate int[] sort(int[] arr);         // делегат для сортировки

    class Program
    {
        public static int countChange = 0;
        public static int countCompare = 0;

        static void DoSort(int[] arr, sort methodSort)
        {
            // сортировка неупорядоченного массива
            Console.WriteLine("Несортированный массив: " + String.Join(", ", arr));
            countChange = 0;                                    // обнуление счетчиков операций
            countCompare = 0;                                   // и времени
            int[] sortArr = methodSort(arr);
            Console.WriteLine("\nОтсортированный неупорядоченный массив: " + String.Join(", ", sortArr));
            Console.WriteLine("Затрачено {0} сравнений, {1} перессылок",
               countCompare, countChange);

            // сортировка упорядоченного по возрастанию массива
            countChange = 0;                                    // обнуление счетчиков операций
            countCompare = 0;                                   // и времени
            int[] sortSortedUpArr = methodSort(sortArr);
            Console.WriteLine("\nОтсортированный упорядоченный по возрастанию массив: " + String.Join(", ", sortSortedUpArr));
            Console.WriteLine("Затрачено {0} сравнений, {1} перессылок",
               countCompare, countChange);

            Array.Reverse(sortArr);
            countChange = 0;                                    // обнуление счетчиков операций
            countCompare = 0;                                   // и времени
            int[] sortSortedDownArr = methodSort(sortArr);
            Console.WriteLine("\nОтсортированный упорядоченный во убыванию массив: " + String.Join(", ", sortSortedDownArr));
            Console.WriteLine("Затрачено {0} сравнений, {1} перессылок",
              countCompare, countChange);
        }

        static int[] MergeSort(int[] arr)
        {
            countCompare++;
            if (arr.Length == 1)
                return arr;
            int midPos = arr.Length / 2;
            return Merge(MergeSort(arr.Take(midPos).ToArray()), MergeSort(arr.Skip(midPos).ToArray()));
        }

        static int[] Merge(int[] arr1, int[] arr2)
        {
            int a = 0, b = 0;                                   // позиции в массивах
            int[] merged = new int[arr1.Length + arr2.Length];  // новый массив, полученный в результате слияния arr1 и arr2
            for (int i = 0; i < arr1.Length + arr2.Length; i++)
            {
                if (b < arr2.Length && a < arr1.Length)
                {
                    countCompare++;
                    countChange++;
                    if (arr1[a] > arr2[b])
                        merged[i] = arr2[b++];
                    else
                        merged[i] = arr1[a++];
                }
                else
                {
                    countCompare++;
                    countChange++;
                    if (b < arr2.Length)
                        merged[i] = arr2[b++];
                    else
                        merged[i] = arr1[a++];
                }
            }
            return merged;
        }

        private static TreeElement root;
        private static readonly List<int> result = new List<int>(); // отсортированный массив

        private static void AddToTreeElement(int value, ref TreeElement localRoot)
        {
            if (localRoot == null)
            {
                countChange++;
                localRoot = new TreeElement(value);
                return;
            }
            countCompare++;
            if (localRoot.Data < value)
            {
                countChange++;
                AddToTreeElement(value, ref localRoot.Right);
            }
            else
            {
                countChange++;
                AddToTreeElement(value, ref localRoot.Left);
            }
        }

        public static void FormTree(int[] arr)
        {
            foreach (int el in arr)
                AddToTreeElement(el, ref root);
        }

        private static void GetSortedNumRec(TreeElement node)
        {
            // обход дерева лево -> корень -> право
            if (node != null)
            {
                GetSortedNumRec(node.Left);
                result.Add(node.Data);
                GetSortedNumRec(node.Right);
            }
        }

        static public int[] TreeSort(int[] arr)
        {
            root = null;
            result.Clear();
            FormTree(arr);
            GetSortedNumRec(root);
            return result.ToArray();
        }

        static void Main(string[] args)
        {
            int[] arr = new int[]{ 1, -2, -5, 9, 3, -1, 7, 2 };

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Сортировка слиянием:");
            Console.ResetColor();
            DoSort(arr, MergeSort);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nСортировка с помощью бинарного дерева:");
            Console.ResetColor();
            DoSort(arr, TreeSort);
        }
    }
}
