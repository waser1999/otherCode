from sympy import *
t = symbols('t')
t0 = symbols('t0')

r1 = Rational(5,6)
r2 = Rational(1,6)
a = Rational(-5)
xt = exp(-t0)
ht = r1 * exp(a *(t-t0)) + r2 * exp(t-t0)

y = simplify(integrate(ht * xt,(t0,0,t)))
pprint(y, use_unicode=False)