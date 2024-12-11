using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ENERPLUS
{
    internal class IDF
    {
        public enum MaterialType
        {
            Concrete,
            Brick,
            Wood,
            Glass,
            Insulation,
            Gypsum,
            Stone,
            Metal,

        }

        private class Zone
        {
            public string Name { get; set; }
            public double XOrigin { get; set; }
            public double YOrigin { get; set; }
            public double ZOrigin { get; set; }

          
            public List<BuildingSurface> Surfaces { get; set; } = new();

            
            public double Volume
            {
                get
                {
                    
                    return CalculateVolume();
                }
            }

            private double CalculateVolume()
            {
                
                return 0; // Заглушка
            }
        }

        
        private class BuildingSurface
        {
            

            public string Name { get; set; }
            public string SurfaceType { get; set; } 
            public string ZoneName { get; set; }    
            public string MaterialName { get; set; } 

            
            public List<(double X, double Y, double Z)> Vertices { get; set; } = new();

            public MaterialType Material { get; set; } 
        }

        private class Material
        {
            public MaterialType Type { get; set; }

            public enum MaterialType
            {
                Concrete,
                Brick,
                Wood,
                Glass,
                Insulation,
                Gypsum,
                Stone,
                Metal,
               
            }

            //public double Thickness { get; set; }
            //public double Conductivity { get; set; }
            //public double Density { get; set; }
            //public double SpecificHeat { get; set; }
        }

        private List<Zone> zones = new();
        private List<BuildingSurface> buildSurfaces = new();
        private List<Material> materials = new();

        public IDF() { }

        public void SetFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File doesn't exist", path);

            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Split('!')[0].Trim();

                    if (string.IsNullOrEmpty(line)) continue;

                    if (line.StartsWith("Zone,"))
                    {
                        ParseZone(reader);
                    }
                    else if (line.StartsWith("BuildingSurface:Detailed,"))
                    {
                        ParseBuildingSurface(reader);
                    }
                    else if (line.StartsWith("Material,"))
                    {
                        ParseMaterial(reader);
                    }
                }
            }

            LinkData();
        }

        private void LinkData()
        {
            foreach (var surface in buildSurfaces)
            {
                var zone = zones.FirstOrDefault(z => z.Name == surface.ZoneName);
                if (zone != null)
                {
                    zone.Surfaces.Add(surface);
                }

                var material = materials.FirstOrDefault(m => m.Name == surface.MaterialName);
                if (material != null)
                {
                    surface.Material = material;
                }
            }
        }

        private void ParseZone(StreamReader reader)
        {
            var zone = new Zone();
            string line;
            while ((line = reader.ReadLine()) != null && !line.EndsWith(";"))
            {
                string[] parts = line.Split(',').Select(p => p.Trim()).ToArray();
                if (parts.Length == 0) continue;

                if (line.Contains("Name"))
                    zone.Name = parts[0];
                else if (line.Contains("X Origin"))
                    zone.XOrigin = double.Parse(parts[0]);
                else if (line.Contains("Y Origin"))
                    zone.YOrigin = double.Parse(parts[0]);
                else if (line.Contains("Z Origin"))
                    zone.ZOrigin = double.Parse(parts[0]);
            }
            zones.Add(zone);
        }

        private void ParseBuildingSurface(StreamReader reader)
        {
            var surface = new BuildingSurface();
            string line;
            while ((line = reader.ReadLine()) != null && !line.EndsWith(";"))
            {
                string[] parts = line.Split(',').Select(p => p.Trim()).ToArray();
                if (parts.Length == 0) continue;

                if (line.Contains("Name"))
                    surface.Name = parts[0];
                else if (line.Contains("Zone Name"))
                    surface.ZoneName = parts[0];
                else if (line.Contains("Material Name"))
                    surface.MaterialName = parts[0];
                else if (line.Contains("Vertex"))
                {
                    double x = double.Parse(parts[0]);
                    double y = double.Parse(parts[1]);
                    double z = double.Parse(parts[2]);
                    surface.Vertices.Add((x, y, z));
                }
            }
            buildSurfaces.Add(surface);
        }

        private void ParseMaterial(StreamReader reader)
        {
            var material = new Material();
            string line;
            while ((line = reader.ReadLine()) != null && !line.EndsWith(";"))
            {
                string[] parts = line.Split(',').Select(p => p.Trim()).ToArray();
                if (parts.Length == 0) continue;

                if (line.Contains("Name"))
                    Enum.TryParse(parts[0], out material);
                //else if (line.Contains("Thickness"))
                //    material.Thickness = double.Parse(parts[0]);
                //else if (line.Contains("Conductivity"))
                //    material.Conductivity = double.Parse(parts[0]);
                //else if (line.Contains("Density"))
                //    material.Density = double.Parse(parts[0]);
                //else if (line.Contains("Specific Heat"))
                //    material.SpecificHeat = double.Parse(parts[0]);
            }
            materials.Add(material);
        }

        private void SaveFile()
        {
            //For make it later
        }
    }
}
