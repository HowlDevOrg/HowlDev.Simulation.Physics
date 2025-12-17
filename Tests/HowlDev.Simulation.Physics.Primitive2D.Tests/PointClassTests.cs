namespace HowlDev.Simulation.Physics.Primitive2D.Tests;
public class PointClassBaseTests {
    [Test]
    public async Task TestPointClassPair() {
        Point2D p = new Point2D();
        p.Pair = (1.2, 3.4);
        await Assert.That(p.X).IsEqualTo(1.2);
        await Assert.That(p.Y).IsEqualTo(3.4);
    }

    [Test]
    public async Task PointClassConstructors() {
        Point2D p1 = new Point2D(1.2, 3.4);
        await Assert.That(p1.Pair).IsEqualTo((1.2, 3.4));

        Point2D p2 = new Point2D((3.4, 5.6));
        await Assert.That(p2.Pair).IsEqualTo((3.4, 5.6));

        Point2D p3 = new Point2D(p1);
        await Assert.That(p3).IsEqualTo(p1);
    }

    [Test]
    [Arguments(0, 0, 0, 0, 0)]
    [Arguments(0, 0, 5, 0, -1)]
    [Arguments(0, 0, 5, -5, -1)]
    [Arguments(0, 0, -5, 5, 1)]
    [Arguments(0, 0, 0, -5, 1)]
    public async Task PointClassComparison(
        double x1, double y1, double x2, double y2, int comparisonValue) {
        Point2D p1 = new Point2D(x1, y1);
        Point2D p2 = new Point2D(x2, y2);

        await Assert.That(p1.CompareTo(p2)).IsEqualTo(comparisonValue);
    }
}
public class PointClassMidpointTests {
    [Test]
    [Arguments(0, 0, 0, 0, 0, 0)]
    [Arguments(0, 0, 10, 10, 5, 5)]
    [Arguments(0, 0, 10, -5, 5, -2.5)]
    [Arguments(10, 10, 0, 0, 5, 5)]
    [Arguments(10, 10, 10, -5, 10, 2.5)]
    public async Task PointClassMidpoint(
        double x1, double y1, double x2, double y2, double xOut, double yOut) {
        Point2D p1 = new Point2D(x1, y1);
        Point2D p2 = new Point2D(x2, y2);

        Point2D answer = p1.GetMidpoint(p2);
        await Assert.That(answer.X).IsEqualTo(xOut);
        await Assert.That(answer.Y).IsEqualTo(yOut);
    }

    [Test]
    [Arguments(0, 0, 0, 0, 0, 0)]
    [Arguments(0, 0, 10, 10, 5, 5)]
    [Arguments(0, 0, 10, -5, 5, -2.5)]
    [Arguments(10, 10, 0, 0, 5, 5)]
    [Arguments(10, 10, 10, -5, 10, 2.5)]
    public async Task PointDoubleMidpoint(
    double x1, double y1, double x2, double y2, double xOut, double yOut) {
        Point2D p1 = new Point2D(x1, y1);

        Point2D answer = p1.GetMidpoint(x2, y2);
        await Assert.That(answer.X).IsEqualTo(xOut);
        await Assert.That(answer.Y).IsEqualTo(yOut);
    }
}
public class PointClassDistanceTests {
    [Test]
    [Arguments(0, 0, 0, 0, 0)]
    [Arguments(0, 0, 10, 0, 10)]
    [Arguments(0, 0, 0, 10, 10)]
    [Arguments(0, 0, 3, 4, 5)]
    [Arguments(0, 0, -3, -4, 5)]
    [Arguments(0, 0, -3, 4, 5)]
    [Arguments(0, 0, 3, -4, 5)]
    public async Task PointClassDistance(
        double x1, double y1, double x2, double y2, double expDistance) {
        Point2D p1 = new Point2D(x1, y1);
        Point2D p2 = new Point2D(x2, y2);

        await Assert.That(p1.GetDistance(p2)).IsEqualTo(expDistance);
    }

    [Test]
    [Arguments(0, 0, 0, 0, 0)]
    [Arguments(0, 0, 10, 0, 10)]
    [Arguments(0, 0, 0, 10, 10)]
    [Arguments(0, 0, 3, 4, 5)]
    [Arguments(0, 0, -3, -4, 5)]
    [Arguments(0, 0, -3, 4, 5)]
    [Arguments(0, 0, 3, -4, 5)]
    public async Task PointDoubleDistance(
    double x1, double y1, double x2, double y2, double expDistance) {
        Point2D p1 = new Point2D(x1, y1);

        await Assert.That(p1.GetDistance(x2, y2)).IsEqualTo(expDistance);
    }
}
public class PointClassAssignPointTests {
    [Test]
    [MethodDataSource(nameof(GetValues))]
    public async Task AssignPointTests(
        double x1, double x2, int angle, double scalar, double pointX, double pointY) {
        Point2D p = new Point2D();
        p.AssignPoint(x1, x2, new Rotation2D(angle), scalar);
        using var _ = Assert.Multiple();

        await Assert.That(Math.Abs(p.X - pointX)).IsLessThanOrEqualTo(0.01);
        await Assert.That(Math.Abs(p.Y - pointY)).IsLessThanOrEqualTo(0.01);
    }

