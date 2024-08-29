def kthMissing(arr,k):
    low = 0
    high = len(arr)-1
    while low<=high:
        mid = (low+high)//2
        missing = arr[mid]-(mid+1)
        if missing<k:
            low = mid+1
        else:
            high = mid-1
    
    print(high+1+k)
            
kthMissing([2,3,9,15],3)