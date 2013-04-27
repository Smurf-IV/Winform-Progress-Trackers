namespace TestTracker
{
   partial class Form1
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.button1 = new System.Windows.Forms.Button();
         this.button2 = new System.Windows.Forms.Button();
         this.userControl11 = new ProgressTracker.NodeBorderProgressTracker();
         this.numberThenTextProgressTracker1 = new ProgressTracker.NumberThenTextProgressTracker();
         this.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.userControl11)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.numberThenTextProgressTracker1)).BeginInit();
         // 
         // button1
         // 
         this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.button1.Location = new System.Drawing.Point(24, 394);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(75, 23);
         this.button1.TabIndex = 1;
         this.button1.Text = "Back";
         this.button1.UseVisualStyleBackColor = true;
         this.button1.Click += new System.EventHandler(this.button1_Click);
         // 
         // button2
         // 
         this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.button2.Location = new System.Drawing.Point(156, 394);
         this.button2.Name = "button2";
         this.button2.Size = new System.Drawing.Size(75, 23);
         this.button2.TabIndex = 2;
         this.button2.Text = "Forward";
         this.button2.UseVisualStyleBackColor = true;
         this.button2.Click += new System.EventHandler(this.button2_Click);
         // 
         // userControl11
         // 
         this.userControl11.Dock = System.Windows.Forms.DockStyle.Top;
         this.userControl11.FillColor = System.Drawing.SystemColors.ControlLight;
         this.userControl11.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.userControl11.LineColor = System.Drawing.SystemColors.ControlDarkDark;
         this.userControl11.LineWidth = ((sbyte)(2));
         this.userControl11.Location = new System.Drawing.Point(0, 0);
         this.userControl11.Margin = new System.Windows.Forms.Padding(0);
         this.userControl11.Name = "userControl11";
         this.userControl11.ShadowOffset = ((byte)(0));
         this.userControl11.ShadowOrientation = System.Drawing.ContentAlignment.BottomCenter;
         this.userControl11.Size = new System.Drawing.Size(442, 123);
         this.userControl11.TabIndex = 0;
         this.userControl11.TextLabels = new object[] {
        ((object)("Sart")),
        ((object)("Middle")),
        ((object)("End"))};
         // 
         // numberThenTextProgressTracker1
         // 
         this.numberThenTextProgressTracker1.CurrentCellFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.numberThenTextProgressTracker1.Dock = System.Windows.Forms.DockStyle.Top;
         this.numberThenTextProgressTracker1.FillColor = System.Drawing.SystemColors.ControlText;
         this.numberThenTextProgressTracker1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.numberThenTextProgressTracker1.LineColor = System.Drawing.SystemColors.ControlText;
         this.numberThenTextProgressTracker1.LineWidth = ((sbyte)(2));
         this.numberThenTextProgressTracker1.Location = new System.Drawing.Point(0, 123);
         this.numberThenTextProgressTracker1.Margin = new System.Windows.Forms.Padding(0);
         this.numberThenTextProgressTracker1.Name = "numberThenTextProgressTracker1";
         this.numberThenTextProgressTracker1.ShadowOffset = ((byte)(0));
         this.numberThenTextProgressTracker1.ShadowOrientation = System.Drawing.ContentAlignment.BottomLeft;
         this.numberThenTextProgressTracker1.Size = new System.Drawing.Size(442, 61);
         this.numberThenTextProgressTracker1.TabIndex = 3;
         this.numberThenTextProgressTracker1.TextLabels = new object[] {
        ((object)("Start")),
        ((object)("Middle")),
        ((object)("End"))};
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(442, 423);
         this.Controls.Add(this.numberThenTextProgressTracker1);
         this.Controls.Add(this.button2);
         this.Controls.Add(this.button1);
         this.Controls.Add(this.userControl11);
         this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.Name = "Form1";
         this.Text = "Form1";
         ((System.ComponentModel.ISupportInitialize)(this.numberThenTextProgressTracker1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.userControl11)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private ProgressTracker.NodeBorderProgressTracker userControl11;
      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.Button button2;
      private ProgressTracker.NumberThenTextProgressTracker numberThenTextProgressTracker1;
   }
}

