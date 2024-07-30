
def fibnacciSeries(n):
    if n <=1:
        return n
    return fibnacciSeries(n-1)+fibnacciSeries(n-2)

print(fibnacciSeries(4))
    