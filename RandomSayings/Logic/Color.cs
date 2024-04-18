namespace RandomSayings;

public static class Color
{
    public static string Get(double freq, double i)
    {
        double red = Math.Sin(freq * i + 0) * 127 + 128;
        double green = Math.Sin(freq * i + 2 * Math.PI / 3) * 127 + 128;
        double blue = Math.Sin(freq * i + 4 * Math.PI / 3) * 127 + 128;
        return $"#{((int)red):X2}{((int)green):X2}{((int)blue):X2}";
    }
}