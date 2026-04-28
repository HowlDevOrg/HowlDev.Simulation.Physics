using BenchmarkDotNet.Attributes;

namespace HowlDev.Simulation.Physics.Primitive2D.Benchmarks;

[MemoryDiagnoser]
[BenchmarkCategory("Rotation")]
// [ShortRunJob]
[Config(typeof(CheapConfig))]
public class RotationInitBenchmark {
    [Params(120.1, 12503.3, -150.25, -20345.99)]
    public double Init;

    [Benchmark]
    public Rotation2D Initialization() => new Rotation2D(Init);
}

[MemoryDiagnoser]
[BenchmarkCategory("Rotation")]
// [ShortRunJob]
[Config(typeof(CheapConfig))]
public class RotationFlipBenchmark {
    [Params(20, 138, 210, 321)]
    public Rotation2D Flips;

    [Benchmark]
    public Rotation2D X_Flip() => Flips.FlipX();

    [Benchmark]
    public Rotation2D Y_Flip() => Flips.FlipY();
}

[MemoryDiagnoser]
[BenchmarkCategory("Rotation")]
// [ShortRunJob]
[Config(typeof(CheapConfig))]
public class RotationDistanceToBenchmark {
    [Params(0, 34)]
    public Rotation2D Angle;

    [Params(0, 90, 180, 270)]
    public double angleToCheck;

    [Benchmark]
    public Rotation2D DistanceTo() => Angle.DistanceTo(angleToCheck);
}