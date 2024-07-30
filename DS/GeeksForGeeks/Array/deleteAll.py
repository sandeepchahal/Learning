def delete_all(arr, val):
    length = len(arr)
    for index,item in enumerate(arr):
        if item == val:
            length -= 1
            start = index
            for i in range(start+1,len(arr)):
                if arr[i] != val:
                    arr[start]= arr[i]
                    start +=1
    print(arr[:length])


a = [2,12,34,123,12,12,12]
delete_all(a,12)