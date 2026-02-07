0.8.3 (1/1/26)

- Vector2D now has an addition operator between two vectors, converting them into points and adding them using that operator, then returning the resulting vector. 

0.8.2 (12/19/25)

- Vector2D has some additional operators that I hope will be useful in making my game easier. 
    - *, /, and the unary - operator (to flip it immediately). 
- Point2D now has as vector-getter from one point to another. (this -> parameter)

0.8.1 (12/19/25)

- Circle2D now has methods to change one property and leave the rest.

0.8 (12/19/25)

- BREAKING CHANGE
    - Changed Rotation, Point, Vector, Line, and Equation to Value types (structs) because I was foolish and it's what I should have done in the first place. 
        - I do kinda dislike how the API turned out, but I kept all the methods I wanted to, so...
- Created Circle2D struct with minimal methods, I'll see how many more I need to add. And I just want to get it into production to start testing it, so I'm going to publish. 

0.7.5 (12/17/25)

Somehow I don't have a changelog for everything I've done up to this point. Well, I have a bunch of classes, and I'm just working through all the primitives I want to contain. Right now, I'm building it so I can make a simple Asteroids clone, so my work will focus on the primitives I need for that to work. 

Also, testing the workflow. 