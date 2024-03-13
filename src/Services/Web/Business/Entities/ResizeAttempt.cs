using PixieFit.Web.Business.Enums;

namespace PixieFit.Web.Business.Entities;
public class ResizeAttempt : BaseEntity
{
    public Guid UserId { get; set; }
    public string? FileName { get; set; }
    public double ResizeWidth { get; set; }
    public double ResizeHeight { get; set; }
    public long ImageSize {get ;set ;}
    public int OriginalWidth { get; set; }
    public int OriginalHeight { get; set; }
    public ResizeStatus Status { get; set; }
}
