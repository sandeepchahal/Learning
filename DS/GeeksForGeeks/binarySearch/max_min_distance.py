def minimiseMaxDistance(arr,k):
    low = 0
    high = getMax(arr)
    temp = 1e-6
    answer = high
    while (high-low) > temp:
        mid = (low + high) / 2.0
        count = getCount(arr,mid)
        if count<=k:
            high = mid
        else:
            low =mid
    return high

def getCount(arr,mid):
    count =0
    for i in range(1,len(arr),1):
        diff = ((arr[i]-arr[i-1])/mid)
        if (arr[i]-arr[i-1]) == (diff*mid):
            diff -=1
        count += diff
    return count
        
def getMax(arr):
    maxx = -1
    end =0
    for i in range(len(arr)-1,0,-1):
        diff = arr[i]-(arr[i-1])
        maxx = max(diff,maxx)
    return maxx
        
    
a = [1,2,3,4,5,6,7,8,9,10]
#print(getCount(a,0.67,4))
print(minimiseMaxDistance(a,1))
    
        