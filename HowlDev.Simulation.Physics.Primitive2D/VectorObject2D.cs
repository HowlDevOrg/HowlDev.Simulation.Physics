using HowlDev.Simulation.Physics.Primitve2D.Interfaces;
using System.Collections;

namespace HowlDev.Simulation.Physics.Primitve2D;

/// <summary>
/// <c>VectorObject</c> implements the <c>IPointObject2D</c> interface. It holds a center point and a list of 
/// <c>Vector</c> objects. It is designed for more constant rotations, as adjusting a Vector's rotation value
/// is straightforward. Moving the entire object is as simple as moving the center point. 
/// It does have to calculate trigonometric functions every time points are retrieved, however. 
/// </summary>
public class VectorObject2D : IPointObject2D, IComparable<VectorObject2D>, IEquatable<VectorObject2D> {
    private Point2D centerPoint;
    private List<Vector2D> vertices;
    private double maxDistance;

    /// <summary>
    /// Center Point of the object (must be within the object) and serves as the rotation point. 
    /// </summary>
    public Point2D CenterPoint { get => centerPoint; }

    /// <summary>
    /// Retrieves the largest distance in the Vector list.
    /// </summary>
    public double MaxDistance { get => maxDistance; }

    /// <summary>
    /// Default constructor. Initializes the center point at (0, 0) and with no vertices. 
    /// </summary>
    public VectorObject2D() {
        centerPoint = new Point2D(0, 0);
        vertices = new List<Vector2D>();
        maxDistance = 0;
    }

    /// <summary>
    /// Takes in a <c>Point</c> and list of <c>Vector</c>s and assigns it to the inner structure. Order matters! 
    /// </summary>
    public VectorObject2D(Point2D centerPoint, List<Vector2D> vertices) {
        this.centerPoint = new Point2D(centerPoint);
        this.vertices = new List<Vector2D>(vertices);
        vertices.Sort();
        vertices.Reverse();
        maxDistance = vertices[0].Velocity;
    }

    /// <summary>
    /// Takes in a <c>Point</c> and list of <c>Point</c>s and assigns it to the inner structure. 
    /// </summary>
    public VectorObject2D(Point2D centerPoint, List<Point2D> points) {
        this.centerPoint = centerPoint;
        vertices = new List<Vector2D>();
        double maximumDistance = 0;
        foreach (Point2D point in points) {
            Vector2D vector = new Vector2D();
            vector.AssignToCoordinates(point.X - centerPoint.X, point.Y - centerPoint.Y);
            vertices.Add(vector);

            double distance = centerPoint.GetDistance(point);
            if (distance > maximumDistance) {
                maximumDistance = distance;
            }
        }
        maxDistance = maximumDistance;
    }

    /// <summary>
    /// Returns the <c>Point</c> of a given internal index.
    /// </summary>
    public Point2D this[int i] => centerPoint + vertices[i];

    /// <summary>
    /// Moves the center point (and all other points) by the specified vector.
    /// </summary>
    /// <param name="walls">Walls to collide with</param>
    /// <param name="vector"><c>Vector</c> to move by</param>
    public void MoveTo(List<IPointObject2D> walls, Vector2D vector) {
        centerPoint += vector;
    }

    /// <summary>
    /// Rotates all the outer points by the specified angle. If a collision occurs, stops at the point of touching, and exerts 
    /// no force afterwards. 
    /// </summary>
    /// <param name="walls">Walls to collide with</param>
    /// <param name="angle">Angle in degrees</param>
    public void RotateBy(List<IPointObject2D> walls, double angle) {
        List<Point2D> currentPosition = new List<Point2D>(this);
        foreach (Vector2D v in vertices) {
            v.Rotation.AdjustBy(angle);
        }
        List<Point2D> afterPosition = new List<Point2D>(this);

        List<Vector2D> vectorLines = new List<Vector2D>();
        for (int i = 0; i < currentPosition.Count; i++) {
            Vector2D v = new Vector2D();
            v.AssignToCoordinates(currentPosition[i], afterPosition[i]);
            vectorLines.Add(v);
        }
        HandleRotations(currentPosition, vectorLines, walls);
    }

