namespace HowlDev.Simulation.Physics.Primitive2D.Tests;


public class VectorClassBasicTests {
    [Test]
    public async Task DefaultConstructorTest() {
        Vector2D m1 = new Vector2D();
        await Assert.That(m1.Rotation.RotationAngle).IsEqualTo(0);
        await Assert.That(m1.Velocity).IsEqualTo(0.0);
    }

    [Test]
    public async Task SimpleConstructorTest() {
        Vector2D m1 = new Vector2D(25, 2.3);
        await Assert.That(m1.Rotation.RotationAngle).IsEqualTo(25);
        await Assert.That(m1.Velocity).IsEqualTo(2.3);
    }

    [Test]
    public async Task ObjectConstructorTest() {
        Vector2D m1 = new Vector2D(new Rotation2D(25), 2.3);
        await Assert.That(m1.Rotation.RotationAngle).IsEqualTo(25);
        await Assert.That(m1.Velocity).IsEqualTo(2.3);
    }

    [Test]
    public async Task UpdateTests() {
        Vector2D m1 = new Vector2D();
        m1.UpdateRotation(15);
        m1.UpdateVelocity(2.2);
        await Assert.That(m1.Rotation.RotationAngle).IsEqualTo(15);
        await Assert.That(m1.Velocity).IsEqualTo(2.2);

        m1.UpdateRotation(new Rotation2D(25));
        await Assert.That(m1.Rotation.RotationAngle).IsEqualTo(25);
    }
}
public class VectorClassOperatorTests {
    [Test]
    [Arguments(45, 2.2, 45, 2.2, true)]
    [Arguments(45, 2.2, 85, 2.2, false)]
    [Arguments(45, 2.3, 45, 2.2, false)]
    public async Task EqualityOperators(
        int rotAngle1, double velocity1, int rotAngle2, double velocity2, bool shouldBeEqual) {
        Vector2D m1 = new Vector2D(rotAngle1, velocity1);
        Vector2D m2 = new Vector2D(rotAngle2, velocity2);

        await Assert.That(m1.Equals(m2)).IsEqualTo(shouldBeEqual);
        await Assert.That(m1 == m2).IsEqualTo(shouldBeEqual);
        await Assert.That(m1 != m2).IsEqualTo(!shouldBeEqual);
    }

    [Test] // Validated with Desmos
    [Arguments(-0.2, -2.7, 162, 1.05, -1.2, -2.37)]
    [Arguments(0.7, 0.7, 343, 2.69, 3.3, 0)]
    [Arguments(-0.7, 2.4, 327, 2.49, 1.4, 1.1)]
    [Arguments(-1.3, -0.1, 189, 3.89, -5.2, -0.7)]
    [Arguments(-1.4, 1.7, 87, 0.35, -1.4, 2.0)]
    [Arguments(1.4, -2.3, 80, 0.13, 1.4, -2.2)]
    [Arguments(-2.1, -2.2, 184, 1.25, -3.4, -2.3)]
    [Arguments(2.8, 1.9, 274, 2.51, 3.0, -0.6)]
    [Arguments(3.2, -0.2, 157, 3.83, -0.3, 1.3)]
    [Arguments(3.2, -2.0, 49, 2.88, 5.1, 0.2)]
    public async Task AddOperator(
        double pointX, double pointY, double rotAngle, double velocity,
        double answerX, double answerY) {
        Point2D p = new Point2D(pointX, pointY);
        Vector2D m = new Vector2D(rotAngle, velocity);

        Point2D answer = p + m;
        await Assert.That(Math.Abs(answer.X - answerX)).IsLessThanOrEqualTo(0.1);
        await Assert.That(Math.Abs(answer.Y - answerY)).IsLessThanOrEqualTo(0.1);
    }

