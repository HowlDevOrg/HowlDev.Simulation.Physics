namespace HowlDev.Simulation.Physics.Primitive2D.Tests;

public class RotationClassSorting {
    [Test]
    public async Task RotationClassSortsCorrectly() {
        Rotation2D[] rotations = {
            new Rotation2D(112),
            new Rotation2D(284),
            new Rotation2D(315),
            new Rotation2D(255),
            new Rotation2D(335),
            new Rotation2D(192),
            new Rotation2D(316),
            new Rotation2D(77),
            new Rotation2D(285),
            new Rotation2D(78),
            new Rotation2D(29),
            new Rotation2D(242),
            new Rotation2D(230),
            new Rotation2D(193),
            new Rotation2D(109),
            new Rotation2D(244),
            new Rotation2D(31),
            new Rotation2D(222),
            new Rotation2D(268),
            new Rotation2D(100),
            new Rotation2D(0),
            new Rotation2D(360)
        };

        Array.Sort(rotations);

        Rotation2D[] answer = {
            new Rotation2D(0),
            new Rotation2D(360),
            new Rotation2D(29),
            new Rotation2D(31),
            new Rotation2D(77),
            new Rotation2D(78),
            new Rotation2D(100),
            new Rotation2D(109),
            new Rotation2D(112),
            new Rotation2D(192),
            new Rotation2D(193),
            new Rotation2D(222),
            new Rotation2D(230),
            new Rotation2D(242),
            new Rotation2D(244),
            new Rotation2D(255),
            new Rotation2D(268),
            new Rotation2D(284),
            new Rotation2D(285),
            new Rotation2D(315),
            new Rotation2D(316),
            new Rotation2D(335)
        };

        for (int i = 0; i < answer.Length; i++) {
            await Assert.That(rotations[i]).IsEqualTo(answer[i]);
        }
    }
}
public class PointClassSorting {
    [Test]
    public async Task IntegerPointClassSorting() {
        Point2D[] points = {
            new Point2D(6, 7),
            new Point2D(8, 5),
            new Point2D(-2, 5),
            new Point2D(-4, 0),
            new Point2D(0, -5),
            new Point2D(-1, 3),
            new Point2D(-8, -6),
            new Point2D(-8, 7),
            new Point2D(5, -10),
            new Point2D(9, 3),
            new Point2D(0, -10),
            new Point2D(-4, 4),
            new Point2D(9, -2),
            new Point2D(5, 1),
            new Point2D(-5, -8),
            new Point2D(-9, 1),
            new Point2D(-5, 4),
            new Point2D(7, -5),
            new Point2D(8, 6),
            new Point2D(7, 2),
            new Point2D(-4, 9),
            new Point2D(9, 0),
            new Point2D(7, 9),
            new Point2D(-9, 5),
            new Point2D(-10, 3),
            new Point2D(8, 0),
            new Point2D(1, -6),
            new Point2D(-8, 4),
            new Point2D(7, 3),
            new Point2D(3, -3)
        };

        Array.Sort(points);

        Point2D[] answer = {
            new Point2D(-10, 3),
            new Point2D(-9, 1),
            new Point2D(-9, 5),
            new Point2D(-8, -6),
            new Point2D(-8, 4),
            new Point2D(-8, 7),
            new Point2D(-5, -8),
            new Point2D(-5, 4),
            new Point2D(-4, 0),
            new Point2D(-4, 4),
            new Point2D(-4, 9),
            new Point2D(-2, 5),
            new Point2D(-1, 3),
            new Point2D(0, -10),
            new Point2D(0, -5),
            new Point2D(1, -6),
            new Point2D(3, -3),
            new Point2D(5, -10),
            new Point2D(5, 1),
            new Point2D(6, 7),
            new Point2D(7, -5),
            new Point2D(7, 2),
            new Point2D(7, 3),
            new Point2D(7, 9),
            new Point2D(8, 0),
            new Point2D(8, 5),
            new Point2D(8, 6),
            new Point2D(9, -2),
            new Point2D(9, 0),
            new Point2D(9, 3)
        };

        for (int i = 0; i < answer.Length; i++) {
            await Assert.That(points[i]).IsEqualTo(answer[i]);
        }
    }

