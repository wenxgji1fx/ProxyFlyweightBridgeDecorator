using ProxyFlyweightBridgeDecorator.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyFlyweightBridgeDecorator.Models
{
    public class ImageProxy : IImage
    {
        private string _fileName;
        private HighResolutionImage _realImage;

        public ImageProxy(string fileName)
        {
            _fileName = fileName;
            Console.WriteLine($"Создан Proxy для {_fileName}");
        }

        public void EnsureImageLoaded()
        {
            if(_realImage == null)
            {
                _realImage = new HighResolutionImage(_fileName);
            }
        }

        public void Draw()
        {
            EnsureImageLoaded();
            _realImage.Draw();
        }

        public int GetWidth()
        {
            EnsureImageLoaded();
            return _realImage.GetWidth();
        }

        public int GetHeight()
        {
            EnsureImageLoaded();
            return _realImage.GetHeight();
        }
    }
}
