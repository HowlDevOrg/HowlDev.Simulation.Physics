using HowlDev.Simulation.Physics.Primitve2D;

namespace HowlDev.Simulation.Physics.Primitve2D.Tests;

// Ints in Arguments are a legacy of the way they used to be handled. It's more tests to check at least. 
public class RotationClassConstraintTests {
    [Test]
    public async Task RotationCanBeSet1() {
        Rotation2D r = new Rotation2D();
        r.RotationAngle = 25;
        await Assert.That(r.RotationAngle).IsEqualTo(25);
    }

    [Test]
    public async Task RotationCanBeSet2() {
        Rotation2D r = new Rotation2D();
        r.RotationAngle = 25.352;
        await Assert.That(r.RotationAngle).IsEqualTo(25.35);
    }

    [Test]
    public async Task RotationCanBeSetInConstructor1() {
        Rotation2D r = new Rotation2D(25);
        await Assert.That(r.RotationAngle).IsEqualTo(25);
    }

    [Test]
    public async Task RotationCanBeSetInConstructor2() {
        Rotation2D r1 = new Rotation2D(90.4);
        Rotation2D r2 = new Rotation2D(r1);
        await Assert.That(r2.RotationAngle).IsEqualTo(90.4);
    }

    [Test]
    public async Task RotationEnforcesLowerConstraint() {
        Rotation2D r = new Rotation2D(-25.4);
        await Assert.That(r.RotationAngle).IsEqualTo(334.6);
    }

    [Test]
    public async Task RotationEnforcesUpperConstraint() {
        Rotation2D r = new Rotation2D(365.23);
        await Assert.That(r.RotationAngle).IsEqualTo(5.23);
    }

    [Test]
    [Arguments(150, 50, 200)]
    [Arguments(150, -50.5, 99.5)]
    [Arguments(150, -200, 310)]
    public async Task RotationCanBeAdjusted(
        double startingDegree, double adjustment, double outDegree) {
        Rotation2D r = new Rotation2D(startingDegree);
        r.AdjustBy(adjustment);
        await Assert.That(r.RotationAngle).IsEqualTo(outDegree);
    }

    [Test]
    public async Task RotationCanBeCopied() {
        Rotation2D r1 = new Rotation2D(15);
        Rotation2D r2 = new Rotation2D(r1);

        r2.RotationAngle = 25;
        await Assert.That(r1.RotationAngle).IsEqualTo(15);
        await Assert.That(r2.RotationAngle).IsEqualTo(25);
    }
}
public class RotationClassCoordinateTests {
    private double d = Math.Round(Math.Sqrt(2) / 2, 2);

    [Test]
    public async Task RotationCanGetCoords1() {
        Rotation2D r = new Rotation2D(0);
        await Assert.That(r.X_Coord).IsEqualTo(1);
        await Assert.That(r.Y_Coord).IsEqualTo(0);
    }

    [Test]
    public async Task RotationCanGetCoords2() {
        Rotation2D r = new Rotation2D(45);
        await Assert.That(r.X_Coord).IsEqualTo(d);
        await Assert.That(r.Y_Coord).IsEqualTo(d);
    }

    [Test]
    public async Task RotationCanGetCoords3() {
        Rotation2D r = new Rotation2D(90);
        await Assert.That(r.X_Coord).IsEqualTo(0);
        await Assert.That(r.Y_Coord).IsEqualTo(1);
    }

    [Test]
    public async Task RotationCanGetCoords4() {
        Rotation2D r = new Rotation2D(135);
        await Assert.That(r.X_Coord).IsEqualTo(-d);
        await Assert.That(r.Y_Coord).IsEqualTo(d);
    }

    [Test]
    public async Task RotationCanGetCoords5() {
        Rotation2D r = new Rotation2D(180);
        await Assert.That(r.X_Coord).IsEqualTo(-1);
        await Assert.That(r.Y_Coord).IsEqualTo(0);
    }

