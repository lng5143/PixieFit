using PixieFit.Core.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;

namespace PixieFit.Core.Managers;

public interface IResizer
{
    byte[] Resize(ResizingRequest request);
}

public class Resizer : IResizer
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
        Console.WriteLine(request.Height);
        Console.WriteLine(request.Width);
        Console.WriteLine(request.PhotoBytes.Length);

        Initialize(request);

        var vSeamCount = (int)Math.Round(Width() * request.Width);
        var hSeamCount = (int)Math.Round(Height() * request.Height);

        for (int i = 0; i < vSeamCount; i++)
        {
            int[] seam = FindVerticalSeam();
            RemoveVerticalSeam(seam);
        }

        for (int i = 0; i < hSeamCount; i++)
        {
            int[] seam = FindHorizontalSeam();
            RemoveHorizontalSeam(seam);
        }

        var processPicture = Picture();
        using (var stream = new MemoryStream())
        {
            processPicture.Save(stream, ImageFormat.Jpeg);
            return stream.ToArray();
        }
    }

    public void Initialize (ResizingRequest request)
    {
        MemoryStream stream = new MemoryStream(request.PhotoBytes);
        picture = new Bitmap(stream);
        Console.WriteLine(picture.Height);
        Console.WriteLine(picture.Width);

        var initWidth = picture.Width;
        var initHeight = picture.Height;

        InitColorArray(initWidth, initHeight);
        InitEnergyArray(initWidth, initHeight);

        isTransposed = false;
    }

    public Bitmap Picture() {
        var picture = new Bitmap(Width(), Height());
        for (int i = 0; i < Width(); i++)
        {
            for (int j = 0; j < Height(); j++)
            {
                picture.SetPixel(i, j, Color.FromArgb((int)color[i,j]));
            }
        }
        return picture;
    }

    public void InitEnergyArray(int width, int height)
    {
        energy = new double[width, height];
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                energy[i,j] = Energy(i, j);
            }
        }
    }

    public void InitColorArray(int width, int height)
    {
        Console.WriteLine(picture.GetPixel(1199, 629).ToArgb());

        color = new long[width, height];
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                color[i,j] = picture.GetPixel(i, j).ToArgb();
            }
        }
    }

    // width of current picture
    public int Width()
    {
        return color.GetLength(0);
    }

    // height of current picture 
    public int Height()
    {
        return color.GetLength(1);
    }

    public double Energy(int x, int y)
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

    public double CalculateColorDiffSquared(long argb1, long argb2)
    {
        var r1 = Convert.ToInt16((argb1 >> 16) & 0xFF);
        var g1 = Convert.ToInt16((argb1 >> 8) & 0xFF);
        int b1 = Convert.ToInt16(argb1 & 0xFF);

        var r2 = Convert.ToInt16((argb2 >> 16) & 0xFF);
        var g2 = Convert.ToInt16((argb2 >> 8) & 0xFF);
        int b2 = Convert.ToInt16(argb2 & 0xFF);

        return Math.Pow(r1 - r2, 2) + Math.Pow(g1 - g2, 2) + Math.Pow(b1 - b2, 2);
    }

    public int[] FindVerticalSeam()
    {
        distTo = InitDistTo();
        edgeTo = new DirectedEdge2D[Width(), Height()];

        for (int i = 0; i < Width(); i++)
        {
            for (int j = 0; j < Height() - 1; j++)
            {
                if (i == 0)
                {
                    Relax(new DirectedEdge2D(i, j, i, j + 1, energy[i, j + 1]));
                    Relax(new DirectedEdge2D(i, j, i + 1, j + 1, energy[i + 1, j + 1]));
                }
                else if(i == Width() - 1)
                {
                    Relax(new DirectedEdge2D(i, j, i, j + 1, energy[i, j + 1]));
                    Relax(new DirectedEdge2D(i, j, i - 1, j + 1, energy[i - 1, j + 1]));
                }
                else
                {
                    Relax(new DirectedEdge2D(i, j, i, j + 1, energy[i, j + 1]));
                    Relax(new DirectedEdge2D(i, j, i - 1, j + 1, energy[i - 1, j + 1]));
                    Relax(new DirectedEdge2D(i, j, i + 1, j + 1, energy[i + 1, j + 1]));
                }
            }
        }

        int endPoint = GetShortShortestPathEndpoint();

        return GetShortestPath(endPoint);
    }

    public int GetShortShortestPathEndpoint()
    {
        double minPathLength = double.PositiveInfinity;
        int shortestPathEndpoint = 0;

        for (int i = 0; i < Width() - 1; i++)
        {
            double pathLength = distTo[i, Height() - 1];
            if (pathLength < minPathLength)
            {
                minPathLength = pathLength;
                shortestPathEndpoint = i;
            }
        }

        return shortestPathEndpoint;
    }

    public int[] GetShortestPath(int v)
    {
        int[] result = new int[Height()];
        int xTo = v;

        for (int i = result.Length - 1; i > 0; i--) // iterate to the second row
        {
            var edge = edgeTo[xTo, i];
            result[i] = xTo;
            xTo = edge.xFrom;
        }

        if (xTo == 0)
            result[0] = xTo + 1;
        // this probably won't happen because relaxing is from left to right, so there will never be top right side-v in the seam
        else if (xTo == Width() - 1) 
            result[0] = xTo - 1;
        else
            result[0] = xTo;

        return result;
    }

    public int[] FindHorizontalSeam()
    {
        if (!isTransposed)
        {
            TransposeEnergyMatrix();
            TransposeColorMatrix();
            isTransposed = true;
        }

        return FindVerticalSeam();
    }

    public void RemoveVerticalSeam(int[] seam)
    {
        if (seam == null)
            throw new ArgumentException("Cannot remove seam because seam is null");

        int newWidth = Width() - 1;
        int newHeigth = Height();

        // reset color[,]
        long[,] newColor = new long[newWidth, newHeigth];

        // traverse per row
        for (int i = 0; i < newHeigth; i++)
        {
            for (int j = 0; j < newWidth; j++)
            {
                if (j >= seam[i])
                {
                    newColor[j,i] = color[j + 1, i];
                }
                else
                {
                    newColor[j,i] = color[j,i];
                }
            }
        }

        color = newColor;

        // reset energy[][]
        double[,] newEnergy = new double[newWidth,newHeigth];

        // traverse per row
        for (int i = 0; i < newHeigth; i++)
        {
            for (int j = 0; j < newWidth; j++)
            {
                if (j >= seam[i]) {
                    newEnergy[j,i] = energy[j + 1, i];
                }
                else
                {
                    newEnergy[j,i] = energy[j,i];
                }
            }

            for (int y = 0; y < seam.Length; y++)
            {
                newEnergy[seam[y] - 1,y] = Energy(seam[y] - 1, y);
                newEnergy[seam[y], y] = Energy(seam[y], y);
            }
        }

        energy = newEnergy;
    }

    public void RemoveHorizontalSeam(int[] seam)
    {
        if (seam == null)
            throw new ArgumentException("Cannot remove seam because seam is null");

        if (!isTransposed)
        {
            TransposeColorMatrix();
            TransposeColorMatrix();
            isTransposed = true;
        }

        RemoveVerticalSeam(seam);
    }

    public void TransposeEnergyMatrix()
    {
        int width = energy.GetLength(0);
        int height = energy.GetLength(1);
        double[,] tranEnergy = new double[height, width];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tranEnergy[j,i] = energy[i,j];
            }
        }

        energy = tranEnergy;
    }

    public void TransposeColorMatrix()
    {
        int width = color.GetLength(0);
        int height = color.GetLength(1);
        long[,] tranColor = new long[height, width];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tranColor[j,i] = color[i,j];
            }
        }

        color = tranColor;
    }

    public void Relax(DirectedEdge2D e)
    {
        if (distTo[e.xTo, e.yTo] > distTo[e.xFrom, e.yFrom] + e.Weight)
        {
            distTo[e.xTo, e.yTo] = distTo[e.xFrom, e.yFrom] + e.Weight;
            edgeTo[e.xTo, e.yTo] = e;
        }
    }

    public double[,] InitDistTo()
    {
        double[,] distTo = new double[Width(), Height()];

        for (int i = 0; i < Width(); i++)
        {
            for (int j = 0; j < Height(); j++)
            {
                distTo[i,j] = double.PositiveInfinity;
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