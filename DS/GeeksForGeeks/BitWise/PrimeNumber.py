def IsPrime(num):
    if(num<=1):
        return "no"
    if num%2 ==0 or num%3 == 0:
        return "yes"
    for i in range(5,int(num**0.5)+1, 6):
        if(num%i ==0 or num % (i+2) ==0):
            return "no"
    return "yes" 
print(IsPrime(5))