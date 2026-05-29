using System;
using System.Collections.Generic;
using System.Text;
using ProxyFlyweightBridgeDecorator.Models.Interfaces;

namespace ProxyFlyweightBridgeDecorator.Models
{
    public class TransparencyDecorator : DrawableDecorator
    {
        private float _transparencyLevel;
        public TransparencyDecorator(IDrawable wrappee, float transparencyLevel) : base(wrappee)
        {
            _transparencyLevel = transparencyLevel;
        }

        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Установка прозрачности на уровне " + _transparencyLevel);
        }

    }
}
