using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProgressTracker
{
   public partial class ProgressTracker : UserControl
   {
      public ProgressTracker()
      {
         SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
         InitializeComponent();
         CreateImages();
      }

      #region Design Attributes

      private ContentAlignment shadowOrientation = ContentAlignment.BottomLeft;
      private Color fillColor = DefaultForeColor;
      private Color lineColor = DefaultForeColor;
      private sbyte lineWidth = 2;

      private object[] textLabels = new object[]
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
               CreateImages();
               AssignImages();
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
               CreateImages();
               AssignImages();

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
               CreateImages();
               AssignImages();
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
               CreateImages();
               AssignImages();
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
            ReSizeTable();
            Refresh();
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
               ReSizeTable();
               Refresh();
            }
         }
      }

      #endregion


      private void ReSizeTable()
      {
         tableLayoutPanel1.ResumeLayout(true);
         #region Create table
         //Clear out the existing controls, we are generating a new table layout
         tableLayoutPanel1.Controls.Clear();

         //Clear out the existing row and column styles
         tableLayoutPanel1.ColumnStyles.Clear();
         tableLayoutPanel1.RowStyles.Clear();

         int columnCount = textLabels.Length * 2;
         const int rowCount = 2; // fixed to 2 rows at the moment.
         //Now we will generate the table, setting up the row and column counts first
         tableLayoutPanel1.ColumnCount = columnCount;
         tableLayoutPanel1.RowCount = 2;

         float colWidth = 100F / columnCount;

         for (int x = 0; x < columnCount; x++)
         {
            //First add a column
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, colWidth));

            for (int y = 0; y < rowCount; y++)
            {
               //Next, add a row.  Only do this when once, when creating the first column
               if (x == 0)
               {
                  tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
               }
            }
         }
         #endregion

         #region Fill in the image drawers

         for (int topOffset = 0; topOffset < columnCount; topOffset++)
         {
            Label labelImage = new Label
            {
               Dock = DockStyle.Fill,
               ImageAlign = ContentAlignment.MiddleCenter,
               AutoSize = false,
               Margin = new Padding(0)
            };
            tableLayoutPanel1.Controls.Add(labelImage, topOffset, 0);
         }

         #endregion


         #region Create labels and add to the bottom Row

         int labelOffset = 0;
         foreach (Label labelText in textLabels.Select(o => new Label
            {
               Dock = DockStyle.Fill,
               Text = o.ToString(),
               TextAlign = ContentAlignment.TopCenter,
// ReSharper disable RedundantThisQualifier
               Font = this.Font,
// ReSharper restore RedundantThisQualifier
               AutoEllipsis = true,
               AutoSize = false,
               Margin = new Padding(0)
            })
         )
         {
            tableLayoutPanel1.Controls.Add(labelText, labelOffset++, 1);
            tableLayoutPanel1.SetColumnSpan(labelText, 2);
         }
         #endregion
         tableLayoutPanel1.ResumeLayout(false);
         CreateImages();
         AssignImages();
      }

      private void Control_Resize(object sender, EventArgs e)
      {
         CreateImages();
         AssignImages();
         Refresh();
      }

      private int step = 0;

      public void Step(bool forward = true)
      {
         if (forward)
            step++;
         else
            step--;
         AssignImages();
      }

   }
}
