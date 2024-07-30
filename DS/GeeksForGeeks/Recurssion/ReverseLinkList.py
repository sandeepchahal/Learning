from typing import Optional


class ListNode:
    def __init__(self, val=0, next=None):
        self.val = val
        self.next = next
class Solution:
    def reverseList(self, head: Optional[ListNode]) -> Optional[ListNode]:
        if head.next == None:
            return head
        temp = self.reverseList(head.next)
        temp.next.next = head
        head.next = None
        return temp