from typing import List


def removeElement(nums: List[int], val: int) -> int:
    length = len(nums)
    for i in range(0, len(nums)):
        if nums[i] == val:
            j=i
            for index in range(i+1, len(nums)):
                if(nums[index] != val):
                    nums[j] = nums[index]
                    j+=1
                    
    print(length)
    print(nums)                
removeElement([1,1,1,1],1)