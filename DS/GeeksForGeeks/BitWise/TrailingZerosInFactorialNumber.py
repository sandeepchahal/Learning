

def CountZeros(factNumber):
    i=5
    count =0
    while factNumber//i>0:
        count = count+int(factNumber/i)
        i = i*5
    return count
print(CountZeros(3000))
