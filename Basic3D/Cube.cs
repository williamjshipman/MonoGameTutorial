using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Basic3D
{
    public sealed class Cube
    {
        private List<VertexPositionNormalTexture> m_Vertices;
        private List<UInt16> m_Indices;
        private List<Quad> m_Faces;

        public float Size { get; set; }
        
        public Cube(float Size_)
        {
            Size = Size_;

            m_Faces = new List<Quad>();
            m_Faces.Add(new Quad( Vector3.UnitX * Size / 2,  Vector3.UnitX, Vector3.Up, Size, Size));
            m_Faces.Add(new Quad(-Vector3.UnitX * Size / 2, -Vector3.UnitX, Vector3.Up, Size, Size));
            m_Faces.Add(new Quad( Vector3.UnitY * Size / 2,  Vector3.UnitY, Vector3.Backward, Size, Size));
            m_Faces.Add(new Quad(-Vector3.UnitY * Size / 2, -Vector3.UnitY, Vector3.Forward, Size, Size));
            m_Faces.Add(new Quad( Vector3.UnitZ * Size / 2,  Vector3.UnitZ, Vector3.Up, Size, Size));
            m_Faces.Add(new Quad(-Vector3.UnitZ * Size / 2, -Vector3.UnitZ, Vector3.Up, Size, Size));

            m_Vertices = new List<VertexPositionNormalTexture>();
            m_Indices = new List<UInt16>();
            foreach (Quad Face in m_Faces)
            {
                // m_Indices.AddRange(Face.Indices);
                foreach (UInt16 iIndex in Face.Indices)
                {
                    m_Indices.Add((UInt16)(iIndex + m_Vertices.Count));
                }
                m_Vertices.AddRange(Face.Vertices);
            }
        }

        public List<VertexPositionNormalTexture> Vertices
        {
            get { return m_Vertices; }
        }

        public List<UInt16> Indices
        {
            get { return m_Indices; }
        }
    }
}