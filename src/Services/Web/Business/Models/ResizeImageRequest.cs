namespace PixieFit.Web.Business.Models;

public class ResizeImageRequest
{
    public byte[] Image { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    
}