    [Test]
    public async Task DoublePointClassSorting() {
        Point2D[] points = {
            new Point2D(-7.15, 6.17),
            new Point2D(9.31, 8.98),
            new Point2D(7.31, 1.23),
            new Point2D(-6.91, 0.55),
            new Point2D(2.89, 1.95),
            new Point2D(2.57, 8.69),
            new Point2D(-4.61, 0.93),
            new Point2D(-7.44, 8.49),
            new Point2D(5.81, 7.7),
            new Point2D(-8.02, 7.71),
            new Point2D(-7.4, -5.34),
            new Point2D(-6.11, 7.69),
            new Point2D(9.02, -5.67),
            new Point2D(2.74, 0.65),
            new Point2D(8.27, 5.81),
            new Point2D(-8.76, 7.76),
            new Point2D(-0.4, 7.46),
            new Point2D(-9.53, 1.88),
            new Point2D(4.32, -5.7),
            new Point2D(7.13, -5.43)
        };

        Array.Sort(points);

        Point2D[] answer = {
            new Point2D(-9.53, 1.88),
            new Point2D(-8.76, 7.76),
            new Point2D(-8.02, 7.71),
            new Point2D(-7.44, 8.49),
            new Point2D(-7.4, -5.34),
            new Point2D(-7.15, 6.17),
            new Point2D(-6.91, 0.55),
            new Point2D(-6.11, 7.69),
            new Point2D(-4.61, 0.93),
            new Point2D(-0.4, 7.46),
            new Point2D(2.57, 8.69),
            new Point2D(2.74, 0.65),
            new Point2D(2.89, 1.95),
            new Point2D(4.32, -5.7),
            new Point2D(5.81, 7.7),
            new Point2D(7.13, -5.43),
            new Point2D(7.31, 1.23),
            new Point2D(8.27, 5.81),
            new Point2D(9.02, -5.67),
            new Point2D(9.31, 8.98),
        };

        for (int i = 0; i < answer.Length; i++) {
            await Assert.That(points[i]).IsEqualTo(answer[i]);
        }
    }
}
public class EquationSorting {
    [Test]
    public async Task IntegerEquationSorting() {
        Equation2D[] equations = {
            new Equation2D(4, -6),
            new Equation2D(5, -5),
            new Equation2D(6, -10),
            new Equation2D(-10, -10),
            new Equation2D(1, -5),
            new Equation2D(-7, -3),
            new Equation2D(-3, -4),
            new Equation2D(9, -9),
            new Equation2D(8, 9),
            new Equation2D(-4, -2),
            new Equation2D(-7, -4),
            new Equation2D(9, -8),
            new Equation2D(-9, -4),
            new Equation2D(-9, -9),
            new Equation2D(-9, -7),
            new Equation2D(-9, -8),
            new Equation2D(-6, 0),
            new Equation2D(5, 6),
            new Equation2D(8, -9),
            new Equation2D(4, 5),
            new Equation2D(-4, -3),
            new Equation2D(8, -6),
            new Equation2D(4, -9),
            new Equation2D(3, 5),
            new Equation2D(7, 4),
            new Equation2D(8, -7),
            new Equation2D(4, 9),
            new Equation2D(9, 7),
            new Equation2D(-1, 1),
            new Equation2D(8, -8)
        };

        Array.Sort(equations);

        Equation2D[] answer = {
            new Equation2D(-10, -10),
            new Equation2D(-9, -9),
            new Equation2D(-9, -8),
            new Equation2D(-9, -7),
            new Equation2D(-9, -4),
            new Equation2D(-7, -4),
            new Equation2D(-7, -3),
            new Equation2D(-6, 0),
            new Equation2D(-4, -3),
            new Equation2D(-4, -2),
            new Equation2D(-3, -4),
            new Equation2D(-1, 1),
            new Equation2D(1, -5),
            new Equation2D(3, 5),
            new Equation2D(4, -9),
            new Equation2D(4, -6),
            new Equation2D(4, 5),
            new Equation2D(4, 9),
            new Equation2D(5, -5),
            new Equation2D(5, 6),
            new Equation2D(6, -10),
            new Equation2D(7, 4),
            new Equation2D(8, -9),
            new Equation2D(8, -8),
            new Equation2D(8, -7),
            new Equation2D(8, -6),
            new Equation2D(8, 9),
            new Equation2D(9, -9),
            new Equation2D(9, -8),
            new Equation2D(9, 7)
        };

        for (int i = 0; i < answer.Length; i++) {
            await Assert.That(equations[i]).IsEqualTo(answer[i]);
        }
    }

