using System.Configuration;
using System.Runtime.InteropServices;

namespace OnTopClock
{
    public partial class Form1 : Form
    {
        private readonly string ClockFormat = ConfigurationManager.AppSettings["ClockFormat"] ?? "U";


        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.tmrClock.Enabled = true;
            this.tmrClock.Interval = 1000;
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            this.Text = $"Clock {ClockFormat}" ;
        }

        private void tmrClock_Tick(object sender, EventArgs e)
        {
            this.lblTimer.Text = DateTime.Now.ToString(ClockFormat);
        }
    }
}