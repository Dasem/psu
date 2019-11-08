#!/usr/bin/env python3
# coding: utf8

import random

message_length = 1000

ver_state_good = 0.9
ver_state_bad = 0.4

ver_error_in_good = 0.01

ver_error_in_bad = 0.8

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

print(result)
