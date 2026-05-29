using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyFlyweightBridgeDecorator.Models.Interfaces
{
    public interface IRenderingEngine
    {
        void BeginRender();
        void EndRender();
        void RenderRectangle(float x, float y, float width, float height);
        void RenderEllipse(float x, float y, float width, float height);
        void RenderLine(float x1, float y1, float x2, float y2);
    }
}
