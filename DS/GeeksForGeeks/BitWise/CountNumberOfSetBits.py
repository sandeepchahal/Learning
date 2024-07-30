def GetCountOfOneBits(n):
    count =0
    for i in range(0, int(n**0.5)+1,1):
        if(n&1):
            count +=1
        n = n>>1
    return count


def GetCountOfOneBits2(n):
    tbl=[0] * 256
    tbl[0]= 0
    for i in range(1,256,1):
        tbl[i]= tbl[i&(i-1)]+1
    return tbl[n]


print(GetCountOfOneBits2(13))


