using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApp1 
{
    class Diagramm : Control
    {
        private Point Offset = new Point(0, 0);
        public Point offset {
            get
            {

                return Offset;
            }
            set 
            {

                Offset = value;
                this.Refresh();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawLine(new Pen(Color.Black, 0.2f), new Point(this.Width, this.Height - offset.Y) , new Point(-this.Width, this.Height - offset.Y));
            e.Graphics.DrawLine(new Pen(Color.Black, 0.2f), new Point(offset.X, this.Height), new Point(offset.X, 0));
        }
    }
}
