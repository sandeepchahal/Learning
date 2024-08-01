def majority(arr):
    my_counter = {}
    for i in arr:
        if i not in my_counter:
            my_counter[i]=1
        else:
            my_counter[i]+=1
    print(max(my_counter.values()))
a = [1,2,4,4,4,2,2]
majority(a)


 