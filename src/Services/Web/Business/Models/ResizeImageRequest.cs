namespace PixieFit.Web.Business.Models;

public class ResizeImageRequest
{
    public byte[] Image { get; set; }
    public string? FileName { get; set; }
    public double ResizeWidth { get; set; }
    public double ResizeHeight { get; set; }
    public long ImageSize {get ;set ;}
    public int OriginalWidth { get; set; }
    public int OriginalHeight { get; set; }
}