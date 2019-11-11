#!/usr/bin/env python3

h = 1 # open('hash', 'r').read()

q = 6703903964971298549787012499102923063739682910296196688861780721860882015036922585419853748190383615062910947743405567510148398820717100282856877776119229

e = h%q

a = 2**511 + 108

b = 5472517130514047254760433071281657274171034389553769779747941603125796549693907036696237273952702637857580071293254240945079496484373854264998452887027990

k = 10 # eandom 4islo

modul = q 


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

c = (a,b)
for i in range(k-1):
    temp = sum((a,b))

print(c)
