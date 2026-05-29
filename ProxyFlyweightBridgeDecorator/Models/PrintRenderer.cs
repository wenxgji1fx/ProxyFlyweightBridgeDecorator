using System;
using System.Collections.Generic;
using System.Text;
using ProxyFlyweightBridgeDecorator.Models.Interfaces;


namespace ProxyFlyweightBridgeDecorator.Models
{
    public class PrintRenderer : IRenderingEngine
    {
        public void BeginRender()
        {
            Console.WriteLine("[Print] Начало рендеринга");
        }
        public void EndRender()
        {
            Console.WriteLine("[Print] Конец рендеринга");
        }
        public void RenderRectangle(float x, float y, float width, float height)
        {
            Console.WriteLine($"[Print] Рендеринг прямоугольника на ({x}, {y}) с шириной {width} и высотой {height}");
        }
        public void RenderEllipse(float x, float y, float width, float height)
        {
            Console.WriteLine($"[Print] Рендеринг эллипса на ({x}, {y}) с шириной {width} и высотой {height}");
        }
        public void RenderLine(float x1, float y1, float x2, float y2)
        {
            Console.WriteLine($"[Print] Рендеринг линии от ({x1}, {y1}) до ({x2}, {y2})");
        }
    }
}
