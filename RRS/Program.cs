using RRS.Logic;

class Program {
    static void Main() {
        //Activate vital systems check
        Functions.VitalSystemsCheck();
        //The RRS program will start here
        ProgramDisplay.Display();
    }
}