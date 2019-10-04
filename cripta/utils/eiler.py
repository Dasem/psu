#!/usr/bin/env python3
# coding: utf8

import sys

n = int(sys.argv[1])

def step(deg, osn, mod):
    acc = 1
    summ=osn
    while deg != 0:
        if deg % 2 != 0:
            acc=(summ*acc)%mod
        summ=(summ*summ)%mod
        deg//=2
    return acc

def ferma_test(a, p):
    return step(p-1, a, p) == 1

def eiler(n, tested):
    simple = []
    current_digit = 1
    while len(simple) != n:
        if ferma_test(tested[0], current_digit):
            simple.append(current_digit)
        current_digit+=1

    new_simple = []
    for test in tested:
        new_simple = []
        for digit in simple:
            if ferma_test(test, digit):
                new_simple.append(digit)
    
    return new_simple

def Isprime(n):
    i = 2
    j = 0
    while(True):
        if(i*i <= n and j != 1):
            if(n % i == 0):
                j=j+1
            i=i+1
        elif(j==1):
            return False
        else:
            return True

ans = eiler(n,[2,3,5])

for i in ans:
    if not Isprime(i):
        print('ALARM!!!!', i)

print(ans, len(ans))
