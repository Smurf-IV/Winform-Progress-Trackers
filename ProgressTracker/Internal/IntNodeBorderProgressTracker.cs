
using System.ComponentModel;

namespace ProgressTracker.Internal
{
   [ToolboxItem(false)]
   internal class IntNodeBorderProgressTracker : NodeBorderProgressTracker
   {
      public IntNodeBorderProgressTracker()
      {
         InitializeComponent();
      }

      private void InitializeComponent()
      {
         ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
         this.SuspendLayout();
         // 
         // IntNodeBorderProgressTracker
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
         this.Name = "IntNodeBorderProgressTracker";
         this.Size = new System.Drawing.Size(562, 68);
         ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }
   }
}
