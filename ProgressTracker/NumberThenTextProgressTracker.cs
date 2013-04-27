﻿using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProgressTracker
{
   [ToolboxItem(true)]
   public partial class NumberThenTextProgressTracker : BaseDesignAttributes
   {
      public NumberThenTextProgressTracker()
      {
         InitializeComponent();
      }

      #region Design Attributes

      private Font currentCellFont = DefaultFont;

      [Category("Tracker Attributes")]
      [Browsable(true)]
      [Description("Font for Current")]
      public Font CurrentCellFont
      {
         get { return currentCellFont; }
         set
         {
            if (!currentCellFont.Equals( value) )
            {
               currentCellFont = value;
               Refresh();
            }
         }
      }

#endregion

      protected override void AssignImages()
      {
         //throw new System.NotImplementedException();
      }

      protected override void CreateImages()
      {
         //throw new System.NotImplementedException();
      }

      protected override void ReSizeTable()
      {
         tableLayoutPanel1.SuspendLayout();

         #region Create table

         //Clear out the existing controls, we are generating a new table layout
         tableLayoutPanel1.Controls.Clear();

         //Clear out the existing row and column styles
         tableLayoutPanel1.ColumnStyles.Clear();
         tableLayoutPanel1.RowStyles.Clear();

         int columnCount = textLabels.Length;
         //Now we will generate the table, setting up the row and column counts first
         tableLayoutPanel1.ColumnCount = columnCount;
         tableLayoutPanel1.RowCount = 1;

         for (int x = 0; x < columnCount; x++)
         {
            //First add a column
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            //Next, add a row.  Only do this when once, when creating the first column
            if (x == 0)
            {
               tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            }
         }
         #endregion

         #region Create labels and add to the bottom Row

         int labelOffset = 0;
         foreach (Label labelText in textLabels.Select(o => new Label
         {
            Dock = DockStyle.Fill,
            Text = o.ToString(),
            TextAlign = ContentAlignment.MiddleLeft,
            // ReSharper disable RedundantThisQualifier
            Font = this.Font,
            // ReSharper restore RedundantThisQualifier
            AutoEllipsis = false,
            AutoSize = false,
            Margin = new Padding(0)
         })
         )
         {
            tableLayoutPanel1.Controls.Add(labelText, labelOffset++, 0);
         }
         #endregion
         tableLayoutPanel1.ResumeLayout(true);
         Redraw();
      }
   }
}
