using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyFlyweightBridgeDecorator.Models.Interfaces
{
    public interface IImage : IDrawable
    {
        int GetWidth();
        int GetHeight();
    }
}
