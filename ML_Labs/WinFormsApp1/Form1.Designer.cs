namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            knn_button = new Button();
            knn_Core_button = new Button();
            knn_weight_button = new Button();
            Stolp_Knn_button = new Button();
            Stolp_Knn_Core_Button = new Button();
            Stolp_Knn_Weight_button = new Button();
            DataSelector = new ComboBox();
            SuspendLayout();
            // 
            // knn_button
            // 
            knn_button.Location = new Point(12, 12);
            knn_button.Name = "knn_button";
            knn_button.Size = new Size(94, 50);
            knn_button.TabIndex = 0;
            knn_button.Text = "knn";
            knn_button.UseVisualStyleBackColor = true;
            knn_button.Click += knn_button_Click;
            // 
            // knn_Core_button
            // 
            knn_Core_button.Location = new Point(12, 89);
            knn_Core_button.Name = "knn_Core_button";
            knn_Core_button.Size = new Size(94, 47);
            knn_Core_button.TabIndex = 1;
            knn_Core_button.Text = "knn_Core";
            knn_Core_button.UseVisualStyleBackColor = true;
            knn_Core_button.Click += knn_Core_button_Click;
            // 
            // knn_weight_button
            // 
            knn_weight_button.Location = new Point(12, 167);
            knn_weight_button.Name = "knn_weight_button";
            knn_weight_button.Size = new Size(94, 43);
            knn_weight_button.TabIndex = 0;
            knn_weight_button.Text = "knn_weight";
            knn_weight_button.Click += knn_weight_button_Click;
            // 
            // Stolp_Knn_button
            // 
            Stolp_Knn_button.Location = new Point(133, 12);
            Stolp_Knn_button.Name = "Stolp_Knn_button";
            Stolp_Knn_button.Size = new Size(117, 50);
            Stolp_Knn_button.TabIndex = 2;
            Stolp_Knn_button.Text = "Stolp_Knn";
            Stolp_Knn_button.UseVisualStyleBackColor = true;
            Stolp_Knn_button.Click += Stolp_Knn_button_Click;
            // 
            // Stolp_Knn_Core_Button
            // 
            Stolp_Knn_Core_Button.Location = new Point(133, 89);
            Stolp_Knn_Core_Button.Name = "Stolp_Knn_Core_Button";
            Stolp_Knn_Core_Button.Size = new Size(117, 47);
            Stolp_Knn_Core_Button.TabIndex = 3;
            Stolp_Knn_Core_Button.Text = "Stolp_Knn_Core";
            Stolp_Knn_Core_Button.UseVisualStyleBackColor = true;
            Stolp_Knn_Core_Button.Click += Stolp_Knn_Core_Button_Click;
            // 
            // Stolp_Knn_Weight_button
            // 
            Stolp_Knn_Weight_button.Location = new Point(133, 167);
            Stolp_Knn_Weight_button.Name = "Stolp_Knn_Weight_button";
            Stolp_Knn_Weight_button.Size = new Size(117, 43);
            Stolp_Knn_Weight_button.TabIndex = 4;
            Stolp_Knn_Weight_button.Text = "Stolp_Knn_Weight";
            Stolp_Knn_Weight_button.UseVisualStyleBackColor = true;
            Stolp_Knn_Weight_button.Click += Stolp_Knn_Weight_button_Click;
            // 
            // DataSelector
            // 
            DataSelector.FormattingEnabled = true;
            DataSelector.Location = new Point(299, 18);
            DataSelector.Name = "DataSelector";
            DataSelector.Size = new Size(121, 23);
            DataSelector.TabIndex = 0;
            DataSelector.Text = "Выберете датасет";
            DataSelector.SelectedIndexChanged += DataSelector_SelectedIndexChanged;
            DataSelector.Items.AddRange(new object[] {
                "Рандом",
                "Рандом фиксированный",
                "Пресет_1",
                "Пресет_2",
                "Другое..."});
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(432, 250);
            Controls.Add(DataSelector);
            Controls.Add(Stolp_Knn_Weight_button);
            Controls.Add(Stolp_Knn_Core_Button);
            Controls.Add(Stolp_Knn_button);
            Controls.Add(knn_weight_button);
            Controls.Add(knn_Core_button);
            Controls.Add(knn_button);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button knn_button;
        private Button knn_Core_button;
        private Button knn_weight_button;
        private Button Stolp_Knn_button;
        private Button Stolp_Knn_Core_Button;
        private Button Stolp_Knn_Weight_button;
        private ComboBox DataSelector;
    }
}
