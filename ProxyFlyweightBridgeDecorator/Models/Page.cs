using System;
using System.Collections.Generic;
using System.Text;
using ProxyFlyweightBridgeDecorator.Models.Interfaces;

namespace ProxyFlyweightBridgeDecorator.Models
{
    public class Page
    {
        private List<IDrawable> _drawables = new();

        public void Add(IDrawable drawable)
        {
            _drawables.Add(drawable);
        }

        public void Render()
        {
            Console.WriteLine("Рендер страницы:");
            foreach (var _ in _drawables)
            {
                _.Draw();
                Console.WriteLine();
            }
        }
    }
}
