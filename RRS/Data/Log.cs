public static class Log {
    private static string currentLogTimestamp {get;} = GetTimeStamp();
    private static string logfileLocation = $"{currentLogTimestamp}_log.txt";

    public static string GetTimeStamp() => DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm tt");

    public static void Write(string logline) {
        using (StreamWriter outputFile = new StreamWriter(logfileLocation)) {
            outputFile.WriteLine($"{GetTimeStamp()} - {logline}");
        }
    }

    public static void WriteError(string logline) {
        using (StreamWriter outputFile = new StreamWriter(logfileLocation)) {
            outputFile.WriteLine($"{GetTimeStamp()} - ERROR: {logline}");
        }
    }

    public static void WriteError(string logline, Exception exception) {
        using (StreamWriter outputFile = new StreamWriter(logfileLocation)) {
            outputFile.WriteLine($"{GetTimeStamp()} - {logline} - {exception}");
        }
    }
}