using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Generic;
using System.IO;

namespace ENERPLUS
{
    [SimpleJob(launchCount: 1, warmupCount: 1, iterationCount: 3)]
    public class IDFBenchmarks
    {
        private IDF _idf;
        private string _filePath;

        public IDFBenchmarks(string filePath)
        {
            _idf = new IDF();

            _filePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString() + "\\test_file";

            File.WriteAllText(_filePath, CreateTestFileContent());
        }

        private string CreateTestFileContent()
        {
            return @"
                Zone,
                LivingRoom,
                0,
                0,
                0;
                BuildingSurface:Detailed,
                Wall1,
                Wall,
                ConcreteWall,
                LivingRoom,
                Outdoors,
                ,
                SunExposed,
                WindExposed,
                4,
                0,0,0,
                5,0,0,
                5,5,0,
                0,5,0;
                Material,
                Concrete,
                0.2,
                1.4,
                2400,
                900;";
        }

        [Benchmark]
        public void LoadFile()
        {
            _idf.SetFile(_filePath);
        }

        [Benchmark]
        public void SaveFile()
        {
            _idf.SaveFile("output_file.idf");
        }

        [Benchmark]
        public void AddZone()
        {
            _idf.addZone("NewZone", (10.0, 10.0, 10.0));
        }

        [Benchmark]
        public void AddSurface()
        {
            _idf.addSurface("TestZone", "NewSurface", "Wall", "Outdoors",
                ("TestMaterial", 0.2, 0.05, 900, 1000));
        }

        [Benchmark]
        public void GetZones()
        {
            List<string> zones = _idf.getZones();
        }

        [Benchmark]
        public void EditZone()
        {
            _idf.editZone("TestZone", "UpdatedZone", (5.0, 5.0, 5.0));
        }
    }

}
