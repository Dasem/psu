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
        if ferma_test(tested, current_digit):
            simple.append(current_digit)
        current_digit+=1

    new_simple = []
    for test in tested:
        new_simple = []
        for digit in simple:
            if ferma_test(test, digit):
                new_simple.append(digit)
    
    return new_simple

print(eiler(100, [2,3,4,5])
