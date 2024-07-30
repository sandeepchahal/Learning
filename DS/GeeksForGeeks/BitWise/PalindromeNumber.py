def isPalindrome(number:int):
    rev:int =0
    temp = number
    while(temp!=0):
        lastDigit = temp%10
        rev = rev*10+lastDigit
        temp = int(temp/10)
    print(rev)
    return (number == rev)

result = isPalindrome(121)
print("Yes" if result else "No")
      