#!/usr/bin/env python3
# coding: utf8

import sys

if ('help' in sys.argv):
    print('Using: ./step.py [osn] [deg] [mod]')
    exit(0)

if (len(sys.argv) == 4):
    osn = int(sys.argv[1])
    deg = int(sys.argv[2])
    mod = int(sys.argv[3])
else:
    deg=int(input('Input deg: '))
    osn=int(input('Input osnovanie: '))
    mod=int(input('Input mod: '))

acc = 1
summ=osn
while deg != 0:
    if deg % 2 != 0:
        acc=(summ*acc)%mod
    summ=(summ*summ)%mod
    deg//=2

print(acc)

