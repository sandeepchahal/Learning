def OddOccurrenceNumber(arr):
    result =arr[0]

    for i in range(1,len(arr)):
        result = result ^ arr[i]
    return result

def TwoOddOccurrenceNumber(arr):
    even =0
    odd =0

    for i in range(0,len(arr)):
        if arr[i]&1 != 0: #odd
            odd = odd^arr[i] if odd==0 else arr[i]
        else:
            even = even^arr[i] if even == 0 else arr[i]

    return {even,odd}


def NetSolution(arr):
    x = arr[0]
    for i in range(1,len(arr)):
        x = x^arr[i]
    k = x&(~(x-1))
    res1=0
    res2=0
    for i in range(0,len(arr)):
        if(arr[i]&k !=0):
            res1= res1^arr[i]
        else:
            res2 = res2^arr[i]
    print(res1,res2)

print(NetSolution([1,2,3,3,2,1,33,3]))