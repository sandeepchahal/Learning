def IsPrime(num):
    if(num==1):
        return "no"
    for i in range(2,int(num**0.5)+1):
        if(num%i ==0 or num % (i+2) ==0):
            return "no"
    return "yes" 
print(IsPrime(25))