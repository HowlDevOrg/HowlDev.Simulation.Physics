namespace HowlDev.Simulation.Physics.Primitve2D;

/// <summary>
/// Equation holds at most 2 points, returning the slope and Y-intercept of a pair of 
/// points (or the X-intercept if the points are vertical). It handles some logic, 
/// such as intersection points and defining if a point is on a line. 
/// </summary>
public class Equation2D : IEquatable<Equation2D>, IComparable<Equation2D> {
    private double[] coefficients = new double[2];
    private bool x_Intercept = false;

    /// <summary>
    /// Get internal Coefficient array. [1] refers to the slope, [0] to the y-intercept.
    /// </summary>
    public double[] Coefficients { get { return coefficients; } }

    /// <summary>
    /// Gets the current Slope.
    /// </summary>
    public double Slope { get { return coefficients[1]; } }

    /// <summary>
    /// Gets the current Intercept.
    /// </summary>
    public double Intercept { get { return coefficients[0]; } }

    /// <summary>
    /// Default constructor, leaves coefficients as <c>0.0</c>.
    /// </summary>
    public Equation2D() { }

    /// <summary>
    /// Assign slope and intercept directly.
    /// </summary>
    public Equation2D(double slope, double intercept) {
        coefficients[1] = slope;
        coefficients[0] = intercept;
    }

    /// <summary>
    /// Takes in two coordinate pairs and derives their equation.
    /// </summary>
    public Equation2D(double x1, double y1, double x2, double y2) {
        AssignToCoordinates(x1, y1, x2, y2);
    }

    /// <summary>
    /// Takes in a pair of <c>Point</c>s and derives their equation.
    /// </summary>
    public Equation2D(Point2D p1, Point2D p2) {
        AssignToCoordinates(p1.X, p1.Y, p2.X, p2.Y);
    }

    /// <summary>
    /// Takes in a <c>Line</c> and derives its equation.
    /// </summary>
    public Equation2D(Line2D l1) {
        AssignToCoordinates(l1[0].X, l1[0].Y, l1[1].X, l1[1].Y);
    }

    /// <summary>
    /// Duplication constructor. 
    /// </summary>
    public Equation2D(Equation2D equation) {
        coefficients[0] = equation[0];
        coefficients[1] = equation[1];
    }

    /// <summary>
    /// Index into the inner array directly. See <see cref="Coefficients"/> property for the reference.
    /// </summary>
    public double this[int i] => coefficients[i];

    /// <summary>
    /// Allows you to directly write to a value in the Coefficient array.
    /// </summary>
    /// <param name="index">0 for the intercept, 1 for the slope.</param>
    /// <param name="value">Value to be input.</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void UpdateCoefficient(int index, double value) {
        if (index > 1 || index < 0) throw new ArgumentOutOfRangeException("Index must be either 0 or 1.");
        coefficients[index] = value;
    }

    /// <summary>
    /// Assings this instance to the equation for these new points. 
    /// </summary>
    public void AssignToCoordinates(double x1, double y1, double x2, double y2) {
        if (x1 == x2) {
            coefficients[1] = 0;
            coefficients[0] = x1;
            x_Intercept = true;
        } else {
            coefficients[1] = (y2 - y1) / (x2 - x1);
            coefficients[0] = y1 - x1 * coefficients[1];
            x_Intercept = false;
        }
    }

    /// <summary>
    /// Assings this instance to the equation for these new points. 
    /// </summary>
    public void AssignToCoordinates(Point2D p1, Point2D p2) {
        AssignToCoordinates(p1.X, p1.Y, p2.X, p2.Y);
    }

    /// <summary>
    /// Assings this instance to the equation for these new points. 
    /// </summary>
    public void AssignToCoordinates(Line2D l1) {
        AssignToCoordinates(l1[0].X, l1[0].Y, l1[1].X, l1[1].Y);
    }

    /// <summary>
    /// Takes in a <c>Point</c> object and, maintaining the current slope, moves the intercept to where 
    /// it intersects the given point. 
    /// </summary>
    public void AssignToPoint(double x, double y) {
        coefficients[0] = y - coefficients[1] * x;
    }

    /// <summary>
    /// Maintains the current slope and adjusts the intercept to hit this point.
    /// </summary>
    public void AssignToPoint(Point2D point) {
        AssignToPoint(point.X, point.Y);
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
            double right = x * coefficients[1] + coefficients[0];
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
        if (coefficients[1] == e1[1]) return null; // Covers both cases.
        if (x_Intercept && e1.x_Intercept) return null; // Covers vertical line cases

        if (x_Intercept) {
            return new Point2D(Intercept, Intercept * e1[1] + e1[0]);
        } else if (e1.x_Intercept) {
            return new Point2D(e1.Intercept, e1.Intercept * this[1] + this[0]);
        }

        double x = (coefficients[0] - e1[0]) / (e1[1] - coefficients[1]);
        double y = e1[1] * x + e1[0];

        return new Point2D(x, y);
    }

    /// <summary>
    /// Inverts the slope (shorthand).
    /// </summary>
    public static Equation2D operator -(Equation2D obj) {
        return new Equation2D(-obj[1], obj[0]);
    }

    /// <summary>
    /// Adds the slope and intercept together, and returns a new object.
    /// </summary>
    public static Equation2D operator +(Equation2D left, Equation2D right) {
        return new Equation2D(left[1] + right[1], left[0] + right[0]);
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
        if (left[1] > right[1]) {
            return true;
        } else if (left[1] == right[1] && left[0] > right[0]) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Returns <c>True</c> if the slope is &lt;, or if slopes are equal and the y-intercept is &lt;.
    /// </summary>
    public static bool operator <(Equation2D left, Equation2D right) {
        if (left[1] < right[1]) {
            return true;
        } else if (left[1] == right[1] && left[0] < right[0]) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Returns <c>True</c> if the slope is &gt;=, or if slopes are equal and the y-intercept is &gt;=.
    /// </summary>
    public static bool operator >=(Equation2D left, Equation2D right) {
        if (left[1] >= right[1]) {
            return true;
        } else if (left[1] == right[1] && left[0] >= right[0]) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Returns <c>True</c> if the slope is &lt;=, or if slopes are equal and the y-intercept is &lt;=.
    /// </summary>
    public static bool operator <=(Equation2D left, Equation2D right) {
        if (left[1] <= right[1]) {
            return true;
        } else if (left[1] == right[1] && left[0] <= right[0]) {
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
    public bool Equals(Equation2D? other) {
        if (other is null) return false;
        return other[0] == coefficients[0] && other[1] == coefficients[1];
    }

    /// <summary>
    /// Sorts by slope, then by y-intercept.
    /// </summary>
    /// <returns><include file="_SharedXML.xml" path='doc/member[@name="Phrases.Compare.Return"]/*'/></returns>
    public int CompareTo(Equation2D? other) {
        if (other is null) return 0;
        int value = coefficients[1].CompareTo(other[1]);
        if (value == 0) {
            return coefficients[0].CompareTo(other[0]);
        }
        return value;
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.Equals"]/*'/> <see cref="Equals(Equation2D?)"/>.
    /// </summary>
    public override bool Equals(object? obj) {
        return base.Equals(obj);
    }

    /// <summary>
    /// Returns the summed and weighted hash of the two coefficients.
    /// </summary>
    public override int GetHashCode() {
        return coefficients[0].GetHashCode() + coefficients[1].GetHashCode() * 2;
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.ToString"]/*'/> "y = {coefficients[1]}x + {coefficients[0]}". 
    /// </summary>
    public override string ToString() {
        return $"y = {coefficients[1]}x + {coefficients[0]}";
    }
}