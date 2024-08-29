from typing import List


def shipWithinDays(weights: List[int], days: int) -> int:
    high = sum(weights)
    low = high//days
    answer = high
    while low<=high:
        mid = (low+high)//2
        noDays = noOfDays(weights,mid)
        if noDays<=days:
            answer = min(answer,mid)
            high = mid-1
        else:
            low = mid+1
    return answer

def noOfDays(arr,weight):
    days =0
    total =0
    for i in arr:
        if total+i>weight:
            days +=2
            total =i


a = [1,2,3,4,5,6,7,8,9,10]

print(shipWithinDays(a,5))