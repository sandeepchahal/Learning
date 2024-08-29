public class MyLinkList
{
    private Node? head = null;
    private Node? tail = null;

    public void Insert(int num)
    {
        Node node = new Node(num);
        if (head == null)
        {
            head = node;
            tail = node;
        }
        else
        {
            tail!.next = node;
            tail = node;
        }
    }

    // 10, 1 --> 20, 2 --> 30, null 

    public void Delete(int num)
    {
        Node? temp = head;
        if (head?.value == num)
        {
            head = head.next;
            if (head == null)
            {
                tail = null;
            }
        }
        else
        {
            Node? prev = temp;
            while (temp != null)
            {
                if (temp.value == num)
                {
                    if (temp.next == null)
                    {
                        tail = prev;
                    }
                    prev!.next = temp.next;
                    break;
                }
                prev = temp;
                temp = temp.next;
            }
        }
    }
    public void Print()
    {
        Node? temp = head;
        while (temp != null)
        {
            System.Console.WriteLine(temp.value);
            temp = temp.next;
        }
    }

}

public class Node
{
    public Node? next { get; set; } = null;
    public int value { get; set; }

    public Node(int val)
    {
        value = val;
        next = null;
    }

}
