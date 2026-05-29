using System;
using System.Collections.Generic;
using System.Text;
using ProxyFlyweightBridgeDecorator.Models.Interfaces;

namespace ProxyFlyweightBridgeDecorator.Models
{
    internal class ShadowDecorator : DrawableDecorator
    {
        private int _shadowOffset;
        public ShadowDecorator(IDrawable wrappee, int shadowOffset) : base(wrappee)
        {
            _shadowOffset = shadowOffset;
        }
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Добавление тени с отступом " + _shadowOffset);
        }
    }
}
