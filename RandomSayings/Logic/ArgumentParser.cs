using System;
using System.Collections.Generic;

namespace RandomSayings
{
    public class ArgumentParser
    {
        public static (string endpoint, int sleepTime, bool rainbowMode, bool animate, int animationDuration, double spread) ParseArguments(string[] args)
        {
            string endpoint = "";
            int sleepTime = 3000;
            bool rainbowMode = false;
            bool animate = false;
            int animationDuration = 20;
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
                                sleepTime = time * 1000;
                                i++; // Move to the next argument as it's the value of -t
                            }
                            else
                            {
                                Console.WriteLine("Invalid time value provided after -t flag.");
                                return (null, 0, false, false, 0, 0);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No time value provided after -t flag.");
                            return (null, 0, false, false, 0, 0);
                        }
                        break;
                    case "--rainbow":
                        rainbowMode = true;
                        break;
                    case "-a":
                        animate = true;
                        break;
                    case "--animation_duration":
                        if (i + 1 < args.Length)
                        {
                            if (int.TryParse(args[i + 1], out int duration))
                            {
                                animationDuration = duration;
                                i++;
                            }
                            else
                            {
                                Console.WriteLine("Invalid value provided after --animation_duration flag.");
                                return (null, 0, false, false, 0, 0);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No value provided after --animation_duration flag.");
                            return (null, 0, false, false, 0, 0);
                        }
                        break;
                    case "--spread":
                        if (i + 1 < args.Length)
                        {
                            if (double.TryParse(args[i + 1], out double spreadValue))
                            {
                                spread = spreadValue;
                                i++;
                            }
                            else
                            {
                                Console.WriteLine("Invalid value provided after --spread flag.");
                                return (null, 0, false, false, 0, 0);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No value provided after --spread flag.");
                            return (null, 0, false, false, 0, 0);
                        }
                        break;
                    default:
                        Console.WriteLine($"Unknown argument: {args[i]}");
                        return (null, 0, false, false, 0, 0);
                }
            }

            return (endpoint, sleepTime, rainbowMode, animate, animationDuration, spread);
        }
    }
}
