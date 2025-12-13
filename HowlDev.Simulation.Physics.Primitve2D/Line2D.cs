using System.Collections;
using System.Net.Security;
using System.Runtime.CompilerServices;

namespace Physics2DLibrary;

/// <summary>
/// This is a collection of 2 <c>Points</c> in an array (see <see cref="Point2D"/>). It contains methods to 
/// handle line crosses and endpoint equivalence, get the max and min, and many methods the <c>Point</c>s contained 
/// are passed through.
/// </summary>
public class Line2D : IComparable<Line2D>, IEquatable<Line2D>, IEnumerable<Point2D> {
    private Point2D[] points = new Point2D[2];

    /// <summary>
    /// Returns the minimum X value of the two <c>Point</c>s. 
    /// </summary>
    public double MinX { get { return Math.Min(points[0].X, points[1].X); } }

    /// <summary>
    /// Returns the maximum X value of the two <c>Point</c>s. 
    /// </summary>
    public double MaxX { get { return Math.Max(points[0].X, points[1].X); } }

    /// <summary>
    /// Returns the minimum Y value of the two <c>Point</c>s. 
    /// </summary>
    public double MinY { get { return Math.Min(points[0].Y, points[1].Y); } }

    /// <summary>
    /// Returns the maximum Y value of the two <c>Point</c>s. 
    /// </summary>
    public double MaxY { get { return Math.Max(points[0].Y, points[1].Y); } }

    /// <summary>
    /// Gets the length of the line.
    /// </summary>
    public double Length { get { return points[0].GetDistance(points[1]); } }

    /// <summary>
    /// Gets the midpoint of the line. 
    /// </summary>
    public Point2D Midpoint { get { return points[0].GetMidpoint(points[1]); } }

    /// <summary>
    /// Gets the angle from the first point to the second point.
    /// </summary>
    public Rotation2D Angle { get { return points[0] ^ points[1]; } }

    /// <summary>
    /// An array of length 2 holding both points. 
    /// </summary>
    public Point2D[] Points { get { return points; } }

    /// <summary>
    /// Default constructor. Creates two points at the origin. 
    /// </summary>
    public Line2D() {
        points[0] = new Point2D();
        points[1] = new Point2D();
    }

    /// <summary>
    /// Takes in two coordinate pairs and assigns them in order.
    /// </summary>
    public Line2D(double x1, double y1, double x2, double y2) {
        points[0] = new Point2D(x1, y1);
        points[1] = new Point2D(x2, y2);
    }

    /// <summary>
    /// Takes in an array of length 4 and assigns it in the same order as the coordinate 
    /// pair constructor <see cref="Line2D.Line2D(double, double, double, double)"/>.
    /// </summary>
    /// <param name="incomingPoints"><c>double[]</c> of length 4</param>
    /// <exception cref="ArgumentException"></exception>
    public Line2D(double[] incomingPoints) {
        if (incomingPoints.Length != 4) throw new ArgumentException("Array length must be equal to 4.");

        points[0] = new Point2D(incomingPoints[0], incomingPoints[1]);
        points[1] = new Point2D(incomingPoints[2], incomingPoints[3]);
    }

    /// <summary>
    /// Takes in 2 <c>Point</c>s and assigns them to the array. Copies reference directly.
    /// </summary>
    public Line2D(Point2D p1, Point2D p2) { 
        points[0] = p1;
        points[1] = p2;
    }

    /// <summary>
    /// Duplicates a Line. Points inside are duplicated as well.
    /// </summary>
    public Line2D(Line2D line) {
        points[0] = new Point2D(line.Points[0]);
        points[1] = new Point2D(line.Points[1]);
    }

    /// <summary>
    /// Index into the inner array directly.
    /// </summary>
    public Point2D this[int i] => points[i];