    [Test] // Validated with Desmos
    [Arguments(-0.2, -2.7, 162, 1.05, 0.8, -3.0)]
    [Arguments(0.7, 0.7, 343, 2.69, -1.8, 1.48)]
    [Arguments(-0.7, 2.4, 327, 2.49, -2.8, 3.7)]
    [Arguments(-1.3, -0.1, 189, 3.89, 2.5, 0.5)]
    [Arguments(-1.4, 1.7, 87, 0.35, -1.4, 1.4)]
    [Arguments(1.4, -2.3, 80, 0.13, 1.4, -2.4)]
    [Arguments(-2.1, -2.2, 184, 1.25, -0.8, -2.1)]
    [Arguments(2.8, 1.9, 274, 2.51, 2.6, 4.4)]
    [Arguments(3.2, -0.2, 157, 3.83, 6.7, -1.7)]
    [Arguments(3.2, -2.0, 49, 2.88, 1.3, -4.16)]
    public async Task SubtractOperator(
    double pointX, double pointY, int rotAngle, double velocity,
    double answerX, double answerY) {
        Point2D p = new Point2D(pointX, pointY);
        Vector2D m = new Vector2D(rotAngle, velocity);

        Point2D answer = p - m;
        await Assert.That(Math.Abs(answer.X - answerX)).IsLessThanOrEqualTo(0.1);
        await Assert.That(Math.Abs(answer.Y - answerY)).IsLessThanOrEqualTo(0.1);
    }
}
public class VectorClassComparisonTests {
    [Test]
    [Arguments(45, 0, 45, 2, -1)]
    [Arguments(45, 0, 45, -2, 1)]
    [Arguments(45, 0, 45, 0, 0)]
    [Arguments(85, 0, 45, 0, 1)]
    [Arguments(45, 0, 85, 0, -1)]
    public async Task ComparisonTests(
        int rot1, double vel1, int rot2, double vel2, int compairsonValue) {
        Vector2D m1 = new Vector2D(rot1, vel1);
        Vector2D m2 = new Vector2D(rot2, vel2);

        await Assert.That(m1.CompareTo(m2)).IsEqualTo(compairsonValue);
    }
}
public class VectorClassCoordinateAssignmentTests {
    [Test]
    [Arguments(1, 0, 0, 1)]
    [Arguments(0, 1, 90, 1)]
    [Arguments(-1, 0, 180, 1)]
    [Arguments(0, -1, 270, 1)]
    public async Task SimpleCoordinateTests(
        double x, double y, int outRotation, double outVelocity) {
        Vector2D m = new Vector2D();
        m.AssignToCoordinates(x, y);
        await Assert.That(m.Rotation.RotationAngle).IsEqualTo(outRotation);
        await Assert.That(m.Velocity).IsEqualTo(outVelocity);
    }

    [Test]
    [Arguments(8.3, 1.5, 10.24, 8.4)]
    [Arguments(8.8, 2.5, 15.86, 9.1)]
    [Arguments(6.8, 4.8, 35.22, 8.3)]
    [Arguments(0.5, 3.5, 81.87, 3.5)]
    [Arguments(0.8, 7.5, 83.91, 7.5)]
    [Arguments(-0.6, 10.7, 93.21, 10.7)]
    [Arguments(-1.0, 10.2, 95.6, 10.2)]
    [Arguments(-1.3, 5.1, 104.3, 5.2)]
    [Arguments(-3.7, 5.3, 124.92, 6.4)]
    [Arguments(-6.8, 6.6, 135.86, 9.5)]
    [Arguments(-6.1, 5.5, 137.96, 8.2)]
    [Arguments(-9.6, 3.0, 162.65, 10.0)]
    [Arguments(-7.0, -4.2, 210.96, 8.1)]
    [Arguments(-9.3, -5.7, 211.5, 10.9)]
    [Arguments(-10.0, -8.8, 221.35, 13.3)]
    [Arguments(-2.4, -8.8, 254.74, 9.1)]
    [Arguments(-2.0, -7.8, 255.62, 8.0)]
    [Arguments(6.8, -9.5, 305.59, 11.6)]
    [Arguments(6.6, -3.5, 332.06, 7.5)]
    [Arguments(8.5, -3.0, 340.56, 9.0)]
    public async Task ComplexCoordinateTests(
    double x, double y, double outRotation, double outVelocity) {
        Vector2D m = new Vector2D();
        m.AssignToCoordinates(new Point2D(x, y));

        await Assert.That(m.Rotation.RotationAngle).IsEqualTo(outRotation);
        await Assert.That(m.Velocity - outVelocity).IsLessThanOrEqualTo(0.1);
    }

    [Test]
    [Arguments(-1.85, 2.42, 2.08, -0.62, 322.28, 4.96)]
    [Arguments(1.88, -0.46, -1.25, -3.37, 222.91, 4.27)]
    [Arguments(-2.13, 1.33, 5.25, 0.54, 353.89, 7.42)]
    [Arguments(-2.68, -4.67, 4.83, 5.10, 52.45, 12.32)]
    [Arguments(-3.55, 5.65, -0.82, 1.17, 301.36, 5.24)]
    [Arguments(-3.65, 5.92, -4.80, -3.65, 263.15, 9.63)]
    [Arguments(4.40, -2.28, -3.61, 3.42, 144.56, 9.83)]
    [Arguments(-4.92, -0.95, 3.17, 3.49, 28.76, 9.22)]
    [Arguments(4.98, -2.66, -4.10, 0.75, 159.42, 9.69)]
    [Arguments(5.18, 4.73, 3.38, -1.92, 254.85, 6.88)]
    public async Task TwoPointCoordinateTests(
        double x1, double y1, double x2, double y2, double outRotation, double outVelocity) {
        Vector2D vector = new Vector2D();
        vector.AssignToCoordinates(x1, y1, x2, y2);
        await Assert.That(vector.Rotation.RotationAngle).IsEqualTo(outRotation);
        await Assert.That(vector.Velocity - outVelocity).IsLessThanOrEqualTo(0.01);

        vector = new Vector2D();
        vector.AssignToCoordinates(new Point2D(x1, y1), new Point2D(x2, y2));
        await Assert.That(vector.Rotation.RotationAngle).IsEqualTo(outRotation);
        await Assert.That(vector.Velocity - outVelocity).IsLessThanOrEqualTo(0.01);
    }
}