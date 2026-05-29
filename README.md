# ProxyFlyweightBridgeDecorator - Документация по проекту

## Содержание
1. [Proxyflyweightbridgedecorator](#proxyflyweightbridgedecorator)
   1. [Program.cs](#programcs)
   2. [ProxyFlyweightBridgeDecorator.csproj](#proxyflyweightbridgedecoratorcsproj)
2. [Proxyflyweightbridgedecorator/Models](#proxyflyweightbridgedecorator-models)
   1. [BorderDecorator.cs](#borderdecoratorcs)
   2. [Character.cs](#charactercs)
   3. [Document.cs](#documentcs)
   4. [DrawableDecorator.cs](#drawabledecoratorcs)
   5. [Ellipse.cs](#ellipsecs)
   6. [GraphicObject.cs](#graphicobjectcs)
   7. [HighResolutionImage.cs](#highresolutionimagecs)
   8. [ImageProxy.cs](#imageproxycs)
   9. [Line.cs](#linecs)
   10. [Page.cs](#pagecs)
   11. [PrintRenderer.cs](#printrenderercs)
   12. [Rectangle.cs](#rectanglecs)
   13. [ScreenRenderer.cs](#screenrenderercs)
   14. [ShadowDecorator.cs](#shadowdecoratorcs)
   15. [TransparencyDecorator.cs](#transparencydecoratorcs)
3. [Proxyflyweightbridgedecorator/Models/Factories](#proxyflyweightbridgedecorator-models-factories)
   1. [CharacterFactory.cs](#characterfactorycs)
4. [Proxyflyweightbridgedecorator/Models/Interfaces](#proxyflyweightbridgedecorator-models-interfaces)
   1. [IDrawable.cs](#idrawablecs)
   2. [IImage.cs](#iimagecs)
   3. [IRenderingEngine.cs](#irenderingenginecs)

## FILE 1: BorderDecorator.cs

<a id='borderdecoratorcs'></a>

```csharp
﻿using System;
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
```

---

## FILE 2: Character.cs

<a id='charactercs'></a>

```csharp
﻿using System;
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
```

---

## FILE 3: Document.cs

<a id='documentcs'></a>

```csharp
﻿using System;
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
```

---

## FILE 4: DrawableDecorator.cs

<a id='drawabledecoratorcs'></a>

```csharp
﻿using System;
using System.Collections.Generic;
using System.Text;
using ProxyFlyweightBridgeDecorator.Models.Interfaces;

namespace ProxyFlyweightBridgeDecorator.Models
{
    public class DrawableDecorator : IDrawable
    {
        protected IDrawable _wrappee;
        public DrawableDecorator(IDrawable wrappee)
        {
            _wrappee = wrappee;
        }
        public virtual void Draw()
        {
            _wrappee.Draw();
        }
    }
}
```

---

## FILE 5: Ellipse.cs

<a id='ellipsecs'></a>

```csharp
﻿using System;
using System.Collections.Generic;
using System.Text;
using ProxyFlyweightBridgeDecorator.Models.Interfaces;

namespace ProxyFlyweightBridgeDecorator.Models
{
    public class Ellipse : GraphicObject
    {
        private float _x;
        private float _y;
        private float _width;
        private float _height;
        public Ellipse(IRenderingEngine renderingEngine, float x, float y, float width, float height) : base(renderingEngine)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }
        public override void Draw()
        {
            _renderingEngine.BeginRender();
            _renderingEngine.RenderEllipse(_x, _y, _width, _height);
            _renderingEngine.EndRender();
        }
        public override void Move(float deltaX, float deltaY)
        {
            _x += deltaX;
            _y += deltaY;
            Console.WriteLine($"Эллипс перемещен на ({_x}, {_y})");
        }
    }
}
```

---

## FILE 6: CharacterFactory.cs

<a id='characterfactorycs'></a>

```csharp
﻿using System;
using System.Collections.Generic;
using System.Text;
using ProxyFlyweightBridgeDecorator.Models;

namespace ProxyFlyweightBridgeDecorator.Models.Factories
{
    public class CharacterFactory
    {
        private readonly Dictionary<string, Character> _characters = new();

        public Character GetCharacter(char symbol, string font, int size)
        {
            string key = $"{symbol}-{font}-{size}";

            if(!_characters.ContainsKey(key))
            {
                _characters[key] = new Character(symbol, font, size);
            }

            return _characters[key];
        }

        public int GetCharacterCount()
        {
            return _characters.Count;
        }
    }
}
```

---

## FILE 7: GraphicObject.cs

<a id='graphicobjectcs'></a>

```csharp
﻿using ProxyFlyweightBridgeDecorator.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyFlyweightBridgeDecorator.Models
{
    public abstract class GraphicObject : IDrawable
    {
        protected IRenderingEngine _renderingEngine;

        public GraphicObject(IRenderingEngine renderingEngine)
        {
            _renderingEngine = renderingEngine;
        }

        public abstract void Draw();
        public abstract void Move(float deltaX, float deltaY);
    }
}
```

---

## FILE 8: HighResolutionImage.cs

<a id='highresolutionimagecs'></a>

```csharp
﻿using ProxyFlyweightBridgeDecorator.Models.Interfaces;
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
```

---

## FILE 9: ImageProxy.cs

<a id='imageproxycs'></a>

```csharp
﻿using ProxyFlyweightBridgeDecorator.Models.Interfaces;
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
```

---

## FILE 10: IDrawable.cs

<a id='idrawablecs'></a>

```csharp
﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyFlyweightBridgeDecorator.Models.Interfaces
{
    public interface IDrawable
    {
        void Draw();
    }
}
```

---

## FILE 11: IImage.cs

<a id='iimagecs'></a>

```csharp
﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyFlyweightBridgeDecorator.Models.Interfaces
{
    public interface IImage : IDrawable
    {
        int GetWidth();
        int GetHeight();
    }
}
```

---

## FILE 12: IRenderingEngine.cs

<a id='irenderingenginecs'></a>

```csharp
﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProxyFlyweightBridgeDecorator.Models.Interfaces
{
    public interface IRenderingEngine
    {
        void BeginRender();
        void EndRender();
        void RenderRectangle(float x, float y, float width, float height);
        void RenderEllipse(float x, float y, float width, float height);
        void RenderLine(float x1, float y1, float x2, float y2);
    }
}
```

---

## FILE 13: Line.cs

<a id='linecs'></a>

```csharp
﻿using System;
using System.Collections.Generic;
using System.Text;
using ProxyFlyweightBridgeDecorator.Models.Interfaces;

namespace ProxyFlyweightBridgeDecorator.Models
{
    public class Line : GraphicObject
    {
        private float _x1;
        private float _y1;
        private float _x2;
        private float _y2;
        public Line(IRenderingEngine renderingEngine, float x1, float y1, float x2, float y2) : base(renderingEngine)
        {
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
        }
        public override void Draw()
        {
            _renderingEngine.BeginRender();
            _renderingEngine.RenderLine(_x1, _y1, _x2, _y2);
            _renderingEngine.EndRender();
        }
        public override void Move(float deltaX, float deltaY)
        {
            _x1 += deltaX;
            _y1 += deltaY;
            _x2 += deltaX;
            _y2 += deltaY;
            Console.WriteLine($"Линия перемещена от ({_x1}, {_y1}) до ({_x2}, {_y2})");
        }
    }
}
```

---

## FILE 14: Page.cs

<a id='pagecs'></a>

```csharp
﻿using System;
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
```

---

## FILE 15: PrintRenderer.cs

<a id='printrenderercs'></a>

```csharp
﻿using System;
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
```

---

## FILE 16: Rectangle.cs

<a id='rectanglecs'></a>

```csharp
﻿using ProxyFlyweightBridgeDecorator.Models.Interfaces;
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
```

---

## FILE 17: ScreenRenderer.cs

<a id='screenrenderercs'></a>

```csharp
﻿using System;
using System.Collections.Generic;
using System.Text;
using ProxyFlyweightBridgeDecorator.Models.Interfaces;

namespace ProxyFlyweightBridgeDecorator.Models
{
    public class ScreenRenderer : IRenderingEngine
    {
        public void BeginRender()
        {
            Console.WriteLine("[Screen] Начало рендеринга");
        }

        public void EndRender()
        {
            Console.WriteLine("[Screen] Конец рендеринга");
        }

        public void RenderRectangle(float x, float y, float width, float height)
        {
            Console.WriteLine($"[Screen] Рендеринг прямоугольника на ({x}, {y}) с шириной {width} и высотой {height}");
        }

        public void RenderEllipse(float x, float y, float width, float height)
        {
            Console.WriteLine($"[Screen] Рендеринг эллипса на ({x}, {y}) с шириной {width} и высотой {height}");
        }

        public void RenderLine(float x1, float y1, float x2, float y2)
        {
            Console.WriteLine($"[Screen] Рендеринг линии от ({x1}, {y1}) до ({x2}, {y2})");
        }
    }
}
```

---

## FILE 18: ShadowDecorator.cs

<a id='shadowdecoratorcs'></a>

```csharp
﻿using System;
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
```

---

## FILE 19: TransparencyDecorator.cs

<a id='transparencydecoratorcs'></a>

```csharp
﻿using System;
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
```

---

## FILE 20: Program.cs

<a id='programcs'></a>

```csharp
﻿using ProxyFlyweightBridgeDecorator.Models;
using ProxyFlyweightBridgeDecorator.Models.Factories;
using ProxyFlyweightBridgeDecorator.Models.Interfaces;

namespace ProxyFlyweightBridgeDecorator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Flyweight
            Console.WriteLine(new string('=', 75));
            Console.WriteLine("Паттерн Flyweight");

            CharacterFactory characterFactory = new();

            Character character1 = characterFactory.GetCharacter('A', "Arial", 12);
            Character character2 = characterFactory.GetCharacter('A', "Arial", 12);
            Character character3 = characterFactory.GetCharacter('B', "Arial", 12);
            Character character4 = characterFactory.GetCharacter('A', "Times New Roman", 12);

            Console.WriteLine("Отрисовка символов");

            character1.Draw(10, 10);
            character2.Draw(10, 20);
            character3.Draw(20, 10);
            character4.Draw(20, 20);

            Console.WriteLine($"Всего создано символов: {characterFactory.GetCharacterCount()}");
            Console.WriteLine(new string('=', 75));

            Console.WriteLine("\n" + new string('=', 75));
            #endregion

            #region Proxy
            Console.WriteLine("Паттерн Proxy");
            Console.WriteLine("Создание прокси (без загрузки файла)");
            IImage image = new ImageProxy("photo.jpg");
            Console.WriteLine("\n--- Первое обращение к данным (триггер загрузки)");
            int width = image.GetWidth();
            Console.WriteLine($"Ширина: {width}");

            Console.WriteLine("\n--- Второе обращение к данным (использование кэша)");
            int height = image.GetHeight();
            Console.WriteLine($"Высота: {height}");

            Console.WriteLine("\nОтрисовка");
            image.Draw();
            Console.WriteLine(new string('=', 75));

            Console.WriteLine("\n" + new string('=', 75));
            #endregion

            #region Bridge
            Console.WriteLine("Паттерн Bridge");

            IRenderingEngine screenEngine = new ScreenRenderer();
            IRenderingEngine printEngine = new PrintRenderer();

            Console.WriteLine("Отрисовка на экране:");
            GraphicObject rectScreen = new Rectangle(screenEngine, 10, 10, 100, 50);
            GraphicObject lineScreen = new Line(screenEngine, 0, 0, 100, 100);

            rectScreen.Draw();
            lineScreen.Draw();

            Console.WriteLine("\nОтрисовка на принтере:");
            GraphicObject rectPrint = new Rectangle(printEngine, 10, 10, 100, 50);
            GraphicObject linePrint = new Line(printEngine, 0, 0, 100, 100);
            rectPrint.Draw();
            linePrint.Draw();

            Console.WriteLine("Демонстрация независимости");
            rectScreen.Move(5, 5);
            rectScreen.Draw();
            Console.WriteLine(new string('=', 75));

            Console.WriteLine("\n" + new string('=', 75));
            #endregion

            #region Decorator
            Console.WriteLine("Паттерн Decorator");

            Console.WriteLine("--- Демонстрация декораторов ---");

            screenEngine = new ScreenRenderer();
            IDrawable basicRect = new Rectangle(screenEngine, 10, 10, 100, 50);

            IDrawable decoratedRect = new BorderDecorator(basicRect, 5);
            decoratedRect = new ShadowDecorator(decoratedRect, 3);
            decoratedRect = new TransparencyDecorator(decoratedRect, 0.7f);

            Console.WriteLine("Отрисовка декорированного прямоугольника:");
            decoratedRect.Draw();
            Console.WriteLine("\n");

            Console.WriteLine("Работа с Документом (Bridge + Decorator)");

            Document doc = new Document(screenEngine);
            Page page1 = doc.CreatePage();

            IDrawable decoratedEllipse = new Ellipse(screenEngine, 50, 50, 30, 20);
            decoratedEllipse = new BorderDecorator(decoratedEllipse, 3);
            decoratedEllipse = new ShadowDecorator(decoratedEllipse, 5);

            page1.Add(decoratedEllipse);

            IDrawable basicLine = new Line(screenEngine, 0, 0, 200, 200);
            page1.Add(basicLine);

            doc.RenderAll();

            Console.WriteLine("\nРабота с Proxy в Документе");

            Page page2 = doc.CreatePage();

            IImage proxyImage = new ImageProxy("document_photo.jpg");
            page2.Add(proxyImage);

            doc.RenderAll();
            #endregion
        }
    }
}
```

---

## FILE 21: ProxyFlyweightBridgeDecorator.csproj

<a id='proxyflyweightbridgedecoratorcsproj'></a>

```xml
﻿<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
```

---

