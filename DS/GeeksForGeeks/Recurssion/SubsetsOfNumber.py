def subsets(mylist:list[int]):
    output=[]
    result = set()
    for i in range(len(mylist)):
        result.add(mylist[i])
        output.append(set(result))
        for j in range(i+1,len(mylist)):
            temp = result.copy()
            temp.add(mylist[j])
            output.append(set(temp))    
    print(output)

subsets([1,2,3])
        