    [Test]
    public async Task RotationCanGetCoords6() {
        Rotation2D r = new Rotation2D(225);
        await Assert.That(r.X_Coord).IsEqualTo(-d);
        await Assert.That(r.Y_Coord).IsEqualTo(-d);
    }

    [Test]
    public async Task RotationCanGetCoords7() {
        Rotation2D r = new Rotation2D(270);
        await Assert.That(r.X_Coord).IsEqualTo(0);
        await Assert.That(r.Y_Coord).IsEqualTo(-1);
    }

    [Test]
    public async Task RotationCanGetCoords8() {
        Rotation2D r = new Rotation2D(315);
        await Assert.That(r.X_Coord).IsEqualTo(d);
        await Assert.That(r.Y_Coord).IsEqualTo(-d);
    }
}
public class RotationClassXFlipTests {
    [Test]
    [Arguments(0, 0)]
    [Arguments(180, 180)]
    [Arguments(30, 330)]
    [Arguments(75, 285)]
    [Arguments(110, 250)]
    [Arguments(170, 190)]
    [Arguments(210, 150)]
    [Arguments(235, 125)]
    [Arguments(271, 89)]
    [Arguments(325, 35)]
    [Arguments(74.72, 285.28)]
    [Arguments(194.6, 165.4)]
    [Arguments(209.26, 150.74)]
    [Arguments(237.35, 122.65)]
    [Arguments(325.05, 34.95)]
    public async Task RotationCanBeXFlipped(
        double inDegree, double outDegree) {
        Rotation2D r = new Rotation2D(inDegree);
        r.FlipX();
        await Assert.That(r.RotationAngle).IsEqualTo(outDegree);
    }
}
public class RotationClassYFlipTests {
    [Test]
    [Arguments(90, 90)]
    [Arguments(270, 270)]
    [Arguments(30, 150)]
    [Arguments(75, 105)]
    [Arguments(110, 70)]
    [Arguments(170, 10)]
    [Arguments(210, 330)]
    [Arguments(235, 305)]
    [Arguments(271, 269)]
    [Arguments(325, 215)]
    [Arguments(40.89, 139.11)]
    [Arguments(92.87, 87.13)]
    [Arguments(190.89, 349.11)]
    [Arguments(226.24, 313.76)]
    [Arguments(285.47, 254.53)]
    public async Task RotationCanBeYFlipped(
        double inDegree, double outDegree) {
        Rotation2D r = new Rotation2D(inDegree);
        r.FlipY();
        await Assert.That(r.RotationAngle).IsEqualTo(outDegree);
    }
}
public class RotationClassDoubleFlipTests {
    [Test]
    [Arguments(45, 225)]
    [Arguments(359, 179)]
    [Arguments(0, 180)]
    [Arguments(90, 270)]
    [Arguments(99.33, 279.33)]
    [Arguments(195.29, 15.29)]
    [Arguments(216.27, 36.27)]
    [Arguments(314.61, 134.61)]
    public async Task RotationCanBeDoubleFlipped(
        double inDegree, double outDegree) {
        Rotation2D r = new Rotation2D(inDegree);
        r.FlipY();
        r.FlipX();
        await Assert.That(r.RotationAngle).IsEqualTo(outDegree);
    }

