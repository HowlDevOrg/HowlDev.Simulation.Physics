namespace Physics2DLibrary; 

/// <summary>
/// <c>Vector</c> holds a rotation and velocity value, and can move <see cref="Point2D"/> values 
/// by those parameters. 
/// </summary>
public class Vector2D : IComparable<Vector2D>, IEquatable<Vector2D> {
    private Rotation2D rotation;
    private double velocity;

    /// <summary>
    /// <c>Rotation</c> of the momentum object. 
    /// </summary>
    public Rotation2D Rotation { get { return rotation; } }

    /// <summary>
    /// Velocity (speed per unit of time).
    /// </summary>
    public double Velocity { get { return velocity; } }

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
    /// Updates the <c>Rotation</c> with a new reference at the angle specified.
    /// </summary>
    public void UpdateRotation(double rotationInt) {
        rotation = new Rotation2D(rotationInt);
    }

    /// <summary>
    /// Updates the <c>Rotation</c> with a new reference by duplicating the object. 
    /// </summary>
    public void UpdateRotation(Rotation2D rotationObject) {
        UpdateRotation(rotationObject.RotationAngle);
    }

    /// <summary>
    /// Updates the Velocity value directly.
    /// </summary>
    public void UpdateVelocity(double velocityDouble) {
        velocity = velocityDouble;
    }

    /// <summary>
    /// Assigns the <c>Rotation</c> and Velocity values to that point. 
    /// </summary>
    public void AssignToCoordinates(double x, double y) {
        rotation.AssignToCoordinates(x, y);
        velocity = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
    }

    /// <summary>
    /// Assigns the <c>Rotation</c> and Velocity values to that <c>Point</c>. 
    /// </summary>
    public void AssignToCoordinates(Point2D point) {
        AssignToCoordinates(point.X, point.Y);
    }

    /// <summary>
    /// Assigns the vector from the first to the second point.
    /// </summary>
    public void AssignToCoordinates(double x1, double y1, double x2, double y2) {
        AssignToCoordinates(x2 - x1, y2 - y1);
    }

    /// <summary>
    /// Assigns the vector from the first to the second Point.
    /// </summary>
    public void AssignToCoordinates(Point2D point1, Point2D point2) {
        AssignToCoordinates(point2.X - point1.X, point2.Y - point1.Y);
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
    public bool Equals(Vector2D? other) {
        if (other is null) return false;
        return velocity == other.Velocity && rotation == other.Rotation;
    }

    /// <summary>
    /// Sorts by Velocity value, then by the Rotation value.
    /// </summary>
    /// <returns><include file="_SharedXML.xml" path='doc/member[@name="Phrases.Compare.Return"]/*'/></returns>
    public int CompareTo(Vector2D? other) {
        if (other is null) return 0;

        int value = velocity.CompareTo(other.Velocity);
        if (value == 0) {
            return rotation.CompareTo(other.Rotation);
        }
        return value;
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.Equals"]/*'/> <see cref="Equals(Vector2D?)"/>.
    /// </summary>
    public override bool Equals(object? obj) {
        return base.Equals(obj);
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
