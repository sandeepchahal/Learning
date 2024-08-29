def findRepeating(arr):
    slow = arr[0]+1
    fast = arr[0]+1
    
    while True:
        slow = arr[slow]+1
        fast = arr[arr[fast]+1]+1
        if slow == fast:
            break
    slow = arr[0]+1
    while True:
        if slow == fast:
            break
        fast = arr[fast]+1
        slow  = arr[slow]+17
        
    print(slow-1)
    

a = [0,2,1,3,4,4,4]
findRepeating(a)