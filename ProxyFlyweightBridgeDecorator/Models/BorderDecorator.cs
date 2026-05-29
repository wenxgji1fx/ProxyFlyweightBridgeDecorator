using System;
using System.Collections.Generic;
using System.Text;
using ProxyFlyweightBridgeDecorator.Models.Interfaces;

namespace ProxyFlyweightBridgeDecorator.Models
{
    public class BorderDecorator : DrawableDecorator
    {
        private int _borderWidth;

        public BorderDecorator(IDrawable wrappee, int borderWidth) : base(wrappee)
        {
            _borderWidth = borderWidth;
        }

        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Добавление рамки с шириной " + _borderWidth);
        }
    }
}
