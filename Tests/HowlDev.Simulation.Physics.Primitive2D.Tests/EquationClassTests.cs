namespace HowlDev.Simulation.Physics.Primitive2D.Tests;

public class EquationBasicTests {
    [Test]
    public async Task DefaultConstructorTest() {
        Equation2D e = new Equation2D();
        await Assert.That(e[0]).IsEqualTo(0.0);
    }

    [Test]
    public async Task PointConstructorTest() {
        Equation2D e = new Equation2D(new Point2D(0, 0), new Point2D(5, 5));
        await Assert.That(e[1]).IsEqualTo(1.0);
        await Assert.That(e[0]).IsEqualTo(0.0);
    }

    [Test]
    public async Task LineConstructorTest() {
        Line2D line = new Line2D(new Point2D(0, 0), new Point2D(5, 5));
        Equation2D e = new Equation2D(line);
        await Assert.That(e[1]).IsEqualTo(1.0);
        await Assert.That(e[0]).IsEqualTo(0.0);
    }

    [Test]
    public async Task CoefficientTest() {
        Equation2D e = new Equation2D(1, 4);
        await Assert.That(e.Coefficients).IsEquivalentTo(new double[] { 4, 1 });
    }

    [Test]
    public async Task EquationCanBeCopied() {
        Equation2D e1 = new Equation2D(15, 10);
        Equation2D e2 = new Equation2D(e1);

        e2 = e2.WithCoefficient(0, 25);
        await Assert.That(e1[0]).IsEqualTo(10);
        await Assert.That(e2[0]).IsEqualTo(25);
    }
}
public class EquationCoordinateAssignmentTests {
    [Test]
    [Arguments(0, 0, 5, 0, 0, 0)]
    [Arguments(0, 0, 5, 5, 1, 0)]
    [Arguments(0, 5, 5, 10, 1, 5)]
    [Arguments(0, 0, -5, 5, -1, 0)]
    [Arguments(0, 5, -5, 10, -1, 5)]
    [Arguments(0, -3, -2, -2, -0.5, -3)]
    public async Task SimpleAssignmentTests(
        double x1, double y1, double x2, double y2, double slope, double intercept) {
        Equation2D equation = new Equation2D(x1, y1, x2, y2);
        await Assert.That(equation[0]).IsEqualTo(intercept);
        await Assert.That(equation[1]).IsEqualTo(slope);
    }

    [Test]
    [Arguments(3.43, 5.44, -1.83, 3.83, 0.30, 4.41)]
    [Arguments(3.60, -1.29, -4.23, 2.26, -0.45, 0.32)]
    [Arguments(3.47, 1.65, -4.63, -3.48, 0.63, -0.53)]
    [Arguments(1.71, 3.01, 1.16, -0.26, 5.87, -7.07)]
    [Arguments(1.09, -0.09, -0.59, -2.59, 1.46, -1.70)]
    [Arguments(-0.29, 5.17, 4.60, -1.10, -1.28, 4.78)]
    [Arguments(-2.66, 3.96, -1.90, -2.80, -8.89, -19.68)]
    [Arguments(-2.29, 3.42, -2.76, -1.08, 9.59, 25.39)]
    [Arguments(5.47, 0.73, 5.76, -2.75, -12.00, 66.37)]
    [Arguments(4.77, -4.43, -4.21, -3.99, -0.04, -4.23)]
    [Arguments(-4.55, -4.71, -0.42, 0.82, 1.34, 1.38)]
    [Arguments(3.19, 0.33, -3.83, -4.25, 0.65, -1.73)]
    [Arguments(0.91, 2.03, -2.78, -3.13, 1.39, 0.76)]
    [Arguments(-1.23, -0.63, -3.50, -3.44, 1.23, 0.87)]
    [Arguments(-2.81, -0.42, 5.02, -4.32, -0.49, -1.80)]
    [Arguments(5.55, 4.32, -1.10, -0.42, 0.71, 0.37)]
    [Arguments(2.29, 4.35, -3.38, 5.23, -0.15, 4.69)]
    [Arguments(5.89, -1.72, 3.55, 0.59, -0.99, 4.11)]
    [Arguments(0.67, 0.48, 2.11, 0.12, -0.24, 0.64)]
    [Arguments(5.47, -2.41, -4.69, 3.91, -0.62, 0.98)]
    public async Task ComplexAssignmentTests(
        double x1, double y1, double x2, double y2, double slope, double intercept) {
        Equation2D equation = Equation2D.FromCoordinates(x1, y1, x2, y2);
        await Assert.That(equation[0] - intercept).IsLessThanOrEqualTo(0.1);
        await Assert.That(equation[1] - slope).IsLessThanOrEqualTo(0.1);

        equation = Equation2D.FromCoordinates(new Point2D(x1, y1), new Point2D(x2, y2));
        await Assert.That(equation[0] - intercept).IsLessThanOrEqualTo(0.1);
        await Assert.That(equation[1] - slope).IsLessThanOrEqualTo(0.1);

        equation = Equation2D.FromCoordinates(new Line2D(x1, y1, x2, y2));
        await Assert.That(equation[0] - intercept).IsLessThanOrEqualTo(0.1);
        await Assert.That(equation[1] - slope).IsLessThanOrEqualTo(0.1);
    }
}
public class EquationAssignToPointTests {
    [Test]
    [Arguments(5, 5, 0)]
    [Arguments(1.18, 7.53, 6.35)]
    [Arguments(7.75, 5.84, -1.91)]
    [Arguments(1.35, 8.16, 6.80)]
    [Arguments(2.99, 1.41, -1.58)]
    [Arguments(-8.12, -1.43, 6.68)]
    [Arguments(2.39, 7.69, 5.30)]
    [Arguments(8.48, 8.43, -0.05)]
    [Arguments(-2.36, 5.50, 7.86)]
    [Arguments(-3.30, -8.09, -4.78)]
    [Arguments(-6.85, -0.08, 6.76)]

