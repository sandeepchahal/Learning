from typing import List


def validMountainArray(arr: List[int]) -> bool:
    if len(arr)<3:
        return False
    j =0
    for i in range(1,len(arr)):
        if arr[i] ==arr[i-1]:
            return False
        elif arr[i]<arr[i-1]:
            j =i-1
            break
    if j ==0:
        return False
        
    for i in range(j,len(arr)):
        if arr[i]<=arr[i+1]:
            return False
    return True

validMountainArray([14,82,89,84,79,70,70,68,67,66,63,60,58,54,44,43,32,28,26,25,22,15,13,12,10,8,7,5,4,3])