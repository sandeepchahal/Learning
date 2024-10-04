def cowCount(arr,diffRequested, cows):
    i =0
    j =1
    count =1
    while i<len(arr) and j<len(arr):
        diff = arr[j]-arr[i]
        if diff<diffRequested:
            j +=1
        else:
            count +=1
            i = j
            j+=1
    if count>=cows:
        return True
    return False



a = [4,2,1,3,6]
a = sorted(a)
number_of_cows = 2
final = -1

low = min(a)
high = max(a)-low
while low<= high:
    mid = (low+high)//2
    if cowCount(a,mid,number_of_cows):
        final = max(final,mid)
        low = mid+1
    else:
        high = mid-1
print(final)
    