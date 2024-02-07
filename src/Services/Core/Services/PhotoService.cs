using PixieFit.Core.Managers;
using PixieFit.Core.Models;

namespace PixieFit.Core.Services;

public interface IPhotoService
{
    byte[] Resize(ResizingRequest request);
}

public class PhotoService : IPhotoService
{
    IResizer _resizer;

    public PhotoService(
        IResizer resizer
    )
    {
        _resizer = resizer;
    }

    public byte[] Resize(ResizingRequest request)
    {
        return _resizer.Resize(request);
    }
}