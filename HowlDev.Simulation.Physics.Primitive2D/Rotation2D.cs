namespace HowlDev.Simulation.Physics.Primitive2D;

/// <summary>
/// Class <c>Rotation</c> stores a single RotationAngle as a double (rounded to 2 decimal places) 
/// and provides a number of helpful methods and properties to work with 2D angles. It 
/// recalculates unit coordinates every time the value is updated for faster and less costly 
/// retreival. It implements the IEquatable and IComparable interfaces.
/// </summary>
public class Rotation2D : IEquatable<Rotation2D>, IComparable<Rotation2D> {
    private double rotationAngle;
    private double xCoord;
    private double yCoord;

    #region Properties
    /// <value>
    /// On set, ensures the valid range is between 0 and 359.
    /// </value>
    public double RotationAngle {
        get { return rotationAngle; }
        set {
            double incomingValue = value;
            while (incomingValue < 0) {
                incomingValue += 360;
            }
            rotationAngle = Math.Round(incomingValue % 360, 2);
            AssignCoords();
        }
    }

    // Used for some inner math. Result of legacy code. 
    private double RotationRadian {
        get {
            return Math.PI / 180 * rotationAngle;
        }
        set {
            RotationAngle = value / Math.PI * 180;
        }
    }

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
        rotationAngle = 0;
    }

    /// <summary>
    /// Constructor assigns angle to the input angle.
    /// </summary>
    public Rotation2D(double angle) {
        RotationAngle = angle; // Purposefully calls Property logic
    }

    /// <summary>
    /// Constructor takes in a <c>Rotation</c> object and duplicates it.
    /// </summary>
    public Rotation2D(Rotation2D value) {
        RotationAngle = value.RotationAngle;
    }
    #endregion
    #region Methods
    /// <summary>
    /// Given an input angle, it adds or removes by that value and ensures 
    /// the bounds of 0 - 359.
    /// </summary>
    /// <param name="angleDifference">Positive or negative int to adjust the current angle by</param>
    public void AdjustBy(double angleDifference) {
        RotationAngle += angleDifference;
    }

    /// <summary>
    /// Takes in a pair of coordinates from origin (so convert them to that beforehand or use the other method) 
    /// and assigns the class's rotation to that value. 
    /// </summary>
    public void AssignToCoordinates(double x, double y) {
        RotationRadian = Math.Atan2(y, x);
    }

    /// <summary>
    /// Assigns the class's rotation value to the angle from the first point to the second point.
    /// See also: <seealso cref="AssignToCoordinates(double, double)"/>
    /// </summary>
    /// <param name="x1">Current X position</param>
    /// <param name="y1">Current Y position</param>
    /// <param name="x2">Target X position</param>
    /// <param name="y2">Target Y position</param>
    public void AssignToCoordinates(double x1, double y1, double x2, double y2) {
        AssignToCoordinates(x2 - x1, y2 - y1);
    }

    /// <summary>
    /// Takes in a pair of tuples and assigns their coordinates to those points, using an overload
    /// of this method. 
    /// See also: <seealso cref="AssignToCoordinates(double, double)"/>
    /// </summary>
    public void AssignToCoordinates((double, double) point1, (double, double) point2) {
        AssignToCoordinates(point1.Item1, point1.Item2, point2.Item1, point2.Item2);
    }

    /// <summary>
    /// Point passthrough for the rotation from the origin.
    /// </summary>
    public void AssignToCoordinates(Point2D p) {
        AssignToCoordinates(p.X, p.Y);
    }

    /// <summary>
    /// Two-Point passthrough for the rotation from the first point. 
    /// See <see cref="AssignToCoordinates(double, double, double, double)"/>.
    /// </summary>
    /// <param name="p1">Current Point</param>
    /// <param name="p2">Target Point</param>
    public void AssignToCoordinates(Point2D p1, Point2D p2) {
        AssignToCoordinates(p1.X, p1.Y, p2.X, p2.Y);
    }

    /// <summary>
    /// Line passthrough for assign to coordinates. 
    /// </summary>
    public void AssignToCoordinates(Line2D l1) {
        AssignToCoordinates(l1[0].X, l1[0].Y, l1[1].X, l1[1].Y);
    }

    /// <summary>
    /// Flips the angle on the X axis.
    /// </summary>
    public void FlipX() {
        if (rotationAngle < 90) {
            RotationAngle = 360 - rotationAngle;
        } else if (rotationAngle < 180) {
            RotationAngle = 180 + (180 - rotationAngle);
        } else if (rotationAngle < 270) {
            RotationAngle = 180 - (rotationAngle - 180);
        } else {
            RotationAngle = 360 - rotationAngle;
        }
    }

    /// <summary>
    /// Flips the angle on the Y axis.
    /// </summary>
    public void FlipY() {
        if (rotationAngle < 90) {
            RotationAngle = 90 + (90 - rotationAngle);
        } else if (rotationAngle < 180) {
            RotationAngle = 180 - rotationAngle;
        } else if (rotationAngle < 270) {
            RotationAngle = 270 + (270 - rotationAngle);
        } else {
            RotationAngle = 270 - (rotationAngle - 270);
        }
    }

    /// <summary>
    /// Flips the angle on both axes.
    /// </summary>
    public void DoubleFlip() {
        RotationAngle -= 180;
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
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void AverageTo(double angle, double percent) {
        if (percent < 0 || percent > 1) { throw new ArgumentOutOfRangeException(nameof(percent), "Percent must be between 0 and 1."); }

        double diff = angle - rotationAngle;
        double distance = DistanceTo(angle);

        if (diff > 180 || diff < 0) { distance *= -1; }

        distance *= percent;
        AdjustBy(distance);
    }

    /// <summary>
    /// Averages between this and the "other" parameter by a percentage. 0 maintains the current angle, 
    /// and 1 sets it to the other angle. This just passes through to the other overload.
    /// See also: <see cref="AverageTo(double, double)"/>
    /// </summary>
    /// <param name="other">Rotation to average to</param>
    /// <param name="percent">Range from 0 to 1</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void AverageTo(Rotation2D other, double percent) {
        AverageTo(other.RotationAngle, percent);
    }

    /// <summary>
    /// Given an input angle, move towards that angle with the max amount provided; otherwise, snap 
    /// to that angle. 
    /// </summary>
    /// <param name="angle">Angle to move towards</param>
    /// <param name="maxAmount">Max amount of angle movement (must be positive)</param>
    /// <exception cref="ArgumentOutOfRangeException" />
    public void MoveTo(double angle, double maxAmount) {
        if (maxAmount <= 0) throw new ArgumentOutOfRangeException(nameof(maxAmount), "Max Amount must be a positive value");

        double distance = DistanceTo(angle, false);

        if (Math.Abs(distance) < maxAmount) {
            rotationAngle = angle;
        } else {
            if (distance > 0) {
                RotationAngle += maxAmount;
            } else {
                RotationAngle -= maxAmount;
            }
        }
    }

    /// <summary>
    /// <c>Rotation</c> passthrough for standard <see cref="MoveTo(double, double)"/> method.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException" />
    public void MoveTo(Rotation2D angle, double maxAmount) {
        MoveTo(angle.RotationAngle, maxAmount);
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
        Rotation2D r = new Rotation2D(obj);
        r.FlipX();
        return r;
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
    public bool Equals(Rotation2D? other) {
        return rotationAngle == other?.rotationAngle;
    }

    /// <summary>
    /// IComparable interface implementation. 
    /// </summary>
    /// <returns><include file="_SharedXML.xml" path='doc/member[@name="Phrases.Compare.Return"]/*'/></returns>
    public int CompareTo(Rotation2D? other) {
        if (other is null) return 1;
        return RotationAngle.CompareTo(other.RotationAngle);
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.Equals"]/*'/> <see cref="Equals(Rotation2D?)"/>.
    /// </summary>
    public override bool Equals(object? obj) {
        return base.Equals(obj);
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
    /// Used to define the coordinates each time the rotation is adjusted. 
    /// </summary>
    private void AssignCoords() {
        (double Sin, double Cos) = GetCoordinates(RotationRadian);
        xCoord = Math.Round(Cos, 2);
        yCoord = Math.Round(Sin, 2);
    }

    private static (double Sin, double Cos) GetCoordinates(double radian) {
        // I can dictionary this later
        return Math.SinCos(radian);
    }
    #endregion
}