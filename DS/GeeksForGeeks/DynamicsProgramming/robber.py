def rob(nums):
    prev1, prev2 = nums[0],0
    for i in range(1,len(nums)):
        pick = nums[i]
        if i>1:
            pick += prev2
        not_pick = prev1
        prev2 = prev1
        prev1 = max(pick,not_pick)
    print(prev1)
        
            

a = [1,2,1]
print(rob(a))