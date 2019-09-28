import random

f = open('input_second', 'r')

count = int(input('Text length: '))

dict = dict()

for line in f:
    splitted = line.split(' ')
    dict[splitted[0]] = float(splitted[1])

out = open('second_out', 'w')

for i in range(count):
    summ = 0
    randomed = random.random()
    for el in dict:
        summ+=dict[el]
        if randomed < summ:
            out.write(el)
            break

f.close()
out.close()
