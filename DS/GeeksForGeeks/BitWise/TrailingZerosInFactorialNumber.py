

def CountZeros(factNumber):
    i=5
    count =0
    for _ in range (factNumber):
        count = count+int(factNumber/i)
        i = i*5
    return count
print(CountZeros(100))
