

def cutRope(n, a,b,c):
    if n < 0:
        return -1
    if n ==0:
        return 0

    result = max(cutRope(n-a, a, b,c),
    cutRope(n-b, a, b,c),
    cutRope(n-c, a, b,c))

    if result ==-1:
        return -1
    return result+1

print(cutRope(23,12,9,11))