    [Test]
    [Arguments(45, 225)]
    [Arguments(359, 179)]
    [Arguments(0, 180)]
    [Arguments(90, 270)]
    [Arguments(99.33, 279.33)]
    [Arguments(195.29, 15.29)]
    [Arguments(216.27, 36.27)]
    [Arguments(314.61, 134.61)]
    public async Task RotationCanBeDoubleFlippedPart2(
        double inDegree, double outDegree) {
        Rotation2D r = new Rotation2D(inDegree);
        r.DoubleFlip();
        await Assert.That(r.RotationAngle).IsEqualTo(outDegree);
    }
}
public class RotationClassAssignsToCoordinatesTests {
    [Test]
    [Arguments(10, 0, 0)]
    [Arguments(10, 10, 45)]
    [Arguments(0, 10, 90)]
    [Arguments(-10, 10, 135)]
    [Arguments(-10, 0, 180)]
    [Arguments(-10, -10, 225)]
    [Arguments(0, -10, 270)]
    [Arguments(10, -10, 315)]
    [Arguments(77.74, 98.37, 51.68)]
    [Arguments(-28.09, -5.60, 191.27)]
    [Arguments(-46.99, -54.87, 229.42)]
    public async Task RotationAngleFromCoordinatesWorks(
        double coordX, double coordY, double outAngle) {
        Rotation2D r = new Rotation2D();
        r.AssignToCoordinates(coordX, coordY);
        await Assert.That(r.RotationAngle).IsEqualTo(outAngle);

        r.AssignToCoordinates(new Point2D(coordX, coordY));
        await Assert.That(r.RotationAngle).IsEqualTo(outAngle);
    }
}
public class RotationClassAssignsto2PointCoordinateTests {
    [Test]
    [Arguments(0, 0, 10, 10, 45)]
    [Arguments(0, 0, -10, 10, 135)]
    [Arguments(0, 0, -10, -10, 225)]
    [Arguments(0, 0, 10, -10, 315)]
    public async Task TwoPointSimpleCoordTests(
        double x1, double y1, double x2, double y2, double outAngle) {
        Rotation2D r = new Rotation2D();
        r.AssignToCoordinates(x1, y1, x2, y2);
        await Assert.That(r.RotationAngle).IsEqualTo(outAngle);
    }

    [Test]
    [Arguments(5, 5, 0, 0, 225)]
    [Arguments(25, 0, -30, 0, 180)]
    [Arguments(-30, 0, 45, -22, 343.65)]
    [Arguments(83, -24, 0, 0, 163.87)]
    public async Task TwoPointComplexCoordTests(
    double x1, double y1, double x2, double y2, double outAngle) {
        Rotation2D r = new Rotation2D();
        r.AssignToCoordinates(x1, y1, x2, y2);
        await Assert.That(r.RotationAngle).IsEqualTo(outAngle);

        r.AssignToCoordinates(new Point2D(x1, y1), new Point2D(x2, y2));
        await Assert.That(r.RotationAngle).IsEqualTo(outAngle);

        r.AssignToCoordinates(new Line2D(x1, y1, x2, y2));
        await Assert.That(r.RotationAngle).IsEqualTo(outAngle);
    }

    [Test]
    [Arguments(5, 5, 0, 0, 225)]
    [Arguments(25, 0, -30, 0, 180)]
    [Arguments(-30, 0, 45, -22, 343.65)]
    [Arguments(83, -24, 0, 0, 163.87)]
    public async Task TuplesCoordTest(
    double x1, double y1, double x2, double y2, double outAngle) {
        Rotation2D r = new Rotation2D();
        r.AssignToCoordinates((x1, y1), (x2, y2));
        await Assert.That(r.RotationAngle).IsEqualTo(outAngle);
    }
}
public class RotationClassDistanceTests {
    [Test]
    [Arguments(0, 0, 0)]
    [Arguments(0, 180, 180)]
    [Arguments(0, 179, 179)]
    [Arguments(0, 181, -179)]
    [Arguments(90, 270, 180)]
    [Arguments(0, 90, 90)]
    [Arguments(0, 270, -90)]
    [Arguments(30, 330, -60)]
    [Arguments(330, 30, 60)]
    [Arguments(270, 0, 90)]
    public async Task DistanceIntTests(
        double rot1, double rot2, double expDistance) {
        Rotation2D r1 = new Rotation2D(rot1);

        await Assert.That(r1.DistanceTo(rot2, true)).IsEqualTo(Math.Abs(expDistance));
        await Assert.That(r1.DistanceTo(rot2, false)).IsEqualTo(expDistance);
    }

