# Changelog

0.9.0 (3/5/2026)

The date was absolutely not intentional. That's pretty funny. 

I've updated 4 of the base types (Rotation, Point, Vector, and Equation) by simplifying their interfaces and updating their tests (they now mostly have 100% coverage). I added a few things that I hope will be useful such as the Equation now being able to get the value it calculates at a given X value (which was overdue), Point can now be converted to a vector or a Vector2, which is used in some drawing systems (and just generally useful), and most of them got updated math operators and implicit/explicit operators.

I also looked around and saw a few methods that I think were pointless and/or were not clear with what they did, so I tried to remove as many of those as I could. I will continue to refine the API to be more useful with in-line methods (simplifying the code you need to write) and be extremely clear. Some methods you used may have been removed; that's why I bumped the minor version!

0.8.~ (??)

This updated everything into Structs, which I think will drastically improve usability and performance. 

I think through this point I also made the Circle class, which helps a little bit, but it needs a lot more work. It's also not very tested well. 

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