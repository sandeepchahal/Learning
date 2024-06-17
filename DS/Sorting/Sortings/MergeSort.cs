public static class MergeSort
{
    // 100, 20, 4, 5, 2, 54, 23, 35
    public static void DivideArray(int[] array)
    {
        int size = array.Length;
        if (size == 1)
        {
            return;
        }
        else
        {
            int mid = size / 2;
            int[] left = new int[mid];
            int[] right = new int[size - mid];
            Array.Copy(array, 0, left, 0, mid);
            Array.Copy(array, mid, right, 0, size - mid);
            DivideArray(left);
            DivideArray(right);
            MergeArray(array, left, right);
        }
    }

    // [10 34] [4 54]
    //[4, 10, 34, ]
    private static void MergeArray(int[] array, int[] left, int[] right)
    {
        int leftIndex = 0, rightIndex = 0, mergeIndex = 0;

        while (leftIndex < left.Length && rightIndex < right.Length)
        {
            if (left[leftIndex] <= right[rightIndex])
            {
                array[mergeIndex] = left[rightIndex];
                leftIndex++;
            }
            else
            {
                array[mergeIndex] = right[rightIndex];
                rightIndex++;
            }
            mergeIndex++;
        }

        while (leftIndex < left.Length)
        {
            array[mergeIndex] = left[leftIndex];
            mergeIndex++;
            leftIndex++;
        }

        while (rightIndex < right.Length)
        {
            array[mergeIndex] = right[rightIndex];
            rightIndex++;
            mergeIndex++;
        }

    }


}