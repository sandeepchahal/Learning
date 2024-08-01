def get_water(arr):
    length = len(arr)
    if length<3:
        print("Length should be atleast 3")
        return 0
    result =0
    
    lmax = [0]* length
    rmax = [0]* length
    
    lmax[0] = arr[0]
    rmax[length-1] = arr[length-1]
    
    for i in range(1,length):
        lmax[i] = max(lmax[i-1], arr[i])
            
    for i in range(length-2, -1, -1):
        rmax[i]= max(rmax[i+1], arr[i])
        
    for i in range(1,length):
        result += min(lmax[i], rmax[i])- arr[i]
    return result
        
a = [3,1,2]
print(get_water(a))