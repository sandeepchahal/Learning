def GetPrimeNumber(num):
    bool_array = [True for _ in range(num+1)]
    
    for i in range(2,num+1):
        if bool_array[i]:
            print(i)
            for j in range(i*i,num+1,i):
                bool_array[j]= False

GetPrimeNumber(100)
        


    