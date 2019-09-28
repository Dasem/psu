#!/usr/bin/env python3
# coding: utf8

import sys
import hashlib

m = hashlib.sha256()

original = sys.argv[1].encode('utf8')

print(original)

m.update(original)

or_hash = m.digest()[0]

def is_mm(cracked, orig):
    for i in orig:
        if i in cracked:
            return True
    return False

def badh(el):
    global m
    m.update(el)
    b = m.digest()
    return b[0]+b[1]*256

print('Lol kek', 'hash', or_hash)

count_try = 0

cr_str = b'FAIRED11!!'
or_str = original

list_or = []
list_cr = []

while True:
    list_or.append(badh(or_str))
    list_cr.append(badh(cr_str))
    count_try += 1
    if is_mm(list_cr, list_or):
        break
    or_str+=b' '
    cr_str+=b' '
    
print('Found!! count spaces: ', count_try)


