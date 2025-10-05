using System;

public class Plant
{
    private string _name;
    private int _age;
    private double _height;
    private PlantType _type;
    private DateTime _plantingDate;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Назва не може бути порожньою.");
            if (value.Length < 2 || value.Length > 50)
                throw new ArgumentException("Назва повинна містити від 2 до 50 символів.");
            if (!value.All(c => char.IsLetter(c) || c == ' '))
                throw new ArgumentException("Назва може містити лише літери та пробіли.");
            _name = value;
        }
    }

    public PlantType Type
    {
        get => _type;
        set
        {
            if (!Enum.IsDefined(typeof(PlantType), value))
                throw new ArgumentException("Невірний тип рослини.");
            _type = value;
        }
    }

    public int Age
    {
        get => _age;
        set
        {
            if (value < 0 || value > 5000)
                throw new ArgumentException("Вік повинен бути в діапазоні від 0 до 5000 років.");
            _age = value;
        }
    }

    public double Height
    {
        get => _height;
        set
        {
            if (value <= 0 || value > 115.7)
                throw new ArgumentException("Висота повинна бути в діапазоні від 0 до 115.7 м (рекорд Hyperion).");
            _height = value;
        }
    }

    public DateTime PlantingDate
    {
        get => _plantingDate;
        set
        {
            ValidatePlantingDate(value);
            _plantingDate = value;
        }
    }

    public bool IsFlowering { get; set; } = true;

    public string AgeCategory
    {
        get
        {
            if (Age < 2) return "Молода";
            if (Age < 10) return "Доросла";
            return "Стара";
        }
    }

    public string LastWatered { get; private set; } = "Ніколи";


    public Plant(string name, PlantType type, int age, double height, DateTime plantingDate)
    {
        Console.WriteLine("Викликано основний конструктор з 5 параметрами");
        Name = name;
        Type = type;
        Age = age;
        Height = height;
        PlantingDate = plantingDate;
    }


    public Plant()
    {
        Console.WriteLine("Викликано конструктор без параметрів");
        _name = "Без назви";
        _type = PlantType.Flower;
        _age = 1;
        _height = 0.1;
        _plantingDate = DateTime.Now;
        IsFlowering = true;
    }


    public Plant(string name, PlantType type) : this(name, type, 1, 0.5, DateTime.Now)
    {
        Console.WriteLine("Викликано конструктор з 2 параметрами (ім'я та тип)");
    }


    public Plant(string name, int age) : this(name, PlantType.Tree, age, 1.0, DateTime.Now.AddYears(-age))
    {
        Console.WriteLine("Викликано конструктор з 2 параметрами (ім'я та вік)");
    }

    private void ValidatePlantingDate(DateTime date)
    {
        if (date.Year < 1900)
            throw new ArgumentException("Дата посадки не може бути раніше 1900 року.");
        if (date > DateTime.Now)
            throw new ArgumentException("Дата посадки не може бути у майбутньому.");
    }

    private string FormatWateringTime(DateTime time)
    {
        return time.ToString("dd.MM.yyyy HH:mm");
    }

    public void WaterPlant()
    {
        DateTime wateringTime = DateTime.Now;
        LastWatered = FormatWateringTime(wateringTime);
        Console.WriteLine($"{Name} було полито. Час останнього поливу: {LastWatered}");
    }


    public void WaterPlant(string waterType)
    {
        WaterPlant();
        Console.WriteLine($"Використано тип води: {waterType}");
    }

    public void WaterPlant(int milliliters)
    {
        WaterPlant();
        Console.WriteLine($"Використано {milliliters} мл води");
    }

    public void WaterPlant(string waterType, int milliliters)
    {
        WaterPlant();
        Console.WriteLine($"Використано {milliliters} мл води типу: {waterType}");
    }

    private string GetFormattedDescription()
    {
        return $"{Name} ({Type}) - {Age} років, {Height} м";
    }

    public string GetDescription()
    {
        return GetFormattedDescription();
    }


    public void Grow(double growth)
    {
        if (growth <= 0)
            throw new ArgumentException("Ріст повинен бути більше 0.");
        Height += growth;
        Console.WriteLine($"{Name} виріс на {growth}м. Нова висота: {Height}м");
    }

    public void Grow()
    {
        double defaultGrowth = 0.1;
        Height += defaultGrowth;
        Console.WriteLine($"{Name} виріс на стандартну величину {defaultGrowth}м. Нова висота: {Height}м");
    }

    public void Grow(int years)
    {
        if (years <= 0)
            throw new ArgumentException("Кількість років повинна бути більше 0.");

        double growthPerYear = 0.2;
        double totalGrowth = years * growthPerYear;
        Height += totalGrowth;
        Age += years;
        Console.WriteLine($"{Name} виріс на {totalGrowth}м за {years} років. Нова висота: {Height}м, Новий вік: {Age} років");
    }

    public string GetPlantingInfo()
    {
        return $"{Name} було висаджено {PlantingDate:dd.MM.yyyy}.";
    }

    public bool IsMature()
    {
        return Age > 5;
    }
}
