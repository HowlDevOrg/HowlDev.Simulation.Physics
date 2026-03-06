namespace HowlDev.Simulation.Physics.Primitive2D;

/// <summary>
/// Given a point and radius, provides methods to determine overlap with other 
/// Circle objects and if points are within them. (This is likely to expand in 
/// the future).
/// </summary>
public readonly struct Circle2D : IEquatable<Circle2D>, IComparable<Circle2D> {
    private readonly Point2D center;
    private readonly double radius;

    #region Properties
    /// <summary>
    /// Returns the Centerpoint of the circle.
    /// </summary>
    public Point2D Center => center;

    /// <summary>
    /// Returns the radius of the circle. 
    /// </summary>
    public double Radius => radius;

    /// <summary>
    /// Returns the diameter of the circle. 
    /// </summary>
    public double Diameter => radius * 2;
    #endregion
    #region Constructors
    /// <summary>
    /// Sets a default Point2D (0, 0) and radius of 1. 
    /// </summary>
    public Circle2D() {
        center = new();
        radius = 1;
    }

    /// <summary>
    /// Given a pair of center coordinates and a radius, creates a circle with those properties.
    /// </summary>
    public Circle2D(double x, double y, double radius) {
        center = new Point2D(x, y);
        this.radius = radius;
    }

    /// <summary>
    /// Given a Point2D and a radius, creates a circle with those properties.
    /// </summary>
    public Circle2D(Point2D center, double radius) {
        this.center = center;
        this.radius = radius;
    }
    #endregion
    #region Methods
    /// <summary>
    /// If the distance between the two are &lt;= the radii combined, returns true (kissing 
    /// circles overlap).
    /// </summary>
    public bool IsOverlapping(Circle2D other) {
        double circleDistance = radius + other.Radius;
        double centerDistance = center.GetDistance(other.center);
        return centerDistance <= circleDistance;
    }

    /// <summary>
    /// Returns True if the given point is within or on the boundary of the Circle. 
    /// </summary>
    public bool Contains(Point2D point) {
        return center.GetDistance(point) <= radius;
    }

    /// <summary>
    /// Returns a new Circle object with a given centerpoint.
    /// </summary>
    public Circle2D WithNewCenter(Point2D point) {
        return new Circle2D(point, radius);
    }

    /// <summary>
    /// Returns a new Circle object with a given radius.
    /// </summary>
    public Circle2D WithNewRadius(double radius) {
        return new Circle2D(center, radius);
    }
    #endregion
    #region Operators
    /// <summary/>
    public static bool operator ==(Circle2D left, Circle2D right) {
        return left.Equals(right);
    }

    /// <summary/>
    public static bool operator !=(Circle2D left, Circle2D right) {
        return !(left == right);
    }

    /// <summary/>
    public static bool operator <(Circle2D left, Circle2D right) {
        return left.CompareTo(right) < 0;
    }

    /// <summary/>
    public static bool operator <=(Circle2D left, Circle2D right) {
        return left.CompareTo(right) <= 0;
    }

    /// <summary/>
    public static bool operator >(Circle2D left, Circle2D right) {
        return left.CompareTo(right) > 0;
    }

    /// <summary/>
    public static bool operator >=(Circle2D left, Circle2D right) {
        return left.CompareTo(right) >= 0;
    }
    #endregion
    #region Custom Operators

    #endregion
    #region Default Overrides
    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Implementation.Equatable"]/*'/>
    /// </summary>
    public bool Equals(Circle2D other) {
        return center == other.center && radius == other.radius;
    }

    /// <summary>
    /// IComparable interface implementation. Checks the point first, then compares the radius.
    /// </summary>
    /// <returns><include file="_SharedXML.xml" path='doc/member[@name="Phrases.Compare.Return"]/*'/></returns>
    public int CompareTo(Circle2D other) {
        int point = center.CompareTo(other.center);
        if (point == 0) {
            return radius.CompareTo(other.radius);
        }

        return point;
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.Equals"]/*'/> <see cref="Equals(Circle2D)"/>.
    /// </summary>
    public override bool Equals(object? obj) {
        return obj is Circle2D other && Equals(other);
    }

    /// <summary>
    /// Gets the hash code of the Angle inside this class.
    /// </summary>
    public override int GetHashCode() {
        return HashCode.Combine(radius.GetHashCode(), center.GetHashCode());
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.ToString"]/*'/> "Angle: {rotationAngle}".
    /// </summary>
    public override string ToString() {
        return $"Center: {center}, Radius: {radius}";
    }
    #endregion
}
