using HowlDev.Simulation.Physics.Primitve2D;
using HowlDev.Simulation.Physics.Primitve2D.Interfaces;

namespace HowlDev.Simulation.Physics.Primitve2D.Tests;

public class ListFilteringTests {
    [Test] // need to validate
    public async Task FilteringWorksAsIntended() {
        List<IPointObject2D> vectorObjects = new List<IPointObject2D>() {
            new VectorObject2D(new Point2D(3, 4), new List<Vector2D>{ new Vector2D(0, 13) }),
            new VectorObject2D(new Point2D(0.50, 2.23), new List<Vector2D>{ new Vector2D(0, 1.50) }),
            new VectorObject2D(new Point2D(-0.66, -4.85), new List<Vector2D>{ new Vector2D(0, 4.77) }),
            new VectorObject2D(new Point2D(3.74, -0.15), new List<Vector2D>{ new Vector2D(0, 1.03) }),
            new VectorObject2D(new Point2D(4.40, 2.40), new List<Vector2D>{ new Vector2D(0, 1.74) }),
            new VectorObject2D(new Point2D(-2.81, 3.91), new List<Vector2D>{ new Vector2D(0, 2.42) }),
            new VectorObject2D(new Point2D(-0.12, -2.51), new List<Vector2D>{ new Vector2D(0, 9.35) }),
            new VectorObject2D(new Point2D(-0.37, -2.10), new List<Vector2D>{ new Vector2D(0, 9.00) }),
            new VectorObject2D(new Point2D(-1.79, -4.54), new List<Vector2D>{ new Vector2D(0, 2.29) }),
            new VectorObject2D(new Point2D(-4.59, -4.27), new List<Vector2D>{ new Vector2D(0, 7.75) }),
            new VectorObject2D(new Point2D(-3.88, 3.48), new List<Vector2D>{ new Vector2D(0, 6.18) }),
        };
        vectorObjects = vectorObjects.FilterByDistance(new Point2D());
        await Assert.That(vectorObjects.Count).IsEqualTo(5);
    }
}
