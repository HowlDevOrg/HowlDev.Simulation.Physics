namespace HowlDev.Simulation.Physics.Primitive2D;

/// <summary>
/// Readonly struct <c>Point</c> holds two double coordinates with a few helpful methods.
/// </summary>
public readonly struct Point2D : IEquatable<Point2D>, IComparable<Point2D> {
    private readonly double x;
    private readonly double y;

    /// <summary>
    /// Returns the stored X coordinate.
    /// </summary>
    public double X => x;

    /// <summary>
    /// Returns the stored Y coordinate.
    /// </summary>
    public double Y => y;

    /// <summary>
    /// Property for handling tuples of data
    /// </summary>
    public (double x, double y) Pair => (x, y);

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
        x = point.x;
        y = point.y;
    }

    /// <summary>
    /// <c>Point</c> duplicator constructor.
    /// </summary>
    public Point2D(Point2D point) {
        x = point.x;
        y = point.y;
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
    /// Returns a vector from this point to the parameter. 
    /// </summary>
    /// <param name="point">Point to target</param>
    public Vector2D GetVector(Point2D point) {
        return Vector2D.FromCoordinates(x, y, point.x, point.y);
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
    /// Returns a new point from a coordinate pair, the rotation scaled from that point. 
    /// </summary>
    /// <param name="x">Coordinate X</param>
    /// <param name="y">Coordinate Y</param>
    /// <param name="r"><c>Rotation</c> value</param>
    /// <param name="scalar">Scalar amount</param>
    /// <returns>A new Point2D with the calculated position</returns>
    public Point2D WithRotation(double x, double y, Rotation2D r, double scalar) {
        Point2D newPoint = r * scalar;
        return new Point2D(this.x + newPoint.x + x, this.y + newPoint.y + y);
    }

    /// <summary>
    /// Returns a new point with a rotation value and a scalar amount
    /// </summary>
    /// <param name="p">Point to assign from</param>
    /// <param name="r">Rotation to rotate by</param>
    /// <param name="scalar">Scalar to scale from</param>
    /// <returns>A new Point2D with the calculated position</returns>
    public Point2D WithRotation(Point2D p, Rotation2D r, double scalar) {
        return WithRotation(p.X, p.Y, r, scalar);
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
        return Rotation2D.FromCoordinates(left.Pair, right.Pair);
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Implementation.Equatable"]/*'/>
    /// </summary>
    public bool Equals(Point2D other) {
        return other.x == x && other.y == y;
    }

    /// <summary>
    /// Sorts by X coord, then by Y. 
    /// </summary>
    /// <returns><include file="_SharedXML.xml" path='doc/member[@name="Phrases.Compare.Return"]/*'/></returns>
    public int CompareTo(Point2D other) {
        int xCoord = x.CompareTo(other.x);
        if (xCoord == 0) {
            return y.CompareTo(other.y);
        }
        return xCoord;
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.Equals"]/*'/> <see cref="Equals(Point2D)"/>.
    /// </summary>
    public override bool Equals(object? obj) {
        return obj is Point2D other && Equals(other);
    }

    /// <summary>
    /// Get the combined hash code of the two points on the plane. 
    /// </summary>
    public override int GetHashCode() {
        return HashCode.Combine(x.GetHashCode(), y.GetHashCode());
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.ToString"]/*'/> "X: {x}, Y: {y}". 
    /// </summary>
    public override string ToString() {
        return $"X: {x}, Y: {y}";
    }
}