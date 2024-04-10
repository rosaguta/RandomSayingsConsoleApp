using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using HttpHelper;
using RestSharp;
using System.Threading;
using Pastel;

class Program
{
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
                    Debug.Write("Rainbow activated");
                    break;
                default:
                    Console.WriteLine($"This option is not implemented yet. Please use -q, -r, or -i.");
                    return;
            }
        }
        
        if(!rainbowmode){
            Loop(endpoint, sleepTime, _RestClientHelper);
        }
        else
        {
            RainbowLoop(endpoint, sleepTime, _RestClientHelper);
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
        Console.WriteLine("-q\t\tGet random quotes");
        Console.WriteLine("-r\t\tGet random rizzes");
        Console.WriteLine("-i\t\tGet random insults");
        Console.WriteLine("-t <time>\tSet the sleep time in seconds (default: 3 seconds)");
        Console.WriteLine("--rainbow\tEnable Rainbow Mode");
        Console.WriteLine("-h, --help\tDisplay this help message");
    }
}
