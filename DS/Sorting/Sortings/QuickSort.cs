public static class QuickSort
{

    // 100, 20, 4, 5, 2,
    public static void Process(int[] num, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = Partition(num, left, right);
            Process(num, left, pivotIndex - 1);
            Process(num, pivotIndex + 1, right);
        }
    }

    private static int Partition(int[] array, int left, int right)
    {
        int pivot = array[array.Length - 1];
        int i = left - 1;
        for (int j = left; j < right; j++)
        {
            if (array[j] <= pivot)
            {
                i++;
                Swap(array, i, j);
            }
        }
        Swap(array, i + 1, right); // Move pivot to its correct place
        return i + 1;
    }
    private static void Swap(int[] array, int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
}