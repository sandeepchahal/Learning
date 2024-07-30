def perm(s:str):
    if len(s)<=1:
        return [s]
    result =[]
    for i in range(len(s)): # abc then first =a and rest = bc
        first = s[i]
        rest = s[:i]+s[i+1:]
        for p in perm(rest):
            result += [first+p]
    return result

print(perm("abc"))
    
