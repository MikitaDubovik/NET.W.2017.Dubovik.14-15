using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace PL.ASP.NET_MVC.Infrastructure
{
    public class CaptchaImage : IDisposable
    {
        public const string CaptchaValueKey = "CaptchaImageText";

        // Internal properties.
        private string text;
        private int width;
        private int height;
        private string familyName;
        private Bitmap image;

        //// For generating random numbers.
        private Random random = new Random();

        public CaptchaImage(string s, int width, int height)
        {
            text = s;
            SetDimensions(width, height);
            GenerateImage();
        }

        public CaptchaImage(string s, int width, int height, string familyName)
        {
            text = s;
            SetDimensions(width, height);
            SetFamilyName(familyName);
            GenerateImage();
        }

        // ====================================================================
        // This member overrides Object.Finalize.
        // ====================================================================
        ~CaptchaImage()
        {
            Dispose(false);
        }

        public string Text => text;

        public Bitmap Image => image;

        public int Width => width;

        public int Height => height;

        // ====================================================================
        // Releases all resources used by this object.
        // ====================================================================
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        // ====================================================================
        // Custom Dispose method to clean up unmanaged resources.
        // ====================================================================
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //// Dispose of the bitmap.
                image.Dispose();
            }
        }

        // ====================================================================
        // Sets the image _width and _hidth.
        // ====================================================================
        private void SetDimensions(int _width, int _hidth)
        {
            // Check the _width and _hidth.
            if (_width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_width), _width, "Argument out of range, must be greater than zero.");
            }

            if (_hidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_hidth), _hidth, "Argument out of range, must be greater than zero.");
            }

            width = _width;
            height = _hidth;
        }

        // ====================================================================
        // Sets the font used for the image text.
        // ====================================================================
        private void SetFamilyName(string _familyName)
        {
            // If the named font is not installed, default to a system font.
            try
            {
                Font font = new Font(_familyName, 12F);
                familyName = _familyName;
                font.Dispose();
            }
            catch (Exception)
            {
                familyName = FontFamily.GenericSerif.Name;
            }
        }

        // ====================================================================
        // Creates the bitmap image.
        // ====================================================================
        private void GenerateImage()
        {
            //// Create a new 32-bit bitmap image.
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            //// Create a graphics object for drawing.
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, width, height);

            //// Fill in the background.
            HatchBrush hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, Color.White);
            g.FillRectangle(hatchBrush, rect);

            //// Set up the text font.
            SizeF size;
            float fontSize = rect.Height + 1;
            Font font;
            //// Adjust the font size until the text fits within the image.
            do
            {
                fontSize--;
                font = new Font(familyName, fontSize, FontStyle.Bold);
                size = g.MeasureString(text, font);
            }
            while (size.Width > rect.Width);

            //// Set up the text format.
            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            //// Create a path using the text and warp it randomly.
            GraphicsPath path = new GraphicsPath();
            path.AddString(text, font.FontFamily, (int)font.Style, font.Size, rect, format);
            float v = 4F;
            PointF[] points =
            {
                new PointF(random.Next(rect.Width) / v, random.Next(rect.Height) / v),
                new PointF(rect.Width - (random.Next(rect.Width) / v), random.Next(rect.Height) / v),
                new PointF(random.Next(rect.Width) / v, rect.Height - (random.Next(rect.Height) / v)),
                new PointF(rect.Width - (random.Next(rect.Width) / v), rect.Height - (random.Next(rect.Height) / v))
            };
            Matrix matrix = new Matrix();
            matrix.Translate(0F, 0F);
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

            //// Draw the text.
            hatchBrush = new HatchBrush(HatchStyle.LargeConfetti, Color.LightGray, Color.DarkGray);
            g.FillPath(hatchBrush, path);

            //// Add some random noise.
            int m = Math.Max(rect.Width, rect.Height);
            for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
            {
                int x = random.Next(rect.Width);
                int y = random.Next(rect.Height);
                int w = random.Next(m / 50);
                int h = random.Next(m / 50);
                g.FillEllipse(hatchBrush, x, y, w, h);
            }

            //// Clean up.
            font.Dispose();
            hatchBrush.Dispose();
            g.Dispose();

            //// Set the image.
            image = bitmap;
        }
    }
}