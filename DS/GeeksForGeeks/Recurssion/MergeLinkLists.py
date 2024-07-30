from typing import Optional

class ListNode:
    def __init__(self, val=0, next=None):
        self.val = val
        self.next = next

def mergeTwoLists(list1: Optional[ListNode], list2: Optional[ListNode]) -> Optional[ListNode]:
    if not list1:
            return list2
    if not list2:
        return list1
        
        # Recursive case
    if list1.val <= list2.val:
        list1.next = mergeTwoLists(list1.next, list2)
        return list1
    else:
        list2.next = mergeTwoLists(list1, list2.next)
        return list2

node1 = ListNode(1)
temp = node1
temp.next = ListNode(2)
temp.next.next = ListNode(4)


node2 = ListNode(1)
temp = node2
temp.next = ListNode(3)
temp.next.next = ListNode(4)

result = mergeTwoLists(node1,node2)
while(result != None):
    print(result.val)
    result = result.next

