using System;
using System.Collections.Generic;
using System.Text;
using ProxyFlyweightBridgeDecorator.Models.Interfaces;

namespace ProxyFlyweightBridgeDecorator.Models
{
    public class Document
    {
        private List<Page> _pages = new();
        private IRenderingEngine _renderingEngine;

        public Document(IRenderingEngine renderingEngine)
        {
            _renderingEngine = renderingEngine;
        }

        public Page CreatePage()
        {
            var page = new Page();
            _pages.Add(page);
            return page;
        }

        public void RenderAll()
        {
            _renderingEngine.BeginRender();
            int count = 1;
            foreach (var _ in _pages)
            {
                Console.WriteLine($"--- Страница {count++} ---");
                _.Render();
            }
            _renderingEngine.EndRender();
        }
    }
}
