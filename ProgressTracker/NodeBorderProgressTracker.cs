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

using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProgressTracker
{
   // Specifies a type that indicates the assembly to search, and the name 
   // of an image resource to look for. Has to be 256 colors, 16x16 bmp or ico!
   [ToolboxBitmap(typeof(NodeBorderProgressTracker), "Resources.NodeBorderProgressTracker.bmp")]
   [ToolboxItem(true)]
   public partial class NodeBorderProgressTracker : BaseDesignAttributes
   {
      public NodeBorderProgressTracker()
      {
         InitializeComponent();
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

         int columnCount = textLabels.Length * 2;
         //Now we will generate the table, setting up the row and column counts first
         tableLayoutPanel1.ColumnCount = columnCount;
         tableLayoutPanel1.RowCount = 2;

         for (int x = 0; x < columnCount; x++)
         {
            //First add a column
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            //Next, add a row.  Only do this when once, when creating the first column
            if (x == 0)
            {
               tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 75F));
               tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
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
         tableLayoutPanel1.ResumeLayout(true);
         Redraw();
      }

   }
}
