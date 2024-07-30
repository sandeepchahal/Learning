def getKthPermuataion(n,k):
    def getList(s:str, j:int):
        if j == len(s)-1:
            return s
        result =[]
        
        for i in range(j,len(s)):
            temp = s[i]
            s=s.replace(s[i],s[j],1)
            s=s.replace(s[j],temp,1)
            r = getList(s,j+1)
            result.append(r)
            temp = s[i]
            s=s.replace(s[i],s[j],1)
            s=s.replace(s[j],temp,1)
        return result
    print(getList("abc",0))
getKthPermuataion(3,2)