    [Test]
    public async Task DoubleEquationSorting() {
        Equation2D[] equations = {
            new Equation2D(-7.15, -0.28),
            new Equation2D(-4.22, 2.44),
            new Equation2D(9.04, -8.65),
            new Equation2D(7.17, -6.15),
            new Equation2D(-5.17, -5.67),
            new Equation2D(5.03, 6.96),
            new Equation2D(-1.48, -5.93),
            new Equation2D(9.25, 3.71),
            new Equation2D(-5.71, 3.68),
            new Equation2D(-2.18, 4.75),
            new Equation2D(8.48, 7.18),
            new Equation2D(3.65, -7.15),
            new Equation2D(-5.98, -8.07),
            new Equation2D(-9.82, 0.6),
            new Equation2D(2.54, -1.02),
            new Equation2D(-9.51, 3.29),
            new Equation2D(8.95, -2.48),
            new Equation2D(-7.82, 2.09),
            new Equation2D(2.15, 6.67),
            new Equation2D(4.82, -8.54)
        };

        Array.Sort(equations);

        Equation2D[] answer = {
            new Equation2D(-9.82, 0.6),
            new Equation2D(-9.51, 3.29),
            new Equation2D(-7.82, 2.09),
            new Equation2D(-7.15, -0.28),
            new Equation2D(-5.98, -8.07),
            new Equation2D(-5.71, 3.68),
            new Equation2D(-5.17, -5.67),
            new Equation2D(-4.22, 2.44),
            new Equation2D(-2.18, 4.75),
            new Equation2D(-1.48, -5.93),
            new Equation2D(2.15, 6.67),
            new Equation2D(2.54, -1.02),
            new Equation2D(3.65, -7.15),
            new Equation2D(4.82, -8.54),
            new Equation2D(5.03, 6.96),
            new Equation2D(7.17, -6.15),
            new Equation2D(8.48, 7.18),
            new Equation2D(8.95, -2.48),
            new Equation2D(9.04, -8.65),
            new Equation2D(9.25, 3.71),
        };

        for (int i = 0; i < answer.Length; i++) {
            await Assert.That(equations[i]).IsEqualTo(answer[i]);
        }
    }
}
// public class LineSorting {
//     [Test]
//     public async Task IntegerLineSorting() {
//         Line2D[] lines = {
//             new Line2D(2, -1, 5, 0),
//             new Line2D(-4, -3, -2, 3),
//             new Line2D(2, 3, 4, 1),
//             new Line2D(-3, -3, -1, -5),
//             new Line2D(-4, 2, 4, -4),
//             new Line2D(1, 0, -4, 2),
//             new Line2D(-5, 1, -1, 3),
//             new Line2D(-5, -4, -3, -1),
//             new Line2D(2, 5, -1, -5),
//             new Line2D(-1, 1, 2, -3),
//             new Line2D(2, 3, 1, -2),
//             new Line2D(2, 5, -5, 2),
//             new Line2D(1, -1, -4, 1),
//             new Line2D(0, 4, -1, -1),
//             new Line2D(-4, -1, -3, -1),
//             new Line2D(-3, -5, 4, 3),
//             new Line2D(-1, 1, -3, 1),
//             new Line2D(-3, 2, -4, 1),
//             new Line2D(-5, -3, -3, -4),
//             new Line2D(1, 0, 3, 5),
//             new Line2D(5, -5, 2, 5),
//             new Line2D(2, -1, 4, -3),
//             new Line2D(3, 5, -4, 0),
//             new Line2D(-1, 2, -4, -2),
//             new Line2D(2, 4, 5, 3),
//             new Line2D(-5, 5, -2, 4),
//             new Line2D(-1, -1, 5, -3),
//             new Line2D(2, 0, 1, -2),
//             new Line2D(4, -2, -4, 4),
//             new Line2D(-4, -1, -3, 1)
//         };

