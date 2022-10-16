namespace BTD6E_Module_Helper;
public sealed class Logger13 {
    public static event Action<string, string> LogEvent = (_, _) => { };
    public static event Action<string, string> WarnEvent = (_, _) => { };
    public static event Action<string, string> ErrorEvent = (_, _) => { };

    private static string LastAssemblyName;

    public static void Format() {
        var time = DateTime.Now;
        var assembly = Assembly.GetCallingAssembly();
        var name = LastAssemblyName = assembly.GetCustomAttribute<MelonInfoAttribute>().Name;

        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write('[');
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write(time.ToString("hh:mm:ss.f t"));
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write("] ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write('[');
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(name);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write("] ");
    }

    public static void Log(string message) {
        Format();
        LogEvent(LastAssemblyName, null);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void Warn(string message) {
        Format();
        WarnEvent(LastAssemblyName, null);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void Error(string message) {
        Format();
        ErrorEvent(LastAssemblyName, null);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void Log(object message) {
        Format();
        LogEvent(LastAssemblyName, null);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message.ToString());
        Console.ResetColor();
    }
}