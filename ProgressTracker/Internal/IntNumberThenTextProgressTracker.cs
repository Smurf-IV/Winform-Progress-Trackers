
using System.ComponentModel;

namespace ProgressTracker.Internal
{
   [ToolboxItem(false)]
   internal class IntNumberThenTextProgressTracker : NumberThenTextProgressTracker
   {
      public IntNumberThenTextProgressTracker()
      {
         InitializeComponent();
      }

      private void InitializeComponent()
      {
         ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
         this.SuspendLayout();
         // 
         // IntNumberThenTextProgressTracker
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
         this.Name = "IntNumberThenTextProgressTracker";
         this.Size = new System.Drawing.Size(565, 54);
         ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

   }
}
