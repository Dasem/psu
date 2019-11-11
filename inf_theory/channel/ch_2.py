#!/usr/bin/env python3
# coding: utf8

import random

message_length = 1000
e0 = 0.01 # prob error in good state
e1 = 0.8 # prob error in bad state
psr = 0.1

p1 = (e0 - psr)/(e0 - e1)
p0 = 1-p1


p11 = random.random()
p10 = 1-p11
p01 = (p1/p0)*p10
p00 = 1-p01

print('p11: ', p11, 'p10: ', p10, 'p01: ', p01, 'p00: ', p00)

message_length = 100000

ver_state_good = p00
ver_state_bad = p11

ver_error_in_good = e0

ver_error_in_bad = e1

n = 10

good_state = True

result = ''

for i in range(message_length):
    randomed = random.random()
    if good_state:
        if randomed > ver_state_good:
            good_state = False
    else:
        if randomed > ver_state_bad:
            good_state = True

    if good_state:
        result+= '0' if random.random() > ver_error_in_good else '1'
    else:
        result+= '0' if random.random() > ver_error_in_bad else '1'

count1 = 0

for i in result:
    if i == '1':
        count1+=1

model_dict = dict()

for i in range(n+1):
    model_dict[i]=0

for i in range(0, len(result), n):
    count_err = 0
    for j in range(i,i+n):
        if result[j] == '1':
            count_err += 1
    model_dict[count_err] += 1

for i in range(n+1):
    model_dict[i]/=message_length/n

#for i in range(11):
#    print(i,': ', p1**i * p0**(10-i))

print((p0*(1-e0))**10)

print('Modelled probability: ', model_dict)

print('probability in general (modelled): ', count1/message_length)

# print(result)
