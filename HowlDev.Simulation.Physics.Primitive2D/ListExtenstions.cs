using HowlDev.Simulation.Physics.Primitve2D.Interfaces;

namespace HowlDev.Simulation.Physics.Primitve2D;

/// <summary>
/// Extends Lists with Distance filters. 
/// </summary>
public static class ListExtensions {
    public static List<IPointObject2D> FilterByDistance(this List<IPointObject2D> objects, Point2D checkPoint, double additionalDistance = 0) {
        List<IPointObject2D> returnObjects = new List<IPointObject2D>(
            objects.Where((x) => checkPoint.GetDistance(x.CenterPoint) < x.MaxDistance + additionalDistance)
        );
        return returnObjects;
    }

    public static List<VectorObject2D> FilterByDistance(this List<VectorObject2D> objects, Point2D checkPoint, double additionalDistance = 0) {
        List<VectorObject2D> returnObjects = new List<VectorObject2D>(
            objects.Where((x) => checkPoint.GetDistance(x.CenterPoint) < x.MaxDistance + additionalDistance)
        );
        return returnObjects;
    }
}