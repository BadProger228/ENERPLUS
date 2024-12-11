using OpenTK.GLControl;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform.Windows;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenTK.Mathematics;

namespace ENERPLUS
{
    public partial class Form1 : Form
    {
        private GLControl glControl;
        public Form1()
        {
            InitializeComponent();

            glControl = new GLControl(/*new GraphicsMode(32, 24, 0, 4)*/)
            {
                Dock = DockStyle.Fill
            };

            glControl.Load += GlControl_Load;
            glControl.Paint += GlControl_Paint;

            Controls.Add(glControl);


        }

        private void Form1_Load(object sender, EventArgs e)
        {

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

    }
}
