def Power(n,k): # n =2 and k = 3 mean 2*2*2 = 8
    if k ==1:
        return n
    if k%2 ==0:
        half_power = Power(n, k // 2)
        return half_power*half_power
    return n*Power(n,k-1)

print(Power(7,1))
    