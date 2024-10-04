def gcdOfStrings(str1: str, str2: str) -> str:
    
    minn = min(str1,str2)
    maxx = str1 if minn != str1 else str2
    def solve(a,b):
        if len(a)==0:
            return ""
        k = len(b)//len(a)
        if a*k ==b and (len(minn)//len(a))*a== minn:
            return a
        return solve(a[:len(a)-1], b)
    return solve(minn, maxx)
                
print(gcdOfStrings("ababab","abab"))