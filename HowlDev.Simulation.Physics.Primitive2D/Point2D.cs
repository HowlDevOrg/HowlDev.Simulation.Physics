namespace HowlDev.Simulation.Physics.Primitve2D;

/// <summary>
/// Class <c>Point</c> holds two double coordinates with a few helpful methods.
/// </summary>
public class Point2D : IEquatable<Point2D>, IComparable<Point2D> {
    private double x;
    private double y;

    /// <summary>
    /// Returns the stored X coordinate.
    /// </summary>
    public double X {
        get { return x; }
        set { x = value; }
    }

    /// <summary>
    /// Returns the stored Y coordinate.
    /// </summary>
    public double Y {
        get { return y; }
        set { y = value; }
    }

    /// <summary>
    /// Property for handling tuples of data
    /// </summary>
    public (double x, double y) Pair {
        get {
            return (x, y);
        }
        set {
            x = value.x;
            y = value.y;
        }
    }

    /// <summary>
    /// Default constructor. Sets to (0, 0).
    /// </summary>
    public Point2D() {
        x = 0;
        y = 0;
    }

    /// <summary>
    /// Individual double constructor.
    /// </summary>
    /// <param name="X">X-coordinate</param>
    /// <param name="Y">Y-coordinate</param>
    public Point2D(double X, double Y) {
        x = X;
        y = Y;
    }

    /// <summary>
    /// Paired double constructor.
    /// </summary>
    /// <param name="point">(X, Y) coordinate</param>
    public Point2D((double x, double y) point) {
        Pair = point;
    }

    /// <summary>
    /// <c>Point</c> duplicator constructor.
    /// </summary>
    public Point2D(Point2D point) {
        Pair = point.Pair;
    }

    /// <summary>
    /// Returns a new <c>Point</c> object at the midpoint between this point and the provided point.
    /// </summary>
    /// <returns>New <c>Point</c> object at the midpoint.</returns>
    public Point2D GetMidpoint(double x, double y) {
        return new Point2D((x - this.x) / 2.0 + this.x, (y - this.y) / 2.0 + this.y);
    }

    /// <summary>
    /// Returns a new <c>Point</c> object at the midpoint between this point and the provided point.
    /// </summary>
    /// <param name="point">Other point to find the midpoint to</param>
    /// <returns>New <c>Point</c> object at the midpoint.</returns>
    public Point2D GetMidpoint(Point2D point) {
        return new Point2D((point.X - x) / 2.0 + x, (point.Y - y) / 2.0 + y);
    }

    /// <summary>
    /// Gets the distance to another point on the plane. 
    /// </summary>
    public double GetDistance(double x, double y) {
        return Math.Sqrt(Math.Pow(x - this.x, 2) + Math.Pow(y - this.y, 2));
    }

    /// <summary>
    /// Gets the distance to another point on the plane. See the direct version: <see cref="GetDistance(double, double)"/>
    /// </summary>
    public double GetDistance(Point2D point) {
        return GetDistance(point.X, point.Y);
    }

    /// <summary>
    /// Assigns from a coordinate pair, the rotation scaled from that point. 
    /// </summary>
    /// <param name="x">Coordinate X</param>
    /// <param name="y">Coordinate Y</param>
    /// <param name="r"><c>Rotation</c> value</param>
    /// <param name="scalar">Scalar amount</param>
    public void AssignPoint(double x, double y, Rotation2D r, double scalar) {
        Point2D newPoint = r * scalar;
        this.x += newPoint.x + x;
        this.y += newPoint.y + y;
    }

    /// <summary>
    /// Assigns this point with a rotation value and a scalar amount
    /// </summary>
    /// <param name="p">Point to assign from</param>
    /// <param name="r">Rotation to rotate by</param>
    /// <param name="scalar">Scalar to scale from</param>
    public void AssignPoint(Point2D p, Rotation2D r, double scalar) {
        AssignPoint(p.X, p.Y, r, scalar);
    }

    /// <summary>
    /// Inverts the given point through the origin.
    /// </summary>
    /// <returns>Inverted point</returns>
    public static Point2D operator -(Point2D obj) {
        return new Point2D(obj.X * -1, obj.Y * -1);
    }

    /// <summary>
    /// Adds two points on the plane as vectors from (0, 0). 
    /// </summary>
    public static Point2D operator +(Point2D left, Point2D right) {
        return new Point2D(left.X + right.X, left.Y + right.Y);
    }

    /// <summary>
    /// Subtracts two points on the plane as vectors from (0, 0). It can also be thought of as inverting 
    /// the second point and adding them. 
    /// </summary>
    public static Point2D operator -(Point2D left, Point2D right) {
        return new Point2D(left.X - right.X, left.Y - right.Y);
    }

    /// <summary>
    /// Equality operator override. 
    /// </summary>
    public static bool operator ==(Point2D left, Point2D right) {
        return left.Equals(right);
    }

    /// <summary>
    /// Inequality operator override. 
    /// </summary>
    public static bool operator !=(Point2D left, Point2D right) {
        return !left.Equals(right);
    }

    // Custom operators
    /// <summary>
    /// Scales the point from the origin.
    /// </summary>
    /// <param name="left"><c>Point</c></param>
    /// <param name="scalar">Scalar</param>
    /// <returns>Scaled point</returns>
    public static Point2D operator *(Point2D left, double scalar) {
        return new Point2D(left.X * scalar, left.Y * scalar);
    }

    /// <summary>
    /// Assigns the left to be the origin and the right to its place relative to it, and returns that point. 
    /// It's also the opposite of the subtraction operator.
    /// </summary>
    /// <param name="left">Origin point</param>
    /// <param name="right">Moved point</param>
    /// <returns>New <c>Point</c> in the modulo'd location</returns>
    public static Point2D operator %(Point2D left, Point2D right) {
        return new Point2D(right.X - left.X, right.Y - left.Y);
    }

    /// <summary>
    /// Gets the angle from the left point to the right point. 
    /// </summary>
    public static Rotation2D operator ^(Point2D left, Point2D right) {
        Rotation2D r = new Rotation2D();
        r.AssignToCoordinates(left.Pair, right.Pair);
        return r;
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Implementation.Equatable"]/*'/>
    /// </summary>
    public bool Equals(Point2D? other) {
        if (other is null) return false;
        return other.x == x && other.y == y;
    }

    /// <summary>
    /// Sorts by X coord, then by Y. 
    /// </summary>
    /// <returns><include file="_SharedXML.xml" path='doc/member[@name="Phrases.Compare.Return"]/*'/></returns>
    public int CompareTo(Point2D? other) {
        if (other is null) return 0;

        int xCoord = x.CompareTo(other.x);
        if (xCoord == 0) {
            return y.CompareTo(other.y);
        }
        return xCoord;
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.Equals"]/*'/> <see cref="Equals(Point2D?)"/>.
    /// </summary>
    public override bool Equals(object? obj) {
        return base.Equals(obj);
    }

    /// <summary>
    /// Get the combined hash code of the two points on the plane. 
    /// </summary>
    public override int GetHashCode() {
        return x.GetHashCode() + y.GetHashCode();
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.ToString"]/*'/> "X: {x}, Y: {y}". 
    /// </summary>
    public override string ToString() {
        return $"X: {x}, Y: {y}";
    }
}