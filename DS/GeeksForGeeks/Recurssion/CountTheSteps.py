# If there are 2 steps: You can climb 1+1 or 2 steps. So 2 ways.
# If there are 3 steps: You can climb 1+1+1, 1+2, or 2+1 steps. So 3 ways.
# If there are 4 steps: you can climb 1+1+1+1, 1+1+2,1+2+1, 2+1+1, 2+2+1, 2+1+2, 
'''

'''

def countSteps(n):
    if n == 0:
        return 1
    if n<0:
        return 0
    
    return countSteps(n-1)+ countSteps(n-2)
    

print(countSteps(6))
    
    