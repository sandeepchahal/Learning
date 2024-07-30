def subString(s:str,c, i):
    if i == len(s):
        print(c)
        return 
    subString(s, c+s[i], i+1)
    subString(s,c,i+1)

subString("abc", "",0)
