def solve(mylist,k, index):
        n = len(mylist)
        if n ==1:
            return mylist[0]
        while index+k>n:
            index = index-n
        index = abs(index+k-1)
        mylist.pop(index)
        return solve(mylist,k,index)

def realSolve(n,k):
    if n ==1:
        return 0
    return (realSolve(n-1, k)+k)%n

mylist = [i for i in range(5)]
#print(solve(mylist,3,0))
print(realSolve(3,2))
    