# 2D Physics Library

This is a junior-in-school's first public library. As of now, it contains a variety of base objects for base 2D physics; 
there's a Rotation to handle orientation, a Point to handle positions, Lines that are pairs of points, and Vectors that 
hold a rotation and magnitude. None of these classes are done, but all of the base classes are in place. In the works 
are a few objects that are collections of points or vectors, and they will handle collisions and such. I just haven't 
figured out how to best make it yet. 

I add XML Comments (the ones VS Code and Visual Studio usually read for their Intellisense) to every property, method, and class, 
so I hope those are of a bit more use to you than a generalized readme. There is an external documentation page that you can 
[look at right now](https://wiki.codyhowell.dev/2dphysicslibrary) to read through all of the classes that are ready to be used. 
They should be pretty stable, just making more methods and tests for them.  

## Changelog

0.7.4 (3/5/2025)
	
- SOME BREAKING CHANGES: Adjusted class names to more closely align with the library name, and the Equation name conflict. 
	- There's a default class with the name Equation, so I had to rename everything.. welp!
	- Rotation2D no longer supports Radians. A second RotationRadian2D class will be added to work with radians instead, if necessary. I also added 2 decimals of precision to my angles.  
- Added more operators to Rotation class
- Added all overrides to Rotation class (GetHashCode, Equals, and ToString)
- Created Point class
	- Has methods for Midpoint, Scaling, Angles from each other, Distance, and a handful of helpful operators
- Created Equation(2D) class
	- Defines a line
	- Can determine points along that line
	- Can determine an intersection point
- Created VectorObject
- Created LineObject
- Created Vector
- Standardized a lot of my methodology for randomized testing and ensuring I hit every method at least once. 
- Testing is not complete; library has about ~~10~~ ...40 hours more of finishing touches. 
- Many things planned in the future, but I want to get an update out. This adds a *lot* of nice touches and new classes, at least enough to be useful.

0.2.5 (12/7/24) 

- Completed Rotation class
- DistanceTo, AverageTo, Cloning Constructor, IEquatable, and IComparable methods added
- Overrode operators + (addition), - (subtraction), % (modulo), and ^ (average of two angles)
- Ensured XML comments were included in the package
- Test cases up to 148

0.2.0 (12/6/24) 

- Added Rotation class