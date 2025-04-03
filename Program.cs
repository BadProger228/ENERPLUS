using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using ENERPLUS;
using System.Diagnostics;
using System.Text.Json;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Exporters.Json;
using System.Xml;
using Newtonsoft.Json;

namespace ENERPLUS
{
    internal static class Program
    {
        static string CurrentPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString();
        
        [STAThread]
        static void Main()
        {


            MessageBox.Show(CurrentPath);

            var summary = BenchmarkRunner.Run<IDFBenchmarks>();
            SaveBenchmarkResults(summary);
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
        private static void SaveBenchmarkResults(BenchmarkDotNet.Reports.Summary summary)
        {
            var results = new List<Dictionary<string, object>>();

            foreach (var report in summary.Reports)
            {
                var methodName = report.BenchmarkCase.Descriptor.WorkloadMethod.Name;
                var meanTime = report.ResultStatistics.Mean;

                results.Add(new Dictionary<string, object>
                {
                    { "Method", methodName },
                    { "MeanTime", meanTime }
                });
            }

            var json = System.Text.Json.JsonSerializer.Serialize(results, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(CurrentPath + "\\benchmark_results.json", json);
        }

    }
}
