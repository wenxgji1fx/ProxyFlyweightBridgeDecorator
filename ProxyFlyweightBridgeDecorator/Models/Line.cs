using System;
using System.Collections.Generic;
using System.Text;
using ProxyFlyweightBridgeDecorator.Models.Interfaces;

namespace ProxyFlyweightBridgeDecorator.Models
{
    public class Line : GraphicObject
    {
        private float _x1;
        private float _y1;
        private float _x2;
        private float _y2;
        public Line(IRenderingEngine renderingEngine, float x1, float y1, float x2, float y2) : base(renderingEngine)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
        }
        public override void Draw()
        {
            _renderingEngine.BeginRender();
            _renderingEngine.RenderLine(_x1, _y1, _x2, _y2);
            _renderingEngine.EndRender();
        }
        public override void Move(float deltaX, float deltaY)
        {
            _x1 += deltaX;
            _y1 += deltaY;
            _x2 += deltaX;
            _y2 += deltaY;
            Console.WriteLine($"Линия перемещена от ({_x1}, {_y1}) до ({_x2}, {_y2})");
        }
    }
}
