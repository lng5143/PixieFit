namespace PixieFit.Core.Models;

public class ResizingRequest 
{
    public decimal Height { get; set; }
    public decimal Width { get; set; }
    public byte[] PhotoBytes { get; set; }
}