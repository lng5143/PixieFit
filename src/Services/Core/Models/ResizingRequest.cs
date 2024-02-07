namespace PixieFit.Core.Models;

public class ResizingRequest 
{
    public double Height { get; set; }
    public double Width { get; set; }
    public byte[] PhotoBytes { get; set; }
}