//         Array.Sort(lines);

//         Line2D[] answer = {
//             new Line2D(-5, -4, -3, -1),
//             new Line2D(-5, -3, -3, -4),
//             new Line2D(-5, 1, -1, 3),
//             new Line2D(-5, 5, -2, 4),
//             new Line2D(-4, -3, -2, 3),
//             new Line2D(-4, -1, -3, -1),
//             new Line2D(-4, -1, -3, 1),
//             new Line2D(-4, 2, 4, -4),
//             new Line2D(-3, -5, 4, 3),
//             new Line2D(-3, -3, -1, -5),
//             new Line2D(-3, 2, -4, 1),
//             new Line2D(-1, -1, 5, -3),
//             new Line2D(-1, 1, -3, 1),
//             new Line2D(-1, 1, 2, -3),
//             new Line2D(-1, 2, -4, -2),
//             new Line2D(0, 4, -1, -1),
//             new Line2D(1, -1, -4, 1),
//             new Line2D(1, 0, -4, 2),
//             new Line2D(1, 0, 3, 5),
//             new Line2D(2, -1, 4, -3),
//             new Line2D(2, -1, 5, 0),
//             new Line2D(2, 0, 1, -2),
//             new Line2D(2, 3, 1, -2),
//             new Line2D(2, 3, 4, 1),
//             new Line2D(2, 4, 5, 3),
//             new Line2D(2, 5, -5, 2),
//             new Line2D(2, 5, -1, -5),
//             new Line2D(3, 5, -4, 0),
//             new Line2D(4, -2, -4, 4),
//             new Line2D(5, -5, 2, 5),
//         };

//         for (int i = 0; i < answer.Length; i++) {
//             await Assert.That(lines[i]).IsEqualTo(answer[i]);
//         }
//     }
// }
public class VectorSorting {
    [Test]
    public async Task FullSorting() {
        Vector2D[] momentum = {
            new Vector2D(295, 0.63),
            new Vector2D(210, 3.38),
            new Vector2D(275, 3.16),
            new Vector2D(81, 2.83),
            new Vector2D(317, 0.87),
            new Vector2D(64, 2.54),
            new Vector2D(177, 3.03),
            new Vector2D(254, 3.95),
            new Vector2D(59, 2.76),
            new Vector2D(344, 0.43),
            new Vector2D(263, 2.04),
            new Vector2D(316, 0.77),
            new Vector2D(258, 0.38),
            new Vector2D(311, 1.11),
            new Vector2D(80, 2.22),
            new Vector2D(244, 2.05),
            new Vector2D(46, 3.64),
            new Vector2D(314, 3.78),
            new Vector2D(358, 0.17),
            new Vector2D(272, 0.55)
        };

        Array.Sort(momentum);

        Vector2D[] answer = {
            new Vector2D(358, 0.17),
            new Vector2D(258, 0.38),
            new Vector2D(344, 0.43),
            new Vector2D(272, 0.55),
            new Vector2D(295, 0.63),
            new Vector2D(316, 0.77),
            new Vector2D(317, 0.87),
            new Vector2D(311, 1.11),
            new Vector2D(263, 2.04),
            new Vector2D(244, 2.05),
            new Vector2D(80, 2.22),
            new Vector2D(64, 2.54),
            new Vector2D(59, 2.76),
            new Vector2D(81, 2.83),
            new Vector2D(177, 3.03),
            new Vector2D(275, 3.16),
            new Vector2D(210, 3.38),
            new Vector2D(46, 3.64),
            new Vector2D(314, 3.78),
            new Vector2D(254, 3.95),
        };

        for (int i = 0; i < answer.Length; i++) {
            await Assert.That(momentum[i]).IsEqualTo(answer[i]);
        }
    }
}