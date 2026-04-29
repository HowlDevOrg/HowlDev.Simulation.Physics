using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Exporters;

namespace HowlDev.Simulation.Physics.Primitive2D.Benchmarks;

/// <summary>
/// This is a good enough config for direction and rough estimates. You should debug with 
/// a simple [ShortJob], because I don't know how this library is supposed to work.
/// </summary>
internal class CheapConfig : ManualConfig {
    public CheapConfig() {
        AddJob(new Job(Job.Default) {
            Run = {
                LaunchCount = 5,
                WarmupCount = 50,
                IterationCount = 100,
                InvocationCount = 48
            },
            Accuracy = { MaxRelativeError = 0.05 }
        });
        AddLogger(ConsoleLogger.Default);
        AddColumn(TargetMethodColumn.Method,
            StatisticColumn.Mean,
            StatisticColumn.StdDev,
            StatisticColumn.Max,
            StatisticColumn.OperationsPerSecond
        );
        UnionRule = ConfigUnionRule.AlwaysUseLocal;
        AddAnalyser(EnvironmentAnalyser.Default);
        AddExporter(MarkdownExporter.GitHub);
    }
}
