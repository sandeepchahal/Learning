def delete_all(arr, val):
    i =0
    for j in range(len(arr)):
        if arr[j] != val:
            arr[i]= arr[j]
            i +=1
    return arr[:i]

a= [0,1,2,2,3,0,4,2]
print(delete_all(a,2))