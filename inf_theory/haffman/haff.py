#!/usr/bin/env python3
# coding: utf8

import sys
import operator

debug = 'debug' in sys.argv
custom_filename = 'custom' in sys.argv

def haffman(source_dict):
    global keys
    next_dict = source_dict
    srt = sorted(source_dict.items(), key=lambda kv: kv[1], reverse = False)
    
    next_dict[srt[0][0] + srt[1][0]]=srt[0][1]+srt[1][1]
    
    for key in srt[0][0]:
        keys[key]+='0'
    for key in srt[1][0]:
        keys[key]+='1'
    del next_dict[srt[0][0]]
    del next_dict[srt[1][0]]
     
    if debug:
        print('next_dict', next_dict)

    if len(next_dict) != 1:
        haffman(next_dict)

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


f = open(input('Enter filename for coding: ') if custom_filename else 'original', 'r')
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

f.close()

keys = dict()
for key in dicto: # preparing result map
    keys[key] = ''

haffman(dicto)

for key in keys:# reverse codes
    keys[key] = keys[key][::-1]

print(keys)
       
bitstring = ''

for char in text:
    bitstring+=keys[char]

if debug:
    print(bitstring)
mas_bytes = list(to_stringbytes(bitstring))
if debug:
    print(mas_bytes)
coded = bitstring_to_bytes(mas_bytes)
if debug:
    print(coded)

w = open('coded', 'wb')
w.write(coded)
