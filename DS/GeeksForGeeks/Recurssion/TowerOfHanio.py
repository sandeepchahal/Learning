def hanoi(n, f, to, via):
    if n == 1:
        print("Move disk 1 from",f,"to",to);
    else:
        hanoi(n-1, f, via, to)
        print("Move disk",n,"from",f,"to",to);
        hanoi(n-1, via, to, f)
hanoi(4,'a','c','b')