    [Test]
    [MethodDataSource(nameof(GetValues))]
    public async Task AssignPointAsPointTests(
    double x1, double x2, int angle, double scalar, double pointX, double pointY) {
        Point2D p = new Point2D();
        Point2D innerPoint = new Point2D(x1, x2);
        p.AssignPoint(innerPoint, new Rotation2D(angle), scalar);
        await Assert.That(Math.Abs(p.X - pointX)).IsLessThanOrEqualTo(0.01);
        await Assert.That(Math.Abs(p.Y - pointY)).IsLessThanOrEqualTo(0.01);
    }

    public static IEnumerable<(double, double, int, double, double, double)> GetValues() {
        yield return (-0.5, -2.6, 1, 2.43, 1.93, -2.55);
        yield return (-0.9, 4.3, 219, 3.40, -3.55, 2.16);
        yield return (1.2, 1.9, 148, 1.47, -0.05, 2.67);
        yield return (-2.4, -0.7, 290, 0.66, -2.18, -1.32);
        yield return (-2.6, 0.7, 204, 3.66, -5.93, -0.8);
        yield return (3.9, 3.3, 232, 0.13, 3.82, 3.2);
        yield return (4.1, -5.0, 45, 2.65, 5.98, -3.12);
        yield return (-4.5, 2.4, 359, 1.59, -2.91, 2.37);
    }
}
public class PointClassOperatorTests {
    [Test]
    public async Task PointCanInvert() {
        Point2D p = new Point2D(3, 5);
        p = -p;
        await Assert.That(p.X).IsEqualTo(-3);
        await Assert.That(p.Y).IsEqualTo(-5);
    }

    [Test]
    public async Task PointCanBeAdded() {
        Point2D p1 = new Point2D(3, 5);
        Point2D p2 = new Point2D(5, 8);
        Point2D answer = p1 + p2;

        await Assert.That(answer.X).IsEqualTo(8);
        await Assert.That(answer.Y).IsEqualTo(13);
    }

    [Test]
    public async Task PointCanBeSubtracted() {
        Point2D p1 = new Point2D(3, 5);
        Point2D p2 = new Point2D(5, 8);
        Point2D answer = p1 - p2;

        await Assert.That(answer.X).IsEqualTo(-2);
        await Assert.That(answer.Y).IsEqualTo(-3);
    }

    [Test]
    public async Task PointCanBeModulod() {
        Point2D p1 = new Point2D(3, 5);
        Point2D p2 = new Point2D(5, 8);
        Point2D answer = p1 % p2;

        await Assert.That(answer.X).IsEqualTo(2);
        await Assert.That(answer.Y).IsEqualTo(3);
    }

    [Test]
    [Arguments(10, 0, 0)]
    [Arguments(10, 10, 45)]
    [Arguments(0, 10, 90)]
    [Arguments(-10, 10, 135)]
    [Arguments(-10, 0, 180)]
    [Arguments(-10, -10, 225)]
    [Arguments(0, -10, 270)]
    [Arguments(10, -10, 315)]
    public async Task AngleCanBeGotten(
        double x1, double y1, int expAngle) {
        Point2D p1 = new Point2D(0, 0);
        Point2D p2 = new Point2D(x1, y1);
        Rotation2D answer = p1 ^ p2;

        await Assert.That(answer.RotationAngle).IsEqualTo(expAngle);
    }

    [Test]
    public async Task PointsCanCheckEquality() {
        Point2D p1 = new Point2D(2, 3);
        Point2D p2 = new Point2D(3, 5);

        await Assert.That(p1 == p2).IsFalse();
        await Assert.That(p1 != p2).IsTrue();

        p2 = new Point2D(p1);
        await Assert.That(p1 == p2).IsTrue();
    }

    [Test]
    [Arguments(10, 0, 0, 0, 0)]
    [Arguments(10, 0, 1, 10, 0)]
    [Arguments(10, 10, 0.25, 2.5, 2.5)]
    [Arguments(10, 10, 0.5, 5, 5)]
    public async Task PointCanScale(
    double x1, double y1, double scalar, double xOut, double yOut) {
        Point2D p1 = new Point2D(x1, y1);
        Point2D answer = p1 * scalar;

        await Assert.That(answer.X).IsEqualTo(xOut);
        await Assert.That(answer.Y).IsEqualTo(yOut);
    }
}