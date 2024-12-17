using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Xml.Linq;

namespace ENERPLUS
{
    public class IDF
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

        public enum BuildingSurfaceType
        {
            Wall,
            Floor,
            Roof,
            Window,
            Door,
            Ceiling,
            Foundation
        }

        public enum OutsideBoudaryCondition
        {
            Adiabatic,
            Outdoors
        }

        public enum Sun
        {
            SunExposed,
            NoSun
        }
        public enum Wind
        {
            WindExposed,
            NoWind
        }
        private class Zone
        {
            public string Name { get; set; }
            public (double X, double Y, double Z) Position;

            public List<BuildingSurface> Surfaces { get; set; } = new();
        }

        private class BuildingSurface
        {
            public string Name { get; set; }
            public BuildingSurfaceType Type {get; set;}
            public string ZoneName { get; set; }
            //public string MaterialName { get; set; }
            public string ConstructionName { get; set; }
            
            public List<(double X, double Y, double Z)> Vertices { get; set; } = new();
            //public MaterialType Material { get; set; }
            public OutsideBoudaryCondition outsideBoudary { get; set; }
        }

        private class Material
        {
            public string Name { get; set; }
            //public MaterialType Type { get; set; }
            public double Thickness { get; set; }
            public double Conductivity { get; set; }
            public double Density { get; set; }
            public double SpecificHeat { get; set; }
        }

        private class Construction
        {
            public string Name { get; set; }
            public string MaterialName { get; set; }
        }

        private List<Zone> zones = new();
        private List<BuildingSurface> buildSurfaces = new();
        private List<Material> materials = new();
        private List<Construction> constructions = new();
        private Sun sun = Sun.NoSun;
        private Wind wind = Wind.NoWind;

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
                    line = line.Split('!')[0].Trim(); // Remove comments

                    if (string.IsNullOrEmpty(line)) continue;

