#!/usr/bin/env python3
# -*- coding: utf-8 -*-
import operator 
import math
import time
import pprint
import sys

debug = 'debug' in sys.argv
custom_filename = 'custom' in sys.argv
take_help = 'help' in sys.argv

if take_help:
    print('"debug" - print debug output;\n"custom" - enter custom filename which need to be coded;\n"help" - view this help')
    exit(0)

# Делит входной словарь на 2, с равными вероятностями
def splitter(orig_dict):
    sort_keys = sorted(orig_dict.items(),key = operator.itemgetter(1), reverse = True)

    if debug:
        print(sort_keys)

    first_half = dict()
    second_half = dict()
    
    summ = 0

    for dig in orig_dict.values():
        summ+=dig
    
    summ_prob = 0

    for char in sort_keys:
        if summ_prob < summ/2:
            summ_prob += char[1]
            first_half[char[0]]=char[1]
        else:
            second_half[char[0]]=char[1]

    return first_half, second_half

def to_stringbytes(s):
    for i in range(0, len(s), 8):
        ins = s[i:i + 8]
        while len(ins) != 8:
            ins+='0'
        yield ins

def bitstring_to_bytes(mas):
    byte_mas = bytes()
    for byte in mas:
        if debug:
            print('bitstring_to_bytes ' ,byte)
        byte_mas+=(int(byte,2).to_bytes(1,byteorder='big'))
    return byte_mas

def fano(minecraft):
    first_half, second_half = splitter(minecraft)
    global dict_code

    for char in first_half:
        dict_code[char]+='1'
    for char in second_half:
        dict_code[char]+='0'

    if len(first_half)>1:
        fano(first_half)
    if len(second_half)>1:
        fano(second_half)


f = open(input('Enter filename for coding: ') if custom_filename else 'fano_original', 'r')
dict_code=dict()

dicto = dict()

count = 0

text = f.read()
for char in text:
    count+=1
    if char not in dicto:
        dicto[char] = 1
    else:
        dicto[char] += 1

print('Char in text: ',count)
for char in dicto:
    dicto[char]=dicto[char]*1.0/count
#    print(dict[char])
    if debug:
        print(char, (dicto[char]))

enthropy = 0

for el in dicto:
#    print(math.log(dict[el],2))
    enthropy += - dicto[el]*math.log(dicto[el],2)
f.close()

dc_sort = sorted(dicto.items(),key = operator.itemgetter(1), reverse = True)

print(dc_sort)

for char in dicto:
    dict_code[char]=''

fano(dicto)

# print('Result for coded: ', dict_code)
for code in dict_code:
    if debug:
        print('Key: ' + code + ', code: ' + dict_code[code])

bitstring = ''

for char in text:
    bitstring+=dict_code[char]

if debug:
    print(bitstring)
mas_bytes = list(to_stringbytes(bitstring))
if debug:
    print(mas_bytes)
coded = bitstring_to_bytes(mas_bytes)
if debug:
    print(coded)

print ('Len of coded text: ', len(bitstring), 'original/coded = ', len(bitstring)/len(text))

w = open('coded', 'wb')
w.write(coded)
