def frog(arr,k):
    n = len(arr)
    dp = (n+1)*[0]
    #print(jumpRecurrsion(arr,n-1,k,dp))
    return jumpDP(arr,n,k)
        
def jumpRecurrsion(arr,index,k,dp):
    if index ==0:
        return 0
   
    enregy = float('inf')
    for j in range(1,k+1):
        if index-j>=0:
            temp = jumpRecurrsion(arr,index-j,k,dp)+abs(arr[index]-arr[index-j])
            enregy = min(temp,enregy)
            dp[index]= enregy
            print(dp)
    return enregy

def jumpDP(arr,n,k):
    dp = (n+1)*[0]
    for i in range(1,n):
        answer = float('inf')
        for j in range(1,k+1,1):
            if i-j>=0:
                jum = dp[i-j]+abs(arr[i]-arr[i-j])
                answer = min(answer,jum)
        dp[i]= answer
    return answer
    
 # [0,20,10,0,0]      
a = [10,20,15,40]
print(frog(a,4))

