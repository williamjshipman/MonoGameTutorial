using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Quad
{
    private VertexPositionNormalTexture[] m_pVertices;
    private UInt16[] m_piIndices;
    
    public Quad(Vector3 Position_, Vector3 Normal_, Vector3 Up_, float Width_, float Height_)
    {
        m_pVertices = new VertexPositionNormalTexture[4];
        m_piIndices = new UInt16[IndexCount];
        Vector3 Left = Vector3.Cross(Normal_, Up_);
        Vector3 UpperCenter = (Up_ * Height_ / 2) + Position_;
        Vector3 UpperLeft = UpperCenter + (Left * Width_) / 2;
        Vector3 UpperRight = UpperCenter - (Left * Width_) / 2;
        Vector3 LowerLeft = UpperLeft - Up_ * Height_;
        Vector3 LowerRight = UpperRight - Up_ * Height_;

        Left.Normalize();

        m_pVertices[0] = new VertexPositionNormalTexture(
            UpperLeft,
            Normal_,
            new Vector2(0, 0));
        m_pVertices[1] = new VertexPositionNormalTexture(
            UpperRight,
            Normal_,
            new Vector2(1, 0));
        m_pVertices[2] = new VertexPositionNormalTexture(
            LowerLeft,
            Normal_,
            new Vector2(0, 1));
        m_pVertices[3] = new VertexPositionNormalTexture(
            LowerRight,
            Normal_,
            new Vector2(1, 1));

        // Create the front faces.
        m_piIndices[0] = 0;
        m_piIndices[1] = 1;
        m_piIndices[2] = 3;
        m_piIndices[3] = 0;
        m_piIndices[4] = 3;
        m_piIndices[5] = 2;
        
        // Create the back faces.
        m_piIndices[6] = 3;
        m_piIndices[7] = 1;
        m_piIndices[8] = 0;
        m_piIndices[9] = 2;
        m_piIndices[10] = 3;
        m_piIndices[11] = 0;
    }

    public VertexPositionNormalTexture[] Vertices
    {
        get
        {
            return m_pVertices;
        }
    }

    public UInt16[] Indices
    {
        get
        {
            return m_piIndices;
        }
    }

    public static UInt16 IndexCount
    {
        get { return 12; }
    }

    public static UInt16 VertexCount
    {
        get { return 4; }
    }
}