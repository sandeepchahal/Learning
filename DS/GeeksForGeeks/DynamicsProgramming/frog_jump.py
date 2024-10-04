def frog(arr,k):
    dp = (len(arr)+1)*[0]
    
    for i in range(1,len(dp)-1):
        fs = dp[i-1]+abs(arr[i]-arr[i-1])
        ss = float('inf')
        if i>1:
            ss = dp[i-2]+abs(arr[i]-arr[i-2])
        dp[i]= min(fs,ss)
    return dp[len(arr)-1]

    # we can use the below recurssion as well by calling
    # set the 
    #jump(arr,len(arr)-1,dp)

def jump(arr,i,pre):
    if i ==0:
        return 0
    if pre[i]!= 0:
        return pre[i]
        
    en1= jump(arr, i-1,pre) + abs(arr[i]-arr[i-1])
    if i >1:
        en2 = jump(arr,i-2,pre)+abs(arr[i]-arr[i-2])
        pre[i] = min(en1,en2)
        return pre[i]
    pre[i]= en1
    return en1
a = [10,20,15,40]
print(frog(a,2))