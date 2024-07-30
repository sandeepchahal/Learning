var list = new int[] { 100, 20, 4, 5, 11 };

//SortAlgoritms.SelectionSort(list);
//SortAlgoritms.BubbleSort(list);
//SortAlgoritms.InsertionSort(list);
//SortAlgoritms.MergeSortAlgo(list);
//SortAlgoritms.QuickSortAlgo(list);
//HeapSort.Sort(new int[] { 10, 8, 13, 5, 6, 11, 7 });

//RadixSort.Sort(new int[] { 100, 81, 1300, 5, 6, 11, 7 });

List<int> list1 = new List<int> { 1, 3, 5, 7, 9 };

list1.Sort();
Int64 minSum = 0, maxSum = 0;
for (int i = 0; i < list1.Count; i++)
{
    if (i < list1.Count - 1)
    {
        minSum += list1[i];
    }
    if (i > 0)
    {
        maxSum += list1[i];
    }
}
Console.WriteLine($"{minSum} {maxSum}");
Console.ReadLine();





