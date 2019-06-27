import Rhino as rc

#find a point between A and B
D = A + t * (B - A)

#find mid point between A and B
C1 = A + 0.5 * (D - A)

#find mid point between D and B
C2 = D + 0.5 * (B - D)

#find spheres radius
r1 = A.DistanceTo(C1) #C1 - A
r2 = B.DistanceTo(C2)

#create spheres and assing to output
s1 = rc.Geometry.Sphere(C1, r1)
s2 = rc.Geometry.Sphere(C2, r2)