    [Test]
    [Arguments(0, 0, 0)]
    [Arguments(0, 180, 180)]
    [Arguments(90, 270, 180)]
    [Arguments(0, 90, 90)]
    [Arguments(0, 270, -90)]
    [Arguments(30, 330, -60)]
    [Arguments(330, 30, 60)]
    [Arguments(270, 0, 90)]
    public async Task DistanceClassTests(
    int rot1, int rot2, int expDistance) {
        Rotation2D r1 = new Rotation2D(rot1);
        Rotation2D r2 = new Rotation2D(rot2);

        await Assert.That(r1.DistanceTo(r2, true)).IsEqualTo(Math.Abs(expDistance));
        await Assert.That(r1.DistanceTo(r2, false)).IsEqualTo(expDistance);
    }
}
public class RotationClassAverageToTests {
    [Test]
    [Arguments(0, 90, 0, 0)]
    [Arguments(0, 90, 1, 90)]
    [Arguments(0, 270, 0, 0)]
    [Arguments(0, 270, 1, 270)]
    public async Task SimpleAverageToClassTests(
        int rot1, int rot2, double percent, int outAngle) {
        Rotation2D r1 = new Rotation2D(rot1);
        Rotation2D r2 = new Rotation2D(rot2);

        r1.AverageTo(r2, percent);
        await Assert.That(r1.RotationAngle).IsEqualTo(outAngle);
        await Assert.That(r2.RotationAngle).IsEqualTo(rot2);
    }

    [Test]
    [Arguments(0, 90, 0, 0)]
    [Arguments(0, 90, 1, 90)]
    [Arguments(0, 270, 0, 0)]
    [Arguments(0, 270, 1, 270)]
    public async Task SimpleAverageToIntTests(
    int rot1, int rot2, double percent, int outAngle) {
        Rotation2D r1 = new Rotation2D(rot1);

        r1.AverageTo(rot2, percent);
        await Assert.That(r1.RotationAngle).IsEqualTo(outAngle);
    }

    [Test]
    [Arguments(0, 90, 0.5, 45)]
    [Arguments(0, 90, 0.2, 18)]
    [Arguments(0, 270, 0.5, 315)]
    [Arguments(0, 270, 0.2, 342)]
    [Arguments(10, 214, 0.15, 346.6)]
    [Arguments(14, 256, 0.7, 291.4)]
    [Arguments(196, 218, 0.28, 202.16)]
    [Arguments(207, 321, 0.83, 301.62)]
    [Arguments(218, 180, 0.59, 195.58)]
    [Arguments(232, 97, 0.42, 175.3)]
    [Arguments(242, 231, 0.24, 239.36)]
    [Arguments(26, 101, 0.08, 32)]
    [Arguments(28, 168, 0.17, 51.8)]
    [Arguments(45, 168, 0.24, 74.52)]
    public async Task ComplexAverageToTests(
        int rot1, int rot2, double percent, double outAngle) {
        Rotation2D r1 = new Rotation2D(rot1);
        Rotation2D r2 = new Rotation2D(rot2);

        r1.AverageTo(r2, percent);
        await Assert.That(r1.RotationAngle).IsEqualTo(outAngle);
        await Assert.That(r2.RotationAngle).IsEqualTo(rot2);
    }

    [Test]
    [Arguments(-1)]
    [Arguments(1.001)]
    [Arguments(230)]
    [Arguments(-928735)]
    public async Task AverageToThrowsErrorsTests(
        double average) {
        Rotation2D r1 = new Rotation2D();
        Rotation2D r2 = new Rotation2D();
        await Assert.That(() => r1.AverageTo(r2, average))
            .Throws<ArgumentOutOfRangeException>()
            .WithMessage("Percent must be between 0 and 1. (Parameter 'percent')");
    }
}
public class RotationClassMoveToTests {
    [Test]
    [Arguments(0, 90, 5)]
    [Arguments(0, 270, 355)]
    [Arguments(90, 0, 85)]
    [Arguments(90, 180, 95)]
    [Arguments(90, 93, 93)]
    [Arguments(90, 86, 86)]

