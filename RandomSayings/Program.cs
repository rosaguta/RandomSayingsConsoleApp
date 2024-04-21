using RandomSayings;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0 || args[0].Equals("-h") || args[0].Equals("--help"))
        {
            DisplayHelp();
            return;
        }

        var (endpoint, sleepTime, rainbowMode, animate, animationDuration, spread) = ArgumentParser.ParseArguments(args);

        if (endpoint == null)
        {
            return;
        }

        if (!rainbowMode)
        {
            LoopManager.PlainLoop(endpoint, sleepTime);
        }
        else
        {
            if (animate)
            {
                LoopManager.AnimatedRainbowLoop(endpoint, sleepTime, animationDuration, sleepTime, spread);
            }
            else
            {
                LoopManager.RainbowLoop(endpoint, sleepTime, spread);
            }
        }
    }
    
    static void DisplayHelp()
    {
        Console.WriteLine("Usage: RandomSayings [OPTIONS]");
        Console.WriteLine("Options:");
        Console.WriteLine("-q\t\t\t\tGet random quotes");
        Console.WriteLine("-r\t\t\t\tGet random rizzes");
        Console.WriteLine("-i\t\t\t\tGet random insults");
        Console.WriteLine("-t <time>\t\t\tSet the sleep time in seconds (default: 3 seconds)");
        Console.WriteLine("--rainbow\t\t\tEnable Rainbow Mode");
        Console.WriteLine("-a \t\t\t\tEnable animation. This needs to be activated with the \x1b[1m--rainbow \x1b[0mparameter (RandomQuotes --rainbow -a)");
        Console.WriteLine("--animation_duration <value> \tSet the animation duration by X value. This value isn't related to time (default: 20)");
        Console.WriteLine("--spread <value> \t\tSet the spread of the rainbow. Lower = more spread (default: 0.5)");
        Console.WriteLine("-h, --help\t\t\tDisplay this help message");
    }
}