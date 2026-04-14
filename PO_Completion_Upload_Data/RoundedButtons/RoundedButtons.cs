using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PO_Completion_Upload_Data.RoundedButtons
{
    class RoundedButtons : Button
    {
        // Fields

        private int borderize = 0;
        private int borderRadius = 40;
        private Color borderColor = Color.PaleVioletRed;

        public int Borderize { get => borderize; set => borderize = value; }
        public int BorderRadius { get => borderRadius; set => borderRadius = value; }
        public Color BorderColor { get => borderColor; set => borderColor = value; }


        public Color BackGroundColor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }

        }


        // Constructor

        public RoundedButtons()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 40);
            this.BackColor = Color.MediumSlateBlue;
            this.ForeColor = Color.White;

        }

        // Methods

        public GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            return path;
        }


        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            RectangleF rectsurface = new RectangleF(0, 0, this.Width, this.Height);
            RectangleF rectborder = new RectangleF(1, 1, this.Width - 0.8F, this.Height - 1);
            if (borderRadius > 2)   // Rounded Button
            {
                using (GraphicsPath pathsurface = GetFigurePath(rectsurface, borderRadius))
                using (GraphicsPath pathborder = GetFigurePath(rectborder, borderRadius))
                using (Pen pensurface = new Pen(this.Parent.BackColor, 2))
                using (Pen penborder = new Pen(borderColor, borderize))
                {
                    penborder.Alignment = PenAlignment.Inset;

                    //  Button Surface
                    this.Region = new Region(pathsurface);

                    // Draw Surface border for HD View
                    pevent.Graphics.DrawPath(pensurface, pathsurface);


                    // Button Border
                    if (borderize >= 1)
                        pevent.Graphics.DrawPath(penborder, pathborder);

                }

            }
            else      // Normal Button
            {
                this.Region = new Region(rectsurface);
                if (borderize >= 1)
                {
                    using (Pen penborder = new Pen(borderColor, borderize))
                    {
                        penborder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penborder, 0, 0, this.Width - 1, this.Height - 1);

                    }
                }
            }
        }


        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChange);
        }

        private void Container_BackColorChange(object sender, EventArgs e)
        {
            if (this.DesignMode)
                this.Invalidate();

        }

    }
}
