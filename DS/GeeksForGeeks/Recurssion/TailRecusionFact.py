def NonTailfact(n):
    if n ==0 or n ==1:
        return 1
    return n*NonTailfact(n-1)

def TailFact(n,k):
    s:str ="abddf"
    result =""
    for c in s:
        if c not in result:
            result += c

    print(result)

print(NonTailfact(5))
print(TailFact(5,1))
