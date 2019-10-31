#!/usr/bin/env python3
from sys import *

modul = int(argv[1])

tochki = []

for x in range(modul):
    for y in range(modul):
        if (y**2) % modul == (x**3 + x + 3) % modul:
            tochki.append((x,y))

def rever(x):
    global modul
    for i in range(modul):
        if i * x % modul == 1:
            return i

#first, second = tochki (x, y)
def lam(first, second):
    res = 0
    global modul
    if first == second:
        div = 2*first[1] % modul
        if div == 0:
            return None
        res = (3 * first[0] ** 2 + 1) % modul * rever(div)
    else:
        div = (second[0] - first[0]) % modul
        if div == 0:
            return None
        res = (second[1] - first[1]) % modul * rever(div) 
    return res % modul

def summ(first, second):
    l = lam(first, second)
    if l == None:
        return None
    x_res = (l**2 - first[0] - second[0]) % modul
    y_res = (l * (first[0] - x_res) - first[1]) % modul
    
    res_tochka = (x_res, y_res)

    return res_tochka 

def poryadok(tochka):
    step = tochka
    i = 0
    while step != None:
        i+=1
        step = summ(step, tochka)
    return 1+i

for tochka in tochki:
    print(poryadok(tochka), tochka)

print("tochki: ", tochki)

print("len tochki: ", len(tochki))

# For manual calc
a = (1,28)
a2 = summ(a,a)
a4 = summ(a2,a2)
a8 = summ(a4,a4)
a12 = summ(a4,a8)
print(summ(a12,(7,36)))
