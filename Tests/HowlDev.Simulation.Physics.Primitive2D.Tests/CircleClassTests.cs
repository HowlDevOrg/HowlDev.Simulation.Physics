namespace HowlDev.Simulation.Physics.Primitive2D.Tests;

public class CircleClassBaseTests {
    [Test]
    public async Task CircleCanBeCreatedAndRead() {
        Circle2D p = new Circle2D();
        await Assert.That(p.Center.X).IsEqualTo(0);
        await Assert.That(p.Center.Y).IsEqualTo(0);
        await Assert.That(p.Radius).IsEqualTo(1);
    }

    [Test]
    public async Task CircleCanBeInitializedAndRead1() {
        Circle2D p = new Circle2D(1, 2, 3);
        await Assert.That(p.Center.X).IsEqualTo(1);
        await Assert.That(p.Center.Y).IsEqualTo(2);
        await Assert.That(p.Radius).IsEqualTo(3);
    }

    [Test]
    public async Task CircleCanBeInitializedAndRead2() {
        Circle2D p = new Circle2D(new Point2D(1, 2), 3);
        await Assert.That(p.Center.X).IsEqualTo(1);
        await Assert.That(p.Center.Y).IsEqualTo(2);
        await Assert.That(p.Radius).IsEqualTo(3);
    }
}
public class CircleClassOverlappingTests {
    [Test]
    [Arguments(0, 0, 1, 0, 0, 1, true)]
    [Arguments(0, 0, 1, 1, 1, 1, true)]
    [Arguments(0, 0, 1, 2, 0, 1, true)]
    [Arguments(0, 0, 1, 2, 0, 0.5, false)]
    [Arguments(0, 0, 0.5, 1, 0, 0.5, true)]
    [Arguments(0, 0, 0.5, 1, 0, 1, true)]
    [Arguments(0, 0, 20, 0, 50, 20, false)]
    [Arguments(0, 0, 20, 0, 50, 30, true)]
    public async Task CircleCanBeCreatedAndRead(
        double x1, double y1, double r1, double x2, double y2, double r2, bool shouldOverlap
    ) {
        Circle2D p1 = new Circle2D(x1, y1, r1);
        Circle2D p2 = new Circle2D(x2, y2, r2);
        await Assert.That(p1.IsOverlapping(p2)).IsEqualTo(shouldOverlap);
    }
}
public class CircleClassPointWithinTests {
    [Test]
    [Arguments(0, 0, 1, 0, 0, true)]
    [Arguments(0, 0, 1, 1, 0, true)]
    [Arguments(0, 0, 1, 2, 0, false)]
    [Arguments(0, 0, 0.5, 1, 0, false)]
    [Arguments(0, 0, 0.5, 0.5, 0, true)]
    [Arguments(0, 0, 20, 0, 50, false)]
    [Arguments(0, 0, 20, 0, 10, true)]
    public async Task CircleCanBeCreatedAndRead(
        double x1, double y1, double r1, double x2, double y2, bool isWithin
    ) {
        Circle2D p1 = new Circle2D(x1, y1, r1);
        Point2D p2 = new Point2D(x2, y2);
        await Assert.That(p1.Contains(p2)).IsEqualTo(isWithin);
    }
}
