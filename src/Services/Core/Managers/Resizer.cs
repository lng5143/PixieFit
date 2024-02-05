using PixieFit.Core.Models;
using System.Drawing;
using System.Reflection;

namespace PixieFit.Core.Managers;

public class Resizer
{
    private Bitmap picture { get; set; }
    private double[,] energy { get; set; }
    private long[,] color { get; set; }
    private DirectedEdge2D[,] edgeTo { get; set; }
    private double[,] distTo { get; set; }
    private bool isTransposed { get; set; }

    public Resizer()
    {

    }

    public byte[] Resize(ResizingRequest request)
    {
        MemoryStream stream = new MemoryStream(request.PhotoBytes);
        picture = new Bitmap(stream);

        var initWidth = picture.Width;
        var initHeight = picture.Height;

        InitColorArray(initHeight, initHeight);
        InitEnergyArray(initWidth, initHeight);

        return null;
    }

    private void InitEnergyArray(int width, int height)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                energy[i,j] = Energy(i, j);
            }
        }
    }

    private void InitColorArray(int width, int height)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                color[i,j] = picture.GetPixel(i, j).ToArgb();
            }
        }
    }

    // width of current picture
    private int Width()
    {
        return color.GetLength(0);
    }

    // height of current picture 
    private int Height()
    {
        return color.GetLength(1);
    }

    private double Energy(int x, int y)
    {
        if (
                x == 0 // outer left col
                || x == Width() - 1 // outer right col
                || y == 0 // top row
                || y == Height() - 1 // bottom row
        )
        {
            return 1000;
        }
        else
        {
            long leftARGB = color[x - 1, y];
            long rightARGB = color[x + 1, y];
            long upARGB = color[x, y - 1];
            long downARGB = color[x, y + 1];

            double xDiffSquared = CalculateColorDiffSquared(leftARGB, rightARGB);
            double yDiffSquared = CalculateColorDiffSquared(upARGB, downARGB);

            return Math.Sqrt(xDiffSquared + yDiffSquared);
        }
    }

    private double CalculateColorDiffSquared(long argb1, long argb2)
    {
        var r1 = Convert.ToInt16((argb1 >> 16) & 0xFF);
        var g1 = Convert.ToInt16((argb1 >> 8) & 0xFF);
        int b1 = Convert.ToInt16(argb1 & 0xFF);

        var r2 = Convert.ToInt16((argb2 >> 16) & 0xFF);
        var g2 = Convert.ToInt16((argb2 >> 8) & 0xFF);
        int b2 = Convert.ToInt16(argb2 & 0xFF);

        return Math.Pow(r1 - r2, 2) + Math.Pow(g1 - g2, 2) + Math.Pow(b1 - b2, 2);
    }

    private int[] FindVerticalSeam()
    {
        return null;
    }

    private int[] FindHorizontalSeam()
    {
        return null;
    }

    private void RemoveVerticalSeam()
    {

    }

    private void RemoveHorizontalSeam()
    {

    }

    private void transposeEnergyMatrix()
    {
        int width = energy.GetLength(0);
        int height = energy.GetLength(1);
        double[,] tranEnergy = new double[height, width];

        for (int i = 0; i < width - 1; i++)
        {
            for (int j = 0; j < height - 1; j++)
            {
                tranEnergy[j,i] = energy[i,j];
            }
        }

        energy = tranEnergy;
    }

    private void transposeColorMatrix()
    {
        int width = color.GetLength(0);
        int height = color.GetLength(1);
        long[,] tranColor = new long[height, width];

        for (int i = 0; i < width - 1; i++)
        {
            for (int j = 0; j < height - 1; j++)
            {
                tranColor[j,i] = color[i,j];
            }
        }
    }

    private void Relax(DirectedEdge2D e)
    {
        if (distTo[e.xTo, e.yTo] > distTo[e.xFrom, e.yFrom] + e.Weight)
        {
            distTo[e.xTo, e.yTo] = distTo[e.xFrom, e.yFrom] + e.Weight;
            edgeTo[e.xTo, e.yTo] = e;
        }
    }

    private double[,] InitDistTo()
    {
        double[,] distTo = new double[Width(), Height()];

        for (int i = 0; i < Width() - 1; i++)
        {
            for (int j = 0; j < Height() - 1; j++)
            {
                distTo[i,j] = Double.PositiveInfinity;
            }
            distTo[i,0] = 0.0;
        }
        return distTo;
    }
    
}

public class DirectedEdge2D 
{
    public int xFrom { get; set; }
    public int yFrom { get; set; }
    public int xTo { get; set; }
    public int yTo { get; set; }
    public double Weight { get; set; }

    public DirectedEdge2D(int xFrom, int yFrom, int xTo, int yTo, double weight)
    {
        this.xFrom = xFrom;
        this.yFrom = yFrom;
        this.xTo = xTo;
        this.yTo = yTo;
        Weight = weight;
    }
}