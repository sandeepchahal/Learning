def setZeroes(matrix):
    columns = set()
    rows = set()
    for i in range(0,len(matrix)):
        for j in range(0,len(matrix[i])):
            if matrix[i][j] ==0:
                columns.add(j)
                rows.add(i)
        
    print(columns)
    print(rows)
    for row in rows:
        makeRowZeros(row,matrix)
    print(matrix)
    makeColZeros(columns,matrix)
    print(matrix)
    
   
    
def makeRowZeros(rowIndex,mat):
    for j in range(len(mat[rowIndex])):
        mat[rowIndex][j]=0
        
def makeColZeros(columns, mat):
    for i in range(len(mat)):
        for j in columns:
            mat[i][j]=0
            
        
    
                
# [[4,1,2,3],
#  [3,4,5,2],
#  [0,3,1,0]]       

matrix = [[2,0,2,4],[3,2,5,2],[0,3,1,0]]
setZeroes(matrix=matrix)