def PrimeFactors(num):
    output =[]

    while(num %2 ==0):
        output.append(2)
        num //=2

    while(num %3 ==0):
        output.append(3)
        num //=3

    for i in range (5, int(num ** 0.5)+1, 6):
        while(num%i ==0):
             output.append(i)
             num //=i

        while(num % (i+2)==0):
            output.append(i+2)
            num //=(i+2)
        
    if(num>3):
        output.append(num)

    for item in output:
        print(item)

PrimeFactors(14)
    
            