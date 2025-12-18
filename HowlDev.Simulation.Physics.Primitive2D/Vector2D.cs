namespace HowlDev.Simulation.Physics.Primitive2D;

/// <summary>
/// Readonly struct <c>Vector</c> holds a rotation and velocity value, and can move <see cref="Point2D"/> values 
/// by those parameters. 
/// </summary>
public readonly struct Vector2D : IComparable<Vector2D>, IEquatable<Vector2D> {
    private readonly Rotation2D rotation;
    private readonly double velocity;

    /// <summary>
    /// <c>Rotation</c> of the momentum object. 
    /// </summary>
    public Rotation2D Rotation => rotation;

    /// <summary>
    /// Velocity (speed per unit of time).
    /// </summary>
    public double Velocity => velocity;

    /// <summary>
    /// Assigns a default <see cref="Rotation2D"/> constructor and velocity to <c>0.0</c>.
    /// </summary>
    public Vector2D() {
        rotation = new Rotation2D();
        velocity = 0.0;
    }

    /// <summary>
    /// Creates a new <c>Rotation</c> object and assigns velocity directly.
    /// </summary>
    public Vector2D(double rotation, double velocity) {
        this.rotation = new Rotation2D(rotation);
        this.velocity = velocity;
    }

    /// <summary>
    /// Duplicates the <c>Rotation</c> object and assigns velocity directly.
    /// </summary>
    public Vector2D(Rotation2D rotation, double velocity) {
        this.rotation = new Rotation2D(rotation);
        this.velocity = velocity;
    }

    /// <summary>
    /// Returns a new Vector2D with the updated rotation at the angle specified.
    /// </summary>
    /// <returns>A new Vector2D with the updated rotation</returns>
    public Vector2D WithRotation(double rotationInt) {
        return new Vector2D(rotationInt, velocity);
    }

    /// <summary>
    /// Returns a new Vector2D with the updated rotation by duplicating the object. 
    /// </summary>
    /// <returns>A new Vector2D with the updated rotation</returns>
    public Vector2D WithRotation(Rotation2D rotationObject) {
        return WithRotation(rotationObject.RotationAngle);
    }

    /// <summary>
    /// Returns a new Vector2D with the updated velocity value.
    /// </summary>
    /// <returns>A new Vector2D with the updated velocity</returns>
    public Vector2D WithVelocity(double velocityDouble) {
        return new Vector2D(rotation, velocityDouble);
    }

    /// <summary>
    /// Creates a Vector2D with rotation and velocity from coordinates. 
    /// </summary>
    /// <returns>A new Vector2D with calculated rotation and velocity</returns>
    public static Vector2D FromCoordinates(double x, double y) {
        Rotation2D rot = Rotation2D.FromCoordinates(x, y);
        double vel = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        return new Vector2D(rot, vel);
    }

    /// <summary>
    /// Creates a Vector2D with rotation and velocity from a <c>Point</c>. 
    /// </summary>
    /// <returns>A new Vector2D with calculated rotation and velocity</returns>
    public static Vector2D FromCoordinates(Point2D point) {
        return FromCoordinates(point.X, point.Y);
    }

    /// <summary>
    /// Creates a Vector2D from the first to the second point.
    /// </summary>
    /// <returns>A new Vector2D representing the vector between points</returns>
    public static Vector2D FromCoordinates(double x1, double y1, double x2, double y2) {
        return FromCoordinates(x2 - x1, y2 - y1);
    }

    /// <summary>
    /// Creates a Vector2D from the first to the second Point.
    /// </summary>
    /// <returns>A new Vector2D representing the vector between points</returns>
    public static Vector2D FromCoordinates(Point2D point1, Point2D point2) {
        return FromCoordinates(point2.X - point1.X, point2.Y - point1.Y);
    }

    /// <summary>
    /// Adds a given <c>Point</c> with the rotation value scaled by the velocity value. Returns a new reference.
    /// </summary>
    public static Point2D operator +(Point2D point, Vector2D momentum) {
        Point2D momentumPoint = momentum.Rotation * momentum.Velocity; // this is so sick
        return new Point2D(point.X + momentumPoint.X, point.Y + momentumPoint.Y);
    }

    /// <summary>
    /// Adds a given <c>Point</c> and the inverted rotation value scaled by the velocity value. Returns a new reference.
    /// </summary>
    public static Point2D operator -(Point2D point, Vector2D momentum) {
        Point2D momentumPoint = momentum.Rotation * momentum.Velocity;
        return new Point2D(point.X - momentumPoint.X, point.Y - momentumPoint.Y);
    }

    /// <summary>
    /// Uses the Equals method to determine equality. 
    /// </summary>
    public static bool operator ==(Vector2D m1, Vector2D m2) {
        return m1.Equals(m2);
    }

    /// <summary>
    /// Inverts the Equals method to determine non-equality.
    /// </summary>
    public static bool operator !=(Vector2D m1, Vector2D m2) {
        return !m1.Equals(m2);
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Implementation.Equatable"]/*'/>
    /// </summary>
    public bool Equals(Vector2D other) {
        return velocity == other.Velocity && rotation == other.Rotation;
    }

    /// <summary>
    /// Sorts by Velocity value, then by the Rotation value.
    /// </summary>
    /// <returns><include file="_SharedXML.xml" path='doc/member[@name="Phrases.Compare.Return"]/*'/></returns>
    public int CompareTo(Vector2D other) {
        int value = velocity.CompareTo(other.Velocity);
        if (value == 0) {
            return rotation.CompareTo(other.Rotation);
        }
        return value;
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.Equals"]/*'/> <see cref="Equals(Vector2D)"/>.
    /// </summary>
    public override bool Equals(object? obj) {
        return obj is Vector2D other && Equals(other);
    }

    /// <summary>
    /// Combines the hash codes of the Rotation and Velocity values. 
    /// </summary>
    public override int GetHashCode() {
        return rotation.GetHashCode() + velocity.GetHashCode();
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.ToString"]/*'/> "Rotation: {rotation}, Velocity: {velocity}". 
    /// </summary>
    public override string ToString() {
        return $"Rotation: {rotation}, Velocity: {velocity}";
    }
}