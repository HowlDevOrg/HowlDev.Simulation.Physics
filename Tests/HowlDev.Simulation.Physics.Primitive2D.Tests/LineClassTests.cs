using HowlDev.Simulation.Physics.Primitve2D;
using System.Net.WebSockets;
namespace HowlDev.Simulation.Physics.Primitve2D.Tests;


public class LineClassBasicTests {
    [Test]
    public async Task LineClassConstructorsTest() {
        Line2D l1 = new Line2D();
        Line2D l2 = new Line2D([1, 2, 3, 4]);
        Line2D l3 = new Line2D(l2);

        l3.UpdatePoint(0, 0, 2);
        l3.UpdatePoint(1, new Point2D(5, 6));
        await Assert.That(l2[0].X).IsEqualTo(1);
        await Assert.That(l3.Points[0]).IsEqualTo(new Point2D(0, 2));
        await Assert.That(l3.Points[1]).IsEqualTo(new Point2D(5, 6));
    }

    [Test]
    public async Task RemainingPropertyTests() {
        Line2D l1 = new Line2D(new Point2D(0, 0), new Point2D(3, 4));
        await Assert.That(l1.Length).IsEqualTo(5);
        await Assert.That(l1.Midpoint.X).IsEqualTo(1.5);
        await Assert.That(l1.Midpoint.Y).IsEqualTo(2);
        await Assert.That(l1.Angle.RotationAngle).IsEqualTo(53.13);
    }
}
public class LineClassMinMaxTests {
    [Test]
    [Arguments(1, 2, 3, 4, 1, 2, 3, 4)]
    [Arguments(4.0, 3.9, 3.1, 2.5, 3.1, 2.5, 4.0, 3.9)]
    [Arguments(3.5, -4.7, -3.7, 5.4, -3.7, -4.7, 3.5, 5.4)]
    [Arguments(-0.2, 5.1, -3.8, 5.5, -3.8, 5.1, -0.2, 5.5)]
    [Arguments(2.7, -2.6, -1.4, 1.0, -1.4, -2.6, 2.7, 1.0)]
    [Arguments(0.2, -4.7, -0.4, 5.6, -0.4, -4.7, 0.2, 5.6)]
    [Arguments(5.5, -3.5, 1.7, -3.7, 1.7, -3.7, 5.5, -3.5)]
    [Arguments(1.0, 4.1, 4.9, 1.2, 1.0, 1.2, 4.9, 4.1)]
    [Arguments(-0.7, -2.5, 1.9, 5.5, -0.7, -2.5, 1.9, 5.5)]
    [Arguments(-4.2, -3.9, -0.2, -4.9, -4.2, -4.9, -0.2, -3.9)]
    [Arguments(3.6, -4.7, -1.2, -3.7, -1.2, -4.7, 3.6, -3.7)]
    [Arguments(2.7, -3.3, 2.5, 4.5, 2.5, -3.3, 2.7, 4.5)]
    [Arguments(-2.4, -2.6, -3.3, 5.9, -3.3, -2.6, -2.4, 5.9)]
    [Arguments(2.4, 5.1, -2.4, 2.5, -2.4, 2.5, 2.4, 5.1)]
    [Arguments(5.1, 4.1, -4.4, 0.5, -4.4, 0.5, 5.1, 4.1)]
    [Arguments(1.2, 4.3, 1.0, -1.1, 1.0, -1.1, 1.2, 4.3)]
    [Arguments(2.4, -0.5, 3.3, 5.1, 2.4, -0.5, 3.3, 5.1)]
    [Arguments(0.7, 5.3, -0.7, 0.2, -0.7, 0.2, 0.7, 5.3)]
    [Arguments(4.9, 1.7, -3.0, -4.2, -3.0, -4.2, 4.9, 1.7)]
    [Arguments(-0.2, 1.7, 1.7, 2.7, -0.2, 1.7, 1.7, 2.7)]
    [Arguments(-2.8, 2.4, -3.3, -0.7, -3.3, -0.7, -2.8, 2.4)]
    public async Task AllMinMaxTests(
        double x1, double y1, double x2, double y2, double minX, double minY, double maxX, double maxY) {
        Line2D line = new Line2D(x1, y1, x2, y2);
        await Assert.That(line.MinX).IsEqualTo(minX);
        await Assert.That(line.MinY).IsEqualTo(minY);
        await Assert.That(line.MaxX).IsEqualTo(maxX);
        await Assert.That(line.MaxY).IsEqualTo(maxY);
    }
}
public class LineClassConnectionTests {
    [Test]
    [Arguments(1, 2, 3, 4, 2, 3, 4, 5, false)]
    [Arguments(1, 2, 3, 4, 1, 2, 3, 4, false)]
    [Arguments(1, 2, 3, 4, 0, 5, 3, 4, true)]
    public async Task ConnectedLineTests(
        double l1x1, double l1y1, double l1x2, double l1y2,
        double l2x1, double l2y1, double l2x2, double l2y2, bool isConnected) {
        Line2D l1 = new Line2D(l1x1, l1y1, l1x2, l1y2);
        Line2D l2 = new Line2D(l2x1, l2y1, l2x2, l2y2);
        await Assert.That(l1.IsConnected(l2)).IsEqualTo(isConnected);
    }

