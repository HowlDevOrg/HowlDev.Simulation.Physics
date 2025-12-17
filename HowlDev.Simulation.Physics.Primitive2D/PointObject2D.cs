
namespace HowlDev.Simulation.Physics.Primitve2D;

// <summary>
// <c>PointObject</c> implements the <c>IPointObject2D</c> interface. It holds a center point and a list of 
// <c>Point2D</c> objects. Here, retrieving points is very straightforward, but calculating rotations 
// requires turning all the points into vectors, rotating them, then back into objects. Moving is 
// also more costly, as it has to work on each point individually. 
// </summary>
//public class PointObject2D : IPointObject2D, IComparable<PointObject2D>, IEquatable<PointObject2D> {

//}