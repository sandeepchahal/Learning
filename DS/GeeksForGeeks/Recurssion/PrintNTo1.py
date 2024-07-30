def PrintNTo1(n):
    if n ==0:
        return
    print(n)
    PrintNTo1(n-1)

PrintNTo1(5)