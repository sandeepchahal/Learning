public class SortAlgoritms
{
    static int count = 0;
    // 100, 20, 4, 5, 2, 54, 23, 35
    public static void SelectionSort(int[] num)
    {
        for (int i = 0; i < num.Length - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < num.Length; j++)
            {
                if (num[j] < num[minIndex])
                {
                    minIndex = j;
                }
            }
            if (minIndex != i)
            {
                int temp = num[i];
                num[i] = num[minIndex];
                num[minIndex] = temp;
            }
        }

        foreach (var item in num)
        {
            Console.WriteLine(item);
        }
    }


    // 100, 20, 4, 5, 2, 54, 23, 35
    public static void BubbleSort(int[] num)
    {
        bool swapped;
        for (int pass = 0; pass < num.Length - 1; pass++)
        {
            swapped = false;
            for (int j = 0; j < num.Length - pass - 1; j++)
            {
                if (num[j] > num[j + 1])
                {
                    int temp = num[j];
                    num[j] = num[j + 1];
                    num[j + 1] = temp;
                    swapped = true;
                }
            }
            if (!swapped)
            {
                break;
            }
        }
        foreach (var item in num)
        {
            Console.WriteLine(item);
        }

    }

    // 100, 20, 4, 5, 2, 54, 23, 35
    public static void InsertionSort(int[] num)
    {
        for (int i = 1; i < num.Length; i++)
        {
            int current = num[i];
            int j = i - 1;

            while (j >= 0 && num[j] > current)
            {
                num[j + 1] = num[j];
                j--;
            }
            num[j + 1] = current;
        }
        foreach (var item in num)
        {
            Console.WriteLine(item);
        }
    }

    public static void MergeSortAlgo(int[] num)
    {
        MergeSort.DivideArray(num);
        foreach (var item in num)
        {
            Console.WriteLine(item);
        }
    }

    // 100, 20, 4, 5, 2, 54, 23, 35
    public static void QuickSortAlgo(int[] num)
    {
        QuickSort.Process(num, 0, num.Length - 1);
    }


}