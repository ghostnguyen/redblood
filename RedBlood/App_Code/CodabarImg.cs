using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

/// <summary>
/// Summary description for Codabar
/// </summary>
public class CodabarImg
{
    private Dictionary<string, string> codeDef;
    public CodabarImg()
    {
        codeDef = new Dictionary<string, string>();
        codeDef.Add("0", "0000011");
        codeDef.Add("1", "0000110");
        codeDef.Add("2", "0001001");
        codeDef.Add("3", "1100000");
        codeDef.Add("4", "0010010");
        codeDef.Add("5", "1000010");
        codeDef.Add("6", "0100001");
        codeDef.Add("7", "0100100");
        codeDef.Add("8", "0110000");
        codeDef.Add("9", "1001000");
        codeDef.Add("-", "0001100");
        codeDef.Add("$", "0011000");
        codeDef.Add(":", "1000101");
        codeDef.Add("/", "1010001");
        codeDef.Add(".", "1010100");
        codeDef.Add("+", "0010101");
        codeDef.Add("a", "0011010");
        codeDef.Add("b", "0101001");
        codeDef.Add("c", "0001011");
        codeDef.Add("d", "0001110");
        codeDef.Add("t", "0011010");
        codeDef.Add("n", "0101001");
        codeDef.Add("*", "0001011");
        codeDef.Add("e", "0001110");

        Guid g = new Guid();
        byte[] d = g.ToByteArray();
    }

    private int CountWidth(char c)
    {
        return (7 + codeDef[c.ToString().ToLower()].Replace("0", "").Length + 1);
    }

    private int CountWidth(string code)
    {
        int width = 0;
        foreach (char ch in code)
        {
            width += CountWidth(ch);
        }
        return width;
    }

    private void DrawChar(Graphics graphic, char ch, Point start, int bold, int height)
    {
        string def = codeDef[ch.ToString().ToLower()];

        SolidBrush brush = new SolidBrush(Color.Black);

        Rectangle rect = new Rectangle(start, new Size(bold, height));

        for (int i = 0; i < 7; i++)
        {
            brush.Color = i % 2 == 0 ? Color.Black : Color.White;
            rect.Width = def[i] == '1' ? rect.Width * 2 : rect.Width;

            graphic.FillRectangle(brush, rect);

            start.X = def[i] == '1' ? start.X + bold * 2 : start.X + bold;

            rect.X = start.X;
            rect.Width = bold;
        }

        brush.Color = Color.White;
        rect.Width = bold;

        graphic.FillRectangle(brush, rect);
    }

    public Bitmap Draw(string code, int width, int height, bool isDrawCode, string topleft, string topright)
    {
        // Multiply the lenght of the code by 40 (just to have enough width)
        int w = CountWidth(code) * width;

        // Create a bitmap object of the width that we calculated and height of 100        
        Bitmap oBitmap = new Bitmap(w, height, PixelFormat.Format32bppArgb);

        if (isDrawCode)
            oBitmap = new Bitmap(w, oBitmap.Height + 16, PixelFormat.Format32bppArgb);

        if (!string.IsNullOrEmpty(topleft) || !string.IsNullOrEmpty(topright))
        {
            oBitmap = new Bitmap(w, oBitmap.Height + 16 + 16, PixelFormat.Format32bppArgb);
        }

        // then create a Graphic object for the bitmap we just created.
        Graphics oGraphics = Graphics.FromImage(oBitmap);

        // Now create a Font object for the Barcode Font
        // (in this case the IDAutomationHC39M) of 18 point size
        //Font oFont = new Font("Arial", 12);
        //Font oFont = new Font("CodabarMedium", 24);

        // Let's create the Point and Brushes for the barcode

        SolidBrush oBrush = new SolidBrush(Color.Black);

        // Now lets create the actual barcode image
        // with a rectangle filled with white color



        //oGraphics.FillRectangle(oBrush, 0, 0, w, height);

        // We have to put prefix and sufix of an asterisk (*),
        // in order to be a valid barcode
        //oGraphics.DrawString("*" + Code + "*", oFont, oBrushWrite, oPoint);

        //oGraphics.DrawString(Code, oFont, Brushes.Black, oPoint);
        //PointF oPoint = new PointF(0, 0);

        Font oFontNum = new Font("Courier New", 12);
        //Font oFontNum = new Font("Times New Roman", 12);

        Point start = new Point(0, 0);

        if (!string.IsNullOrEmpty(topleft) || !string.IsNullOrEmpty(topright))
        {
            oGraphics.DrawString(topleft, oFontNum, oBrush, 0, 0);
            oGraphics.DrawString(topright, oFontNum, oBrush, 0, 16);
            start.Y += 16 + 16;
        }

        foreach (char ch in code)
        {
            DrawChar(oGraphics, ch, start, width, height);
            start.X += CountWidth(ch) * width;
        }
        if (isDrawCode)
            oGraphics.DrawString(code, oFontNum, oBrush, 0, start.Y + height);

        return oBitmap;
    }


}