    /// <summary>
    /// Returns <c>True</c> if the point is within this object. 
    /// </summary>
    public bool IsPointWithin(Point2D point) {
        Vector2D vector = new Vector2D(Math.Sqrt(2), 2 * MaxDistance);

        int count = 0;
        foreach (Line2D line in GetLines()) {
            if (line.IsIntersecting(point, vector)) {
                count++;
            }
        }

        return count % 2 != 0;
    }

    /// <summary>
    /// Returns <c>true</c> if the center point is the same and all points are in the same position and equal in the 
    /// list.
    /// </summary>
    public bool Equals(VectorObject2D? other) {
        if (other is null) return false;
        if (centerPoint != other.centerPoint) return false;

        List<Point2D> v1 = new List<Point2D>(GetPoints());
        List<Point2D> v2 = new List<Point2D>(other.GetPoints());

        for (int i = 0; i < v1.Count; i++) {
            if (v1[i] != v2[i]) return false;
        }
        return true;
    }

    /// <summary>
    /// Sorts by center point, then by sequential points. 
    /// </summary>
    public int CompareTo(VectorObject2D? other) {
        if (other is null) return 0;

        int pointCheck = centerPoint.CompareTo(other.centerPoint);
        if (pointCheck != 0) return pointCheck;

        foreach (Point2D p1 in GetPoints()) {
            foreach (Point2D p2 in other.GetPoints()) {
                int check = p1.CompareTo(p2);
                if (check != 0) return check;
            }
        }
        return 0;
    }

    /// <summary>
    /// Returns an Enumerable of this in <c>Point</c> format. 
    /// </summary>
    public IEnumerable<Point2D> GetPoints() {
        return this;
    }

    /// <summary>
    /// Returns an Enumerable of this in <c>Line</c> format. 
    /// </summary>
    public IEnumerable<Line2D> GetLines() {
        return this;
    }

    /// <summary>
    /// Please specify either <see cref="GetPoints"/> or <see cref="GetLines"/>.
    /// </summary>
    /// <exception cref="EntryPointNotFoundException"></exception>
    public IEnumerator GetEnumerator() {
        throw new EntryPointNotFoundException("Please use GetPoints() or GetLines() methods.");
    }

    IEnumerator<Point2D> IEnumerable<Point2D>.GetEnumerator() {
        return PointEnumerator();
    }

    IEnumerator<Line2D> IEnumerable<Line2D>.GetEnumerator() {
        return LineEnumerator();
    }

    private IEnumerator<Point2D> PointEnumerator() {
        foreach (Vector2D v in vertices) {
            yield return centerPoint + v;
        }
    }

    private IEnumerator<Line2D> LineEnumerator() {
        List<Point2D> points = new List<Point2D>(this); // Now THIS is awesome shorthand.

        for (int i = 0; i < points.Count - 1; i++) {
            yield return new Line2D(points[i], points[(i + 1) % points.Count]);
        }
    }

    private void HandleRotations(List<Point2D> points, List<Vector2D> vectors, List<IPointObject2D> walls) {
        double maxRotation = 1.0; // How much rotation I can do.
        walls = walls.FilterByDistance(CenterPoint, MaxDistance);

        foreach (IPointObject2D obj in walls) {
            foreach (Line2D l in obj.GetLines()) {
                for (int i = 0; i < points.Count; i++) {
                    if (l.IsIntersecting(points[i], vectors[i])) {
                        Line2D l2 = new Line2D(points[i], points[i] + vectors[i]);
                        Point2D intersectionPoint = Line2D.IntersectionPoint(l, l2)!;

                        Vector2D v = new Vector2D();
                        v.AssignToCoordinates(points[i], intersectionPoint);
                        maxRotation = Math.Min(maxRotation, v.Velocity / vectors[i].Velocity);
                    }
                }
            }
        }

        // Multiply all vectors by maxRotation and perform the actions
        if (maxRotation == 1.0) return; // Action was already taken
        for (int i = 0; i < points.Count; i++) {
            Vector2D v = new Vector2D();
            vectors[i].UpdateVelocity(vectors[i].Velocity * maxRotation);
            v.AssignToCoordinates(centerPoint, points[i] + vectors[i]);
            vertices[i] = v;
        }
    }
}