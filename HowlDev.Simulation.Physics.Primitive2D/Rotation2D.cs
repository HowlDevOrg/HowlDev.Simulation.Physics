namespace HowlDev.Simulation.Physics.Primitive2D;

/// <summary>
/// Class <c>Rotation</c> stores a single RotationAngle as a double (rounded to 2 decimal places) 
/// and provides a number of helpful methods and properties to work with 2D angles. It 
/// recalculates unit coordinates every time the value is updated for faster and less costly 
/// retreival. It implements the IEquatable and IComparable interfaces.
/// </summary>
public readonly struct Rotation2D : IEquatable<Rotation2D>, IComparable<Rotation2D> {
    private readonly double rotationAngle;
    private readonly double xCoord;
    private readonly double yCoord;

    #region Properties
    /// <value>
    /// Gets the rotation angle. Always between 0 and 359.99.
    /// </value>
    public double RotationAngle => rotationAngle;

    /// <value>
    /// Gets the X coordinate of the angle on a unit circle (as a radius 
    /// of 1).
    /// </value>
    public double X_Coord => xCoord;

    /// <value>
    /// Gets the Y coordinate of the angle on a unit circle (as a radius 
    /// of 1).
    /// </value>
    public double Y_Coord => yCoord;

    /// <summary>
    /// A point on the Unit Circle. 
    /// </summary>
    public Point2D Coords {
        get {
            return new Point2D(X_Coord, Y_Coord);
        }
    }
    #endregion
    #region Constructors
    /// <summary>
    /// Default constructor assigns angle to 0.
    /// </summary>
    public Rotation2D() {
        (rotationAngle, xCoord, yCoord) = CalculateRotationData(0);
    }

    /// <summary>
    /// Constructor assigns angle to the input angle.
    /// </summary>
    public Rotation2D(double angle) {
        (rotationAngle, xCoord, yCoord) = CalculateRotationData(angle);
    }

    /// <summary>
    /// Constructor takes in a <c>Rotation</c> object and duplicates it.
    /// </summary>
    public Rotation2D(Rotation2D value) {
        (rotationAngle, xCoord, yCoord) = (value.rotationAngle, value.xCoord, value.yCoord);
    }
    #endregion
    #region Methods
    /// <summary>
    /// Given an input angle, it adds or removes by that value and ensures 
    /// the bounds of 0 - 359.
    /// </summary>
    /// <param name="angleDifference">Positive or negative int to adjust the current angle by</param>
    /// <returns>A new Rotation2D with the adjusted angle</returns>
    public Rotation2D AdjustBy(double angleDifference) {
        return new Rotation2D(rotationAngle + angleDifference);
    }

    /// <summary>
    /// Takes in a pair of coordinates from origin (so convert them to that beforehand or use the other method) 
    /// and returns a rotation with that angle value. 
    /// </summary>
    /// <returns>A new Rotation2D with the angle to the coordinates</returns>
    public static Rotation2D FromCoordinates(double x, double y) {
        double radian = Math.Atan2(y, x);
        return FromRadian(radian);
    }

    /// <summary>
    /// Returns a rotation with the angle from the first point to the second point.
    /// See also: <seealso cref="FromCoordinates(double, double)"/>
    /// </summary>
    /// <param name="x1">Current X position</param>
    /// <param name="y1">Current Y position</param>
    /// <param name="x2">Target X position</param>
    /// <param name="y2">Target Y position</param>
    /// <returns>A new Rotation2D with the angle from point 1 to point 2</returns>
    public static Rotation2D FromCoordinates(double x1, double y1, double x2, double y2) {
        return FromCoordinates(x2 - x1, y2 - y1);
    }

    /// <summary>
    /// Takes in a pair of tuples and returns a rotation with the angle to those points, using an overload
    /// of this method. 
    /// See also: <seealso cref="FromCoordinates(double, double)"/>
    /// </summary>
    /// <returns>A new Rotation2D with the angle between the points</returns>
    public static Rotation2D FromCoordinates((double, double) point1, (double, double) point2) {
        return FromCoordinates(point1.Item1, point1.Item2, point2.Item1, point2.Item2);
    }

    /// <summary>
    /// Point passthrough for the rotation from the origin.
    /// </summary>
    /// <returns>A new Rotation2D with the angle to the point</returns>
    public static Rotation2D FromCoordinates(Point2D p) {
        return FromCoordinates(p.X, p.Y);
    }

    /// <summary>
    /// Two-Point passthrough for the rotation from the first point. 
    /// See <see cref="FromCoordinates(double, double, double, double)"/>.
    /// </summary>
    /// <param name="p1">Current Point</param>
    /// <param name="p2">Target Point</param>
    /// <returns>A new Rotation2D with the angle from p1 to p2</returns>
    public static Rotation2D FromCoordinates(Point2D p1, Point2D p2) {
        return FromCoordinates(p1.X, p1.Y, p2.X, p2.Y);
    }

    /// <summary>
    /// Line passthrough for creating rotation from coordinates. 
    /// </summary>
    /// <returns>A new Rotation2D with the angle of the line</returns>
    public static Rotation2D FromCoordinates(Line2D l1) {
        return FromCoordinates(l1.StartPoint.X, l1.StartPoint.Y, l1.EndPoint.X, l1.EndPoint.Y);
    }

    /// <summary>
    /// Flips the angle on the X axis.
    /// </summary>
    /// <returns>A new Rotation2D flipped on the X axis</returns>
    public Rotation2D FlipX() {
        double newAngle;
        if (rotationAngle < 90) {
            newAngle = 360 - rotationAngle;
        } else if (rotationAngle < 180) {
            newAngle = 180 + (180 - rotationAngle);
        } else if (rotationAngle < 270) {
            newAngle = 180 - (rotationAngle - 180);
        } else {
            newAngle = 360 - rotationAngle;
        }

        return new Rotation2D(newAngle);
    }

    /// <summary>
    /// Flips the angle on the Y axis.
    /// </summary>
    /// <returns>A new Rotation2D flipped on the Y axis</returns>
    public Rotation2D FlipY() {
        double newAngle;
        if (rotationAngle < 90) {
            newAngle = 90 + (90 - rotationAngle);
        } else if (rotationAngle < 180) {
            newAngle = 180 - rotationAngle;
        } else if (rotationAngle < 270) {
            newAngle = 270 + (270 - rotationAngle);
        } else {
            newAngle = 270 - (rotationAngle - 270);
        }

        return new Rotation2D(newAngle);
    }

    /// <summary>
    /// Flips the angle on both axes.
    /// </summary>
    /// <returns>A new Rotation2D flipped on both axes</returns>
    public Rotation2D DoubleFlip() {
        return new Rotation2D(rotationAngle - 180);
    }

    /// <summary>
    /// Returns the closest distance ( &lt;= 180 ) to the given angle.
    /// </summary>
    /// <param name="angle">Angle to compare against</param>
    /// <param name="absValue">Set to <c>True</c> to return the absolute value of the distance</param>
    /// <returns></returns>
    public double DistanceTo(double angle, bool absValue = true) {
        double[] distances = [angle - (rotationAngle + 360), angle + 360 - rotationAngle, angle - rotationAngle];
        Array.Sort(distances, (a, b) => {
            if (Math.Abs(a) == Math.Abs(b)) return 0;
            if (Math.Abs(a) < Math.Abs(b)) return -1;
            return 1;
        });

        if (Math.Abs(distances[0]) == 180) return 180; // Hard-coded for 180 degrees. 

        if (absValue) {
            return Math.Abs(distances[0]);
        } else {
            return distances[0];
        }
    }

    /// <summary>
    /// Returns the closest distance ( &lt;= 180 ) to the given object.
    /// See <see cref="DistanceTo(double, bool)"/>
    /// </summary>
    /// <param name="angle">Angle to compare against</param>
    /// <param name="absValue">Set to <c>True</c> to return the absolute value of the distance</param>
    public double DistanceTo(Rotation2D angle, bool absValue = true) {
        return DistanceTo(angle.RotationAngle, absValue);
    }

    /// <summary>
    /// Averages between this and the "other" parameter by a percentage. 0 maintains the current angle, 
    /// and 1 sets it to the other angle. 
    /// </summary>
    /// <param name="angle">Angle to average to</param>
    /// <param name="percent">Range from 0 to 1</param>
    /// <returns>A new Rotation2D averaged toward the target angle</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public Rotation2D AverageTo(double angle, double percent) {
        if (percent < 0 || percent > 1) { throw new ArgumentOutOfRangeException(nameof(percent), "Percent must be between 0 and 1."); }

        double diff = angle - rotationAngle;
        double distance = DistanceTo(angle);

        if (diff > 180 || diff < 0) { distance *= -1; }

        distance *= percent;
        return AdjustBy(distance);
    }

    /// <summary>
    /// Averages between this and the "other" parameter by a percentage. 0 maintains the current angle, 
    /// and 1 sets it to the other angle. This just passes through to the other overload.
    /// See also: <see cref="AverageTo(double, double)"/>
    /// </summary>
    /// <param name="other">Rotation to average to</param>
    /// <param name="percent">Range from 0 to 1</param>
    /// <returns>A new Rotation2D averaged toward the target angle</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public Rotation2D AverageTo(Rotation2D other, double percent) {
        return AverageTo(other.RotationAngle, percent);
    }

    /// <summary>
    /// Given an input angle, move towards that angle with the max amount provided; otherwise, snap 
    /// to that angle. 
    /// </summary>
    /// <param name="angle">Angle to move towards</param>
    /// <param name="maxAmount">Max amount of angle movement (must be positive)</param>
    /// <returns>A new Rotation2D moved toward the target angle</returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    public Rotation2D MoveTo(double angle, double maxAmount) {
        if (maxAmount <= 0) throw new ArgumentOutOfRangeException(nameof(maxAmount), "Max Amount must be a positive value");

        double distance = DistanceTo(angle, false);

        if (Math.Abs(distance) < maxAmount) {
            return new Rotation2D(angle);
        } else {
            if (distance > 0) {
                return new Rotation2D(rotationAngle + maxAmount);
            } else {
                return new Rotation2D(rotationAngle - maxAmount);
            }
        }
    }

    /// <summary>
    /// <c>Rotation</c> passthrough for standard <see cref="MoveTo(double, double)"/> method.
    /// </summary>
    /// <returns>A new Rotation2D moved toward the target angle</returns>
    /// <exception cref="ArgumentOutOfRangeException" />
    public Rotation2D MoveTo(Rotation2D angle, double maxAmount) {
        return MoveTo(angle.RotationAngle, maxAmount);
    }

    /// <summary>
    /// Static method to return the angle to a point in space. 
    /// </summary>
    public static double AngleOf(double x, double y) {
        double value = Math.Atan2(y, x) / Math.PI * 180;
        if (value < 0) { value += 360; }
        
        return Math.Round(value, 2);
    }

    /// <summary>
    /// Static method to return the angle to a point in space. 
    /// </summary>
    public static double AngleOf(Point2D point) {
        return AngleOf(point.X, point.Y);
    }

    /// <summary>
    /// Suggested by AI. I think it could be a useful method. <br/>
    /// Returns true if the distance to the other value is less or equal to the tolerance.  
    /// </summary>
    public bool IsCloseTo(Rotation2D other, double tolerance)
        => DistanceTo(other) <= tolerance;

    /// <summary>
    /// In-place mutation. For performance benefits only if needed. 
    /// </summary>
    public static void RotateBy(ref Rotation2D rotation, double delta) {
        rotation = new Rotation2D(rotation.RotationAngle + delta);
    }
    #endregion
    #region Operators
    /// <summary>
    /// Implicit conversion as a constructor shorthand.
    /// </summary>
    public static implicit operator Rotation2D(double value) {
        return new Rotation2D(value);
    }

    /// <summary>
    /// Explicit double retrieval.
    /// </summary>
    public static explicit operator double(Rotation2D value) {
        return value.RotationAngle;
    }

    /// <summary>
    /// Returns a rotation that was flipped on the X axis (shorthand).
    /// </summary>
    public static Rotation2D operator -(Rotation2D obj) {
        return obj.FlipX();
    }

    /// <summary>
    /// Returns the not! of the object. See <see cref="operator true(Rotation2D)"/> and <see cref="operator false(Rotation2D)"/>.
    /// </summary>
    public static bool operator !(Rotation2D obj) {
        if (obj) {
            return false;
        } else {
            return true;
        }
    }

    /// <summary>
    /// Returns <c>true</c> if the rotation angle is not 0. It acts like JavaScript's "truthy".
    /// </summary>
    public static bool operator true(Rotation2D obj) {
        return obj.RotationAngle != 0;
    }

    /// <summary>
    /// Returns <c>false</c> if the rotation angle is 0. See also <seealso cref="operator true(Rotation2D)"/>.
    /// </summary>
    public static bool operator false(Rotation2D obj) {
        return obj.RotationAngle == 0;
    }

    /// <summary>
    /// Increments the rotation angle by 1.
    /// </summary>
    public static Rotation2D operator ++(Rotation2D obj) {
        return new Rotation2D(obj.RotationAngle + 1);
    }

    /// <summary>
    /// Decrements the rotation angle by 1.
    /// </summary>
    public static Rotation2D operator --(Rotation2D obj) {
        return new Rotation2D(obj.RotationAngle - 1);
    }

    /// <summary>
    /// Adds one rotation angle to the other, and returns a normalized angle.
    /// </summary>
    /// <returns>Adjusted left object</returns>
    public static Rotation2D operator +(Rotation2D left, Rotation2D right) {
        return new Rotation2D(left.RotationAngle + right.RotationAngle);
    }

    /// <summary>
    /// Subtracts one rotation angle from the other, and returns a normalized angle.
    /// </summary>
    /// <returns>Adjusted left object.</returns>
    public static Rotation2D operator -(Rotation2D left, Rotation2D right) {
        return new Rotation2D(left.RotationAngle - right.RotationAngle);
    }

    /// <summary>
    /// Averages the two angles together (with a separation above 180, so 0 and 180 returns 90). 
    /// </summary>
    /// <returns>New <c>Rotation</c> with the average angle set</returns>
    public static Rotation2D operator ^(Rotation2D left, Rotation2D right) {
        double diff = Math.Abs(left.RotationAngle - right.RotationAngle);
        double average = (left.RotationAngle + right.RotationAngle) / 2;

        if (diff > 180) { average += 180; }

        return new Rotation2D(average);
    }

    /// <summary>
    /// Returns a new rotation with the first modulo'd by the second angle (angle, not radians)
    /// </summary>
    /// <param name="left">Rotation to be modulo'd</param>
    /// <param name="right">Rotation to do the modulo-ing</param>
    /// <returns>New <c>Rotation</c> object</returns>
    public static Rotation2D operator %(Rotation2D left, Rotation2D right) {
        return new Rotation2D(left.RotationAngle % right.RotationAngle);
    }

    /// <summary>
    /// Equality operator override.
    /// </summary>
    public static bool operator ==(Rotation2D left, Rotation2D right) {
        return left.Equals(right);
    }

    /// <summary>
    /// Inequality operator override.
    /// </summary>
    public static bool operator !=(Rotation2D left, Rotation2D right) {
        return !left.Equals(right);
    }

    /// <summary>
    /// Returns true if left is less than right.
    /// </summary>
    public static bool operator <(Rotation2D left, Rotation2D right) {
        return left.RotationAngle < right.RotationAngle;
    }

    /// <summary>
    /// Returns true if left is greater than right.
    /// </summary>
    public static bool operator >(Rotation2D left, Rotation2D right) {
        return left.RotationAngle > right.RotationAngle;
    }

    /// <summary>
    /// Returns true if left is less than or equal to right.
    /// </summary>
    public static bool operator <=(Rotation2D left, Rotation2D right) {
        return left.RotationAngle <= right.RotationAngle;
    }

    /// <summary>
    /// Returns true if left is greater than or equal to right.
    /// </summary>
    public static bool operator >=(Rotation2D left, Rotation2D right) {
        return left.RotationAngle >= right.RotationAngle;
    }
    #endregion
    #region Custom operators
    /// <summary>
    /// Returns a pair of coordinates by multiplying the X and Y coordinates of the Rotation value 
    /// by the given scalar.
    /// </summary>
    /// <param name="left">Rotation object to get coordinates of</param>
    /// <param name="scalar">Scalar to scale unit circle by</param>
    /// <returns>Coordinate pair</returns>
    public static Point2D operator *(Rotation2D left, double scalar) {
        Point2D p = left.Coords;
        p *= scalar;
        return p;
    }
    #endregion
    #region Default Overrides
    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Implementation.Equatable"]/*'/>
    /// </summary>
    public bool Equals(Rotation2D other) {
        return rotationAngle == other.rotationAngle;
    }

    /// <summary>
    /// IComparable interface implementation. 
    /// </summary>
    /// <returns><include file="_SharedXML.xml" path='doc/member[@name="Phrases.Compare.Return"]/*'/></returns>
    public int CompareTo(Rotation2D other) {
        return rotationAngle.CompareTo(other.rotationAngle);
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.Equals"]/*'/> <see cref="Equals(Rotation2D)"/>.
    /// </summary>
    public override bool Equals(object? obj) {
        return obj is Rotation2D other && Equals(other);
    }

    /// <summary>
    /// Gets the hash code of the Angle inside this class.
    /// </summary>
    public override int GetHashCode() {
        return rotationAngle.GetHashCode();
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.ToString"]/*'/> "Angle: {rotationAngle}".
    /// </summary>
    /// <returns></returns>
    public override string ToString() {
        return $"Angle: {rotationAngle}";
    }
    #endregion
    #region Private methods
    /// <summary>
    /// Calculates rotation angle and coordinates from an angle in degrees.
    /// Ensures the angle is in the valid range [0, 360) and computes unit circle coordinates.
    /// </summary>
    /// <param name="angle">Angle in degrees</param>
    /// <returns>Tuple containing (normalizedAngle, xCoordinate, yCoordinate)</returns>
    private static (double angle, double x, double y) CalculateRotationData(double angle) {
        // Normalize angle to [0, 360)
        double incomingValue = angle;
        while (incomingValue < 0) {
            incomingValue += 360;
        }
        
        double normalizedAngle = Math.Round(incomingValue % 360, 2);

        // Calculate radian and coordinates
        double radian = Math.PI / 180 * normalizedAngle;
        (double Sin, double Cos) = Math.SinCos(radian);
        double x = Math.Round(Cos, 2);
        double y = Math.Round(Sin, 2);

        return (normalizedAngle, x, y);
    }

    /// <summary>
    /// Creates a new Rotation2D from a radian value.
    /// </summary>
    /// <param name="radian">Angle in radians</param>
    /// <returns>A new Rotation2D with the angle in degrees</returns>
    private static Rotation2D FromRadian(double radian) {
        double angle = radian / Math.PI * 180;
        return new Rotation2D(angle);
    }
    #endregion
}
