using OpenTK.GLControl;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform.Windows;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenTK.Mathematics;
using System.Windows.Forms;

namespace ENERPLUS
{
    public partial class Form1 : Form
    {
        private GLControl glControl;
        private Project project;
        private string choosedZone;
        public Form1()
        {
            InitializeComponent();
            ApplyModernStyle();

            project = new();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ZoneListBoxRefresh();
            SunStatusComboBox.SelectedItem = project.file.getSunStatus();
            WindStatusComboBox.SelectedItem = project.file.getWindStatus();
            enabledZoneEditForm(true);
            enabledSurfaceEditForm(false);
            enabledVerticalEditForm(false);
        }

        private void ZoneListBoxRefresh()
        {
            ZoneListBox.Items.Clear();
            foreach (var zone in project.file.getZones())
            {
                ZoneListBox.Items.Add(zone);
            }
        }

        private bool checkZoneListBoxFor(string item)
        {
            foreach (var zone in project.file.getZones())
            {
                if (zone == item)
                {
                    return true;
                }
            }
            return false;
        }

        private bool checkSurfaceListBoxFor(string item)
        {
            foreach (var surface in BuildingSurfaceListBox.Items)
            {
                if (item.Equals(surface))
                {
                    return true;
                }
            }
            return false;
        }

        private bool checkVerticalListFor(string item)
        {
            foreach (var vertical in VerticalesListBox.Items)
            {
                if (item.Equals(vertical))
                {
                    return true;
                }
            }
            return false;
        }

        private bool checkPossibleToSaveFile()
        {
            return project.file.checkForSave();
        }

        private void SurfaceListBoxRefresh()
        {
            BuildingSurfaceListBox.Items.Clear();
            foreach (var surface in project.file.getBuildingSurface(choosedZone))
            {
                BuildingSurfaceListBox.Items.Add(surface);
            }
        }

        private void cleanVerticalEditForms()
        {
            VerticalX.Text = string.Empty;
            VerticalY.Text = string.Empty;
            VerticalZ.Text = string.Empty;
        }

        private void enabledVerticalEditForm(bool boolean)
        {
            VerticalX.Enabled = boolean;
            VerticalY.Enabled = boolean;
            VerticalZ.Enabled = boolean;

            AddVerticalButton.Enabled = boolean;
            EditVerticalButton.Enabled = boolean;
        }

        private void cleanSurfaceEditForms()
        {
            SurfaceNameTextBox.Text = string.Empty;

            TypeOfSurfaceComboBox.SelectedItem = null;
            OutsideBoundaryConditionComboBox.SelectedItem = null;

            MaterialNameTextBox.Text = string.Empty;
            ThicknessTextBox.Text = string.Empty;
            ConductivityTextBox.Text = string.Empty;
            DensityTextBox.Text = string.Empty;
            SpecificHeatTextBox.Text = string.Empty;
        }

        private void enabledSurfaceEditForm(bool boolean)
        {
            SurfaceNameTextBox.Enabled = boolean;

            TypeOfSurfaceComboBox.Enabled = boolean;
            OutsideBoundaryConditionComboBox.Enabled = boolean;

            MaterialNameTextBox.Enabled = boolean;
            ThicknessTextBox.Enabled = boolean;
            ConductivityTextBox.Enabled = boolean;
            DensityTextBox.Enabled = boolean;
            SpecificHeatTextBox.Enabled = boolean;

            AddSurfaceButton.Enabled = boolean;
            EditSurfaceButton.Enabled = boolean;
        }

        private void cleanZoneEditForms()
        {
            ZoneNameTextBox.Text = string.Empty;
            XTextBox.Text = string.Empty;
            YTextBox.Text = string.Empty;
            ZTextBox.Text = string.Empty;
        }

        private void enabledZoneEditForm(bool boolean)
        {
            ZoneNameTextBox.Enabled = boolean;
            XTextBox.Enabled = boolean;
            YTextBox.Enabled = boolean;
            ZTextBox.Enabled = boolean;

            AddZoneButton.Enabled = boolean;
            EditZoneButton.Enabled = boolean;
        }


        private void GlControl_Load(object sender, EventArgs e)
        {
            // Настройка OpenGL
            GL.ClearColor(Color4.CornflowerBlue);
        }

        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            // Рисование 3D
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Rotate(30, 1.0, 0.0, 0.0); // Поворот для обзора
            GL.Rotate(30, 0.0, 1.0, 0.0);

