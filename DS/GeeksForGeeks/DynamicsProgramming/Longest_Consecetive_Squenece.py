def lcs(nums):
    sub_sets = set(nums)
    longest = 0
    for item in sub_sets:
        if item-1 not in sub_sets:
            current =1
            while item+1 in sub_sets:
                current +=1
                item +=1
            longest = max(longest,current)
            
    print(longest)   
            
    
    
    
    
print(lcs([0,3,7,2,5,8,4,6,0,1]))
    
    
    