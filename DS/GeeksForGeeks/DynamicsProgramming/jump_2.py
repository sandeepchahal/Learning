def jump(nums):
    minn = 1e9
    goal = len(nums) - 1  # 4
    i = 0
    counter = 0

    while i < goal:
        counter += 1
        if nums[i] >= goal - i:
            minn = min(minn, counter)
            counter = 0
            i += 1
        else:
            temp = nums[i]
            while nums[temp] == 0:
                temp -= 1
            i = temp

    return minn

print(jump([2,3,1,1,4]))