    public async Task SlopeIsOne(
        double x, double y, double expIntercept) {
        Equation2D e = new Equation2D(1, 0);
        e = e.WithPoint(x, y);
        await Assert.That(e.Intercept - expIntercept).IsLessThanOrEqualTo(0.02);
        await Assert.That(e.Slope).IsEqualTo(1);
        await Assert.That(e.PointIsOnLine(x, y)).IsTrue();
    }

    [Test]
    [Arguments(-9.60, 2.35, -1.9, -15.87)]
    [Arguments(-4.51, -0.82, -2.6, -12.54)]
    [Arguments(-6.91, -4.27, 4.9, 29.58)]
    [Arguments(5.42, -2.41, -4.7, 23.06)]
    [Arguments(-8.45, 6.35, 0.0, 6.35)]
    [Arguments(2.22, 0.16, -1.4, 3.26)]
    [Arguments(-9.50, 5.00, -2.9, -22.55)]
    [Arguments(9.48, 0.55, 0.5, -4.19)]
    [Arguments(-4.89, 10.57, 1.0, 15.46)]
    [Arguments(-9.04, -1.33, -4.2, -39.30)]
    public async Task VariableSlope(
        double x, double y, double slope, double expIntercept) {
        Equation2D e = new Equation2D(slope, 0);
        e = e.WithPoint(new Point2D(x, y));
        await Assert.That(e.Intercept - expIntercept).IsLessThanOrEqualTo(0.02);
        await Assert.That(e.Slope).IsEqualTo(slope);
        await Assert.That(e.PointIsOnLine(x, y)).IsTrue();
    }
}
public class EquationPointIsOnLineTests {
    [Test]
    [Arguments(0, 0, 5, 0, 0, 0, true)]
    [Arguments(0, 0, 5, 5, 100, 100, true)]
    [Arguments(0, 0, 5, 5, 0, 1, false)]
    [Arguments(0, 5, 5, 10, 5, 5, false)]
    [Arguments(0, 0, -5, 5, -100, 100, true)]
    [Arguments(0, 0, -5, 5, -50, 49, false)]
    [Arguments(0, 5, -5, 10, 0, 5, true)]
    [Arguments(0, 5, -5, 10, 0, 6, false)]
    [Arguments(0, -3, -2, -2, -6, 0, true)]
    public async Task SimpleEquationPointTests(
        double x1, double y1, double x2, double y2, double pointX, double pointY, bool isOnLine) {
        Equation2D equation = new Equation2D(new Point2D(x1, y1), new Point2D(x2, y2));
        Point2D p = new Point2D(pointX, pointY);

        await Assert.That(equation.PointIsOnLine(p)).IsEqualTo(isOnLine);
    }

