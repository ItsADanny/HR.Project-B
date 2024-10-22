public static class Log {
    private static string currentLogTimestamp {get;} = GetTimeStamp();
    private static string logfileLocation = $"{currentLogTimestamp}_log.txt";

    public static string GetTimeStamp() => DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm tt");

    public static void Write(string logline) {
        string timestamp = GetTimeStamp();

        using (StreamWriter outputFile = new StreamWriter("WriteLines.txt"))
        {
            outputFile.WriteLine($"{timestamp} - {logline}");
        }
    }

    public static void WriteError(string logline) {
        string timestamp = GetTimeStamp();

        using (StreamWriter outputFile = new StreamWriter("WriteLines.txt"))
        {
            outputFile.WriteLine($"{timestamp} - {logline}");
        }
    }

    public static void WriteError(string logline, Exception exception) {
        string timestamp = GetTimeStamp();

        using (StreamWriter outputFile = new StreamWriter("WriteLines.txt"))
        {
            outputFile.WriteLine($"{timestamp} - {logline} - {exception}");
        }
    }
}