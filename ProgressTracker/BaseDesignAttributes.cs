#region License (c)

// The MIT License (MIT)
// Copyright (c) 2013-2014 Simon Coghlan (Smurf-IV)
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

#endregion License (c)

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable UnusedMember.Global
namespace ProgressTracker
{
   [ToolboxItem(false)]
   [TypeDescriptionProvider(typeof(ReplaceTypeDescriptionProvider<BaseDesignAttributes, BaseDesignAttributesReplace>))]
   public abstract partial class BaseDesignAttributes : UserControl, ISupportInitialize
   {
      protected BaseDesignAttributes()
      {
         InitializeComponent();
         SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      }

      #region Abstract Methods

      /// <summary>
      /// Will take the created images and apply them to the controls in use in the derived class
      /// </summary>
      protected abstract void AssignImages();

      /// <summary>
      /// Draw the images in a "one off" manner for the current size
      /// </summary>
      protected abstract void CreateImages();

      /// <summary>
      /// Triggered when a Font is changed or Labels are changed
      /// </summary>
      protected abstract void ReSizeTable();

      #endregion Abstract Methods

      #region Design Attributes

      private ContentAlignment shadowOrientation = ContentAlignment.BottomLeft;
      protected Color fillColor = DefaultForeColor;
      protected Color lineColor = DefaultForeColor;
      protected sbyte lineWidth = 2;

      protected object[] textLabels = new object[]
         {
            "Start",
            "Middle",
            "End"
         };

      [Category("Tracker Attributes")]
      [Browsable(true)]
      [Description("The existence of the shadow and the direction used")]
      public ContentAlignment ShadowOrientation
      {
         get { return shadowOrientation; }
         set
         {
            if (shadowOrientation != value)
            {
               shadowOrientation = value;
               Redraw();
            }
         }
      }

      [Category("Tracker Attributes")]
      [Browsable(true)]
      [Description("The existence of the shadow and the direction used")]
      [DefaultValue(2)]
      public byte ShadowOffset
      {
         get { return shadowOffset; }
         set
         {
            if (shadowOffset != value)
            {
               shadowOffset = value;
               Redraw();
            }
         }
      }

      [Category("Tracker Attributes")]
      [Browsable(true)]
      [Description("The Fill Colour")]
      public Color FillColor
      {
         get { return fillColor; }
         set
         {
            if (fillColor != value)
            {
               fillColor = value;
               Redraw();
            }
         }
      }

      [Category("Tracker Attributes")]
      [Browsable(true)]
      [Description("The Line Colour")]
      public Color LineColor
      {
         get { return lineColor; }
         set
         {
            if (lineColor != value)
            {
               lineColor = value;
               Redraw();
            }
         }
      }

      [Category("Tracker Attributes")]
      [Browsable(true)]
      [Description("The Line Width")]
      public sbyte LineWidth
      {
         get { return lineWidth; }
         set
         {
            if (lineWidth != value)
            {
               lineWidth = value;
               Redraw();
            }
         }
      }

      [Category("Tracker Attributes")]
      [Browsable(true)]
      [Description("The Text Labels, relies on the ToString() for the label")]
      public object[] TextLabels
      {
         get { return textLabels; }
         set
         {
            if (value == null)
               throw new NoNullAllowedException("Cannot be set to null value");
            if (value.Length == 0)
               throw new ArgumentOutOfRangeException("value", "Must have at least 1 display value");
            textLabels = value;
            if (initialized)
            {
               ReSizeTable();
               Redraw();
            }
         }
      }

      /// <summary>
      /// Overrides the default button font to a more suitable headline font.
      /// </summary>
      public override Font Font
      {
         get
         {
            return base.Font;
         }
         set
         {
            if (!Equals(base.Font, value))
            {
               base.Font = value;
               if (initialized)
               {
                  ReSizeTable();
                  Redraw();
               }
            }
         }
      }

      #endregion Design Attributes

      #region Shadow control

      private byte shadowOffset;

      protected void AddShadow(Image image)
      {
         int xOffset, yOfset;
         switch (ShadowOrientation)
         {
            case ContentAlignment.TopLeft:
               xOffset = -shadowOffset;
               yOfset = -shadowOffset;
               break;

            case ContentAlignment.TopCenter:
               xOffset = shadowOffset;
               yOfset = 0;
               break;

            case ContentAlignment.TopRight:
               xOffset = shadowOffset;
               yOfset = -shadowOffset;
               break;

            case ContentAlignment.MiddleLeft:
               xOffset = 0;
               yOfset = -shadowOffset;
               break;

            case ContentAlignment.MiddleRight:
               xOffset = shadowOffset;
               yOfset = 0;
               break;

            case ContentAlignment.BottomLeft:
               xOffset = -shadowOffset;
               yOfset = shadowOffset;
               break;

            case ContentAlignment.BottomCenter:
               xOffset = 0;
               yOfset = shadowOffset;
               break;

            case ContentAlignment.BottomRight:
               xOffset = shadowOffset;
               yOfset = shadowOffset;
               break;
            //case ContentAlignment.MiddleCenter:
            // No work to do here.
            default:
               return;
         }
         AddShadow(image, xOffset, yOfset);
      }

      // Create the shadow matrix

      private static readonly ColorMatrix sm = new ColorMatrix
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
               g.DrawImage(source, shadowDestRect, 0, 0, shadowDestRect.Width, shadowDestRect.Height, GraphicsUnit.Pixel,
                           ia);
               g.EndContainer(gc);

               // Now draw the picture
               g.DrawImage(source, destRect.X, destRect.Y);
            }
         }
      }

      #endregion Shadow control

      #region Control events

      private void BaseDesignAttributes_Load(object sender, EventArgs e)
      {
         Redraw();
      }

      private void BaseDesignAttributes_Resize(object sender, EventArgs e)
      {
         Redraw();
      }

      internal void Redraw()
      {
         if (initialized)
         {
            CreateImages();
            AssignImages();
            Refresh();
         }
      }

      #endregion Control events

      #region Stepping handler

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public int Progress
      {
         get { return step; }
         set
         {
            if (value != step)
            {
               step = value;
               AssignImages();
            }
         }
      }

      private int step;

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public void Step(bool forward = true)
      {
         if (forward)
            Progress++;
         else
            Progress--;
      }

      #endregion Stepping handler

      #region ISupportInitialize

      private bool initialized;

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public void BeginInit()
      {
         initialized = false;
      }

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public void EndInit()
      {
         initialized = true;
         ReSizeTable();
         Redraw();
      }

      #endregion ISupportInitialize
   }

   internal class BaseDesignAttributesReplace : BaseDesignAttributes
   {
      protected override void AssignImages()
      {
         //throw new NotImplementedException();
      }

      protected override void CreateImages()
      {
         //throw new NotImplementedException();
      }

      protected override void ReSizeTable()
      {
      }
   }
}

// ReSharper restore UnusedMember.Global
// ReSharper restore MemberCanBeProtected.Global
// ReSharper restore MemberCanBePrivate.Global