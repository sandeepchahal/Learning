def count(arr):
    count =1
    result =1
    for i in range(1,len(arr)):
        if ((arr[i-1]%2==0  and arr[i]%2 != 0) or (arr[i-1]%2 !=0 and arr[i]%2 ==0)):
            count+=1
            result = max(result,count)
        else:
            count =1
    return result
a = [1,2,5,2,2]
print(count(a))