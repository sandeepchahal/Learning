num = 5

def sumOfNNumbers(num):
    if(num ==0):
        return 0
    else:
        return num+sumOfNNumbers(num-1)
    
print(sumOfNNumbers(num))


