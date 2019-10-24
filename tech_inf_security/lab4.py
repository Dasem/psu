#!/usr/bin/env python3

import math
from sys import argv

k = 0.6

fi = float(argv[1])
e0 = float(argv[2])
es = float(argv[3])

lam = 300/fi
L0 = 1
L1 = 150/(math.pi*fi)
L2 = 1800/fi

print('L1: ', L1, 'L2: ', L2)

print('lambda:\t\t', lam)
print('lambda * 2/p:\t', lam/(2*math.pi))
print('lambda * 6:\t', lam * 6)

e0fix = 10**(0.05 * e0)
esfix = 10**(0.05 * es)

ecfix = (e0fix**2 - esfix**2)**0.5

#l = 150/(math.pi * fi)

#l2 = 1800/fi

#print ('l1 = ', l, 'l2 = ', l2)

close = L0/((k * esfix) / ecfix)**(1/3)

emiddle = ecfix if L1 <= L0 else ecfix*(L0/L1)**3

middle =  L1/((k * esfix) / emiddle)**(1/2)

efar = ecfix if L2<=L0 else (emiddle*L1/L2 if L0<L1 else ecfix*L0/L2)

far = L2/((k * esfix) / efar)



print('eci = ', ecfix, 'close: ', close, 'middle: ', middle, 'far: ', far)
