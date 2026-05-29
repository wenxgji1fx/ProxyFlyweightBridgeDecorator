using ProxyFlyweightBridgeDecorator.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyFlyweightBridgeDecorator.Models
{
    public abstract class GraphicObject : IDrawable
    {
        protected IRenderingEngine _renderingEngine;

        public GraphicObject(IRenderingEngine renderingEngine)
        {
            _renderingEngine = renderingEngine;
        }

        public abstract void Draw();
        public abstract void Move(float deltaX, float deltaY);
    }
}
