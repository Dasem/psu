#!/usr/bin/env python3
#coding: utf8

modul = 256
pas = 'qwe'


def hs(pas):
    sum = 0
    for char in pas:
        sum+= ord(char)
    return sum % modul

def redux(hs, order):
    char_d = hs%28
    return chr(ord('a')+(char_d+order)%28) + chr(ord('a')+(char_d+order)%28) + chr(ord('a')+(char_d+order)%28)

def hack(hs, order, )

m = 5
k = 5

table = dict()

for i in range(m):
    index = chr(ord('a')+i)
    temp_round = hs(index)
    for j in range(k):
        reducted = redux(temp_round, j)
        temp_round = hs(reducted)
    table[index] = temp_round

print(table)


for char in table:
    temp_round = hs(pas)
    for j in reversed(range(k)):
        reducted = redux(temp_round, j)
        temp_round = hs(reducted)
        if temp_round == table[char]:
            print('Founded!!!!')
            exit(0)


