namespace HowlDev.Simulation.Physics.Primitive2D;

/// <summary>
/// Readonly struct Equation holds at most 2 points, returning the slope and Y-intercept of a pair of 
/// points (or the X-intercept if the points are vertical). It handles some logic, 
/// such as intersection points and defining if a point is on a line. 
/// </summary>
public readonly struct Equation2D : IEquatable<Equation2D>, IComparable<Equation2D> {
    private readonly double intercept;
    private readonly double slope;
    private bool x_Intercept => slope == double.PositiveInfinity;

    /// <summary>
    /// Gets the current Slope.
    /// </summary>
    public double Slope => slope;

    /// <summary>
    /// Gets the current Intercept.
    /// </summary>
    public double Intercept => intercept;

    /// <summary>
    /// Default constructor, leaves coefficients as <c>0.0</c>.
    /// </summary>
    public Equation2D() {
        intercept = 0.0;
        slope = 0.0;
    }

    /// <summary>
    /// Assign slope and intercept directly.
    /// </summary>
    public Equation2D(double slope, double intercept) {
        this.slope = slope;
        this.intercept = intercept;
    }

    /// <summary>
    /// Takes in two coordinate pairs and derives their equation.
    /// </summary>
    public Equation2D(double x1, double y1, double x2, double y2) {
        (intercept, slope) = CalculateFromCoordinates(x1, y1, x2, y2);
    }

    /// <summary>
    /// Takes in a pair of <c>Point</c>s and derives their equation.
    /// </summary>
    public Equation2D(Point2D p1, Point2D p2) {
        (intercept, slope) = CalculateFromCoordinates(p1.X, p1.Y, p2.X, p2.Y);
    }

    /// <summary>
    /// Takes in a <c>Line</c> and derives its equation.
    /// </summary>
    public Equation2D(Line2D l1) {
        (intercept, slope) = CalculateFromCoordinates(l1.StartPoint.X, l1.StartPoint.Y, l1.EndPoint.X, l1.EndPoint.Y);
    }

    /// <summary>
    /// Duplication constructor. 
    /// </summary>
    public Equation2D(Equation2D equation) {
        intercept = equation.intercept;
        slope = equation.slope;
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
    /// Returns the value at a given X coordinate.
    /// </summary>
    public double ValueAt(double x) {
        return Slope * x + Intercept;
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
        if (Slope == e1.Slope) return null; // Covers both cases.
        if (x_Intercept && e1.x_Intercept) return null; // Covers vertical line cases

        if (x_Intercept) {
            return new Point2D(Intercept, Intercept * e1.slope + e1.intercept);
        } else if (e1.x_Intercept) {
            return new Point2D(e1.Intercept, e1.Intercept * slope + intercept);
        }

        double x = (intercept - e1.intercept) / (e1.slope - slope);
        double y = e1.slope * x + e1.intercept;

        return new Point2D(x, y);
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
        return other.intercept == intercept && other.slope == slope;
    }

    /// <summary>
    /// Sorts by slope, then by y-intercept.
    /// </summary>
    /// <returns><include file="_SharedXML.xml" path='doc/member[@name="Phrases.Compare.Return"]/*'/></returns>
    public int CompareTo(Equation2D other) {
        int value = slope.CompareTo(other.slope);
        if (value == 0) {
            return intercept.CompareTo(other.intercept);
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
        return HashCode.Combine(Slope.GetHashCode(), Intercept.GetHashCode());
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.ToString"]/*'/> "y = {coefficient1}x + {coefficient0}". 
    /// </summary>
    public override string ToString() {
        return $"y = {Slope}x + {Intercept}";
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