    /// <summary>
    /// Updates the <c>Point</c> at the given index with a new reference. 
    /// </summary>
    /// <param name="index">Index of the array (either 0 or 1)</param>
    /// <param name="x">X-coordinate of the new <c>Point</c></param>
    /// <param name="y">Y-coordinate of the new <c>Point</c></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void UpdatePoint(int index, double x, double y) {
        if (index < 0 || index >= points.Length) throw new ArgumentOutOfRangeException(nameof(index), "Index must be between 0 and 1.");
        points[index] = new Point2D(x, y);
    }

    /// <summary>
    /// Updates the <c>Point</c> at the given index with a new reference. 
    /// </summary>
    /// <param name="index">Index of the array (either 0 or 1)</param>
    /// <param name="p"><c>Point</c> to be duplicated and updated into the array</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void UpdatePoint(int index, Point2D p) {
        UpdatePoint(index, p.X, p.Y);
    }

    /// <summary>
    /// Returns <c>True</c> if this line intersects with the other line. This also 
    /// returns <c>True</c> if they share endpoints.
    /// </summary>
    /// <param name="l1"><c>Line</c> to compare against.</param>
    public bool IsIntersecting(Line2D l1) {
        if (ContainsEndpoint(l1)) return true;
        // Provided by ChatGPT
        double dx1 = points[1].X - points[0].X;
        double dy1 = points[1].Y - points[0].Y;
        double dx2 = l1[1].X - l1[0].X;
        double dy2 = l1[1].Y - l1[0].Y;

        double denominator = dx1 * dy2 - dy1 * dx2;
        if (denominator == 0) {
            return false;
        }

        double dx3 = l1[0].X - points[0].X;
        double dy3 = l1[0].Y - points[0].Y;

        double t = (dx3 * dy2 - dy3 * dx2) / denominator;
        double u = (dx3 * dy1 - dy3 * dx1) / denominator;

        return 0 <= t && t <= 1 && 0 <= u && u <= 1;
    }

    /// <summary>
    /// Returns <c>True</c> if this line intersects with the given Vector.
    /// </summary>
    public bool IsIntersecting(Point2D startingPoint, Vector2D vector) {
        if (ContainsPoint(startingPoint)) return true;

        Point2D finalPoint = startingPoint + vector;
        return IsIntersecting(new Line2D(startingPoint, finalPoint));
    }

    /// <summary>
    /// Returns <c>True</c> if they share exactly one endpoint in common.
    /// </summary>
    /// <param name="l1"><c>Line</c> to connect to.</param>
    public bool IsConnected(Line2D l1) {
        int count = 0;
        foreach (Point2D p1 in this) {
            foreach (Point2D p2 in l1) {
                if (p1.Equals(p2)) { count++; }
            }
        }

        return count == 1;
    }

    /// <summary>
    /// Returns <c>True</c> if the given point is one of the endpoints. 
    /// </summary>
    /// <param name="p"><c>Point</c> to compare with.</param>
    public bool ContainsEndpoint(Point2D p) {
        return points[0].Equals(p) || points[1].Equals(p);
    }

    /// <summary>
    /// Returns <c>True</c> if they share at least one endpoint in common.
    /// </summary>
    /// <param name="l1"><c>Line</c> to connect to.</param>
    public bool ContainsEndpoint(Line2D l1) {
        foreach (Point2D p1 in this) {
            foreach (Point2D p2 in l1) {
                if (p1.Equals(p2)) return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Uses an <see cref="Equation2D"/> to determine if a point is on the line, with a degree 
    /// of inaccuracy. Uses the <see cref="Equation2D.PointIsOnLine(double, double, double)"/> method.
    /// </summary>
    public bool ContainsPoint(double x, double y, double precision = 0.1) {
        if (x + precision < MinX || y + precision < MinY ||
            x - precision > MaxX || y - precision > MaxY) return false;

        Equation2D e = new Equation2D(this);
        return e.PointIsOnLine(x, y, precision);
    }

    /// <summary>
    /// Uses an <see cref="Equation2D"/> to determine if a point is on the line, with a degree 
    /// of inaccuracy. Uses the <see cref="Equation2D.PointIsOnLine(Point2D, double)"/> method.
    /// </summary>
    public bool ContainsPoint(Point2D point, double precision = 0.1) {
        return ContainsPoint(point.X, point.Y, precision);
    }

    /// <summary>
    /// Returns the minimum distance to an endpoint on this line from a given coordinate.
    /// </summary>
    public double MinimumDistance(double x, double y) {
        // This method will also be taking the inverse of the slope in the equation of the line, 
        // then min that with the two points. 
        throw new NotImplementedException();
        Point2D point2D = new Point2D(x, y);
        return Math.Min(point2D.GetDistance(this[0]),
                        point2D.GetDistance(this[1]));
    }

    /// <summary>
    /// Returns the minimum distance to an endpoint on this line from a given <c>Point</c>.
    /// </summary>
    public double MinimumDistance(Point2D point) {
        return MinimumDistance(point.X, point.Y);
    }

    /// <summary>
    /// Returns a null if <see cref="Equation2D.IntersectionPoint(Equation2D)"/> would return null. Otherwise, 
    /// returns the point at which the two lines intersect. 
    /// </summary>
    /// <returns><c>Point</c> or <c>null</c></returns>
    public static Point2D? IntersectionPoint(Line2D l1, Line2D l2) {
        Equation2D e1 = new Equation2D(l1);
        Equation2D e2 = new Equation2D(l2);
        Point2D? point = e1.IntersectionPoint(e2);

        if (point is null) { return null; }
        if (point.X < l1.MinX || point.X < l2.MinX ||
            point.X > l1.MaxX || point.X > l2.MaxX ||
            point.Y < l1.MinY || point.Y < l2.MinY ||
            point.Y > l1.MaxY || point.Y > l2.MaxY) {
            return null;
        }
        return point;
    }

    /// <summary>
    /// Returns a new <c>Line</c> with the points swapped.
    /// </summary>
    public static Line2D operator -(Line2D l1) {
        return new Line2D(l1[1], l1[0]);
    }

    /// <summary>
    /// Adds the lower <c>Point</c>s 1-to-1 to return a new <c>Line</c>.
    /// </summary>
    public static Line2D operator +(Line2D l1, Line2D l2) {
        return new Line2D(new Point2D(l1[0] + l2[0]), new Point2D(l1[1] + l2[1]));
    }

    /// <summary>
    /// Returns <c>True</c> if all of the points are equal (and in the same position in the array). 
    /// </summary>
    public static bool operator ==(Line2D l1, Line2D l2) {
        return l1[0] == l2[0] && l1[1] == l2[1];
    }

    /// <summary>
    /// Returns <c>True</c> if any of the points are not equal.
    /// </summary>
    public static bool operator !=(Line2D l1, Line2D l2) {
        return l1[0] != l2[0] || l1[1] != l2[1];
    }

    // Custom operators
    /// <summary>
    /// Returns <c>True</c> if the given lines intersect (shorthand). 
    /// </summary>
    public static bool operator ^(Line2D l1, Line2D l2) {
        return l1.IsIntersecting(l2);
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Implementation.Equatable"]/*'/>
    /// </summary>
    public bool Equals(Line2D? other) {
        if (other is null) return false;

        return points[0] == other.Points[0] && points[1] == other.Points[1];
    }

    /// <summary>
    /// Sorts by point 1, then point 2.
    /// </summary>
    /// <returns><include file="_SharedXML.xml" path='doc/member[@name="Phrases.Compare.Return"]/*'/></returns>
    public int CompareTo(Line2D? other) {
        if (other is null) return 0; 

        int value = points[0].CompareTo(other.Points[0]);
        if (value == 0) {
            return points[1].CompareTo(other.Points[1]);
        }
        return value;
    }

    /// <summary>
    /// Returns the pair of points through an enumerator.
    /// </summary>
    /// <returns><c>Point</c> 1 and 2</returns>
    public IEnumerator<Point2D> GetEnumerator() {
        yield return points[0];
        yield return points[1];
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.Equals"]/*'/> <see cref="Equals(Line2D?)"/>.
    /// </summary>
    public override bool Equals(object? obj) {
        return base.Equals(obj);
    }

    /// <summary>
    /// Combines the hash codes of the two <c>Point</c>s in this line. 
    /// </summary>
    public override int GetHashCode() {
        return points[0].GetHashCode() + points[1].GetHashCode();
    }

    /// <summary>
    /// <include file="_SharedXML.xml" path='doc/member[@name="Phrases.Overriden.ToString"]/*'/> "({points[0]}), ({points[1]})". 
    /// </summary>
    public override string ToString() {
        return $"({points[0]}), ({points[1]})";
    }
}