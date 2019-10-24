#!/usr/bin/env python3

import math

f = open(input('Input file name :'), 'r')

dict = dict()

count = 0

for line in f:
    for char in line:
        count+=1
        if char not in dict:
            dict[char] = 1
        else:
            dict[char] += 1

print('Char in text: ',count)
for char in dict:
    dict[char]=dict[char]*1.0/count
#    print(dict[char])
#    print(char, (dict[char]))

enthropy = 0

for el in dict:
#    print(math.log(dict[el],2))
    enthropy += - dict[el]*math.log(dict[el],2)
f.close()
print(enthropy)
