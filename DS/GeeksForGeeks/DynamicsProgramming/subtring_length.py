def lengthOfLongestSubstring(s: str) -> int:
    if len(s) ==0:
        return 0
    length = 0
    i =0
    j =0
    counter = {}
    while j<len(s):
        if s[j] not in counter:
            counter[s[j]] = j
        elif counter[s[j]]>=i:
            i = counter[s[j]]+1
            print(s[j],i)
            counter[s[j]]= j
        else:
            counter[s[j]]=j
        length = max(length,j-i+1)
        j+=1
    return length
    
    
    
    
s ="aabaab!bb"
lengthOfLongestSubstring(s)
    


