namespace PixieFit.Web.Business.Models;

public class ResizeImageResponse 
{
    public byte[] Image { get; set; }
    public string? FileName { get; set; }
    public string Message { get; set; }
}