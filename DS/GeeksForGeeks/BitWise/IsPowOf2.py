def IsPowOfTwo(num):
    if num ==0:
        return False
    if(num&(num-1) ==0):
        return True
    return False
print(IsPowOfTwo(1))