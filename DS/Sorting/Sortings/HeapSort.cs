public static class HeapSort
{

    public static void Sort(int[] array)
    {
        int length = array.Length;
        if (length == 1)
        {
            Console.WriteLine($"Sorted array is - {array[0]}");
        }
        else
        {
            for (int i = (length / 2) - 1; i >= 0; i--)
            {
                Heapify(array, i, length);
            }

            for (int i = length - 1; i > 0; i--)
            {
                int temp = array[0];
                array[0] = array[i];
                array[i] = temp;

                Heapify(array, 0, i);
            }

            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
        }

    }


    // i is the index of last root node. 
    // n is the size of heap
    // array is the heap
    private static void Heapify(int[] array, int i, int n)
    {
        int largest = i;
        int left = (2 * i) + 1;
        int right = (2 * i) + 2;

        if (left < n && array[left] > array[largest])
        {
            largest = left;
        }
        if (right < n && array[right] > array[largest])
        {
            largest = right;
        }

        if (largest != i)
        {
            int temp = array[i];
            array[i] = array[largest];
            array[largest] = temp;

            Heapify(array, largest, n);
        }

    }
}