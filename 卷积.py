from sympy import *
t = symbols('t')
t0 = symbols('t0')

r1 = Rational(3)
a = Rational(-2)
r2 = Rational(-2)
b = Rational(-1)
xt = exp(-t0)
ht = (r1 * exp(a *(t-t0)) + r2 * exp(b*(t-t0)))

y = integrate(xt * ht,(t0,0,t))
y