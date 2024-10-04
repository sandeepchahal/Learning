# class TreeNode:
from typing import Optional
from collections import deque

class TreeNode:
    def __init__(self, val=0, left=None, right=None):
        self.val = val
        self.left = left
        self.right = right
        
def rightSideView(root: Optional[TreeNode]):
    if not root:
        return []
    output = []
    queue = deque([root])
    while queue:
        level_size = len(queue)
        for i in range(level_size):
            node = queue.popleft()
            if i == level_size-1:
                output.append(node.val)

            if node.left:
                queue.append(node.left)
            if node.right:
                queue.append(node.right)
                
                
    print(output)        
        
def list_to_tree(lst):
    if not lst or lst[0] is None:
        return None

    # Create the root node
    root = TreeNode(lst[0])
    queue = deque([root])
    index = 1

    while queue:
        node = queue.popleft()

        # Add left child
        if index < len(lst) and lst[index] is not None:
            node.left = TreeNode(lst[index])
            queue.append(node.left)
        index += 1

        # Add right child
        if index < len(lst) and lst[index] is not None:
            node.right = TreeNode(lst[index])
            queue.append(node.right)
        index += 1

    return root

arr = [1,2,3,None,4,5,None,8,9,None,6,None,None,None,None,7]
# Converting the list into a binary tree
n = len(arr)
root = list_to_tree(arr)


rightSideView(root=root)

