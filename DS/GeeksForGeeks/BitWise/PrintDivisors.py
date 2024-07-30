def Divisors(num):
    for i in range (1,int(num**0.5)+1):
        if(num %i ==0):
            print(i)
    for i in range(int(num**0.5),0,-1):
        if(num % i ==0):
            print(int(num/i))

Divisors(15)