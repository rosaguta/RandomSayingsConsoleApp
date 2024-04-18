using HttpHelper;
using Pastel;

namespace RandomSayings;

public static class LoopManager
{
    static Saying _saying = new Saying("https://quotesapi.divsphere.net");
    public static void PlainLoop(string endpoint, int sleepTime)
    {
        while (true)
        {
            string? response = _saying.Get(endpoint);
            Console.WriteLine(response);
            Thread.Sleep(sleepTime);
            Console.Clear();
            Console.Write("\x1b[3J");
        }
    }

    public static void RainbowLoop(string endpoint, int sleepTime)
    {
        double i = 0.0;    
        double frequency = 0.3;
        while(true)
        {
            string? response = _saying.Get(endpoint);
            
            int responselenght = response.Length;
            for (int l = 0; l < responselenght; l++)
            {
                string ColorHex = Color.Get(frequency, i);
                char letter = response[l];
                Console.Write(letter.ToString().Pastel(ColorHex));
                i = i + 0.1;
            }
            Thread.Sleep(sleepTime);
            Console.Clear();
            Console.Write("\x1b[3J");
        }
    }

    public static void AnimatedRainbowLoop(string endpoint, int sleepTime, int AnimDuration, int waittime, double spread)
    {
        double j = 0.0;    
        double k = 0.0;
        double a = 0.0;

        Console.CursorVisible = false;
        Console.Clear();
        Console.Write("\x1b[3J");
        int origRow = Console.CursorTop;
        int origCol = Console.CursorLeft;
        
        Console.CancelKeyPress += new ConsoleCancelEventHandler((sender, e) =>
        {
            // Reactivate cursor visibility
            Console.CursorVisible = true;
            Console.WriteLine("CTRL+C detected. Exiting...");
            Environment.Exit(0); // You may want to handle this differently based on your needs
        });
        
        while(true) 
        {
            string? response = _saying.Get(endpoint);
            int responselenght = response.Length;
            if(response.Contains('\n')){
                int AmountSlashNInResponse = response.Count(t => t == '\n');
                a = 0.75 / AmountSlashNInResponse;
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
                    string ColorHex = Color.Get(spread, j);
                    if (Console.WindowWidth <= columnindex){
                        rowindex++;
                        columnindex = 0;
                    }
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
}