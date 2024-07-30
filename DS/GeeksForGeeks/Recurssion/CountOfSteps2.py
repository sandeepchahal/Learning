'''
n =3
list = [1,2]

number of ways = [1,1,1], [1,2], [2,1]

'''


def countOfSteps(n,list):
    if n ==0:
        return 1
    if n<0:
        return 0
    total=0
    for i in list:
        total += countOfSteps(n-i, list)
    return total    
        

    
print(countOfSteps(5, [1,2]))
        