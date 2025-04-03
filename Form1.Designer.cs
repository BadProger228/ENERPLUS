using System.Drawing.Drawing2D;

namespace ENERPLUS
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void ApplyModernStyle()
        {
            this.BackColor = Color.LightGray;
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            foreach (Control control in this.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = Color.FromArgb(0, 120, 215); 
                    button.ForeColor = Color.White;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    button.Cursor = Cursors.Hand;
                    button.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

                    button.Paint += (sender, e) =>
                    {
                        Rectangle bounds = button.ClientRectangle;
                        using (GraphicsPath path = RoundedRectangle(bounds, 8))
                        {
                            button.Region = new Region(path);
                        }
                    };
                }
                else if (control is TextBox textBox)
                {
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.BackColor = Color.White;
                }
                else if (control is ListBox listBox)
                {
                    listBox.BorderStyle = BorderStyle.FixedSingle;
                    listBox.BackColor = Color.White;
                }
            }

            menuStrip1.BackColor = Color.FromArgb(240, 240, 240);
            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
        }

        private GraphicsPath RoundedRectangle(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.StartFigure();
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ZoneListBox = new ListBox();
            ZoneListBoxContextMenu = new ContextMenuStrip(components);
            deleteToolStripMenuItem = new ToolStripMenuItem();
            BuildingSurfaceListBox = new ListBox();
            BuuildSurfaceListBoxContextMenu = new ContextMenuStrip(components);
            deleteToolStripMenuItem1 = new ToolStripMenuItem();
            ZoneNameTextBox = new TextBox();
            XTextBox = new TextBox();
            YTextBox = new TextBox();
            ZTextBox = new TextBox();
            SurfaceNameTextBox = new TextBox();
            VerticalX = new TextBox();
            VerticalY = new TextBox();
            VerticalZ = new TextBox();
            TypeOfSurfaceComboBox = new ComboBox();
            AddZoneButton = new Button();
            EditZoneButton = new Button();
            VerticalesListBox = new ListBox();
            VerticalesListBoxContextMenu = new ContextMenuStrip(components);
            deleteToolStripMenuItem2 = new ToolStripMenuItem();
            AddSurfaceButton = new Button();
            EditSurfaceButton = new Button();
            AddVerticalButton = new Button();
            EditVerticalButton = new Button();
            MaterialNameTextBox = new TextBox();
            ThicknessTextBox = new TextBox();
            ConductivityTextBox = new TextBox();
            DensityTextBox = new TextBox();
            SpecificHeatTextBox = new TextBox();
            OutsideBoundaryConditionComboBox = new ComboBox();
            SunStatusComboBox = new ComboBox();
            WindStatusComboBox = new ComboBox();
            XLabel = new Label();
            YLable = new Label();
            ZLable = new Label();
            zoneNameLabel = new Label();
            SurfaceNameLabel = new Label();
            TypeOfSurfaceLabel = new Label();
            OutsideBoundaryConditionLabel = new Label();
            MaterialNameLabel = new Label();
            ThicknessLabel = new Label();
            ConductivityLabel = new Label();
            DensityLabel = new Label();
            SpecificHeatLabel = new Label();
            XVerticalLabel = new Label();
            YVerticalLabel = new Label();
            ZVerticalLabel = new Label();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            ZoneListBoxContextMenu.SuspendLayout();
            BuuildSurfaceListBoxContextMenu.SuspendLayout();
            VerticalesListBoxContextMenu.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // ZoneListBox
            // 
            ZoneListBox.ContextMenuStrip = ZoneListBoxContextMenu;
            ZoneListBox.FormattingEnabled = true;
            ZoneListBox.ItemHeight = 15;
            ZoneListBox.Location = new Point(28, 96);
            ZoneListBox.Name = "ZoneListBox";
            ZoneListBox.Size = new Size(150, 184);
            ZoneListBox.TabIndex = 0;
            ZoneListBox.SelectedIndexChanged += ZoneListBox_SelectedIndexChanged;
            // 
            // ZoneListBoxContextMenu
            // 
            ZoneListBoxContextMenu.Items.AddRange(new ToolStripItem[] { deleteToolStripMenuItem });
            ZoneListBoxContextMenu.Name = "ZoneListBoxContextMenu";
            ZoneListBoxContextMenu.Size = new Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(107, 22);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // BuildingSurfaceListBox
            // 
            BuildingSurfaceListBox.ContextMenuStrip = BuuildSurfaceListBoxContextMenu;
            BuildingSurfaceListBox.FormattingEnabled = true;
            BuildingSurfaceListBox.ItemHeight = 15;
            BuildingSurfaceListBox.Location = new Point(232, 96);
            BuildingSurfaceListBox.Name = "BuildingSurfaceListBox";
            BuildingSurfaceListBox.Size = new Size(298, 184);
            BuildingSurfaceListBox.TabIndex = 1;
            BuildingSurfaceListBox.SelectedIndexChanged += BuildingSurfaceListBox_SelectedIndexChanged;
            // 
            // BuuildSurfaceListBoxContextMenu
            // 
            BuuildSurfaceListBoxContextMenu.Items.AddRange(new ToolStripItem[] { deleteToolStripMenuItem1 });
            BuuildSurfaceListBoxContextMenu.Name = "BuuildSurfaceListBoxContextMenu";
            BuuildSurfaceListBoxContextMenu.Size = new Size(108, 26);
            // 
            // deleteToolStripMenuItem1
            // 
            deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            deleteToolStripMenuItem1.Size = new Size(107, 22);
            deleteToolStripMenuItem1.Text = "Delete";
            deleteToolStripMenuItem1.Click += deleteToolStripMenuItem1_Click;
            // 
            // ZoneNameTextBox
            // 
            ZoneNameTextBox.Location = new Point(28, 317);
            ZoneNameTextBox.Name = "ZoneNameTextBox";
            ZoneNameTextBox.Size = new Size(150, 23);
            ZoneNameTextBox.TabIndex = 2;
            // 
            // XTextBox
            // 
            XTextBox.Location = new Point(41, 346);
            XTextBox.Name = "XTextBox";
            XTextBox.Size = new Size(27, 23);
            XTextBox.TabIndex = 3;
            // 
            // YTextBox
            // 
            YTextBox.Location = new Point(99, 345);
            YTextBox.Name = "YTextBox";
            YTextBox.Size = new Size(27, 23);
            YTextBox.TabIndex = 4;
            // 
            // ZTextBox
            // 
            ZTextBox.Location = new Point(151, 346);
            ZTextBox.Name = "ZTextBox";
            ZTextBox.Size = new Size(27, 23);
            ZTextBox.TabIndex = 5;
            // 
            // SurfaceNameTextBox
            // 
            SurfaceNameTextBox.Location = new Point(232, 308);
            SurfaceNameTextBox.Name = "SurfaceNameTextBox";
            SurfaceNameTextBox.Size = new Size(157, 23);
            SurfaceNameTextBox.TabIndex = 6;
            // 
            // VerticalX
            // 
            VerticalX.Location = new Point(628, 308);
            VerticalX.Name = "VerticalX";
            VerticalX.Size = new Size(27, 23);
            VerticalX.TabIndex = 7;
            // 
            // VerticalY
            // 
            VerticalY.Location = new Point(684, 308);
            VerticalY.Name = "VerticalY";
            VerticalY.Size = new Size(27, 23);
            VerticalY.TabIndex = 7;
            // 
            // VerticalZ
            // 
            VerticalZ.Location = new Point(731, 308);
            VerticalZ.Name = "VerticalZ";
            VerticalZ.Size = new Size(27, 23);
            VerticalZ.TabIndex = 7;
            // 
            // TypeOfSurfaceComboBox
            // 
            TypeOfSurfaceComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            TypeOfSurfaceComboBox.FormattingEnabled = true;
            TypeOfSurfaceComboBox.Items.AddRange(new object[] { "Wall", "Floor", "Roof", "Window", "Door", "Ceiling", "Foundation" });
            TypeOfSurfaceComboBox.Location = new Point(232, 369);
            TypeOfSurfaceComboBox.Name = "TypeOfSurfaceComboBox";
            TypeOfSurfaceComboBox.Size = new Size(157, 23);
            TypeOfSurfaceComboBox.TabIndex = 9;
            // 
            // AddZoneButton
            // 
            AddZoneButton.Location = new Point(12, 389);
            AddZoneButton.Name = "AddZoneButton";
            AddZoneButton.Size = new Size(84, 32);
            AddZoneButton.TabIndex = 10;
            AddZoneButton.Text = "Add Zone";
            AddZoneButton.UseVisualStyleBackColor = true;
            AddZoneButton.Click += AddZoneButton_Click;
            // 
            // EditZoneButton
            // 
            EditZoneButton.Location = new Point(102, 389);
            EditZoneButton.Name = "EditZoneButton";
            EditZoneButton.Size = new Size(86, 32);
            EditZoneButton.TabIndex = 11;
            EditZoneButton.Text = "Edit Zone";
            EditZoneButton.UseVisualStyleBackColor = true;
            EditZoneButton.Click += EditZoneButton_Click;
            // 
            // VerticalesListBox
            // 
            VerticalesListBox.ContextMenuStrip = VerticalesListBoxContextMenu;
            VerticalesListBox.FormattingEnabled = true;
            VerticalesListBox.ItemHeight = 15;
            VerticalesListBox.Location = new Point(597, 96);
            VerticalesListBox.Name = "VerticalesListBox";
            VerticalesListBox.Size = new Size(145, 184);
            VerticalesListBox.TabIndex = 12;
            VerticalesListBox.SelectedIndexChanged += VerticalesListBox_SelectedIndexChanged;
            // 
            // VerticalesListBoxContextMenu
            // 
            VerticalesListBoxContextMenu.Items.AddRange(new ToolStripItem[] { deleteToolStripMenuItem2 });
            VerticalesListBoxContextMenu.Name = "VerticalesListBoxContextMenu";
            VerticalesListBoxContextMenu.Size = new Size(108, 26);
            // 
            // deleteToolStripMenuItem2
            // 
            deleteToolStripMenuItem2.Name = "deleteToolStripMenuItem2";
            deleteToolStripMenuItem2.Size = new Size(107, 22);
            deleteToolStripMenuItem2.Text = "Delete";
            deleteToolStripMenuItem2.Click += deleteToolStripMenuItem2_Click;
            // 
            // AddSurfaceButton
            // 
            AddSurfaceButton.Location = new Point(262, 522);
            AddSurfaceButton.Name = "AddSurfaceButton";
            AddSurfaceButton.Size = new Size(116, 41);
            AddSurfaceButton.TabIndex = 13;
            AddSurfaceButton.Text = "Add Surface";
            AddSurfaceButton.UseVisualStyleBackColor = true;
            AddSurfaceButton.Click += AddSurfaceButton_Click;
            // 
            // EditSurfaceButton
            // 
            EditSurfaceButton.Location = new Point(396, 522);
            EditSurfaceButton.Name = "EditSurfaceButton";
            EditSurfaceButton.Size = new Size(111, 41);
            EditSurfaceButton.TabIndex = 13;
            EditSurfaceButton.Text = "Edit surface";
            EditSurfaceButton.UseVisualStyleBackColor = true;
            EditSurfaceButton.Click += EditSurfaceButton_Click;
            // 
            // AddVerticalButton
            // 
            AddVerticalButton.Location = new Point(564, 353);
            AddVerticalButton.Name = "AddVerticalButton";
            AddVerticalButton.Size = new Size(94, 41);
            AddVerticalButton.TabIndex = 13;
            AddVerticalButton.Text = "Add Vertical";
            AddVerticalButton.UseVisualStyleBackColor = true;
            AddVerticalButton.Click += AddVerticalButton_Click;
            // 
            // EditVerticalButton
            // 
            EditVerticalButton.Location = new Point(664, 353);
            EditVerticalButton.Name = "EditVerticalButton";
            EditVerticalButton.Size = new Size(94, 41);
            EditVerticalButton.TabIndex = 13;
            EditVerticalButton.Text = "Edit Vertical";
            EditVerticalButton.UseVisualStyleBackColor = true;
            EditVerticalButton.Click += EditVerticalButton_Click;
            // 
            // MaterialNameTextBox
            // 
            MaterialNameTextBox.Location = new Point(443, 308);
            MaterialNameTextBox.Name = "MaterialNameTextBox";
            MaterialNameTextBox.Size = new Size(100, 23);
            MaterialNameTextBox.TabIndex = 14;
            // 
            // ThicknessTextBox
            // 
            ThicknessTextBox.Location = new Point(443, 356);
            ThicknessTextBox.Name = "ThicknessTextBox";
            ThicknessTextBox.Size = new Size(100, 23);
            ThicknessTextBox.TabIndex = 16;
            // 
            // ConductivityTextBox
            // 
            ConductivityTextBox.Location = new Point(443, 398);
            ConductivityTextBox.Name = "ConductivityTextBox";
            ConductivityTextBox.Size = new Size(100, 23);
            ConductivityTextBox.TabIndex = 17;
            // 
            // DensityTextBox
            // 
            DensityTextBox.Location = new Point(444, 441);
            DensityTextBox.Name = "DensityTextBox";
            DensityTextBox.Size = new Size(100, 23);
            DensityTextBox.TabIndex = 18;
            // 
            // SpecificHeatTextBox
            // 
            SpecificHeatTextBox.Location = new Point(443, 485);
            SpecificHeatTextBox.Name = "SpecificHeatTextBox";
            SpecificHeatTextBox.Size = new Size(100, 23);
            SpecificHeatTextBox.TabIndex = 18;
            // 
            // OutsideBoundaryConditionComboBox
            // 
            OutsideBoundaryConditionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            OutsideBoundaryConditionComboBox.FormattingEnabled = true;
            OutsideBoundaryConditionComboBox.Items.AddRange(new object[] { "Outdoors", "Adiabatic" });
            OutsideBoundaryConditionComboBox.Location = new Point(232, 425);
            OutsideBoundaryConditionComboBox.Name = "OutsideBoundaryConditionComboBox";
            OutsideBoundaryConditionComboBox.Size = new Size(157, 23);
            OutsideBoundaryConditionComboBox.TabIndex = 19;
            // 
            // SunStatusComboBox
            // 
            SunStatusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            SunStatusComboBox.FormattingEnabled = true;
            SunStatusComboBox.Items.AddRange(new object[] { "SunExposed", "NoSun" });
            SunStatusComboBox.Location = new Point(28, 48);
            SunStatusComboBox.Name = "SunStatusComboBox";
            SunStatusComboBox.Size = new Size(121, 23);
            SunStatusComboBox.TabIndex = 20;
            SunStatusComboBox.SelectedIndexChanged += SunStatusComboBox_SelectedIndexChanged;
            // 
            // WindStatusComboBox
            // 
            WindStatusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            WindStatusComboBox.FormattingEnabled = true;
            WindStatusComboBox.Items.AddRange(new object[] { "WindExposed", "NoWind" });
            WindStatusComboBox.Location = new Point(167, 48);
            WindStatusComboBox.Name = "WindStatusComboBox";
            WindStatusComboBox.Size = new Size(121, 23);
            WindStatusComboBox.TabIndex = 21;
            WindStatusComboBox.SelectedIndexChanged += WindStatusComboBox_SelectedIndexChanged;
            // 
            // XLabel
            // 
            XLabel.AutoSize = true;
            XLabel.Location = new Point(18, 351);
            XLabel.Name = "XLabel";
            XLabel.Size = new Size(17, 15);
            XLabel.TabIndex = 23;
            XLabel.Text = "X:";
            // 
            // YLable
            // 
            YLable.AutoSize = true;
            YLable.Location = new Point(79, 351);
            YLable.Name = "YLable";
            YLable.Size = new Size(17, 15);
            YLable.TabIndex = 24;
            YLable.Text = "Y:";
            YLable.Click += YLable_Click;
            // 
            // ZLable
            // 
            ZLable.AutoSize = true;
            ZLable.Location = new Point(132, 351);
            ZLable.Name = "ZLable";
            ZLable.Size = new Size(17, 15);
            ZLable.TabIndex = 25;
            ZLable.Text = "Z:";
            // 
            // zoneNameLabel
            // 
            zoneNameLabel.AutoSize = true;
            zoneNameLabel.Location = new Point(12, 299);
            zoneNameLabel.Name = "zoneNameLabel";
            zoneNameLabel.Size = new Size(70, 15);
            zoneNameLabel.TabIndex = 26;
            zoneNameLabel.Text = "Zone name:";
            // 
            // SurfaceNameLabel
            // 
            SurfaceNameLabel.AutoSize = true;
            SurfaceNameLabel.Location = new Point(232, 290);
            SurfaceNameLabel.Name = "SurfaceNameLabel";
            SurfaceNameLabel.Size = new Size(82, 15);
            SurfaceNameLabel.TabIndex = 27;
            SurfaceNameLabel.Text = "Surface name:";
            SurfaceNameLabel.Click += SurfaceNameLabel_Click;
            // 
            // TypeOfSurfaceLabel
            // 
            TypeOfSurfaceLabel.AutoSize = true;
            TypeOfSurfaceLabel.Location = new Point(232, 353);
            TypeOfSurfaceLabel.Name = "TypeOfSurfaceLabel";
            TypeOfSurfaceLabel.Size = new Size(96, 15);
            TypeOfSurfaceLabel.TabIndex = 28;
            TypeOfSurfaceLabel.Text = "Type Of Surface: ";
            // 
            // OutsideBoundaryConditionLabel
            // 
            OutsideBoundaryConditionLabel.AutoSize = true;
            OutsideBoundaryConditionLabel.Location = new Point(232, 407);
            OutsideBoundaryConditionLabel.Name = "OutsideBoundaryConditionLabel";
            OutsideBoundaryConditionLabel.Size = new Size(161, 15);
            OutsideBoundaryConditionLabel.TabIndex = 29;
            OutsideBoundaryConditionLabel.Text = "Outside Boundary Condition:";
            // 
            // MaterialNameLabel
            // 
            MaterialNameLabel.AutoSize = true;
            MaterialNameLabel.Location = new Point(444, 290);
            MaterialNameLabel.Name = "MaterialNameLabel";
            MaterialNameLabel.Size = new Size(86, 15);
            MaterialNameLabel.TabIndex = 30;
            MaterialNameLabel.Text = "Material name:";
            // 
            // ThicknessLabel
            // 
            ThicknessLabel.AutoSize = true;
            ThicknessLabel.Location = new Point(445, 338);
            ThicknessLabel.Name = "ThicknessLabel";
            ThicknessLabel.Size = new Size(62, 15);
            ThicknessLabel.TabIndex = 31;
            ThicknessLabel.Text = "Thickness:";
            // 
            // ConductivityLabel
            // 
            ConductivityLabel.AutoSize = true;
            ConductivityLabel.Location = new Point(445, 382);
            ConductivityLabel.Name = "ConductivityLabel";
            ConductivityLabel.Size = new Size(75, 15);
            ConductivityLabel.TabIndex = 32;
            ConductivityLabel.Text = "Conductivity";
            // 
            // DensityLabel
            // 
            DensityLabel.AutoSize = true;
            DensityLabel.Location = new Point(444, 423);
            DensityLabel.Name = "DensityLabel";
            DensityLabel.Size = new Size(52, 15);
            DensityLabel.TabIndex = 33;
            DensityLabel.Text = "Density: ";
            // 
            // SpecificHeatLabel
            // 
            SpecificHeatLabel.AutoSize = true;
            SpecificHeatLabel.Location = new Point(445, 467);
            SpecificHeatLabel.Name = "SpecificHeatLabel";
            SpecificHeatLabel.Size = new Size(82, 15);
            SpecificHeatLabel.TabIndex = 34;
            SpecificHeatLabel.Text = "Specific Heat: ";
            SpecificHeatLabel.Click += SpecificHeatLabel_Click;
            // 
            // XVerticalLabel
            // 
            XVerticalLabel.AutoSize = true;
            XVerticalLabel.Location = new Point(605, 311);
            XVerticalLabel.Name = "XVerticalLabel";
            XVerticalLabel.Size = new Size(17, 15);
            XVerticalLabel.TabIndex = 35;
            XVerticalLabel.Text = "X:";
            // 
            // YVerticalLabel
            // 
            YVerticalLabel.AutoSize = true;
            YVerticalLabel.Location = new Point(661, 311);
            YVerticalLabel.Name = "YVerticalLabel";
            YVerticalLabel.Size = new Size(17, 15);
            YVerticalLabel.TabIndex = 36;
            YVerticalLabel.Text = "Y:";
            // 
            // ZVerticalLabel
            // 
            ZVerticalLabel.AutoSize = true;
            ZVerticalLabel.Location = new Point(717, 311);
            ZVerticalLabel.Name = "ZVerticalLabel";
            ZVerticalLabel.Size = new Size(17, 15);
            ZVerticalLabel.TabIndex = 37;
            ZVerticalLabel.Text = "Z:";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(770, 24);
            menuStrip1.TabIndex = 38;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(112, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(112, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(112, 22);
            saveAsToolStripMenuItem.Text = "Save as";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(770, 575);
            Controls.Add(menuStrip1);
            Controls.Add(ZVerticalLabel);
            Controls.Add(YVerticalLabel);
            Controls.Add(XVerticalLabel);
            Controls.Add(SpecificHeatLabel);
            Controls.Add(DensityLabel);
            Controls.Add(ConductivityLabel);
            Controls.Add(ThicknessLabel);
            Controls.Add(MaterialNameLabel);
            Controls.Add(OutsideBoundaryConditionLabel);
            Controls.Add(TypeOfSurfaceLabel);
            Controls.Add(SurfaceNameLabel);
            Controls.Add(zoneNameLabel);
            Controls.Add(ZLable);
            Controls.Add(YLable);
            Controls.Add(XLabel);
            Controls.Add(WindStatusComboBox);
            Controls.Add(SunStatusComboBox);
            Controls.Add(OutsideBoundaryConditionComboBox);
            Controls.Add(SpecificHeatTextBox);
            Controls.Add(DensityTextBox);
            Controls.Add(ConductivityTextBox);
            Controls.Add(ThicknessTextBox);
            Controls.Add(MaterialNameTextBox);
            Controls.Add(EditVerticalButton);
            Controls.Add(AddVerticalButton);
            Controls.Add(EditSurfaceButton);
            Controls.Add(AddSurfaceButton);
            Controls.Add(VerticalesListBox);
            Controls.Add(EditZoneButton);
            Controls.Add(AddZoneButton);
            Controls.Add(TypeOfSurfaceComboBox);
            Controls.Add(VerticalZ);
            Controls.Add(VerticalY);
            Controls.Add(VerticalX);
            Controls.Add(SurfaceNameTextBox);
            Controls.Add(ZTextBox);
            Controls.Add(YTextBox);
            Controls.Add(XTextBox);
            Controls.Add(ZoneNameTextBox);
            Controls.Add(BuildingSurfaceListBox);
            Controls.Add(ZoneListBox);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ZoneListBoxContextMenu.ResumeLayout(false);
            BuuildSurfaceListBoxContextMenu.ResumeLayout(false);
            VerticalesListBoxContextMenu.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox ZoneListBox;
        private ListBox BuildingSurfaceListBox;
        private TextBox ZoneNameTextBox;
        private TextBox XTextBox;
        private TextBox YTextBox;
        private TextBox ZTextBox;
        private TextBox SurfaceNameTextBox;
        private TextBox VerticalX;
        private TextBox VerticalY;
        private TextBox VerticalZ;
        private ComboBox TypeOfSurfaceComboBox;
        private Button AddZoneButton;
        private Button EditZoneButton;
        private ListBox VerticalesListBox;
        private Button AddSurfaceButton;
        private Button EditSurfaceButton;
        private Button AddVerticalButton;
        private Button EditVerticalButton;
        private TextBox MaterialNameTextBox;
        private TextBox ThicknessTextBox;
        private TextBox ConductivityTextBox;
        private TextBox DensityTextBox;
        private TextBox SpecificHeatTextBox;
        private ContextMenuStrip ZoneListBoxContextMenu;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ContextMenuStrip BuuildSurfaceListBoxContextMenu;
        private ToolStripMenuItem deleteToolStripMenuItem1;
        private ContextMenuStrip VerticalesListBoxContextMenu;
        private ToolStripMenuItem deleteToolStripMenuItem2;
        private ComboBox OutsideBoundaryConditionComboBox;
        private ComboBox SunStatusComboBox;
        private ComboBox WindStatusComboBox;
        private Label XLabel;
        private Label YLable;
        private Label ZLable;
        private Label zoneNameLabel;
        private Label SurfaceNameLabel;
        private Label TypeOfSurfaceLabel;
        private Label OutsideBoundaryConditionLabel;
        private Label MaterialNameLabel;
        private Label ThicknessLabel;
        private Label ConductivityLabel;
        private Label DensityLabel;
        private Label SpecificHeatLabel;
        private Label XVerticalLabel;
        private Label YVerticalLabel;
        private Label ZVerticalLabel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
    }
}
