from IsPalindromString import isPalindrom

'''
s = aba
check for a , ab, ba,b,a, aba
'''
def partition(s:str):
    if not s:
        return [[]]
    
    result = []
    for i in range(1, len(s) + 1):
        if isPalindrom(s[:i]):
            for rest in partition(s[i:]):
                result.append([s[:i]] + rest)
    
    return result

        
print(partition("aba"))
    
    
    
