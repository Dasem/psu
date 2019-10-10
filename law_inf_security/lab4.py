#!/usr/bin/env python3

import math
from sys import argv

k = 0.6

fi = float(argv[1])
e0 = float(argv[2])
es = float(argv[3])

e0fix = 10**(0.05 * e0)
esfix = 10**(0.05 * es)

ecfix = (e0fix**2 - esfix**2)**0.5

# ecfix = 20 * math.log(ec, 10)

l = 150/(math.pi * fi)

l2 = 1800/fi

print ('l1 = ', l, 'l2 = ', l2)

e1 = ecfix*(1/l)**3

rresult = l/((k * esfix) / e1)**0.5

print('eci = ', ecfix, 'rresult = ', rresult)
