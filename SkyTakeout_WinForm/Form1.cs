using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyTakeout_WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load += (s, e) => ApplyRoundedCorners();
            panelCard.SizeChanged += (s, e) => ApplyRoundedCorners();
            buttonLogin.SizeChanged += (s, e) => ApplyRoundedCorners();
        }

        private void ApplyRoundedCorners()
        {
            ApplyRoundedRegion(panelCard, 12);
            ApplyRoundedRegion(buttonLogin, Math.Max(1, buttonLogin.Height / 2));
            buttonLogin.FlatStyle = FlatStyle.Flat;
            buttonLogin.FlatAppearance.BorderSize = 0;
        }

        private static void ApplyRoundedRegion(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0)
            {
                return;
            }

            int safeRadius = Math.Max(1, radius);
            int maxRadius = Math.Min(control.Width, control.Height) / 2;
            if (safeRadius > maxRadius)
            {
                safeRadius = maxRadius;
            }

            Rectangle bounds = new Rectangle(0, 0, control.Width, control.Height);
            using (GraphicsPath path = CreateRoundedRectanglePath(bounds, safeRadius))
            {
                Region old = control.Region;
                control.Region = new Region(path);
                if (old != null)
                {
                    old.Dispose();
                }
            }
        }

        private static GraphicsPath CreateRoundedRectanglePath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Rectangle arc = new Rectangle(bounds.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}
