public static class RadixSort
{

    // 100, 81, 1300, 5, 6, 11, 7 
    public static void Sort(int[] array)
    {
        int maxDigit = GetMax(array);

        for (int i = 1; maxDigit / i > 0; i *= 10)
        {
            CountingSort(array, array.Length, i);
        }
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write($"{array[i]} ");
        }
    }

    // 100, 81, 1300, 5, 6, 11, 7 
    private static void CountingSort(int[] array, int n, int exp)
    {
        int[] outputArray = new int[n];
        int[] counts = new int[10];

        // initialize count[0].... equals 0

        for (int i = 0; i < counts.Length; i++)
        {
            counts[i] = 0;
        }

        for (int i = 0; i < n; i++)
        {
            counts[(array[i] / exp) % 10]++;
        }

        for (int i = 1; i < 10; i++)
        {
            counts[i] += counts[i - 1];
        }

        for (int i = n - 1; i >= 0; i--)
        {
            outputArray[counts[(array[i] / exp) % 10] - 1] = array[i];
            counts[(array[i] / exp) % 10]--;
        }

        for (int i = 0; i < n; i++)
        {
            array[i] = outputArray[i];
        }


    }

    private static int GetMax(int[] array)
    {
        int max = array[0];
        for (int i = 0; i < array.Length; i++)
        {
            if (max < array[i])
            {
                max = array[i];
            }
        }
        return max;
    }
}