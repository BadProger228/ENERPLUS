using Microsoft.VisualStudio.TestTools.UnitTesting;
using ENERPLUS;
using System.Collections.Generic;
using System.IO;

namespace ENERPLUS.Tests
{
    [TestClass]
    public class IDFTests
    {
        private IDF _idf;

        [TestInitialize]
        public void Setup()
        {
            _idf = new IDF();
        }

        [TestMethod]
        public void SetSunStatus_ShouldUpdateSunStatus()
        {
            // Act
            _idf.setSunStatus("SunExposed");
            var result = _idf.getSunStatus();

            // Assert
            Assert.AreEqual("SunExposed", result);
        }

        [TestMethod]
        public void SetWindStatus_ShouldUpdateWindStatus()
        {
            // Act
            _idf.setWindStatus("WindExposed");
            var result = _idf.getWindStatus();

            // Assert
            Assert.AreEqual("WindExposed", result);
        }

        [TestMethod]
        public void AddZone_ShouldAddNewZone()
        {
            // Arrange
            string zoneName = "LivingRoom";
            var position = (X: 0.0, Y: 0.0, Z: 0.0);

            // Act
            _idf.addZone(zoneName, position);
            var zones = _idf.getZones();

            // Assert
            Assert.AreEqual(1, zones.Count);
            Assert.AreEqual(zoneName, zones[0]);
        }

        [TestMethod]
        public void RemoveZone_ShouldRemoveZoneAndRelatedSurfaces()
        {
            // Arrange
            string zoneName = "Kitchen";
            var position = (X: 1.0, Y: 1.0, Z: 1.0);
            _idf.addZone(zoneName, position);

            // Act
            _idf.removeZone(zoneName);
            var zones = _idf.getZones();

            // Assert
            Assert.AreEqual(0, zones.Count);
        }

        [TestMethod]
        public void AddSurface_ShouldAddSurfaceToZone()
        {
            // Arrange
            string zoneName = "LivingRoom";
            _idf.addZone(zoneName, (0.0, 0.0, 0.0));
            var material = ("Material1", 0.1, 0.2, 500.0, 1000.0);

            // Act
            _idf.addSurface(zoneName, "Wall1", "Wall", "Adiabatic", material);
            var surfaces = _idf.getBuildingSurface(zoneName);

            // Assert
            Assert.AreEqual(1, surfaces.Count);
            Assert.AreEqual("Wall1", surfaces[0]);

            
        }

        [TestMethod]
        public void EditZone_ShouldUpdateZoneProperties()
        {
            // Arrange
            string oldName = "OldZone";
            string updatedName = "UpdatedZone";
            var position = (X: 2.0, Y: 3.0, Z: 4.0);

            _idf.addZone(oldName, (0.0, 0.0, 0.0));

            // Act
            _idf.editZone(oldName, updatedName, position);
            var zones = _idf.getZones();
            var newPosition = _idf.getZone(updatedName);

            // Assert
            Assert.AreEqual(1, zones.Count);
            Assert.AreEqual(updatedName, zones[0]);
            Assert.AreEqual(position, newPosition);
        }

        [TestMethod]
        public void RemoveSurface_ShouldRemoveSurfaceFromZone()
        {
            // Arrange
            string zoneName = "LivingRoom";
            string surfaceName = "Wall1";
            _idf.addZone(zoneName, (0.0, 0.0, 0.0));
            var material = ("Material1", 0.1, 0.2, 500.0, 1000.0);
            _idf.addSurface(zoneName, surfaceName, "Wall", "Adiabatic", material);

            // Act
            _idf.removeSurface(surfaceName);
            var surfaces = _idf.getBuildingSurface(zoneName);

            // Assert
            Assert.AreEqual(0, surfaces.Count);
        }

        [TestMethod]
        public void AddVertical_ShouldAddVertexToSurface()
        {
            // Arrange
            string zoneName = "LivingRoom";
            string surfaceName = "Wall1";
            _idf.addZone(zoneName, (0.0, 0.0, 0.0));
            var material = ("Material1", 0.1, 0.2, 500.0, 1000.0);
            _idf.addSurface(zoneName, surfaceName, "Wall", "Adiabatic", material);

            var vertex = (X: 1.0, Y: 2.0, Z: 3.0);

            // Act
            _idf.addVertical(vertex, surfaceName);
            var vertices = _idf.getVerticals(surfaceName);

            // Assert
            Assert.AreEqual(1, vertices.Count);
            Assert.AreEqual("X: 1 Y: 2 Z: 3", vertices[0]);
        }

        [TestMethod]
        public void RemoveVertical_ShouldRemoveVertexFromSurface()
        {
            // Arrange
            string zoneName = "LivingRoom";
            string surfaceName = "Wall1";
            var vertex = (X: 1.0, Y: 2.0, Z: 3.0);

            _idf.addZone(zoneName, (0.0, 0.0, 0.0));
            var material = ("Material1", 0.1, 0.2, 500.0, 1000.0);
            _idf.addSurface(zoneName, surfaceName, "Wall", "Adiabatic", material);
            _idf.addVertical(vertex, surfaceName);

            // Act
            _idf.removeVertical(surfaceName, "X: 1 Y: 2 Z: 3");
            var vertices = _idf.getVerticals(surfaceName);

            // Assert
            Assert.AreEqual(0, vertices.Count);
        }
    }
}
