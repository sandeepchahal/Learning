def sliding_window(arr,k):
    last_sum = sum(arr[0:k])
    result = last_sum
    for i in range(k,len(arr)):
        last_sum= last_sum+arr[i]-arr[i-k]
        if last_sum>result:
            result = last_sum
    return result
    
a = [1,8,3,20,7]
print(sliding_window(a,2))