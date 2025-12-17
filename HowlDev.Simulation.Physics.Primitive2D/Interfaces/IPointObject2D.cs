namespace HowlDev.Simulation.Physics.Primitve2D.Interfaces;

/// <summary>
/// This interface is for 2D objects that contain a list of points. The methods here handle
/// rotation, movement, point and line enumeration, and collisions with other objects that implement this interface. <br/>
/// You can use this interface to define your own classes that handle those operations in a custom way; otherwise, 
/// you can use my two classes, <see cref="VectorObject2D"/> and <see cref="PointObject2D"/>. They have different strengths, 
/// so consider which will be more efficient for your application. 
/// </summary>
public interface IPointObject2D : IEnumerable<Point2D>, IEnumerable<Line2D> {
    /// <summary>
    /// Retrieves the center point for the object. Also serves as its rotation point. 
    /// </summary>
    Point2D CenterPoint { get; }

    /// <summary>
    /// Retrieves the max distance from the <see cref="CenterPoint"/> to the outside points. 
    /// </summary>
    double MaxDistance { get; }

    /// <summary>
    /// Rotates the object around the <c>CenterPoint</c> by the given number of degrees.
    /// Strength allows it to rotate even next to walls, by pushing itself away from it. With a 
    /// value of 0, stops in contact with a wall.
    /// </summary>
    /// <param name="walls">A <c>List</c> of other objects to collide with</param>
    /// <param name="angle">Degrees to rotate by</param>
    void RotateBy(List<IPointObject2D> walls, double angle);

    /// <summary>
    /// Moves the entire object given a <c>Vector</c> object.
    /// </summary>
    /// <param name="walls">A <c>List</c> of other objects to collide with</param>
    /// <param name="vector">Vector to move all points by</param>
    void MoveTo(List<IPointObject2D> walls, Vector2D vector);

    /// <summary>
    /// Returns <c>True</c> if the point is within the given object.
    /// </summary>
    bool IsPointWithin(Point2D point);

    /// <summary>
    /// Retrieves an enumerator of the points. 
    /// </summary>
    IEnumerable<Point2D> GetPoints();

    /// <summary>
    /// Returns an enumerator of the lines. 
    /// </summary>
    IEnumerable<Line2D> GetLines();
}