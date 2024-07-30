def kthBitSetOrNot(n,k):
    leftBit = 1<<k-1
    if n & leftBit != 0:
        print("Yes")
    else:
        print("No")

kthBitSetOrNot(5,3)