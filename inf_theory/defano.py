#!/usr/bin/env python3
import operator 
import math
import time
import sys

custom_filename = 'custom' in sys.argv

def splitter(orig_dict):
    sort_keys = sorted(orig_dict.items(),key = operator.itemgetter(1), reverse = True)

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

def bitstring_to_bytes(s):
    v = int(s, 2)
    b = bytearray()
    while v:
        b.append(v & 0xff)
        v >>= 8
    return bytes(b[::-1])

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


f = open(input('Enter original filename: ') if custom_filename else 'fano_original', 'r')
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
    print('Key: ' + code + ', code: ' + dict_code[code])

coded = open('coded', 'rb')
coded_text = coded.read()
bytearr = ''
for byte in coded_text:
    bytearr += '{0:08b}'.format(byte)

print(bytearr)

byte_list = list(bytearr)

temp=''
decoded = ''
while len(byte_list) != 0:
    temp+=byte_list.pop(0)
    for code in dict_code:
        if dict_code[code] == temp:
            decoded+=code
            temp = ''

open('decoded', 'w').write(decoded)
print(decoded)

