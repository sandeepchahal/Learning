def noOfBouquets(arr, m, k):
    # m no of bouquets
    # k consective flowers
    
    n = len(arr)
    if (m*k>n):
        return -1
    
    low = min(arr)
    high = max(arr)
    answer = float('Inf')
    while low<=high:
        mid= (low+high)//2
        days = noOfDays(arr,mid,k)
        if days>=m:
            answer = min(answer,mid)
            high = mid-1
        else:
            low  = mid+1
        
    return answer    
        
def noOfDays(arr,days,k):
    counter =0
    total =0
    for i in arr:
        if i<=days:
            counter+=1
        else:
            total += counter//k
            counter = 0
    
    total += counter//k
    return total
                
                
a= [7,7,7,7,13,11,12,7]

print(noOfBouquets(a,2,3))