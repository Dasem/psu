#!/usr/bin/env python3
# coding: utf8

from sys import argv

def nod(a, b):
    while a != 0 and b != 0:
    if a > b:
        a %= b
    else:
        b %= a
    return a + b

y = 0

n = int(argv[1])**0.5

solved = false

while not solved:
    