    public async Task FiveDistanceTests(
        double start, double end, double expected) {
        Rotation2D r1 = new Rotation2D(start);
        Rotation2D r2 = new Rotation2D(end);

        r1.MoveTo(r2, 5);
        await Assert.That(r1.RotationAngle).IsEqualTo(expected);
    }

    [Test]
    [Arguments(123.06, 120.90, 5.20, 120.90)]
    [Arguments(192.75, 266.93, 3.39, 196.14)]
    [Arguments(149.43, 7.72, 0.72, 148.71)]
    [Arguments(47.26, 310.59, 1.67, 45.59)]
    [Arguments(282.71, 317.03, 4.38, 287.09)]
    [Arguments(111.51, 1.24, 6.33, 105.18)]
    [Arguments(319.33, 63.56, 8.34, 327.67)]
    [Arguments(63.36, 319.72, 7.82, 55.54)]
    [Arguments(55.40, 330.36, 1.27, 54.13)]
    [Arguments(167.29, 177.47, 7.00, 174.29)]
    public async Task RandomDistanceTests(
    double start, double end, double movingDistance, double expected) {
        Rotation2D r1 = new Rotation2D(start);

        r1.MoveTo(end, movingDistance);
        await Assert.That(r1.RotationAngle).IsEqualTo(expected);
    }
}
public class RotationClassAddOperatorTests {
    [Test]
    [Arguments(90, 0, 90)]
    [Arguments(260, 90, 350)]
    [Arguments(90, 45, 135)]
    [Arguments(360, 90, 90)]
    [Arguments(0, 90, 90)]
    [Arguments(0, 15.2, 15.2)]
    public async Task AdditionTests(
        double rot1, double rot2, double outAngle) {
        Rotation2D r1 = new Rotation2D(rot1);
        Rotation2D r2 = new Rotation2D(rot2);
        r1 += r2;
        await Assert.That(r1.RotationAngle).IsEqualTo(outAngle);
    }
}
public class RotationClassSubtractOperatorTests {
    [Test]
    [Arguments(90, 0, 90)]
    [Arguments(260, 90, 170)]
    [Arguments(90, 45, 45)]
    [Arguments(360, 90, 270)]
    [Arguments(0, 90, 270)]
    [Arguments(90, 4.5, 85.5)]
    public async Task SubtractionTests(
        double rot1, double rot2, double outAngle) {
        Rotation2D r1 = new Rotation2D(rot1);
        Rotation2D r2 = new Rotation2D(rot2);
        r1 -= r2;
        await Assert.That(r1.RotationAngle).IsEqualTo(outAngle);
    }
}
public class RotationClassAverageOperatorTests {
    [Test]
    [Arguments(0, 180, 90)]
    [Arguments(0, 90, 45)]
    [Arguments(0, 45, 22.5)]
    [Arguments(350, 10, 0)]
    [Arguments(330, 90, 30)]
    [Arguments(45, 180, 112.5)]
    public async Task AverageOperatorTests(
        double rot1, double rot2, double outAngle) {
        Rotation2D r1 = new Rotation2D(rot1);
        Rotation2D r2 = new Rotation2D(rot2);
        Rotation2D answer = r1 ^ r2;
        await Assert.That(answer.RotationAngle).IsEqualTo(outAngle);
    }
}
public class RotationClassModuloOperatorTests {
    [Test]
    [Arguments(360, 60, 0)]
    [Arguments(90, 35, 20)]
    [Arguments(180, 60, 0)]
    [Arguments(180, 50, 30)]
    [Arguments(90, 3.5, 2.5)]
    public async Task ModuloTests(
        double rot1, double rot2, double outAngle) {
        Rotation2D r1 = new Rotation2D(rot1);
        Rotation2D r2 = new Rotation2D(rot2);
        Rotation2D answer = r1 % r2;
        await Assert.That(answer.RotationAngle).IsEqualTo(outAngle);
    }
}
public class RotationClassImplicitOperatorTests {
    [Test]
    public async Task ImplicitConstructorAndExplicitReaderTest() {
        Rotation2D r1 = 90;
        await Assert.That(r1).IsEqualTo(90); // Apparently this also works
    }

