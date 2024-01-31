using PixieFit.Core.Managers;
using PixieFit.Core.Models;

namespace PixieFit.Core.Services;

public class PhotoService 
{
    Resizer _resizer;

    public PhotoService(
        Resizer resizer
    )
    {
        _resizer = resizer;
    }

    public byte[] Resize(ResizingRequest request)
    {
        return _resizer.Resize(request);
    }
}