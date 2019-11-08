#!/usr/bin/env python3
# coding: utf8

import random
import math
from sys import argv


def compliance(n, k):
    return math.factorial(n)/(math.factorial(n-k)*math.factorial(k))

koef = 3.84 # for probability 0.9
message_length = 10000
eps = 0.01 # spread
ver_error = 0.1 #float(argv[2])

#if len(argv) > 1:
#    message_length = int(argv[1])

print('koef: ', koef)
print('message_length: ', message_length)
print('eps: ', eps)
print('ver_error', ver_error)

result = ''

for i in range(message_length):
    randomed = random.random()
    result += '0' if randomed > ver_error else '1'

modeled = result # Model

n = 10

if (message_length%n !=0):
    print('Message length should be mod of ', n, '!')
    exit(0)

expect_dict = dict()
model_dict = dict()

for i in range(n+1):
    model_dict[i]=0

for i in range(n+1):
    expect_dict[i] = compliance(n, i) * ver_error**i *(1-ver_error)**(n-i)

print('Expected probability: ', expect_dict)

for i in range(0, len(modeled), n):
    count_err = 0
    for j in range(i,i+n):
        if modeled[j] == '1':
            count_err += 1
    model_dict[count_err] += 1

for i in range(n+1):
    model_dict[i]/=message_length/n

enough = koef*ver_error**(1-ver_error)/eps**2

print('Modelled probability: ', model_dict)
print('Enough length: ', enough)
