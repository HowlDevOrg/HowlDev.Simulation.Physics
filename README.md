# HowlDev.Simulation.Physics

## HowlDev.Simulation.Physics.Primitive2D

Contains a few 2D-plane generics for simple calculations (it uses doubles, sometimes rounded). 

### Rotation2D

Holds an angle between 0 and 360 (with a precision of 2 digits). Has an extreme number of helper methods to point at given points, get distances between other angles, and to flip the angle. 

### Point2D

Holds two full double-precision points for X and Y. Has a number of helper methods to get distances in 2D space and midpoints, as well as some fancy operator functions. 

### Vector2D

Holds a Rotation2D object and a double for the velocity. Allows you to calculate points and angles off of others using operator functions. 

### Equation2D

Holds two coefficient values (for the slope and the y-intercept). Does most of the calculating for the Line2D. Not really intended to be used directly. 

### Line2D

Holds two Point2D objects in an array. Has logic for intersecting lines and retrieving intersecting points. 

Read the wiki (which has links to the package) and get a bunch of class definitions! [Those exist here](https://wiki.codyhowell.dev/2dphysicslibrary).
