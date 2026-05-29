using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyFlyweightBridgeDecorator.Models
{
    public class Character
    {
        private char _symbol;
        private string _font;
        private int _size;

        public Character(char symbol, string font, int size)
        {
            _symbol = symbol;
            _font = font;
            _size = size;
        }

        public void Draw(int x, int y)
        {
            Console.WriteLine($"Drawing character '{_symbol}' at ({x}, {y}) with font '{_font}' and size {_size}.");
        }
    }
}
