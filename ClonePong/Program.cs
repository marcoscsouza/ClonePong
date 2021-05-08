using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace pongClone
{
    class Program : GameWindow
    {
        int xDaBola = 0;
        int yDaBola = 0;
        int tamanhoDaBola = 20;
        int velocidadeDaBolaEmX = 3;
        int velocidadeDaBolaEmY = 3;

        int yJogador1 = 0;
        int yJogador2 = 0;
        int larguraJogador = 20;
        int alturaJogador = 60;

        int xJogador1()
        {
            return -ClientSize.Width / 2 + larguraJogador / 2 ;
        }

        int xJogador2()
        {
            return ClientSize.Width / 2 - larguraJogador / 2;
        }

        protected override void OnUpdateFrame(FrameEventArgs e) 
        {
            MovimentoBola();
            Pontos();
            MovimentoJogador();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {          
            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);

            Matrix4 projection = Matrix4.CreateOrthographic(ClientSize.Width, ClientSize.Height, 0.0f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            DesenharRetangulo(xDaBola, yDaBola, tamanhoDaBola, tamanhoDaBola, 1.0f, 1.0f, 0.0f);    //bola
            DesenharRetangulo(xJogador1(), yJogador1, larguraJogador, alturaJogador, 1.0f, 0.0f, 0.0f); //jogador1
            DesenharRetangulo(xJogador2(), yJogador2, larguraJogador, alturaJogador, 0.0f, 0.0f, 1.0f); //jogador2

            SwapBuffers();
        }

        void DesenharRetangulo(int x, int y, int largura, int altura, float r, float g, float b)
        {
            GL.Color3(r, g, b);
            GL.Begin(PrimitiveType.Quads);  
            GL.Vertex2(-0.5f * largura + x, -0.5f * altura + y);  
            GL.Vertex2(0.5f * largura + x, -0.5f * altura + y);  
            GL.Vertex2(0.5f * largura + x, 0.5f * altura + y);    
            GL.Vertex2(-0.5f * largura + x, 0.5f * altura + y);
            GL.End(); 
        }

        public void MovimentoBola()
        {
            xDaBola = xDaBola + velocidadeDaBolaEmX;
            yDaBola = yDaBola + velocidadeDaBolaEmY;

            if (xDaBola + tamanhoDaBola / 2 > xJogador2() - larguraJogador / 2
             && yDaBola - tamanhoDaBola / 2 < yJogador2 + alturaJogador / 2
             && yDaBola + tamanhoDaBola / 2 > yJogador2 - alturaJogador / 2)
            {
                velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
            }

            if (xDaBola - tamanhoDaBola / 2 < xJogador1() + larguraJogador / 2
             && yDaBola - tamanhoDaBola / 2 < yJogador1 + alturaJogador / 2
             && yDaBola + tamanhoDaBola / 2 > yJogador1 - alturaJogador / 2)
            {
                velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
            }

            if (yDaBola + tamanhoDaBola / 2 > ClientSize.Height / 2)
            {
                velocidadeDaBolaEmY = -velocidadeDaBolaEmY;
            }

            if (yDaBola - tamanhoDaBola / 2 < -ClientSize.Height / 2)
            {
                velocidadeDaBolaEmY = -velocidadeDaBolaEmY;
            }
        }

        public void MovimentoJogador()
        {
            if (Keyboard.GetState().IsKeyDown(Key.W))
            {
                yJogador1 = yJogador1 + 5;
            }

            if (Keyboard.GetState().IsKeyDown(Key.S))
            {
                yJogador1 = yJogador1 - 5;
            }

            if (Keyboard.GetState().IsKeyDown(Key.Up))
            {
                yJogador2 = yJogador2 + 5;
            }

            if (Keyboard.GetState().IsKeyDown(Key.Down))
            {
                yJogador2 = yJogador2 - 5;
            }
        }
        public void Pontos()
        {
            if (xDaBola < -ClientSize.Width / 2 || xDaBola > ClientSize.Width / 2)
            {
                xDaBola = 0;
                yDaBola = 0;
                velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
            }
        }
        static void Main()
        {
            new Program().Run(); 
        }
    }
}
