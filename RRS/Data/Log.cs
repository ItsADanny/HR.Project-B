public static class Log {
    private static string currentLogTimestamp {get;} = GetFileTimeStamp();
    private static string logfileLocation = "";

    public static string GetFileTimeStamp() => DateTime.Now.ToString("MM-dd-yyyy h\\:mm");
    public static string GetTimeStamp() => DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm tt");

    public static void Write(string logline) {
        if(CheckFile()) {
            using (StreamWriter sw = File.AppendText(logfileLocation)) {
                sw.WriteLine($"{GetTimeStamp()} - {logline}");
            }
        }
    }

    public static void WriteError(string logline) {
        if (CheckFile()) {
            using (StreamWriter sw = File.AppendText(logfileLocation)) {
                sw.WriteLine($"{GetTimeStamp()} - ERROR: {logline}");
            }
        }
    }

    public static void WriteError(string logline, Exception exception) {
        if (CheckFile()) {
            using (StreamWriter sw = File.AppendText(logfileLocation)) {
                sw.WriteLine($"{GetTimeStamp()} - ERROR: {logline} - EXCEPTION: {exception}");
            }
        }
    }

    private static bool CheckFile() {
        // Console.WriteLine(System.Runtime.InteropServices.RuntimeInformation.OSDescription.Contains("Darwin"));

        //This is purely to make this part work with Windows and MacOS
        // if (System.Runtime.InteropServices.RuntimeInformation.OSDescription.Contains("Darwin")) {
        //     logfileLocation = $"Data/Logs/{currentLogTimestamp}_log.txt";
        // } else {
            logfileLocation = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent + $"/Data/Logs/{currentLogTimestamp}_log.txt";
        // }

        if (!File.Exists(logfileLocation))
        {
            try {
                FileStream fs = File.Create(logfileLocation);
                fs.Close();
            } catch (Exception ex) {
                return false;
            }
        }
        return true;
    }
}
