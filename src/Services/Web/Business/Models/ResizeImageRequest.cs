namespace PixieFit.Web.Business.Models;

public class ResizeImageRequest
{
    public byte[] Image { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }

}