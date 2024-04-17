using HttpHelper;
using RestSharp;
using Pastel;

class Program
{
    protected static int origRow;
    protected static int origCol;   
    
    static void Main(string[] args)
    {
        if (args.Length == 0 || args[0].Equals("-h") || args[0].Equals("--help"))
        {
            DisplayHelp();
            return;
        }
        RestClientHelper _RestClientHelper = new RestClientHelper("https://quotesapi.divsphere.net");
        
        string endpoint = "";
        int sleepTime = 3000; // Default sleep time
        bool rainbowmode = false;
        bool animate = false;
        int animation_duration = 20;
        double spread = 0.5;
        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i].ToLower())
            {
                case "-q":
                    endpoint = "quotes";
                    break;
                case "-r":
                    endpoint = "rizzes";
                    break;
                case "-i":
                    endpoint = "insults";
                    break;
                case "-t":
                    if (i + 1 < args.Length)
                    {
                        if (int.TryParse(args[i + 1], out int time))
                        {
                            sleepTime= int.Parse(time.ToString() + "000");
                            i++; // Move to the next argument as it's the value of -t
                        }
                        else
                        {
                            Console.WriteLine("Invalid time value provided after -t flag.");
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No time value provided after -t flag.");
                        return;
                    }
                    break;
                case "--rainbow":
                    rainbowmode = true;
                    break;
                case "-a":
                    animate = true;
                    break;
                case "--animation_duration":
                    if (i + 1 < args.Length)
                    {
                        if (int.TryParse(args[i + 1], out int time))
                        {
                            animation_duration= int.Parse(time.ToString() + "000");
                            i++;
                        }
                        else
                        {
                            Console.WriteLine("Invalid value provided after --animation_duration flag.");
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No value provided after --animation_duration flag.");
                        return;
                    }
                    break;
                case "--spread":
                    if (i + 1 < args.Length)
                    {
                        if (double.TryParse(args[i + 1], out double spread_))
                        {
                            spread= double.Parse(spread_.ToString() + "");
                            i++;
                        }
                        else
                        {
                            Console.WriteLine("Invalid value provided after --spread flag.");
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No value provided after --spread flag.");
                        return;
                    }
                    break;
                default:
                    Console.WriteLine($"No value was provided");
                    DisplayHelp();
                    return;
            }
        }
        
        if(!rainbowmode){
            Loop(endpoint, sleepTime, _RestClientHelper);
        }
        else
        {
            if (animate)
            {
                AnimateLoop(endpoint, sleepTime, _RestClientHelper,animation_duration, sleepTime, spread);
            }
            RainbowLoop(endpoint, sleepTime, _RestClientHelper);
        }
    }
    
    static void AnimateLoop(string endpoint, int sleepTime, RestClientHelper restClientHelper,int AnimDuration, int waittime, double spread)
    {
        double j = 0.0;    
        double k = 0.0;
        double a = 0.0;

        Console.CursorVisible = false;
        Console.Clear();
        Console.Write("\x1b[3J");
        origRow = Console.CursorTop;
        origCol = Console.CursorLeft;
        while(true) 
        {
            // string? response = GetRandomSaying(endpoint, restClientHelper);
                        string response = @"dit is een mega coole test voor mn kut -a --rainbow functie ;w;
dit is een mega coole test voor mn kut -a --rainbow functie ;w;
dit is een mega coole test voor mn kut -a --rainbow functie ;w;
dit is een mega coole test voor mn kut -a --rainbow functie ;w;
dit is een mega coole test voor mn kut -a --rainbow functie ;w; ";
            int responselenght = response.Length;
            if(response.Contains('\n')){
                int AmountSlashNInResponse = response.Count(t => t == '\n');
                a = 1.0 / AmountSlashNInResponse;
            }
            for (int i = 0; i < AnimDuration; i++)
            {
                int columnindex = 0;
                int rowindex = 0;
                j = k;
                for (int l = 0; l < responselenght; l++)
                {
                    char letter = response[l];
                    if (letter == '\n')
                    {
                        rowindex++;
                        k = k + a;
                        j = k;
                        columnindex = 0;
                        continue;
                    }
                    string ColorHex = Rainbow(spread, j);
                    // TODO: CHECK FOR TERMINAL SPACE
                    Console.SetCursorPosition(origCol+columnindex, origRow+rowindex);
                    Console.Write(letter.ToString().Pastel(ColorHex));
                    j = j + 0.1;
                    columnindex++;
                }
                Thread.Sleep(50);
                if(!response.Contains(Environment.NewLine)){
                    k = k + 0.5;
                    j = k;
                }
            }
            Thread.Sleep(waittime);
            Console.Clear();
            Console.Write("\x1b[3J");
        }
    }
    


    static string? GetRandomSaying(string endpoint, RestClientHelper restClientHelper)
    {
        RestResponse response = restClientHelper.ExecuteGetRequest(endpoint);
        string? saying = response.Content;
        return saying;
    }

    static void Loop(string endpoint, int sleepTime, RestClientHelper restClientHelper)
    {
        while (true)
        {
            string? response = GetRandomSaying(endpoint, restClientHelper);
            Console.WriteLine(response);
            Thread.Sleep(sleepTime);
            Console.Clear();
            Console.Write("\x1b[3J");
            
        }
    }

    static void RainbowLoop(string endpoint, int sleepTime, RestClientHelper restClientHelper)
    {

        double i = 0.0;    
        double frequency = 0.3;
        while(true)
        {
            string? response = GetRandomSaying(endpoint, restClientHelper);
            
            int responselenght = response.Length;
            for (int l = 0; l < responselenght; l++)
            {
                string ColorHex = Rainbow(frequency, i);
                char letter = response[l];
                Console.Write(letter.ToString().Pastel(ColorHex));
                i = i + 0.1;
            }
            Thread.Sleep(sleepTime);
            Console.Clear();
            Console.Write("\x1b[3J");
        }
    }

    public static string Rainbow(double freq, double i)
    {
        double red = Math.Sin(freq * i + 0) * 127 + 128;
        double green = Math.Sin(freq * i + 2 * Math.PI / 3) * 127 + 128;
        double blue = Math.Sin(freq * i + 4 * Math.PI / 3) * 127 + 128;
        return $"#{((int)red):X2}{((int)green):X2}{((int)blue):X2}";
    }
    
    static void DisplayHelp()
    {
        Console.WriteLine("Usage: RandomSayings [OPTIONS]");
        Console.WriteLine("Options:");
        Console.WriteLine("-q\t\t\tGet random quotes");
        Console.WriteLine("-r\t\t\tGet random rizzes");
        Console.WriteLine("-i\t\t\tGet random insults");
        Console.WriteLine("-t <time>\t\tSet the sleep time in seconds (default: 3 seconds)");
        Console.WriteLine("--rainbow\t\tEnable Rainbow Mode");
        Console.WriteLine("--animation_duration\tSet the animation duration by X value. This value isn't related to time (default: 20");
        Console.WriteLine("--spread \t\tSet the spread of the rainbow. Lower = more spread (default: 0.5)");
        Console.WriteLine("-h, --help\t\tDisplay this help message");
    }
}
