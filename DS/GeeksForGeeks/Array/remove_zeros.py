def remove_zeros(a):
    z =0
    val =0
    for index,item in enumerate(a):
        if item != val:
            a[z]= item
            a[index]= 0
            z +=1
    print(a)      
                
a = [0,10,0,11,12,0,0,0,1,0]
print(remove_zeros(a))