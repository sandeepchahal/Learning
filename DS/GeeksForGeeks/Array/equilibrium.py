def equalibrium(arr):
    r = sum(arr[0:])
    l = 0
    for i in range(0, len(arr)):
        r -= arr[i]
        if l ==r:
            print(i)
            return True
        l +=arr[i]
    return False
a = [-3,5,0,2]

print(equalibrium(a))