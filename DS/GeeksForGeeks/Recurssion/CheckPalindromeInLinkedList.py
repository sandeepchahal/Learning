from typing import Optional


class ListNode:
    def __init__(self, val=0, next=None):
        self.val = val
        self.next = next
        
def isPalindrome(head: Optional[ListNode]) -> bool:
    front= head
    def recursive(current_head:Optional[ListNode]):
        if current_head == None:
            return 
        result = recursive(current_head.next)
        if not result:
            return False
        if current_head != None and current_head.val == front.val:
            front = front.next
            return True
        else:
            return False
    recursive(head)
node1 = ListNode(1)
temp = node1
temp.next = ListNode(2)
temp.next.next = ListNode(4)

isPalindrome(node1)
        
            
        