    [Test]
    [Arguments(3.24, 5.50, 2.21, -4.60, -3.0, 2.90, false)]
    [Arguments(-0.32, 2.96, 5.06, -1.63, 0.0, -2.70, false)]
    [Arguments(2.30, -1.13, 5.33, 3.23, 3.9, 1.16, true)]
    [Arguments(-3.92, 1.54, 3.82, 1.63, 3.0, 0.39, false)]
    [Arguments(-4.32, 3.02, 2.04, 3.64, 0.7, -2.90, false)]
    [Arguments(0.67, -3.44, 5.68, -3.59, 0.5, -2.20, false)]
    [Arguments(3.01, 1.25, 1.17, -3.11, -3.0, -12.89, true)]
    [Arguments(2.76, 4.18, -0.79, -1.14, 2.2, 3.33, true)]
    [Arguments(-2.70, 5.00, -0.53, -4.03, 3.4, 0.39, false)]
    [Arguments(-4.77, -3.58, 4.52, -4.49, -1.7, 1.70, false)]
    [Arguments(2.48, -3.28, 0.42, -1.00, 1.2, 2.50, false)]
    [Arguments(-1.16, 0.79, -3.16, 2.62, 1.2, -1.43, true)]
    [Arguments(-2.30, 3.63, 3.13, -4.58, 1.9, -2.71, true)]
    [Arguments(3.41, 3.31, 0.50, -1.25, -2.7, -3.00, false)]
    [Arguments(3.90, 1.50, -1.90, -2.76, -3.0, -3.52, true)]
    [Arguments(2.29, -0.59, -3.02, 4.44, -0.8, 2.42, true)]
    [Arguments(0.98, 4.88, -1.88, 3.11, 2.5, 5.80, true)]
    [Arguments(-3.96, 0.74, 0.42, -1.67, 1.7, -2.30, true)]
    [Arguments(-2.53, 0.86, 5.91, 0.84, 3.0, 0.86, true)]
    [Arguments(2.29, 0.58, -1.25, 3.67, -1.9, 4.22, true)]
    public async Task ComplexEquationPointTests(
        double x1, double y1, double x2, double y2, double pointX, double pointY, bool isOnLine) {
        Equation2D equation = new Equation2D(new Point2D(x1, y1), new Point2D(x2, y2));

        await Assert.That(equation.PointIsOnLine(pointX, pointY, 0.15)).IsEqualTo(isOnLine);
    }
}
public class EquationIntersectionPointTests {
    [Test]
    [Arguments(1.2, 2.8, 1.2, 5.6, 0, 0, true)]
    [Arguments(1.2, 2.8, 1.2, 2.8, -1.5, 0.93, true)]
    [Arguments(0.7, 5.6, 0.5, -2.9, -42.5, -24.15, false)]
    [Arguments(3.5, 2.5, -4.2, -5.0, -0.9, -0.9, false)]
    [Arguments(-0.2, 5.6, -4.2, 2.5, -0.77, 5.75, false)]
    [Arguments(-4.0, 2.7, 2.0, -0.5, 0.5, 0.56, false)]
    [Arguments(-2.8, 0.0, 3.4, -2.2, 0.3, -0.9, false)]
    [Arguments(-1.0, 5.0, -3.6, 2.2, -1.0, 6.07, false)]
    [Arguments(-2.7, 5.8, -0.7, 5.6, 0.1, 5.5, false)]
    [Arguments(4.1, 4.9, -0.2, 2.5, -0.5, 2.6, false)]
    [Arguments(-1.1, 0.9, -3.1, -2.1, -1.5, 2.5, false)]
    public async Task IntersectionPointTests(
        double slope1, double intercept1, double slope2, double intercept2, double pointX, double pointY, bool isNull) {
        Equation2D e1 = new Equation2D(slope1, intercept1);
        Equation2D e2 = new Equation2D(slope2, intercept2);
        Point2D? answer = e1.IntersectionPoint(e2);

        if (!answer.HasValue) {
            await Assert.That(isNull).IsTrue();
        } else {
            await Assert.That(isNull).IsFalse();
            await Assert.That(answer.Value.X - pointX).IsLessThanOrEqualTo(0.1);
            await Assert.That(answer.Value.Y - pointY).IsLessThanOrEqualTo(0.1);
        }

        answer = e1 ^ e2;

        if (!answer.HasValue) {
            await Assert.That(isNull).IsTrue();
        } else {
            await Assert.That(isNull).IsFalse();
            await Assert.That(answer.Value.X - pointX).IsLessThanOrEqualTo(0.1);
            await Assert.That(answer.Value.Y - pointY).IsLessThanOrEqualTo(0.1);
        }
    }
}
public class EquationOperatorTests {
    [Test]
    [Arguments(4.42, 8.16)]
    [Arguments(5.37, 8.59)]
    [Arguments(5.79, 10.78)]
    [Arguments(4.38, 2.34)]
    public async Task InversionTests(
        double slope, double intercept) {
        Equation2D equation = new Equation2D(slope, intercept);
        equation = -equation;
        await Assert.That(equation[1]).IsEqualTo(-slope);
        await Assert.That(equation[0]).IsEqualTo(intercept);
    }