            DrawZone();

            glControl.SwapBuffers();
        }

        private void DrawZone()
        {
            // Пример: нарисовать зону на основе вершин
            var vertices = new List<(double X, double Y, double Z)>
            {
                (0, 0, 0),
                (1, 0, 0),
                (1, 1, 0),
                (0, 1, 0)
            };

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.0, 1.0, 0.0); // Зеленый цвет

            foreach (var vertex in vertices)
            {
                GL.Vertex3(vertex.X, vertex.Y, vertex.Z);
            }

            GL.End();
        }

        private void ZoneListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ZoneListBox.SelectedItem == null)
                return;

            if (choosedZone == ZoneListBox.SelectedItem.ToString())
                return;

            BuildingSurfaceListBox.Items.Clear();
            VerticalesListBox.Items.Clear();

            enabledZoneEditForm(true);
            enabledSurfaceEditForm(true);
            enabledVerticalEditForm(false);

            cleanSurfaceEditForms();
            cleanVerticalEditForms();


            choosedZone = ZoneListBox.SelectedItem.ToString();
            ZoneNameTextBox.Text = choosedZone;

            SurfaceListBoxRefresh();
            (double X, double Y, double Z) = project.file.getZone(choosedZone);

            XTextBox.Text = X.ToString();
            YTextBox.Text = Y.ToString();
            ZTextBox.Text = Z.ToString();

        }

        private void BuildingSurfaceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BuildingSurfaceListBox.SelectedItem == null)
                return;

            string surface = BuildingSurfaceListBox.SelectedItem.ToString();


            VerticalesListBox.Items.Clear();
            cleanVerticalEditForms();

            enabledSurfaceEditForm(true);
            enabledVerticalEditForm(true);

            foreach (var vetrical in project.file.getVerticals(surface))
            {
                VerticalesListBox.Items.Add(vetrical);
            }

            SurfaceNameTextBox.Text = surface;

            TypeOfSurfaceComboBox.SelectedItem = project.file.getTypeOfSurface(surface);
            OutsideBoundaryConditionComboBox.SelectedItem = project.file.getBoundaryOfSurface(surface);


            (string Name, double Thickness, double Conductivity, double Density, double SpecificHeat) = project.file.getMaterial(surface);

            MaterialNameTextBox.Text = Name;
            ThicknessTextBox.Text = Thickness.ToString();
            ConductivityTextBox.Text = Conductivity.ToString();
            DensityTextBox.Text = Density.ToString();
            SpecificHeatTextBox.Text = SpecificHeat.ToString();



        }

        private void VerticalesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (VerticalesListBox.SelectedItem == null)
                return;
            (double X, double Y, double Z) = project.file.getVerticalByToString(BuildingSurfaceListBox.SelectedItem.ToString(), VerticalesListBox.SelectedItem.ToString());

            VerticalX.Text = X.ToString();
            VerticalY.Text = Y.ToString();
            VerticalZ.Text = Z.ToString();

        }

        private void EditZoneButton_Click(object sender, EventArgs e)
        {
            if (ZoneListBox.SelectedItem == null)
            {
                MessageBox.Show("You didn't choose the item to change");
                return;
            }

            if (ZoneNameTextBox.Text == string.Empty)
            {
                MessageBox.Show("The string of zone name is empty");
                return;
            }

            if (ZoneNameTextBox.Text != ZoneListBox.SelectedItem.ToString())
            {
                foreach (var item in ZoneListBox.Items)
                {
                    if (item.ToString() == ZoneNameTextBox.Text)
                    {
                        MessageBox.Show("You can't edit the item with the same name as one item from the list!");
                        return;
                    }
                }
            }

            string lastZoneName = ZoneListBox.SelectedItem.ToString();
            string updatedZoneName = ZoneNameTextBox.Text;

            if (updatedZoneName == string.Empty)
                return;

            var position = (int.Parse(XTextBox.Text), int.Parse(YTextBox.Text), int.Parse(ZTextBox.Text));

            project.file.editZone(lastZoneName, updatedZoneName, position);

            ZoneListBoxRefresh();

            choosedZone = updatedZoneName;
            ZoneListBox.SelectedItem = updatedZoneName;
        }



        private void AddZoneButton_Click(object sender, EventArgs e)
        {
            string zoneName = ZoneNameTextBox.Text;

            if (zoneName == string.Empty)
            {
                MessageBox.Show("String is empty");
                return;
            }


            if (checkZoneListBoxFor(zoneName))
            {
                MessageBox.Show("The item already exist at the list");
                return;
            }

            bool result = true;

            if (!double.TryParse(XTextBox.Text, out double x))
                result = false;
            if (!double.TryParse(YTextBox.Text, out double y))
                result = false;
            if (!double.TryParse(ZTextBox.Text, out double z))
                result = false;

            if (!result)
            {
                MessageBox.Show("Impossible value for the vertical");
                return;
            }
            var position = (x, y, z);
            project.file.addZone(zoneName, position);

            ZoneListBoxRefresh();
            ZoneListBox.SelectedItem = ZoneNameTextBox.Text;
            choosedZone = ZoneNameTextBox.Text;

            cleanSurfaceEditForms();
            cleanVerticalEditForms();
        }

        private void EditSurfaceButton_Click(object sender, EventArgs e)
        {
            if (BuildingSurfaceListBox.SelectedItem == null)
            {
                MessageBox.Show("You didn't choose the item to change");
                return;
            }

            if (SurfaceNameTextBox.Text != BuildingSurfaceListBox.SelectedItem.ToString())
            {
                foreach (var item in BuildingSurfaceListBox.Items)
                {
                    if (item.ToString() == SurfaceNameTextBox.Text)
                    {
                        MessageBox.Show("You can't edit the item with the same name as one of the list!");
                        return;
                    }
                }
            }


            if (BuildingSurfaceListBox.SelectedItem == null)
            {
                MessageBox.Show("You didn't choose the item to change");
                return;
            }

            var lastSurfaceName = BuildingSurfaceListBox.SelectedItem.ToString();
            var surfaceNameUpdated = SurfaceNameTextBox.Text;
            var Type = TypeOfSurfaceComboBox.SelectedItem.ToString();
            var Boundary = OutsideBoundaryConditionComboBox.SelectedItem.ToString();

            var material = (MaterialNameTextBox.Text, double.Parse(ThicknessTextBox.Text), double.Parse(ConductivityTextBox.Text), double.Parse(DensityTextBox.Text), double.Parse(SpecificHeatTextBox.Text));

            project.file.editSurface(lastSurfaceName, surfaceNameUpdated, Type, Boundary, material);

            SurfaceListBoxRefresh();
            BuildingSurfaceListBox.SelectedItem = SurfaceNameTextBox.Text;
        }

        private void AddSurfaceButton_Click(object sender, EventArgs e)
        {
            try
            {
                var zoneName = ZoneListBox.SelectedItem.ToString();
                var surfaceName = SurfaceNameTextBox.Text;
                var Type = TypeOfSurfaceComboBox.SelectedItem.ToString();
                var Boundary = OutsideBoundaryConditionComboBox.SelectedItem.ToString();

                var materialName = MaterialNameTextBox.Text;
                if (checkSurfaceListBoxFor(surfaceName))
                {
                    MessageBox.Show("The item already exist at the list");
                    return;
                }

                if (zoneName == string.Empty || surfaceName == string.Empty ||
                    Type == string.Empty || Boundary == string.Empty || materialName == string.Empty)
                {
                    MessageBox.Show("Enter and choose any textboxes");
                    return;
                }

                if (!(double.TryParse(ThicknessTextBox.Text, out double Thickness) &&
                    double.TryParse(ConductivityTextBox.Text, out double Conductivity) &&
                    double.TryParse(DensityTextBox.Text, out double Density) &&
                    double.TryParse(SpecificHeatTextBox.Text, out double SpecificHeat)))
                {
                    MessageBox.Show("Enter correct values");
                    return;
                }


                var material = (materialName, Thickness, Conductivity, Density, SpecificHeat);
                project.file.addSurface(zoneName, surfaceName, Type, Boundary, material);

                SurfaceListBoxRefresh();
                BuildingSurfaceListBox.SelectedItem = SurfaceNameTextBox.Text;
                VerticalesListBox.Items.Clear();
                cleanVerticalEditForms();
            }
            catch
            {
                MessageBox.Show("Enter correct values");
                return;
            }

        }

        private void AddVerticalButton_Click(object sender, EventArgs e)
        {


            if (!(double.TryParse(VerticalX.Text, out double x) && double.TryParse(VerticalY.Text, out double y) && double.TryParse(VerticalZ.Text, out double z)))
            {
                MessageBox.Show("Enter avaible value for varible");
                return;
            }

            if (checkVerticalListFor($"X: {x} Y: {y} Z: {z}"))
            {
                MessageBox.Show("You try to add the same vertical from the list");
                return;
            }

            string surface = BuildingSurfaceListBox.SelectedItem.ToString();
            var vertical = (x, y, z);

            project.file.addVertical(vertical, surface);


            VerticalesListBox.Items.Clear();
            cleanVerticalEditForms();
            foreach (var vetrical in project.file.getVerticals(surface))
            {
                VerticalesListBox.Items.Add(vetrical);
            }
        }

        private void EditVerticalButton_Click(object sender, EventArgs e)
        {

            if (VerticalesListBox.SelectedItem == null)
            {
                MessageBox.Show("You didn't choose the item to change");
                return;
            }

            string surface = BuildingSurfaceListBox.SelectedItem.ToString();

            if (!(double.TryParse(VerticalX.Text, out double x) && double.TryParse(VerticalY.Text, out double y) && double.TryParse(VerticalZ.Text, out double z)))
            {
                MessageBox.Show("Enter avaible value for varible");
                return;
            }

            var verticalUpdated = (x, y, z);
            string vertical = VerticalesListBox.SelectedItem.ToString();

            if (vertical != $"X: {x} Y: {y} Z: {z}")
            {
                foreach (var item in VerticalesListBox.Items)
                {
                    if (item.ToString() == $"X: {x} Y: {y} Z: {z}")
                    {
                        MessageBox.Show("You can't edit the item with the same name as one of the list!");
                        return;
                    }
                }
            }

            project.file.editVertical(vertical, verticalUpdated, surface);


            VerticalesListBox.Items.Clear();
            cleanVerticalEditForms();
            foreach (var vetrical in project.file.getVerticals(surface))
            {
                VerticalesListBox.Items.Add(vetrical);
            }


        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ZoneListBox.SelectedItems == null)
                return;

            string zoneName = ZoneListBox.SelectedItem.ToString();
            project.file.removeZone(zoneName);

            choosedZone = null;
            ZoneListBoxRefresh();
            BuildingSurfaceListBox.Items.Clear();
            VerticalesListBox.Items.Clear();

            cleanZoneEditForms();
            cleanSurfaceEditForms();
            enabledSurfaceEditForm(false);
            cleanVerticalEditForms();
            enabledVerticalEditForm(false);

        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (BuildingSurfaceListBox.SelectedItems == null)
                return;

            string surface = BuildingSurfaceListBox.SelectedItem.ToString();
            project.file.removeSurface(surface);

            SurfaceListBoxRefresh();
            VerticalesListBox.Items.Clear();
            cleanSurfaceEditForms();
            cleanVerticalEditForms();
            enabledVerticalEditForm(false);
        }

        private void deleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string vertical = VerticalesListBox.SelectedItem.ToString();
            string surface = BuildingSurfaceListBox.SelectedItem.ToString();

            project.file.removeVertical(surface, vertical);

            cleanVerticalEditForms();
            BuildingSurfaceListBox_SelectedIndexChanged(sender, e);
        }

        private void SunStatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            project.file.setSunStatus(SunStatusComboBox.SelectedItem.ToString());
        }

        private void WindStatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            project.file.setWindStatus(WindStatusComboBox.SelectedItem.ToString());
        }

        private void Save_Click(object sender, EventArgs e)
        {
        
        }

        private void SurfaceNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void SpecificHeatLabel_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            project.OpenFile();
            Form1_Load(sender, e);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!checkPossibleToSaveFile())
            {
                MessageBox.Show("Impossible to save file. Unreal struct of file!");
                return;
            }
            project.SaveFile();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!checkPossibleToSaveFile())
            {
                MessageBox.Show("Impossible to save file. Unreal struct of file!");
                return;
            }
            project.SaveAs();
        }

        private void YLable_Click(object sender, EventArgs e)
        {

        }
    }
}
