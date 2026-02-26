namespace HowlDev.Simulation.Physics.Primitive2D;

/// <summary>
/// Readonly struct Equation holds at most 2 points, returning the slope and Y-intercept of a pair of 
/// points (or the X-intercept if the points are vertical). It handles some logic, 
/// such as intersection points and defining if a point is on a line. 
/// </summary>
public readonly struct Equation2D : IEquatable<Equation2D>, IComparable<Equation2D> {
    private readonly double coefficient0;
    private readonly double coefficient1;
    private bool x_Intercept => coefficient1 == double.PositiveInfinity;

    /// <summary>
    /// Gets the current Slope.
    /// </summary>
    public double Slope => coefficient1;

    /// <summary>
    /// Gets the current Intercept.
    /// </summary>
    public double Intercept => coefficient0;

    /// <summary>
    /// Default constructor, leaves coefficients as <c>0.0</c>.
    /// </summary>
    public Equation2D() {
        coefficient0 = 0.0;
        coefficient1 = 0.0;
    }

    /// <summary>
    /// Assign slope and intercept directly.
    /// </summary>
    public Equation2D(double slope, double intercept) {
        coefficient1 = slope;
        coefficient0 = intercept;
    }

    /// <summary>
    /// Takes in two coordinate pairs and derives their equation.
    /// </summary>
    public Equation2D(double x1, double y1, double x2, double y2) {
        (coefficient0, coefficient1) = CalculateFromCoordinates(x1, y1, x2, y2);
    }

    /// <summary>
    /// Takes in a pair of <c>Point</c>s and derives their equation.
    /// </summary>
    public Equation2D(Point2D p1, Point2D p2) {
        (coefficient0, coefficient1) = CalculateFromCoordinates(p1.X, p1.Y, p2.X, p2.Y);
    }

    /// <summary>
    /// Takes in a <c>Line</c> and derives its equation.
    /// </summary>
    public Equation2D(Line2D l1) {
        (coefficient0, coefficient1) = CalculateFromCoordinates(l1[0].X, l1[0].Y, l1[1].X, l1[1].Y);
    }

    /// <summary>
    /// Duplication constructor. 
    /// </summary>
    public Equation2D(Equation2D equation) {
        coefficient0 = equation.coefficient0;
        coefficient1 = equation.coefficient1;
    }

    /// <summary>
    /// Returns a new Equation2D with an updated coefficient value.
    /// </summary>
    /// <param name="index">0 for the intercept, 1 for the slope.</param>
    /// <param name="value">Value to be input.</param>
    /// <returns>A new Equation2D with the updated coefficient</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public Equation2D WithCoefficient(int index, double value) {
        if (index > 1 || index < 0) throw new ArgumentOutOfRangeException("Index must be either 0 or 1.");
        if (index == 0) {
            return new Equation2D(coefficient1, value);
        } else {
            return new Equation2D(value, coefficient0);
        }
    }

    /// <summary>
    /// Creates an Equation2D from two coordinate pairs.
    /// </summary>
    /// <returns>A new Equation2D derived from the coordinates</returns>
    public static Equation2D FromCoordinates(double x1, double y1, double x2, double y2) {
        return new Equation2D(x1, y1, x2, y2);
    }

    /// <summary>
    /// Creates an Equation2D from two points.
    /// </summary>
    /// <returns>A new Equation2D derived from the points</returns>
    public static Equation2D FromCoordinates(Point2D p1, Point2D p2) {
        return new Equation2D(p1, p2);
    }

    /// <summary>
    /// Creates an Equation2D from a line.
    /// </summary>
    /// <returns>A new Equation2D derived from the line</returns>
    public static Equation2D FromCoordinates(Line2D l1) {
        return new Equation2D(l1);
    }

    /// <summary>
    /// Returns a new Equation2D with the same slope but adjusted intercept to pass through the given point.
    /// </summary>
    /// <returns>A new Equation2D with adjusted intercept</returns>
    public Equation2D WithPoint(double x, double y) {
        double newIntercept = y - coefficient1 * x;
        return new Equation2D(coefficient1, newIntercept);
    }

    /// <summary>
    /// Returns a new Equation2D maintaining the current slope but adjusted to pass through this point.
    /// </summary>
    /// <returns>A new Equation2D with adjusted intercept</returns>
    public Equation2D WithPoint(Point2D point) {
        return WithPoint(point.X, point.Y);
    }

    /// <summary>
    /// Returns <c>True</c> if the given point is within the Precision value of the line. 
    /// </summary>
    /// <param name="x">X-coordinate of the incoming point</param>
    /// <param name="y">Y-coordinate of the incoming point</param>
    /// <param name="precision">Value the difference must be below to return <c>True</c></param>
    public bool PointIsOnLine(double x, double y, double precision = 0.1) {
        if (x_Intercept) {
            double difference = Math.Abs(x - Intercept);
            return difference <= precision;
        } else {
            double right = x * Slope + Intercept;
            double difference = Math.Abs(y - right);
            return difference <= precision;
        }
    }

    /// <summary>
    /// Returns <c>True</c> if the given point is within the Precision value of the line. 
    /// </summary>
    /// <param name="p"><c>Point</c> to check if on the line</param>
    /// <param name="precision">Value the difference must be below to return <c>True</c></param>
    public bool PointIsOnLine(Point2D p, double precision = 0.1) {
        return PointIsOnLine(p.X, p.Y, precision);
    }

    /// <summary>
    /// Returns the intersection point of the two equations. If the lines are exactly equal, returns null. 
    /// If there's no intersection point, returns null. 
    /// </summary>
    public Point2D? IntersectionPoint(Equation2D e1) {
        if (coefficient1 == e1.coefficient1) return null; // Covers both cases.
        if (x_Intercept && e1.x_Intercept) return null; // Covers vertical line cases

        if (x_Intercept) {
            return new Point2D(Intercept, Intercept * e1.coefficient1 + e1.coefficient0);
        } else if (e1.x_Intercept) {
            return new Point2D(e1.Intercept, e1.Intercept * coefficient1 + coefficient0);
        }

        double x = (coefficient0 - e1.coefficient0) / (e1.coefficient1 - coefficient1);
        double y = e1.coefficient1 * x + e1.coefficient0;

        return new Point2D(x, y);
    }

    /// <summary>
    /// Inverts the slope (shorthand).
    /// </summary>
    public static Equation2D operator -(Equation2D obj) {
        return new Equation2D(-obj.Slope, obj.Intercept);
    }

    /// <summary>
    /// Adds the slope and intercept together, and returns a new object.
    /// </summary>
    public static Equation2D operator +(Equation2D left, Equation2D right) {
        return new Equation2D(left.Slope + right.Slope, left.Intercept + right.Intercept);
    }

    /// <summary>
    /// Returns <c>True</c> is all the values are equal.
    /// </summary>
    public static bool operator ==(Equation2D left, Equation2D right) {
        return left.Equals(right);
    }

    /// <summary>
    /// Returns <c>True</c> if any of the values are not equal.
    /// </summary>
    public static bool operator !=(Equation2D left, Equation2D right) {
        return !left.Equals(right);
    }

    /// <summary>
    /// Returns <c>True</c> if the slope is &gt;, or if slopes are equal and the y-intercept is &gt;.
    /// </summary>
    public static bool operator >(Equation2D left, Equation2D right) {
        if (left.Slope > right.Slope) {
            return true;
        } else if (left.Slope == right.Slope && left.Intercept > right.Intercept) {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns <c>True</c> if the slope is &lt;, or if slopes are equal and the y-intercept is &lt;.
    /// </summary>
    public static bool operator <(Equation2D left, Equation2D right) {
        if (left.Slope < right.Slope) {
            return true;
        } else if (left.Slope == right.Slope && left.Intercept < right.Intercept) {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns <c>True</c> if the slope is &gt;=, or if slopes are equal and the y-intercept is &gt;=.
    /// </summary>
    public static bool operator >=(Equation2D left, Equation2D right) {
        if (left.Slope >= right.Slope) {
            return true;
        } else if (left.Slope == right.Slope && left.Intercept >= right.Intercept) {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns <c>True</c> if the slope is &lt;=, or if slopes are equal and the y-intercept is &lt;=.
    /// </summary>
    public static bool operator <=(Equation2D left, Equation2D right) {
        if (left.Slope <= right.Slope) {
            return true;
        } else if (left.Slope == right.Slope && left.Intercept <= right.Intercept) {
            return true;
        }

        return false;
    }

    // Custom Operator
    /// <summary>
    /// Returns the intersection point between the two equations. 
    /// </summary>
    /// <returns>Intersection <c>Point</c></returns>
    public static Point2D? operator ^(Equation2D left, Equation2D right) {
        return left.IntersectionPoint(right);
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Implementation.Equatable"]/*'/>
    /// </summary>
    public bool Equals(Equation2D other) {
        return other.coefficient0 == coefficient0 && other.coefficient1 == coefficient1;
    }

    /// <summary>
    /// Sorts by slope, then by y-intercept.
    /// </summary>
    /// <returns><include file="_SharedXML.xml" path='doc/member[@name="Phrases.Compare.Return"]/*'/></returns>
    public int CompareTo(Equation2D other) {
        int value = coefficient1.CompareTo(other.coefficient1);
        if (value == 0) {
            return coefficient0.CompareTo(other.coefficient0);
        }
        
        return value;
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.Equals"]/*'/> <see cref="Equals(Equation2D)"/>.
    /// </summary>
    public override bool Equals(object? obj) {
        return obj is Equation2D other && Equals(other);
    }

    /// <summary>
    /// Returns the summed and weighted hash of the two coefficients.
    /// </summary>
    public override int GetHashCode() {
        return HashCode.Combine(coefficient0.GetHashCode(), coefficient1.GetHashCode());
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.ToString"]/*'/> "y = {coefficient1}x + {coefficient0}". 
    /// </summary>
    public override string ToString() {
        return $"y = {coefficient1}x + {coefficient0}";
    }

    /// <summary>
    /// Helper method to calculate equation coefficients from coordinates.
    /// </summary>
    private static (double intercept, double slope) CalculateFromCoordinates(double x1, double y1, double x2, double y2) {
        if (x1 == x2) {
            return (x1, double.PositiveInfinity);
        } else {
            double slope = (y2 - y1) / (x2 - x1);
            double intercept = y1 - x1 * slope;
            return (intercept, slope);
        }
    }
}
