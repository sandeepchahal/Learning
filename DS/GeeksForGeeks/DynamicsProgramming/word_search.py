def exist(board, word):
    visited = set()
    n = len(board)
    print(n)
    m = len(board[0])
    
    def f(n1, m1, result, counter):
        print(n1,m1,result,counter)
        if result == word:
            return True
        if n1 > n - 1 and m1 > m - 1:
            return False
    
        if board[n1][m1] == word[counter]:
            result += word[counter]
            counter += 1
    
        temp = str(n1) + str(m1)
        if temp in visited:
            return False
        else:
            visited.add(temp)
        
        right = False
        if n1 + 1 <= n - 1:
            right = f(n1 + 1, m1, result, counter)
            
        both = False
        if m1 + 1 <= m - 1 and n1+1<=n-1:
            bpth = f(n1 + 1, m1 + 1, result, counter)
        
        down = False
        if m1 + 1 <= m - 1:
            down = f(n1, m1 + 1, result, counter)
            
        return right or both or down
        
    return f(0,0,"",0)


board = [["A","B","C","E"],
         ["S","F","C","S"],
         ["A","D","E","E"]]
word = "SEE"
print(exist(board, word))
