def GCD(a, b):
    if(b ==0):
        return a
    else:
        return GCD(b, a%b)
print(GCD(21,14))