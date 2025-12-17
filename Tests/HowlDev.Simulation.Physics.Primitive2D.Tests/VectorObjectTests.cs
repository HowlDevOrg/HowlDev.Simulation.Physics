using HowlDev.Simulation.Physics.Primitve2D.Interfaces;
namespace HowlDev.Simulation.Physics.Primitve2D.Tests;


public class VectorObjectSimpleConstructorTests {
    [Test]
    public async Task Ctor1() {
        VectorObject2D obj = new VectorObject2D();
        await Assert.That(obj.CenterPoint.Equals(new Point2D())).IsTrue();
    }

    [Test]
    public async Task Ctor2() {
        VectorObject2D obj = new VectorObject2D(new Point2D(3, 4), new List<Vector2D> { new Vector2D(0, 1), new Vector2D(90, 1) });
        await Assert.That(obj.CenterPoint.Equals(new Point2D(3, 4))).IsTrue();
        await Assert.That(obj[0].Equals(new Point2D(4, 4))).IsTrue();
        await Assert.That(obj[1].Equals(new Point2D(3, 5))).IsTrue();
    }

    [Test]
    public async Task Ctor3() {
        VectorObject2D obj = new VectorObject2D(new Point2D(3, 4), new List<Point2D> { new Point2D(4, 4), new Point2D(3, 5) });
        await Assert.That(obj.CenterPoint.Equals(new Point2D(3, 4))).IsTrue();
        await Assert.That(obj[0].Equals(new Point2D(4, 4))).IsTrue();
        await Assert.That(obj[1].Equals(new Point2D(3, 5))).IsTrue();
    }
}
public class VectorObjectEnumerationTests {
    [Test]
    public async Task DefaultEnumerator() {
        VectorObject2D obj = new VectorObject2D(new Point2D(3, 4), new List<Point2D> { new Point2D(4, 4), new Point2D(3, 5) });
        Assert.Throws<EntryPointNotFoundException>(() => obj.GetEnumerator());

        //foreach (Point2D point in obj) {
        //    // Should throw error, just for display.
        //}
    }

    [Test]
    public async Task PointEnumerator() {
        VectorObject2D obj = new VectorObject2D(new Point2D(3, 4), new List<Point2D> { new Point2D(4, 4), new Point2D(3, 5) });
        Point2D[] answers = { new Point2D(4, 4), new Point2D(3, 5) };
        int answersIndex = 0;
        foreach (Point2D point in obj.GetPoints()) {
            await Assert.That(answers[answersIndex++].Equals(point)).IsTrue();
        }
    }

    [Test]
    public async Task LineEnumerator() {
        VectorObject2D obj = new VectorObject2D(new Point2D(0, 0), new List<Point2D> {
            new Point2D(1, 0), new Point2D(0, 1),
            new Point2D(-1, 0), new Point2D(0, -1)
        });
        Line2D[] answers = {
            new Line2D(1, 0, 0, 1),
            new Line2D(0, 1, -1, 0),
            new Line2D(-1, 0, 0, -1),
            new Line2D(0, -1, -1, 0)
        };
        int answersIndex = 0;
        foreach (Line2D line in obj.GetLines()) {
            await Assert.That(answers[answersIndex++].Equals(line)).IsTrue();
        }
    }
}
public class VectorObjectPointWithinTests {
    [Test]
    [Arguments(4, 9, true)]
    [Arguments(-10, 15, false)]
    [Arguments(6, 0.1, true)]
    [Arguments(20, 2, false)]
    [Arguments(9, 19, false)]
    [Arguments(8, 2, true)]
    [Arguments(2, 17, false)]
    [Arguments(4, -3, false)]
    [Arguments(12, -5, false)]
    [Arguments(-3, 19, false)]
    [Arguments(9, 6, true)]
    [Arguments(-1, 13, false)]
    [Arguments(5, 5, true)]
    public async Task ActualInsideTests(
    double x, double y, bool isInside) {
        List<Point2D> points = new List<Point2D> {
            new Point2D(0, 10),
            new Point2D(10, 10),
            new Point2D(10, 0),
            new Point2D(0, 0)
        };
        VectorObject2D obj = new VectorObject2D(new Point2D(5, 5), points);
        await Assert.That(obj.IsPointWithin(new Point2D(x, y))).IsEqualTo(isInside);
    }
}
public class VectorObjectEqualityTests {
    [Test]
    public async Task BasicTest1() {
        VectorObject2D v1 = new VectorObject2D(new Point2D(0, 0), new List<Point2D> { new Point2D(1, 2), new Point2D(1, 2), new Point2D(1, 2) });
        VectorObject2D v2 = new VectorObject2D(new Point2D(0, 0), new List<Point2D> { new Point2D(1, 2), new Point2D(1, 2), new Point2D(1, 2) });

        await Assert.That(v1.Equals(v2)).IsTrue();
    }
}
public class VectorObjectRotateByTests {
    [Test]
    public async Task EmptyTest() {
        VectorObject2D obj = new VectorObject2D(
            new Point2D(0, 0),
            new List<Vector2D> {
                new Vector2D(0, 1),
                new Vector2D(90, 1),
                new Vector2D(180, 1),
                new Vector2D(270, 1)
            });
        obj.RotateBy(new List<IPointObject2D>(), 90);

        List<Point2D> newPoints = new List<Point2D>(obj);
        await Assert.That(newPoints[0].Equals(new Point2D(0, 1))).IsTrue();
        await Assert.That(newPoints[1].Equals(new Point2D(-1, 0))).IsTrue();
        await Assert.That(newPoints[2].Equals(new Point2D(0, -1))).IsTrue();
        await Assert.That(newPoints[3].Equals(new Point2D(1, 0))).IsTrue();
    }

    //[Test]
    //public async Task NextToWallTest() {
    //    // (1, 0), (0, 1), (-1, 0), (0, -1)
    //    VectorObject2D obj = new VectorObject2D(
    //        new Point2D(0, 0),
    //        new List<Vector2D> {
    //            new Vector2D(0, 1),
    //            new Vector2D(90, 1),
    //            new Vector2D(180, 1),
    //            new Vector2D(270, 1)
    //        });

    //    // (-1, 2), (0, 3), (3, 0), (2, -1)
    //    VectorObject2D wall1 = new VectorObject2D(
    //        new Point2D(1, 1),
    //        new List<Point2D> {
    //                    new Point2D(-1, 2),
    //                    new Point2D(0, 3),
    //                    new Point2D(3, 0),
    //                    new Point2D(2, -1)
    //        });
    //    VectorObject2D wall2 = new VectorObject2D( // Validating exclusion filtering
    //        new Point2D(100, 100),
    //            new List<Point2D> {
    //                    new Point2D(80, 80),
    //                    new Point2D(80, 120),
    //                    new Point2D(120, 120),
    //                    new Point2D(120, 80)
    //        });

    //    // (1, 0), (0, 1), (-1, 0), (0, -1)
    //    VectorObject2D answer = new VectorObject2D(
    //        new Point2D(0, 0),
    //        new List<Vector2D> {
    //            new Vector2D(0, 1),
    //            new Vector2D(90, 1),
    //            new Vector2D(180, 1),
    //            new Vector2D(270, 1)
    //        });

    //    obj.RotateBy(new List<IPointObject2D>() { wall1, wall2 }, 30);

    //    await Assert.That(obj.Equals(answer)).IsTrue();
    //}
}