    [Test]
    [Arguments(5.79, 3.69, 3.59, 3.54, 9.37, 7.23)]
    [Arguments(6.61, 9.46, 1.73, 9.33, 8.34, 18.79)]
    [Arguments(5.74, 4.10, 9.61, 2.24, 15.35, 6.35)]
    [Arguments(8.92, 4.66, 3.54, 2.62, 12.46, 7.28)]
    [Arguments(10.10, 3.91, 4.55, 2.29, 14.64, 6.20)]
    public async Task PlusTests(
        double slope1, double intercept1, double slope2, double intercept2, double outSlope, double outIntercept) {
        Equation2D e1 = new Equation2D(slope1, intercept1);
        Equation2D e2 = new Equation2D(slope2, intercept2);
        Equation2D answer = e1 + e2;

        await Assert.That(answer[1] - outSlope).IsLessThanOrEqualTo(0.1);
        await Assert.That(answer[0] - outIntercept).IsLessThanOrEqualTo(0.1);
    }

    [Test]
    public async Task DoubleEqualsTest() {
        Equation2D e1 = new Equation2D(-1, 1);
        Equation2D e2 = new Equation2D(1, 1);
        Equation2D e3 = new Equation2D(1, 1);

        await Assert.That(e2 == e3).IsTrue();
        await Assert.That(e1 == e2).IsFalse();
        await Assert.That(e1 != e2).IsTrue();
        await Assert.That(e2 != e3).IsFalse();
    }

    [Test]
    public async Task GTandLTTest() {
        Equation2D e1 = new Equation2D(-1, 1);
        Equation2D e2 = new Equation2D(1, -1);
        Equation2D e3 = new Equation2D(1, 1);
        Equation2D e4 = new Equation2D(3, 2);

        await Assert.That(e1 < e2).IsTrue();
        await Assert.That(e2 < e3).IsTrue();
        await Assert.That(e3 < e4).IsTrue();
        await Assert.That(e2 < e1).IsFalse();
        await Assert.That(e4 < e1).IsFalse();

        await Assert.That(e1 > e2).IsFalse();
        await Assert.That(e2 > e3).IsFalse();
        await Assert.That(e3 > e4).IsFalse();
        await Assert.That(e2 > e1).IsTrue();
        await Assert.That(e4 > e1).IsTrue();
    }

    [Test]
    public async Task GTETandLTETTest() {
        Equation2D e1 = new Equation2D(-1, 1);
        Equation2D e2 = new Equation2D(1, -1);
        Equation2D e3 = new Equation2D(1, 1);
        Equation2D e4 = new Equation2D(3, 2);

        await Assert.That(e1 <= e2).IsTrue();
        await Assert.That(e2 <= e3).IsTrue();
        await Assert.That(e3 <= e4).IsTrue();
        await Assert.That(e2 <= e1).IsFalse();
        await Assert.That(e4 <= e1).IsFalse();

        await Assert.That(e1 >= e2).IsFalse();
        await Assert.That(e2 >= e3).IsTrue();
        await Assert.That(e3 >= e4).IsFalse();
        await Assert.That(e2 >= e1).IsTrue();
        await Assert.That(e4 >= e1).IsTrue();
    }
}
public class EquationInterfaceImplementationTests {
    [Test]
    [Arguments(-1, 1, 2, 0, false)]
    [Arguments(-2, -1, 1, 0, false)]
    [Arguments(2, 1, 2, 1, true)]
    [Arguments(-2, -2, -1, -2, false)]
    [Arguments(1, -1, 2, 1, false)]
    public async Task EqualityTests(
        double slope1, double intercept1, double slope2, double intercept2, bool isEqual) {
        Equation2D e1 = new Equation2D(slope1, intercept1);
        Equation2D e2 = new Equation2D(slope2, intercept2);

        await Assert.That(e1.Equals(e2)).IsEqualTo(isEqual);
    }