    [Test]
    public async Task ExplicitSecondTest() {
        Rotation2D r1 = new Rotation2D(90);
        double d = (double)r1;
        await Assert.That(d).IsEqualTo(90);
    }
}
public class RotationClassEqualityInterfaceTests {
    [Test]
    [Arguments(0, 0, true)]
    [Arguments(0, 360, true)]
    [Arguments(245, 245, true)]
    [Arguments(0, 90, false)]
    [Arguments(18, 278, false)]
    [Arguments(245, 94, false)]
    [Arguments(245.25, 245.24, false)]
    [Arguments(245.25, 245.25, true)]
    public async Task EqualityInterfaceTests(
        double rot1, double rot2, bool equality) {
        Rotation2D r1 = new Rotation2D(rot1);
        Rotation2D r2 = new Rotation2D(rot2);
        await Assert.That(r1.Equals(r2)).IsEqualTo(equality);
    }
}
public class RotationClassComparableInterfaceTests {
    [Test]
    [Arguments(0, 0, 0)]
    [Arguments(90, 90, 0)]
    [Arguments(90, 45, 1)]
    [Arguments(360, 180, -1)] // tricky!
    [Arguments(34, 247, -1)]
    public async Task CompareToTests(
        double rot1, double rot2, int comparison) {
        Rotation2D r1 = new Rotation2D(rot1);
        Rotation2D r2 = new Rotation2D(rot2);
        await Assert.That(r1.CompareTo(r2)).IsEqualTo(comparison);
    }
}
public class RotationClassSmallTests {
    [Test]
    public async Task OneBigTest() {
        Rotation2D rot1 = new Rotation2D(45);
        Rotation2D rot2 = new Rotation2D(185);

        await Assert.That(rot1.Coords.X).IsEqualTo(0.71);
        await Assert.That(rot1.Coords.X).IsEqualTo(0.71);

        Rotation2D a1 = -rot1;
        rot1.FlipX();
        await Assert.That(a1 == rot1).IsTrue();
        rot1.FlipX();

        a1++;
        await Assert.That(a1.RotationAngle).IsEqualTo(316);

        a1--;
        await Assert.That(a1.RotationAngle).IsEqualTo(315);

        await Assert.That(a1 != rot1).IsEqualTo(true);
        await Assert.That(rot2 < a1).IsEqualTo(true);
        await Assert.That(rot2 > a1).IsEqualTo(false);
        await Assert.That(rot2 <= a1).IsEqualTo(true);
        await Assert.That(rot2 >= rot1).IsEqualTo(true);
        await Assert.That(rot2 < a1).IsEqualTo(true);

        Point2D p = a1 * 2;
        await Assert.That(p.X).IsEqualTo(1.42);
        await Assert.That(p.Y).IsEqualTo(-1.42);

        if (a1) { // continue
        } else { throw new Exception("a1 is not true"); }

        a1.RotationAngle = 0;

        if (!a1) { // continue
                   } else { throw new Exception("a1 is not false"); }
    }

    [Test]
    [Arguments(6.21, -6.55, 313.47)]
    [Arguments(-7.30, -4.11, 209.38)]
    [Arguments(7.09, -5.11, 324.22)]
    [Arguments(-4.71, -4.77, 225.36)]
    [Arguments(-0.22, 0.35, 122.15)]
    [Arguments(-8.10, 8.44, 133.82)]
    [Arguments(-1.88, 4.98, 110.68)]
    [Arguments(4.02, 5.66, 54.62)]
    [Arguments(5.77, -8.25, 304.97)]
    [Arguments(-1.13, 5.75, 101.12)]
    public async Task StaticAngleToTests(
        double x, double y, double expAngle) {
        await Assert.That(Rotation2D.AngleOf(x, y)).IsEqualTo(expAngle);
    }
}
