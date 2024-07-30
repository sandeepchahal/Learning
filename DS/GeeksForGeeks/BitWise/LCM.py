from GreatestCommonDivisor import GCD
def LCM(a,b):
    gcd= GCD(a,b)
    return int((a*b)/gcd)
print(LCM(3,7))