    [Test]
    [Arguments(1, -2, 1, -2, 0)]
    [Arguments(0, 2, -2, 1, 1)]
    [Arguments(0, -1, 1, 0, -1)]
    [Arguments(-2, -1, 1, -2, -1)]
    [Arguments(-2, -1, 0, 1, -1)]
    [Arguments(2, 2, 0, -1, 1)]
    [Arguments(-2, 2, -1, -2, -1)]
    [Arguments(1, 2, 0, -1, 1)]
    [Arguments(1, 0, 1, 1, -1)]
    [Arguments(2, 1, -1, 1, 1)]
    [Arguments(2, 1, 0, 1, 1)]
    [Arguments(-1, 2, 1, -1, -1)]
    [Arguments(-1, 1, 1, 2, -1)]
    [Arguments(-2, -2, -1, 2, -1)]
    [Arguments(0, -2, -2, 1, 1)]
    [Arguments(0, 2, -1, 1, 1)]
    [Arguments(-2, -2, 1, 0, -1)]
    [Arguments(0, 0, -2, 1, 1)]
    [Arguments(-2, -2, 0, 1, -1)]
    public async Task CompareToTests(
        double slope1, double intercept1, double slope2, double intercept2, int outValue) {
        Equation2D e1 = new Equation2D(slope1, intercept1);
        Equation2D e2 = new Equation2D(slope2, intercept2);

        await Assert.That(e1.CompareTo(e2)).IsEqualTo(outValue);
    }
}
public class VerticalEquationTests {
    [Test]
    public async Task CanInitialize() {
        Equation2D equation = new Equation2D(5, 5, 5, 0);
    }

    [Test]
    public async Task InitializeIsCorrect() {
        Equation2D equation = new Equation2D(5, 5, 5, 0);
        await Assert.That(equation.Intercept).IsEqualTo(5);
    }

    [Test] // Validated with Desmos
    [Arguments(0.00, 4.59, 5, 4.59)]
    [Arguments(-0.25, -3.81, 5, -5.06)]
    [Arguments(1.92, 0.01, 5, 9.61)]
    [Arguments(-3.37, -0.16, 5, -17.01)]
    [Arguments(-3.61, -4.97, 5, -23.02)]
    [Arguments(3.71, -4.17, 5, 14.38)]
    [Arguments(-4.05, 2.41, 5, -17.84)]
    [Arguments(4.16, 4.36, 5, 25.16)]
    [Arguments(5.31, 2.83, 5, 29.38)]
    [Arguments(5.66, -0.57, 5, 27.73)]
    public async Task VerticalLineIntersectionPoints(
        double slope, double intercept, double intersectionX, double intersectionY) {
        Equation2D e1 = new Equation2D(slope, intercept);
        Equation2D e2 = new Equation2D(5, 5, 5, 0);

        Point2D? answer = e1.IntersectionPoint(e2);
        if (answer.HasValue) {
            await Assert.That(answer.Value.X - intersectionX).IsLessThanOrEqualTo(0.1);
            await Assert.That(answer.Value.Y - intersectionY).IsLessThanOrEqualTo(0.1);
        }
    }

    [Test]
    public async Task VerticalLineNullTest() {
        Equation2D e1 = new Equation2D(3, 3, 3, 0);
        Equation2D e2 = new Equation2D(5, 5, 5, 0);

        Point2D? answer = e1.IntersectionPoint(e2);
        await Assert.That(answer).IsNull();
    }

    [Test]
    [Arguments(5.61, 5.04, false)]
    [Arguments(0.89, 4.50, false)]
    [Arguments(3.005, 2.95, true)]
    [Arguments(5.80, 1.58, false)]
    [Arguments(3.70, 3.07, false)]
    [Arguments(2.3, 5.94, false)]
    public async Task PointIsOnLineTests(
        double x, double y, bool isOnLine) {
        Equation2D e1 = new Equation2D(3, 3, 3, 0);

        await Assert.That(e1.PointIsOnLine(x, y)).IsEqualTo(isOnLine);
    }
}