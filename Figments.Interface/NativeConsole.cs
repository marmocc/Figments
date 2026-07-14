using System.Runtime.InteropServices;

static class NativeConsole
{
    [DllImport("kernel32.dll")]
    private static extern bool AttachConsole(int dwProcessId);
    private const int ATTACH_PARENT_PROCESS = -1;

    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole();

    public static void Attach()
    {
        if (!AttachConsole(ATTACH_PARENT_PROCESS))
        {
            AllocConsole();
        }
    }
}