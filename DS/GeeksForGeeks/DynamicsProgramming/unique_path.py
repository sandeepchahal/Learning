def uniquePaths(m, n):
    def f(mm,nn):
        if mm ==0 and nn ==0:
            return 1
        if mm < 0 or nn<0 :
            return 0
        return f(mm,nn-1) + f(mm-1,nn)
    return f(m-1,n-1)
print(uniquePaths(3,7))