def canCompleteCircuit(gas, cost):
    n = len(gas)
    
    for i in range(n):
        current_gas = 0
        station = i
        print(f"---------------Starting from {i} index---------------------")
        while True:
            current_gas += gas[(station)%n]
            if current_gas<cost[station%n]:
                break
            current_gas -= cost[station % n]
            print(f"Travel from {station%n} To {(station+1)%n}  current gas: {current_gas}")
            station +=1
            if station%n == i:
                return i
        print(f"---------------End--------------------")
    return -1
    

gas = [1,2,3,4,5]
cost = [3,4,5,1,2]
print(f"final Answer - {canCompleteCircuit(gas,cost)}")
