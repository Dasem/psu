#!/usr/bin/env python3

e = 101
b = 35
modul = 1829
s = 42
a = b ** e % modul

mas = [(b,0)]

for i in range(s-1):
    mas.append((mas[-1][0]*a % modul, mas[-1][1] + 1))

big = []
for el in mas:
    t = el[1]+1
    big.append((a ** (t*s) % modul, t*s))

print ('little ', mas)
print('big ', big)

#for i in range(s):
#    print(mas[i][0], '\t', big[i][0])

founded = False

ans = 0

for el in mas:
    if founded:
        break
    for eb in big:
        if el[0] == eb[0]:
            founded = True
            ans = eb[1]-el[1]
            print('little: ', el, 'big: ', eb)
            break

print('answer: ', ans)
