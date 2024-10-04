def ninja(points,n):
    dp =  [[-1] * 4 for _ in range(n)]
    dp[0][0]= max(points[0][1], points[0][2])
    dp[0][1]= max(points[0][0], points[0][2])
    dp[0][2]= max(points[0][0], points[0][1])
    dp[0][3]= max(points[0][0], points[0][1],points[0][2])
    
    for day in range(1,n):
        for last in range(4):
            dp[day][last]=0
            for task in range(3):
                if task != last:
                    temp = points[day][task]+dp[day-1][task]
                    dp[day][last] = max(temp,dp[day][last])
    return dp[day][task]
    
    
    #print(f(n-1,3,points,dp))



def f(day,last, points,dp):
    if day ==0:
        maxx = 0
        for i in range(3):
            if i != last:
                total = points[0][i]
                maxx= max(total,maxx)
        return maxx
    if dp[day][last] !=-1:
        return dp[day][last]
    maxx = 0
    for j in range(0,3):
        if j!= last:
            total = points[day][j]+f(day-1,j,points,dp)
            maxx = max(maxx,total)
    dp[day][last]= maxx
    return dp[day][last]

a = [[10,50,1],[5,100,11],[20,4,10]]
print(ninja(a,len(a)))