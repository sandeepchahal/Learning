def sum(n):
    if n ==0 or n ==1:
        return n
    count =0
    count += n%10 + sum(n//10)
    if count>9:
        first = count%10
        count = first+count//10
    return count

print(sum(1233))