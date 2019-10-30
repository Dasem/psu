#!/usr/bin/env python3

def factor(a, b):
    mas = []
    while True:
        print(a, b)
        mas.append(a//b)
        if b == 1:
            return mas
        a = a % b
        a, b = b, a


factored = factor(101,103)

q0 = factored[0]

P0 = q0
P1 = q0*factored[1]+1

Q0 = 1
Q1 = factored[1]

def res_funQ(i):
    global Q0
    global Q1
    global factored
    if i == 1:
        return Q1
    if i == 0:
        return Q0
    return res_funQ(i-1)*factored[i] + res_funQ(i-2)


def res_funP(i):
    print("i: ", i)
    global P0
    global P1
    global factored
    if i == 1:
        return P1
    if i == 0:
        return P0
    return res_funP(i-1)*factored[i] + res_funP(i-2)


y = (-1)**len(factored) * res_funQ(len(factored)-2)
x = (-1)**(len(factored)-1) * res_funP(len(factored)-2)

print('result: ',x,y)
