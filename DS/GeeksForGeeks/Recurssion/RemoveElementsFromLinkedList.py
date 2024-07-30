from typing import Optional

class ListNode:
    def __init__(self, val=0, next=None):
        self.val = val
        self.next = next
        
def removeElements(head: Optional[ListNode], val: int) -> Optional[ListNode]:
        if head is None:
            return head
        new_head = removeElements(head.next,val)
        if head.val == val:
            return new_head
        else:
            head.next = new_head
            return head
        
            
        
node1 = ListNode(1)


removeElements(node1,4)
