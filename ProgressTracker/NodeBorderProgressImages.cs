#region License (c)
// The MIT License (MIT)
// Copyright (c) 2013 Simon Coghlan (Smurf-IV)
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
// associated documentation files (the "Software"), to deal in the Software without restriction, 
// including without limitation the rights to use, copy, modify, merge, publish, distribute, 
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is 
// furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or 
// substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
// NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace ProgressTracker
{
   public partial class NodeBorderProgressTracker
   {
      private readonly Image[] images = new Image[8];
      private void CreateImages()
      {
         Control control = tableLayoutPanel1.GetControlFromPosition(0, 0);
         int height = Math.Max(control.Height, 40);
         int width = Math.Max(control.Width, 40);
         float circHeight = Math.Min(width, height);
         float percent10 = (float)Math.Ceiling(circHeight * .1);
         float percent15 = (float)Math.Ceiling(circHeight * .15);
         float percent20 = (float)Math.Ceiling(circHeight * .2);
         circHeight -= percent20;
         float circHeight2 = circHeight / 2;
         float yMiddle = height / 2f;
         float yMiddleCirc = yMiddle - circHeight2;

         using (SolidBrush fillBrush = new SolidBrush(fillColor))
         {
            using (Pen line = new Pen(lineColor, lineWidth))
            {
               Bitmap leftEmpty = new Bitmap(width, height);
               leftEmpty.MakeTransparent();
               using (Graphics g = Graphics.FromImage(leftEmpty))
               {
                  g.InterpolationMode = InterpolationMode.High;
                  g.SmoothingMode = SmoothingMode.HighQuality;
                  RectangleF rect = new RectangleF(width - circHeight2, yMiddleCirc, circHeight, circHeight);
                  rect.Inflate(-percent10, -percent10);
                  g.DrawArc(line, rect, 80, 200);
               }
               AddShadow(leftEmpty);
               images[0] = leftEmpty;

               Bitmap leftFull = new Bitmap(width, height);
               leftEmpty.MakeTransparent();
               using (Graphics g = Graphics.FromImage(leftFull))
               {
                  g.InterpolationMode = InterpolationMode.High;
                  g.SmoothingMode = SmoothingMode.HighQuality;

                  g.DrawImage(leftEmpty, 0, 0);
                  RectangleF rect = new RectangleF(width - circHeight2, yMiddleCirc, circHeight, circHeight);
                  rect.Inflate(-percent20, -percent20);
                  g.FillPie(fillBrush, rect.X, rect.Y, rect.Width, rect.Height, 90, 180);
               }
               images[1] = leftFull;

               Bitmap leftLine = new Bitmap(width, height);
               leftEmpty.MakeTransparent();
               using (Graphics g = Graphics.FromImage(leftLine))
               {
                  g.InterpolationMode = InterpolationMode.High;
                  g.SmoothingMode = SmoothingMode.HighQuality;
                  RectangleF rect = new RectangleF(-circHeight2, yMiddleCirc, circHeight, circHeight);
                  rect.Inflate(-percent10, -percent10);
                  g.DrawArc(line, rect, 270, 60);
                  g.DrawArc(line, rect, 30, 60);
                  g.DrawLine(line, circHeight2 - percent15, yMiddle - percent15, width, yMiddle - percent15);
                  g.DrawLine(line, circHeight2 - percent15, yMiddle + percent15, width, yMiddle + percent15);
               }
               AddShadow(leftLine);
               images[2] = leftLine;

               Bitmap leftLineFill = new Bitmap(width, height);
               leftEmpty.MakeTransparent();
               using (Graphics g = Graphics.FromImage(leftLineFill))
               {
                  g.InterpolationMode = InterpolationMode.High;
                  g.SmoothingMode = SmoothingMode.HighQuality;
                  g.DrawImage(leftLine, 0, 0);
                  RectangleF rect = new RectangleF(-circHeight2, yMiddleCirc, circHeight, circHeight);
                  rect.Inflate(-percent20, -percent20);
                  g.FillPie(fillBrush, rect.X, rect.Y, rect.Width, rect.Height, 270, 180);
                  g.FillRectangle(fillBrush, 0, yMiddle - percent10, width, percent20);
               }
               images[3] = leftLineFill;

               // Now rotate the above
               Bitmap rightLine = (Bitmap)leftLine.Clone();
               rightLine.RotateFlip(RotateFlipType.RotateNoneFlipX);
               images[4] = rightLine;

               Bitmap rightLineFill = (Bitmap)leftLineFill.Clone();
               rightLineFill.RotateFlip(RotateFlipType.RotateNoneFlipX);
               images[5] = rightLineFill;

               Bitmap rightEmpty = (Bitmap)leftEmpty.Clone();
               rightEmpty.RotateFlip(RotateFlipType.RotateNoneFlipX);
               images[6] = rightEmpty;

               Bitmap rightFull = (Bitmap)leftFull.Clone();
               rightFull.RotateFlip(RotateFlipType.RotateNoneFlipX);
               images[7] = rightFull;
            }
         }
      }

      private void AddShadow(Image image)
      {
         int xOffset, yOfset;
         switch (ShadowOrientation)
         {
            case ContentAlignment.TopLeft:
               xOffset = -2;
               yOfset = -2;
               break;
            case ContentAlignment.TopCenter:
               xOffset = 2;
               yOfset = 0;
               break;
            case ContentAlignment.TopRight:
               xOffset = 2;
               yOfset = -2;
               break;
            case ContentAlignment.MiddleLeft:
               xOffset = 0;
               yOfset = -2;
               break;
            case ContentAlignment.MiddleRight:
               xOffset = 2;
               yOfset = 0;
               break;
            case ContentAlignment.BottomLeft:
               xOffset = -2;
               yOfset = 2;
               break;
            case ContentAlignment.BottomCenter:
               xOffset = 0;
               yOfset = 2;
               break;
            case ContentAlignment.BottomRight:
               xOffset = 2;
               yOfset = 2;
               break;
            //case ContentAlignment.MiddleCenter:
            // No work to do here.
            default:
               return;
         }
         AddShadow(image, xOffset, yOfset);
      }

      // Create the shadow matrix
      static readonly ColorMatrix sm = new ColorMatrix
      {
         Matrix00 = 1.5f,
         Matrix11 = 1.5f,
         Matrix22 = 1.5f,
         Matrix33 = 0.2f,
         Matrix44 = 1.0f
      };

      private static void AddShadow(Image image, int xOffset, int yOfset)
      {
         Bitmap source = (Bitmap)image.Clone();

         Rectangle destRect = new Rectangle(0, 0, source.Width, source.Height);

         Rectangle shadowDestRect = new Rectangle
         {
            X = xOffset,
            Y = yOfset,
            Width = destRect.Width,
            Height = destRect.Height
         };

         using (ImageAttributes ia = new ImageAttributes())
         {
            ia.SetColorMatrix(sm);
            using (Graphics g = Graphics.FromImage(image))
            {
               g.InterpolationMode = InterpolationMode.High;
               g.SmoothingMode = SmoothingMode.HighQuality;
               GraphicsContainer gc = g.BeginContainer();
               // Draw the shadow 1st
               g.DrawImage(source, shadowDestRect, 0, 0, shadowDestRect.Width, shadowDestRect.Height, GraphicsUnit.Pixel, ia);
               g.EndContainer(gc);

               // Now draw the picture
               g.DrawImage(source, destRect.X, destRect.Y);
            }
         }
      }

      private void AssignImages()
      {
         TableLayoutControlCollection controls = tableLayoutPanel1.Controls;
         int offset = 0;
         int columnCount = textLabels.Length * 2 - 1;
         foreach (Label control in controls.OfType<Label>())
         {
            if (offset == 0)
            {
               control.Image = (step > 0) ? images[1] : images[0];
            }
            else if (offset == columnCount)
            {
               control.Image = (step >= offset) ? images[7] : images[6];
            }
            else
            {
               control.Image = (offset % 2 == 1)
                  ? ((step > offset) ? images[3] : images[2])
                  : ((step > offset) ? images[5] : images[4]);
            }
            offset++;
            if (offset > columnCount)
               break;
         }
      }
   }
}
