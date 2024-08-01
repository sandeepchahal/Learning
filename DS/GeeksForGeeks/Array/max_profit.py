def max_profit(arr):
    profit =0
    for i in range(1, len(arr)):
        if arr[i]> arr[i-1]:
            profit += arr[i]- arr[i-1]
    return profit
a = [2,5,1,6]
print(max_profit(a))