                    if (line.StartsWith("Zone,"))
                    {
                        ParseZone(line, reader);
                    }
                    else if (line.StartsWith("BuildingSurface:Detailed,"))
                    {
                        ParseBuildingSurface(line, reader);
                    }
                    else if (line.StartsWith("Material,"))
                    {
                        ParseMaterial(line, reader);
                    }
                    else if (line.StartsWith("Construction,"))
                    {
                        ParseConstruction(line, reader);
                    }
                }
            }

            //LinkData();
        }

        public string getSunStatus() => sun.ToString();

        public void setSunStatus(string status)  => Enum.TryParse(status, out sun);
        public string getWindStatus() => wind.ToString();
        public void setWindStatus(string status) => Enum.TryParse(status, out wind);

        public List<string> getZones() {
            List<string> zonesBack = new();
            foreach(var zone in zones)
                zonesBack.Add(zone.Name);
            
            return zonesBack;
        }


        public (double X, double Y, double Z) getZone(string name)
        {
            foreach(var zone in zones)
            {
                if (zone.Name == name)
                    return zone.Position;
            }
            return (0,0,0);
        }

        public List<string> getBuildingSurface(string zoneName)
        {
            List<string> result = new();
            foreach(var surface in buildSurfaces)
            {
                if (surface.ZoneName == zoneName)
                    result.Add(surface.Name);
            }
            return result;
        }

        public List<string> getVerticals(string nameSurface)
        {
            List<string> verticales = new();
            foreach (var surface in buildSurfaces)
            {
                if(surface.Name == nameSurface)
                {
                    foreach(var vertical in surface.Vertices)
                    {
                        verticales.Add($"X: {vertical.X} Y: {vertical.Y} Z: {vertical.Z}");
                    }
                }
            }
            return verticales;
        }

        public string getTypeOfSurface(string name)
        {
            foreach (var surface in buildSurfaces)
            {
                if(surface.Name == name)
                {
                    return surface.Type.ToString();
                }
            }
            return string.Empty;
        }

        public string getBoundaryOfSurface(string name)
        {
            foreach(var surface in buildSurfaces)
            {
                if(surface.Name == name)
                {
                    return surface.outsideBoudary.ToString();
                }
            }
            return string.Empty;
        }

        public (double X, double Y, double Z) getVerticalByToString(string name, string vertical)
        {
            foreach (var surface in buildSurfaces)
            {
                if(surface.Name == name)
                {
                    foreach(var vert in surface.Vertices)
                    {
                        if(vertical == $"X: {vert.X} Y: {vert.Y} Z: {vert.Z}")
                        {
                            return vert;
                        }
                    }
                }
            }
            return (0,0,0);
        }

        public (string Name, double Thickness, double Conductivity, double Density, double SpecificHeat) getMaterial(string nameOfSurface)
        {
            foreach (var surface in buildSurfaces)
            {
                if(surface.Name == nameOfSurface)
                {
                    foreach(var construction in constructions)
                    {
                        if(surface.ConstructionName == construction.Name)
                        {
                            foreach (var material in materials)
                            {
                                if(construction.MaterialName == material.Name)
                                {
                                    return (material.Name, material.Thickness, material.Conductivity, material.Density, material.SpecificHeat);
                                }
                            }
                        }
                    }
                }
            }
            return (string.Empty, 0, 0, 0, 0);
        }

        public void editZone(string lastNameZone, string updatedNameZone, (double X, double Y, double Z) position) 
        {
            foreach(var zone in zones)
            {
                if(zone.Name == lastNameZone)
                {
                    zone.Name = updatedNameZone;
                    zone.Position = position;
                }
            }

            foreach (var surface in buildSurfaces) 
            { 
                if(surface.ZoneName == lastNameZone)
                {
                    surface.ZoneName = updatedNameZone;
                }
            }
        }

        public void addZone(string name, (double X, double Y, double Z) position)
        {
            zones.Add(new Zone()
            {
                Name = name,
                Position = position
            });
        }

        public void removeZone(string name)
        {
            foreach (var zone in zones)
            {
                if(zone.Name == name)
                {
                    zones.Remove(zone);
                    break;
                }
            }

            List<BuildingSurface> remove = new();

            foreach(var surface in buildSurfaces)
            {
                if (surface.ZoneName == name)
                {
                    foreach(var construction in constructions)
                    {
                        if(surface.ConstructionName == construction.Name)
                        {
                            foreach(var material in materials)
                            {
                                if(material.Name == construction.MaterialName)
                                {
                                    materials.Remove(material);
                                    break;
                                }
                            }
                            constructions.Remove(construction);
                            break;
                        }
                    }
                    remove.Add(surface);
                }
            }

            foreach(var surface in remove)
            {
                buildSurfaces.Remove(surface);
            }
                
        }

        public void editSurface(string surfaceName, string surfaceNameUpdated, string TypeOfSurface, string Boundary,(string Name, double Thickness, double Conductivity, double Density, double SpecificHeat) material)
        {
            foreach (var surface in buildSurfaces)
            {
                if(surface.Name == surfaceName)
                {
                    surface.Name = surfaceNameUpdated;
                    if(Enum.TryParse(TypeOfSurface, out BuildingSurfaceType buildingSurfaceType))
                    {
                        surface.Type = buildingSurfaceType;
                    }
                    if (Enum.TryParse(Boundary, out OutsideBoudaryCondition outsideBoudary ))
                    {
                        surface.outsideBoudary = outsideBoudary;
                    }

                    foreach (var construction in constructions)
                    {
                        if(construction.Name == surface.ConstructionName) 
                        {
                            foreach(var mtrl in materials)
                            {
                                if(construction.MaterialName == mtrl.Name)
                                {
                                    mtrl.Name = material.Name;
                                    mtrl.Thickness = material.Thickness;
                                    mtrl.Conductivity = material.Conductivity;
                                    mtrl.Density = material.Density;
                                    mtrl.SpecificHeat = material.SpecificHeat;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void addSurface(string zoneName, string surfaceName, string TypeOfSurface, string Boundary, (string Name, double Thickness, double Conductivity, double Density, double SpecificHeat) material)
        {
            BuildingSurfaceType type = new();
            if (Enum.TryParse(TypeOfSurface, out BuildingSurfaceType buildingSurfaceType))
            {
                type = buildingSurfaceType;
            }
            OutsideBoudaryCondition outsideBoudary = new();
            if (Enum.TryParse(Boundary, out OutsideBoudaryCondition outsideBoudaryOut))
            {
                outsideBoudary = outsideBoudaryOut;
            }
            string constructionName = surfaceName + "Construction";
            buildSurfaces.Add(new()
            {
                Name = surfaceName,
                Type = type,
                ConstructionName = constructionName,
                outsideBoudary = outsideBoudary,
                Vertices = new(),
                ZoneName = zoneName
            });

            constructions.Add(new()
            {
                Name = constructionName,
                MaterialName = material.Name,
            });

            materials.Add(new()
            {
                Name = material.Name,
                Thickness = material.Thickness,
                Conductivity = material.Conductivity,
                Density = material.Density,
                SpecificHeat = material.SpecificHeat
            });
        }

        public void removeSurface(string surfaceName) 
        {
            foreach (var surface in buildSurfaces)
            {
                if (surface.Name == surfaceName)
                {
                    foreach (var construction in constructions)
                    {
                        if (surface.ConstructionName == construction.Name)
                        {
                            foreach (var material in materials)
                            {
                                if (material.Name == construction.MaterialName)
                                {
                                    materials.Remove(material);
                                    break;
                                }
                            }
                            constructions.Remove(construction);
                            break;
                        }
                    }
                    buildSurfaces.Remove(surface);
                    return;
                }
            }
        }

        public void editVertical(string vertical, (double X, double Y, double Z) verticalUpdate, string nameOfSurface)
        {
            foreach(var surface in buildSurfaces)
            {
                if(surface.Name == nameOfSurface)
                {
                    foreach(var vert in surface.Vertices)
                    {
                        if (vertical == $"X: {vert.X} Y: {vert.Y} Z: {vert.Z}")
                        {
                            surface.Vertices.Remove(vert);
                            surface.Vertices.Add(verticalUpdate);
                            return;
                        }
                    }
                }
            }
        }

        public void addVertical((double X, double Y, double Z) vertical, string nameOfSurface)
        {
            foreach(var surface in buildSurfaces)
            {
                if(surface.Name == nameOfSurface)
                {
                    surface.Vertices.Add(vertical);
                }
            }
        }

        public void removeVertical(string nameOfSurface, string verticalToString) 
        {
            foreach(var surface in buildSurfaces)
            {
                if(surface.Name == nameOfSurface)
                {
                    foreach(var vert in surface.Vertices)
                    {
                        if (verticalToString == $"X: {vert.X} Y: {vert.Y} Z: {vert.Z}")
                        {
                            surface.Vertices.Remove(vert);
                            return;
                        }
                    }
                }
            }
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

                //var material = materials.FirstOrDefault(m => m.Name == surface.MaterialName);
                //if (material != null)
                //{
                //    surface.Material = material.Type;
                //}
            }
        }

        private void ParseZone(string line, StreamReader reader)
        {
            var zone = new Zone();
            zone.Name = reader.ReadLine().Replace(" ", "").Split(',')[0];

            zone.Position = (
                double.Parse(reader.ReadLine().Split(',')[0]),
                double.Parse(reader.ReadLine().Split(',')[0]),
                double.Parse(reader.ReadLine().Split(';')[0]));

            zones.Add(zone);
        }

        private void ParseBuildingSurface(string line, StreamReader reader)
        {
            var surface = new BuildingSurface();
            //string[] parts = line.Split(',').Select(p => p.Trim()).ToArray();
            surface.Name = reader.ReadLine().Replace(" ", "").Split(',')[0];
            if (Enum.TryParse(reader.ReadLine().Replace(" ", "").Split(',')[0], out BuildingSurfaceType type)) {
                surface.Type = type;
            } // Surface Type
            surface.ConstructionName = reader.ReadLine().Replace(" ", "").Split(',')[0];
            surface.ZoneName = reader.ReadLine().Replace(" ", "").Split(',')[0];
            //surface.OutsideBoundaryCondition = reader.ReadLine().Replace(" ", "").Split(',')[0];
            if (Enum.TryParse(reader.ReadLine().Replace(" ", "").Split(',')[0], out OutsideBoudaryCondition outsideBoudary))
            {
                surface.outsideBoudary = outsideBoudary;
                reader.ReadLine();
            }
            else
                throw new Exception("File is shit");

            if (Enum.TryParse(reader.ReadLine().Replace(" ", "").Split(',')[0], out Sun sun))
                this.sun = sun;

            if (Enum.TryParse(reader.ReadLine().Replace(" ", "").Split(',')[0], out Wind wind))
                this.wind = wind;
                
            

            string vertexLine;
            bool quit = false;

            int NVerticales = int.Parse(reader.ReadLine().Replace(" ", "").Split(',')[0]);
            while ((vertexLine = reader.ReadLine()) != null)
            {
                if (vertexLine.EndsWith(";")) {
                    quit = true;
                }
                
                string[] vertexParts = vertexLine.Split(',', ';').Select(p => p.Trim()).ToArray();
                surface.Vertices.Add((
                    double.Parse(vertexParts[0]),
                    double.Parse(vertexParts[1]),
                    double.Parse(vertexParts[2])
                ));

                if (quit) 
                    break;
            }

            buildSurfaces.Add(surface);
        }

        private void ParseMaterial(string line, StreamReader reader)
        {
            var material = new Material();
            material.Name = reader.ReadLine().Replace(" ", "").Split(',')[0];

            material.Thickness = double.Parse(reader.ReadLine().Split(',')[0]);
            material.Conductivity = double.Parse(reader.ReadLine().Split(',')[0]);
            material.Density = double.Parse(reader.ReadLine().Split(',')[0]);
            material.SpecificHeat = double.Parse(reader.ReadLine().Split(';')[0]);

            materials.Add(material);
        }

        private void ParseConstruction(string line, StreamReader reader)
        {
            constructions.Add(new Construction()
            {
                Name = reader.ReadLine().Replace(" ", "").Split(',')[0],
                MaterialName = reader.ReadLine().Replace(" ", "").Split(',', ';')[0]
            });

        }

        public bool checkForSave()
        {
            foreach(var surface in buildSurfaces) 
            {
                if (surface.Vertices.Count < 3 || surface.Vertices.Count > 4)
                    return false;
            }
            return true;
        }
        public void SaveFile(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (var zone in zones)
                {
                    writer.WriteLine($"Zone,\n{zone.Name},");
                    writer.WriteLine($"{zone.Position.X},\n{zone.Position.Y},\n{zone.Position.Z};");
                }

                foreach (var surface in buildSurfaces)
                {
                    writer.WriteLine($"BuildingSurface:Detailed,\n" +
                        $"{surface.Name},\n" +
                        $"{surface.Type},\n" +
                        $"{surface.ConstructionName},\n" +
                        $"{surface.ZoneName},\n" +
                        $"{surface.outsideBoudary},\n" +
                        ",\n" +
                        $"{sun},\n" +
                        $"{wind},\n" +
                        $"{surface.Vertices.Count},");

                    int i = 0;
                    foreach (var vertex in surface.Vertices)
                    {
                        if(i == surface.Vertices.Count - 1)
                        {
                            writer.WriteLine($"{vertex.X},{vertex.Y},{vertex.Z};");
                        }
                        else
                            writer.WriteLine($"{vertex.X},{vertex.Y},{vertex.Z},");
                        i++;
                    }
                }

                foreach (var material in materials)
                {
                    writer.WriteLine($"Material,\n" +
                        $"{material.Name},\n" +
                        $"{material.Thickness},\n" +
                        $"{material.Conductivity},\n" +
                        $"{material.Density},\n" +
                        $"{material.SpecificHeat};"); 
                }
                
            }
        }
    }
}
