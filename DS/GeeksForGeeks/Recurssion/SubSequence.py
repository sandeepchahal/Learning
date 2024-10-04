def subSequence(s:str,c, i):
    if i == len(s):
        print(c)
        return 
    subSequence(s, c+s[i], i+1)
    subSequence(s,c,i+1)

subSequence("abc", "",0)
