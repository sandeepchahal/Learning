def primes(s:str):
    if len(s)== 0:
        return ['']
    sub = primes(s[1:])
    primeList = []
    for i in range(len(s)):
        temp = s[:i+1]
        if IsPrime(int(temp)):
            primeList.append(temp)
    return sub+primeList
    

def IsPrime(num):
    if num <= 1:
        return False
    for i in range(2, int(num ** 0.5) + 1):
        if num % i == 0:
            return False
    return True

print(primes("1435"))



