MyLinkList linkList = new MyLinkList();

int[] arr = { 1, 2, 3, 4, 5, 6, 7 };

foreach (var item in arr)
{
    linkList.Insert(item);
}

linkList.Print();
System.Console.WriteLine(" ------------------------------");

linkList.Delete(2);
linkList.Delete(5);
linkList.Delete(7);

linkList.Print();