#!/usr/bin/env python3
import sys

def perfectSquare(x):
    return (int(x**0.5))**2 == x
    
def factorizeFerma(n):
    x = int(n ** 0.5) + 1
    while not perfectSquare(x * x - n):
        print('Current x: ', x)
        x += 1
    y = int((x * x - n) ** 0.5)
    a = x - y
    b = x + y
    return a, b

n = int(sys.argv[1])
a, b = factorizeFerma(n)
print(a, b)