    [Test]
    [Arguments(1, 2, 3, 4, 2, 3, 4, 5, false)]
    [Arguments(1, 2, 3, 4, 1, 2, 3, 4, true)]
    [Arguments(1, 2, 3, 4, 0, 5, 3, 4, true)]
    public async Task ContainsEndpointTests(
    double l1x1, double l1y1, double l1x2, double l1y2,
    double l2x1, double l2y1, double l2x2, double l2y2, bool isConnected) {
        Line2D l1 = new Line2D(l1x1, l1y1, l1x2, l1y2);
        Line2D l2 = new Line2D(l2x1, l2y1, l2x2, l2y2);
        await Assert.That(l1.ContainsEndpoint(l2)).IsEqualTo(isConnected);
        await Assert.That(l1.ContainsEndpoint(l2[0]) || l1.ContainsEndpoint(l2[1])).IsEqualTo(isConnected);
    }
}
public class LineClassIntersectionAndPointTests {
    [Test] // All these were validated through Desmos
    [Arguments(-3.6, 0.5, 0, -4.0, 2.5, 3.8, -2.9, -2.1, true)]
    [Arguments(-1.6, -2.2, 5.1, -0.2, -0.9, 2.8, 1.5, -2.3, true)]
    [Arguments(-2.3, 2.7, -2.2, 5.5, 5.6, 0.7, 0.2, -3.6, false)]
    [Arguments(-1.1, 5, 2.5, 0.5, -4.2, 4.9, 0.5, 4.0, true)]
    [Arguments(0.7, -3.8, -1.9, 0.2, -1.5, -2.2, -0.4, 2.0, true)]
    [Arguments(1.9, 2.3, -2.2, 0.4, 5.1, -1.7, -2.1, 0.0, false)]
    [Arguments(-2.3, -2.5, 1.5, 5.4, 4.6, 3.9, 5.6, 5.0, false)]
    [Arguments(1.5, 4.9, -2.2, -0.5, 4.5, -1.9, -3.4, 4.6, true)]
    [Arguments(-4.5, -1.1, 3.8, 1.2, 5.1, -1.2, 1.2, 4.3, true)]
    [Arguments(4.1, -5.0, 5.1, -3.1, -0.5, -4.0, 3.9, -0.7, false)]
    public async Task IntersectionTests(
        double l1x1, double l1y1, double l1x2, double l1y2,
        double l2x1, double l2y1, double l2x2, double l2y2, bool isIntersecting) {
        Line2D l1 = new Line2D(l1x1, l1y1, l1x2, l1y2);
        Line2D l2 = new Line2D(l2x1, l2y1, l2x2, l2y2);
        await Assert.That(l1.IsIntersecting(l2)).IsEqualTo(isIntersecting);
        await Assert.That(l1 ^ l2).IsEqualTo(isIntersecting);

        Vector2D vector = new Vector2D();
        vector.AssignToCoordinates(l2x1, l2y1, l2x2, l2y2);
        await Assert.That(l1.IsIntersecting(new Point2D(l2x1, l2y1), vector)).IsEqualTo(isIntersecting);
    }

    [Test]
    [Arguments(-3.6, 0.5, 0, -4.0, 2.5, 3.8, -2.9, -2.1, -2.2, -1.3, true)]
    [Arguments(-1.6, -2.2, 5.1, -0.2, -0.9, 2.8, 1.5, -2.3, 1.1, -1.4, true)]
    [Arguments(-2.3, 2.7, -2.2, 5.5, 5.6, 0.7, 0.2, -3.6, -2.6, -5.8, false)]
    public async Task IntersectionPointTests(
        double l1x1, double l1y1, double l1x2, double l1y2,
        double l2x1, double l2y1, double l2x2, double l2y2, double pointX, double pointY, bool intersects) {
        Line2D l1 = new Line2D(l1x1, l1y1, l1x2, l1y2);
        Line2D l2 = new Line2D(l2x1, l2y1, l2x2, l2y2);
        Point2D? answer = Line2D.IntersectionPoint(l1, l2);

        if (answer is not null) {
            await Assert.That(answer.X - pointX).IsLessThanOrEqualTo(0.1);
            await Assert.That(answer.Y - pointY).IsLessThanOrEqualTo(0.1);
            await Assert.That(intersects).IsTrue();
        } else {
            await Assert.That(intersects).IsFalse();
        }
    }

    [Test]
    [Arguments(-3.6, 0.5, 0, -4.0, -2.2, -1.3, true)]
    [Arguments(-1.6, -2.2, 5.1, -0.2, 1.1, -1.4, true)]
    [Arguments(-2.3, 2.7, -2.2, 5.5, -2.6, -5.8, false)]
    public async Task ContainsPointTests(
        double l1x1, double l1y1, double l1x2, double l1y2,
        double pointX, double pointY, bool containsPoint) {
        Line2D l1 = new Line2D(l1x1, l1y1, l1x2, l1y2);
        await Assert.That(l1.ContainsPoint(pointX, pointY)).IsEqualTo(containsPoint);
        await Assert.That(l1.ContainsPoint(new Point2D(pointX, pointY))).IsEqualTo(containsPoint);
    }
}




