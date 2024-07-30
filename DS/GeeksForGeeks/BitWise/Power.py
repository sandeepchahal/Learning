def Power(x,n):
    if(n ==0):
        return 1
    temp = Power(x,int(n/2))
    if n%2 ==0:
        return temp*temp
    else:
        return temp * temp * x

def PowerBitApproach(x,n):
    result =1
    count =1
    while n>0:
        if n%2 != 0:
            for i in range(count):
                result = result*x
        x =x*x
        n = n//2
    return result




print(PowerBitApproach(3,10))
