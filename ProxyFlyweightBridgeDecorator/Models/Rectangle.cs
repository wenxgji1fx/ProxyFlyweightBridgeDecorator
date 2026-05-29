using ProxyFlyweightBridgeDecorator.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyFlyweightBridgeDecorator.Models
{
    public class Rectangle : GraphicObject
    {
        private float _x;
        private float _y;
        private float _width;
        private float _height;

        public Rectangle(IRenderingEngine renderingEngine, float x, float y, float width, float height) : base(renderingEngine)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        public override void Draw()
        {
            _renderingEngine.BeginRender();
            _renderingEngine.RenderRectangle(_x, _y, _width, _height);
            _renderingEngine.EndRender();
        }

        public override void Move(float deltaX, float deltaY)
        {
            _x += deltaX;
            _y += deltaY;
            Console.WriteLine($"Прямоугольник перемещен на ({_x}, {_y})");
        }
    }
}
