class Person
{
    public string? name;
    public string? location;

    static void Main()
    {
        Person a = new Person();
        a.GetUserNameAndLocation();
        a.ChristmasCountdown(DateTime.Now);
        GlazerApp.RunExample();
    }

    private void GetUserNameAndLocation()
    {
        Console.WriteLine("What is your name?");
        name = Console.ReadLine();

        Console.WriteLine($"Hi {name}! Where are you from?");
        location = Console.ReadLine();

        Console.WriteLine($"I have never been to {location}. I bet it is nice. Press any key to continue...");
        Console.ReadKey();

    }

    private void ChristmasCountdown(DateTime date)
    {
        Console.WriteLine($"Today's date is {date.Date}");

        int daysTillChristmas = 359 - date.DayOfYear;
        if (daysTillChristmas < 0) daysTillChristmas += 365;

        Console.WriteLine($"There are {daysTillChristmas} days until Christmas!");

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}