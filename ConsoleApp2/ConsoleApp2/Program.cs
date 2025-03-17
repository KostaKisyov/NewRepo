using System;
using System.Collections.Generic;

// Singleton - Великденският заек
class EasterBunny
{
    private static EasterBunny _instance;
    private EasterBunny() { }
    public static EasterBunny Instance => _instance ??= new EasterBunny();

    public void HideEgg(Egg egg, IHidingStrategy strategy)
    {
        strategy.Hide(egg);
    }
}

// Factory - Видове яйца
abstract class Egg
{
    public string Type { get; protected set; }
    public abstract void Display();
}

class ChickenEgg : Egg
{
    public ChickenEgg() { Type = "Кокоше яйце"; }
    public override void Display() => Console.WriteLine(Type);
}

class OstrichEgg : Egg
{
    public OstrichEgg() { Type = "Щраусово яйце"; }
    public override void Display() => Console.WriteLine(Type);
}

class DinosaurEgg : Egg
{
    public DinosaurEgg() { Type = "Динозавърско яйце"; }
    public override void Display() => Console.WriteLine(Type);
}

class EggFactory
{
    public static Egg CreateEgg(string type)
    {
        return type switch
        {
            "chicken" => new ChickenEgg(),
            "ostrich" => new OstrichEgg(),
            "dinosaur" => new DinosaurEgg(),
            _ => throw new ArgumentException("Невалиден тип яйце")
        };
    }
}

// Decorator - Декорация на яйца
abstract class EggDecorator : Egg
{
    protected Egg _egg;
    public EggDecorator(Egg egg) { _egg = egg; }
    public override void Display() => _egg.Display();
}

class ColoredEgg : EggDecorator
{
    public ColoredEgg(Egg egg) : base(egg) { }
    public override void Display() { _egg.Display(); Console.WriteLine(" -> Оцветено"); }
}

class StickerEgg : EggDecorator
{
    public StickerEgg(Egg egg) : base(egg) { }
    public override void Display() { _egg.Display(); Console.WriteLine(" -> Облепено със стикери"); }
}

// Strategy - Скриване на яйца
interface IHidingStrategy
{
    void Hide(Egg egg);
}

class BasketHiding : IHidingStrategy
{
    public void Hide(Egg egg) => Console.WriteLine($"{egg.Type} е скрито в кошница.");
}

class ForestHiding : IHidingStrategy
{
    public void Hide(Egg egg) => Console.WriteLine($"{egg.Type} е скрито в гората.");
}

class BushHiding : IHidingStrategy
{
    public void Hide(Egg egg) => Console.WriteLine($"{egg.Type} е скрито под храст.");
}

// Тест на системата
class Program
{
    static void Main()
    {
        EasterBunny bunny = EasterBunny.Instance;

        Egg egg1 = EggFactory.CreateEgg("chicken");
        egg1 = new ColoredEgg(egg1);

        Egg egg2 = EggFactory.CreateEgg("ostrich");
        egg2 = new StickerEgg(egg2);

        Egg egg3 = EggFactory.CreateEgg("dinosaur");
        egg3 = new ColoredEgg(new StickerEgg(egg3));

        IHidingStrategy basket = new BasketHiding();
        IHidingStrategy forest = new ForestHiding();
        IHidingStrategy bush = new BushHiding();

        bunny.HideEgg(egg1, basket);
        bunny.HideEgg(egg2, forest);
        bunny.HideEgg(egg3, bush);
    }
}
