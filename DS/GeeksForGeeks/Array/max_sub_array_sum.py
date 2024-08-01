def max_sum(arr):
    result = arr[0]
    prev = arr[0]
    for i in range(1,len(arr)):
        prev = max(prev+arr[i], arr[i])
        result = max(result,prev)
    return result  
  
a = [5,-2,3]
print(max_sum(a))
