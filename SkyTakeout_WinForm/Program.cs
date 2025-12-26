using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyTakeout_WinForm
{
    internal static class AppIcon
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool DestroyIcon(IntPtr handle);

        internal static void Apply(Form form)
        {
            if (form == null)
            {
                return;
            }

            Bitmap bitmap = Properties.Resources.favicon_32x32;
            IntPtr hIcon = bitmap.GetHicon();
            form.Icon = Icon.FromHandle(hIcon);
            form.FormClosed += (s, e) => DestroyIcon(hIcon);
        }
    }

    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin());
        }
    }
}
