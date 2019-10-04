#!/usr/bin/env python3
# coding: utf8

import random

message_length = 1000

ver_error = 0.017

result = ''

for i in range(message_length):
    randomed = random.random()
    result += '0' if randomed > ver_error else '1'

print(result)
