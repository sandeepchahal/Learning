from typing import List


def merge(nums1: List[int], m: int, nums2: List[int], n: int) -> None:
        """
        Do not return anything, modify nums1 in-place instead.
        """
        j =0
        i =0
        length = m+n
        while i< length and j<n:
            if nums1[i]>nums2[j] and i<m:
                 nums1.insert(i,nums2[j])
                 nums1.pop(len(nums1)-1)
                 m+=1
                 j+=1
            elif nums1[i] ==0 and i >=m:
                nums1.insert(i,nums2[j])
                nums1.pop(len(nums1)-1)
                j+=1
            i+=1
            
        print(nums1)    
            
merge([1,2,3,0,0,0],3,
[4,5,6],
3)