using BenchmarkDotNet.Running;

namespace HowlDev.Simulation.Physics.Primitive2D.Benchmarks;

public static class BenchmarkSelector {
    public static void RunRotationBenchmark() {
        BenchmarkRunner.Run<RotationInitBenchmark>();
        BenchmarkRunner.Run<RotationFlipBenchmark>();
        BenchmarkRunner.Run<RotationDistanceToBenchmark>();
    }
}