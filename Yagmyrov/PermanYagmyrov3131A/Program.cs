using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class ImmediateMode : GameWindow
    {


        private const int XYZ_SIZE = 75;


        public ImmediateMode() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            Console.WriteLine("OpenGl version: " + GL.GetString(StringName.Version));
            Title = "OpenGl version: " + GL.GetString(StringName.Version) + " (immidiate mode)";

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.972f, 0.972f, 0, 0);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            Matrix4 lookat = Matrix4.LookAt(30, 30, 30, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);


        }


        bool piramida = true, cub = false;
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            piramida = false;
            cub = false;
            base.OnMouseMove(e);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            if(keyboard[Key.C])
            {
              cub = true;
              piramida = false;
            }
            if(keyboard[Key.P])
            {
               piramida = true;
               cub = false;
            }
            if(keyboard[Key.Escape])
            {
             Exit();
            }
            if(mouse[MouseButton.Left])
            {
                piramida = false;
                cub = true;
             
            } 
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            DrawAxes();
            if (cub)
            {
                DrawCube();
            }
            if (piramida)
            {

                DrawPyramid();
            }



            // Se lucrează în modul DOUBLE BUFFERED - câtă vreme se afișează o imagine randată, o alta se randează în background apoi cele 2 sunt schimbate...
            SwapBuffers();
        }

        private void DrawAxes()
        {

            //GL.LineWidth(3.0f);

            // Desenează axa Ox (cu roșu).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(1.0f, 0, 0);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(XYZ_SIZE, 0, 0);
            GL.End();

            // Desenează axa Oy (cu galben).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(1.0f, 0.2f, 0);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, XYZ_SIZE, 0); ;
            GL.End();

            // Desenează axa Oz (cu verde).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(0, 1.0f, 0);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, XYZ_SIZE);
            GL.End();
        }
        void DrawPyramid()
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(0.75f, 0.75f, 075f);
            GL.Vertex3(0.0f, 1.0f, 0.0f);

            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Vertex3(1.0f, -1.0f, 1.0f);


            GL.Color3(0.95f, 1.0f, 0.94f);
            GL.Vertex3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(0.0f, -1.0f, -1.0f);

            GL.Color3(1.0f, 0.89f, 0.7f); GL.Vertex3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(0.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.Color3(0.13f, 0.54f, 0.13f); GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(0.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.End();

        }

        private void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(0.75f, 0.75f, 075f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Color3(0.95f, 1.0f, 0.94f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Color3(1.0f, 0.89f, 0.7f);

            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(0.8f, 0.36f, 0.36f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color3(0.85f, 0.43f, 0.57f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Color3(0.13f, 0.54f, 0.13f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.End();
        }

        static void Main(string[] args)
        {
            using (ImmediateMode example = new ImmediateMode())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}