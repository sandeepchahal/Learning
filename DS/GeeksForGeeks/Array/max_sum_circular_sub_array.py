def get_sum(arr):
    result =arr[0]
    prev_sum = arr[0]
    next_sum =0
    for i in range(1,len(arr)):
        prev_sum = max(prev_sum+arr[i], arr[i])
        next_sum = sum(arr[i+1:])
        circular_sum = next_sum+sum(arr[:i])
        print(f" Prev - {prev_sum} - next - {next_sum} - circular sum - {circular_sum}")
        result = max(next_sum,prev_sum,circular_sum, result)
    return result    
    
a = [8,-4,3,-5,4]
print(get_sum(a)) 