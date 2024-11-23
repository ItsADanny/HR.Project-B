public static class Log {
    private static string currentLogTimestamp {get;} = GetFileTimeStamp();
    private static string logfileLocation = $"Data/Logs/{currentLogTimestamp}_log.txt";

    public static string GetFileTimeStamp() => DateTime.Now.ToString("MM-dd-yyyy h\\:mm");
    public static string GetTimeStamp() => DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm tt");

    public static void Write(string logline) {
        // if(CheckFile()) {
        //     using (StreamWriter sw = File.AppendText(logfileLocation)) {
        //         sw.WriteLine($"{GetTimeStamp()} - {logline}");
        //     }
        // }
    }

    public static void WriteError(string logline) {
        // if (CheckFile()) {
        //     using (StreamWriter sw = File.AppendText(logfileLocation)) {
        //         sw.WriteLine($"{GetTimeStamp()} - ERROR: {logline}");
        //     }
        // }
    }

    public static void WriteError(string logline, Exception exception) {
        // if (CheckFile()) {
        //     using (StreamWriter sw = File.AppendText(logfileLocation)) {
        //         sw.WriteLine($"{GetTimeStamp()} - ERROR: {logline} - EXCEPTION: {exception}");
        //     }
        // }
    }

    private static bool CheckFile() {
        // if (!File.Exists(logfileLocation))
        // {
        //     FileStream fs = File.Create(logfileLocation);
        //     fs.Close();
        // }
        return true;
    }
}
