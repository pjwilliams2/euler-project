import numpy as np


for a in range(1, 400):
    for b in range(1, 600):
        c = np.sqrt(a**2 + b**2)
        if c == np.math.floor(c) and (a+b+c) == 1000:
            print('A: {0}, B: {1}, C: {2}'.format(a, b, c))
            print('A*B*C = {0}'.format(a*b*c))
            quit()



