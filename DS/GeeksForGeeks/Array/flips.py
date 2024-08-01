def flips(arr):
    start_index =-1
    val = arr[1]
    end_index = -1
    for i in range(1,len(arr)):
        if val == arr[i]:
            if start_index ==-1:
                start_index =i
            end_index = i
        elif val != arr[i] and end_index !=-1:
            print(f"from  {start_index} to {end_index}")
            start_index =-1
            end_index =-1
    print(f"from  {start_index} to {end_index}")
            
a = [1,0,0,0]
print(flips(a))