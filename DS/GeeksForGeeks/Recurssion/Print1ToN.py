def Print1ToN(n):
    if n ==0:
        return
    Print1ToN(n-1)
    print(n)
Print1ToN(4)