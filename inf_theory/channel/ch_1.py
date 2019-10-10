#!/usr/bin/env python3
# coding: utf8

import random
import math

def compliance(n, k):
    return math.factorial(n)/(math.factorial(n-k)*math.factorial(k))

message_length = 10000

ver_error = 0.017

result = ''

for i in range(message_length):
    randomed = random.random()
    result += '0' if randomed > ver_error else '1'

modeled = result # Model

n = 10


expect_dict = dict()
model_dict = dict()

for i in range(n):
    model_dict[i]=0

for i in range(n):
    expect_dict[i] = compliance(n, i) * ver_error**i *(1-ver_error)**(n-i)

print('Expected probability: ', expect_dict)

for i in range(0, len(modeled), n):
    count_err = 0
    for j in range(i,i+n):
        if modeled[j] == '1':
            count_err += 1
    model_dict[count_err] += 1

for i in range(n):
    model_dict[i]/=message_length/n

print('Modelled probability: ', model_dict)

# print(result)
