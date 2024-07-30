arr = [1,0,2,3,0,4,5,0]
i =0
i =0
j =arr[0]
n = len(arr)
while i < len(arr)-1:
        if arr[i] == 0:
            arr.insert(i, 0)
            i+= 2
            arr.pop(len(arr)-1)
        else:
            i+= 1
    
                    
         