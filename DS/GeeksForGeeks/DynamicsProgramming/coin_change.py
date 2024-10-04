def coinChange(coins, amount):
    dp = [0]*(amount+1)
    prev = dp.copy()
    for i in range(0,amount+1):
        if i%coins[0]==0:
            prev[i]= i//coins[0]
        else:
            prev[i] = float('inf')
                
    for index in range(1, len(coins)):
        for j in range(0,amount+1):
            not_take = prev[j]
            take = float('inf')
            if coins[index]<=j:
                take = 1+dp[j-coins[index]]
            dp[j] = min(take,not_take)
        prev = dp.copy()    
    return prev[amount] if prev[amount] != float('inf') else -1

coinChange([1,2,5],11)