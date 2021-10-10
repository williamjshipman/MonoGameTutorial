using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Basic3D
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        private Texture2D FelineOverlordTexture;
        private Cube m_Cube;
        private Matrix m_World, m_View, m_Projection;
        private BasicEffect m_Effect;

        private VertexBuffer m_Vertices;
        private IndexBuffer m_Indices;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            FelineOverlordTexture = Content.Load<Texture2D>("img/i-for-one-welcome-our-new-feline-overlords");
            m_Cube = new Cube(1);

            m_World = Matrix.CreateTranslation(0, 0, 0);
            m_View = Matrix.CreateLookAt(new Vector3(0, 0, 5), Vector3.Zero, Vector3.Up);
            m_Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), GraphicsDevice.Viewport.AspectRatio, 0.01f, 10f);

            m_Effect = new BasicEffect(_graphics.GraphicsDevice);
            m_Effect.EnableDefaultLighting();
            m_Effect.World = m_World;
            m_Effect.View = m_View;
            m_Effect.Projection = m_Projection;
            m_Effect.TextureEnabled = true;
            m_Effect.Texture = FelineOverlordTexture;

            m_Vertices = new VertexBuffer(GraphicsDevice, VertexPositionNormalTexture.VertexDeclaration, m_Cube.Vertices.Count, BufferUsage.WriteOnly);
            m_Vertices.SetData<VertexPositionNormalTexture>(m_Cube.Vertices.ToArray());
            m_Indices = new IndexBuffer(GraphicsDevice, typeof(UInt16), m_Cube.Indices.Count, BufferUsage.WriteOnly);
            m_Indices.SetData<UInt16>(m_Cube.Indices.ToArray());
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState KBState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || KBState.IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (KBState.IsKeyDown(Keys.A))
                m_View *= Matrix.CreateTranslation(0.01f, 0, 0);
            if (KBState.IsKeyDown(Keys.D))
                m_View *= Matrix.CreateTranslation(-0.01f, 0, 0);
            if (KBState.IsKeyDown(Keys.Up))
                m_View *= Matrix.CreateTranslation(0, 0, 0.01f);
            if (KBState.IsKeyDown(Keys.Down))
                m_View *= Matrix.CreateTranslation(0, 0, -0.01f);
            if (KBState.IsKeyDown(Keys.OemPlus) || KBState.IsKeyDown(Keys.Add))
                m_View *= Matrix.CreateTranslation(0, 0, 0.01f);
            if (KBState.IsKeyDown(Keys.OemMinus) || KBState.IsKeyDown(Keys.Subtract))
                m_View *= Matrix.CreateTranslation(0, 0, -0.01f);
            if (KBState.IsKeyDown(Keys.Left))
                m_View *= Matrix.CreateRotationY(-MathHelper.ToRadians(1));
            if (KBState.IsKeyDown(Keys.Right))
                m_View *= Matrix.CreateRotationY(MathHelper.ToRadians(1));
            if (KBState.IsKeyDown(Keys.W))
                m_View *= Matrix.CreateRotationX(-MathHelper.ToRadians(1));
            if (KBState.IsKeyDown(Keys.S))
                m_View *= Matrix.CreateRotationX(MathHelper.ToRadians(1));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aqua);

            // TODO: Add your drawing code here
            // _spriteBatch.Begin();
            // _spriteBatch.Draw(FelineOverlordTexture, new Vector2(150, 0), Color.White);

            GraphicsDevice.SetVertexBuffer(m_Vertices);
            GraphicsDevice.Indices = m_Indices;

            m_Effect.World = m_World;
            m_Effect.View = m_View;

            foreach (EffectPass Pass in m_Effect.CurrentTechnique.Passes)
            {
                Pass.Apply();
                // _graphics.GraphicsDevice.DrawUserIndexedPrimitives(
                //     PrimitiveType.TriangleList,
                //     m_Cube.Vertices, 0, m_Cube.Vertices.Length,
                //     m_Cube.Indices, 0, m_Cube.Indices.Length / 3
                // );
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, m_Cube.Indices.Count / 3);
            }

            // _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
