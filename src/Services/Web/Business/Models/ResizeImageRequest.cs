namespace PixieFit.Web.Business.Models;

public class ResizeImageRequest
{
    public byte[] Image { get; set; }
    public string? FileName { get; set; }
    public decimal ResizeWidth { get; set; }
    public decimal ResizeHeight { get; set; }
    public long ImageSize {get ;set ;}
    public int OriginalWidth { get; set; }
    public int OriginalHeight { get; set; }
}