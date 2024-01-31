using PixieFit.Core.Models;
using System.Drawing;
using System.Reflection;

namespace PixieFit.Core.Managers;

public class Resizer 
{
    public Bitmap picture { get; set; }

    public Resizer()
    {

    }

    public byte[] Resize(ResizingRequest request)
    {
        MemoryStream stream = new MemoryStream(request.PhotoBytes);
        picture = new Bitmap(stream);
        
        return null;
    }

    private void InitEnergyArray(int width, int height)
    {
        
    }

    private void InitColorArray(int width, int height)
    {

    }

    // width of current picture
    private int Width()
    {
        return 0;
    }

    // height of current picture 
    private int Height()
    {
        return 0;
    }
}

public class DirectedEdge2D 
{
    public int xFrom { get; set; }
    public int yFrom { get; set; }
    public int xTo { get; set; }
    public int yTo { get; set; }
    public double weight { get; set; }

    public DirectedEdge2D(int xFrom, int yFrom, int xTo, int yTo, double weight)
    {
        this.xFrom = xFrom;
        this.yFrom = yFrom;
        this.xTo = xTo;
        this.yTo = yTo;
        this.weight = weight;
    }
}