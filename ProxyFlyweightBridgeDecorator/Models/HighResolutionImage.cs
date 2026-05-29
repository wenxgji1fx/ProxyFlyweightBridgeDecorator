using ProxyFlyweightBridgeDecorator.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyFlyweightBridgeDecorator.Models
{
    public class HighResolutionImage : IImage
    {
        private string _fileName;
        private int _width;
        private int _height;

        
        public HighResolutionImage(string fileName)
        {
            _fileName = fileName;
            LoadFromDisk();
        }

        private void LoadFromDisk()
        {
            Thread.Sleep(2000);
            _width = 1920;
            _height = 1080;
            Console.WriteLine($"Image '{_fileName}' loaded from disk with resolution {_width}x{_height}.");
        }

        public void Draw()
        {
            Console.WriteLine($"Drawing high resolution image '{_fileName}'.");
        }

        public int GetWidth()
        {
            return _width;
        }

        public int GetHeight()
        {
            return _height;
        }
    }
}
