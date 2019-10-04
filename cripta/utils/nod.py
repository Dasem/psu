#!/usr/bin/env python3
import sys

a = int(sys.argv[1])
b = int(sys.argv[2])

while a != 0 and b != 0:
    if a > b:
        a %= b
    else:
        b %= a

gcd = a + b
print(gcd)
