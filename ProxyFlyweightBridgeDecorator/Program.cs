using ProxyFlyweightBridgeDecorator.Models;
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
