#!/usr/bin/env python3
# coding: utf8

import sys

def step(osn,deg,mod):
    acc = 1
    summ=osn
    while deg != 0:
        if deg % 2 != 0:
            acc=(summ*acc)%mod
        summ=(summ*summ)%mod
        deg//=2
    return acc

e = 17
n = 2773
c = 2342

o = 7987897482374248445949574395849558935839**0
for k in range (1,100):    
    o*=e
    a = step(c, o, n)
    print('Temporary a: ', a)
    if a==c:
        m = step(c, e**(k-1), n)
        break
print('result: ',m)
