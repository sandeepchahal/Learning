def one_count(arr):
    result = 0
    current =0
    for i in arr:
        if i ==1:
            current +=1
        else:
            if current>result:
                result = current
            current =0
    if current> result:
        result = current
    return result
a = [1,1,1,1,1,1,1,0,0,0,0,1,1,1,1,0,0]
print(one_count(a))     