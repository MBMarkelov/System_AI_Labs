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
            density_button = new Button();
            button1 = new Button();
            dichotomy_button = new Button();
            golden_selection_button = new Button();
            coordinate_down_button = new Button();
            fibonachi_button = new Button();
            SuspendLayout();
            // 
            // knn_button
            // 
            knn_button.Location = new Point(14, 16);
            knn_button.Margin = new Padding(3, 4, 3, 4);
            knn_button.Name = "knn_button";
            knn_button.Size = new Size(107, 67);
            knn_button.TabIndex = 0;
            knn_button.Text = "knn";
            knn_button.UseVisualStyleBackColor = true;
            knn_button.Click += knn_button_Click;
            // 
            // knn_Core_button
            // 
            knn_Core_button.Location = new Point(14, 119);
            knn_Core_button.Margin = new Padding(3, 4, 3, 4);
            knn_Core_button.Name = "knn_Core_button";
            knn_Core_button.Size = new Size(107, 63);
            knn_Core_button.TabIndex = 1;
            knn_Core_button.Text = "knn_Core";
            knn_Core_button.UseVisualStyleBackColor = true;
            knn_Core_button.Click += knn_Core_button_Click;
            // 
            // knn_weight_button
            // 
            knn_weight_button.Location = new Point(14, 223);
            knn_weight_button.Margin = new Padding(3, 4, 3, 4);
            knn_weight_button.Name = "knn_weight_button";
            knn_weight_button.Size = new Size(107, 57);
            knn_weight_button.TabIndex = 0;
            knn_weight_button.Text = "knn_weight";
            knn_weight_button.Click += knn_weight_button_Click;
            // 
            // Stolp_Knn_button
            // 
            Stolp_Knn_button.Location = new Point(152, 16);
            Stolp_Knn_button.Margin = new Padding(3, 4, 3, 4);
            Stolp_Knn_button.Name = "Stolp_Knn_button";
            Stolp_Knn_button.Size = new Size(134, 67);
            Stolp_Knn_button.TabIndex = 2;
            Stolp_Knn_button.Text = "Stolp_Knn";
            Stolp_Knn_button.UseVisualStyleBackColor = true;
            Stolp_Knn_button.Click += Stolp_Knn_button_Click;
            // 
            // Stolp_Knn_Core_Button
            // 
            Stolp_Knn_Core_Button.Location = new Point(152, 119);
            Stolp_Knn_Core_Button.Margin = new Padding(3, 4, 3, 4);
            Stolp_Knn_Core_Button.Name = "Stolp_Knn_Core_Button";
            Stolp_Knn_Core_Button.Size = new Size(134, 63);
            Stolp_Knn_Core_Button.TabIndex = 3;
            Stolp_Knn_Core_Button.Text = "Stolp_Knn_Core";
            Stolp_Knn_Core_Button.UseVisualStyleBackColor = true;
            Stolp_Knn_Core_Button.Click += Stolp_Knn_Core_Button_Click;
            // 
            // Stolp_Knn_Weight_button
            // 
            Stolp_Knn_Weight_button.Location = new Point(152, 223);
            Stolp_Knn_Weight_button.Margin = new Padding(3, 4, 3, 4);
            Stolp_Knn_Weight_button.Name = "Stolp_Knn_Weight_button";
            Stolp_Knn_Weight_button.Size = new Size(134, 57);
            Stolp_Knn_Weight_button.TabIndex = 4;
            Stolp_Knn_Weight_button.Text = "Stolp_Knn_Weight";
            Stolp_Knn_Weight_button.UseVisualStyleBackColor = true;
            Stolp_Knn_Weight_button.Click += Stolp_Knn_Weight_button_Click;
            // 
            // DataSelector
            // 
            DataSelector.FormattingEnabled = true;
            DataSelector.Items.AddRange(new object[] { "Рандом", "Рандом фиксированный", "Пресет_1", "Пресет_2", "Другое..." });
            DataSelector.Location = new Point(342, 24);
            DataSelector.Margin = new Padding(3, 4, 3, 4);
            DataSelector.Name = "DataSelector";
            DataSelector.Size = new Size(138, 28);
            DataSelector.TabIndex = 0;
            DataSelector.Text = "Выберете датасет";
            DataSelector.SelectedIndexChanged += DataSelector_SelectedIndexChanged;
            // 
            // density_button
            // 
            density_button.Location = new Point(342, 119);
            density_button.Margin = new Padding(3, 4, 3, 4);
            density_button.Name = "density_button";
            density_button.Size = new Size(138, 63);
            density_button.TabIndex = 5;
            density_button.Text = "ядерное сглаживание";
            density_button.UseVisualStyleBackColor = true;
            density_button.Click += density_button_Click;
            // 
            // button1
            // 
            button1.Location = new Point(342, 223);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(134, 57);
            button1.TabIndex = 6;
            button1.Text = "KR_1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dichotomy_button
            // 
            dichotomy_button.Location = new Point(14, 303);
            dichotomy_button.Margin = new Padding(3, 4, 3, 4);
            dichotomy_button.Name = "dichotomy_button";
            dichotomy_button.Size = new Size(107, 57);
            dichotomy_button.TabIndex = 7;
            dichotomy_button.Text = "dichotomy";
            dichotomy_button.Click += dichotomy_button_Click;
            // 
            // golden_selection_button
            // 
            golden_selection_button.Location = new Point(342, 303);
            golden_selection_button.Margin = new Padding(3, 4, 3, 4);
            golden_selection_button.Name = "golden_selection_button";
            golden_selection_button.Size = new Size(107, 57);
            golden_selection_button.TabIndex = 9;
            golden_selection_button.Text = "golden_selection";
            // 
            // coordinate_down_button
            // 
            coordinate_down_button.Location = new Point(14, 377);
            coordinate_down_button.Margin = new Padding(3, 4, 3, 4);
            coordinate_down_button.Name = "coordinate_down_button";
            coordinate_down_button.Size = new Size(107, 57);
            coordinate_down_button.TabIndex = 10;
            coordinate_down_button.Text = "coordinate_down";
            coordinate_down_button.Click += coordinate_down_button_Click;
            // 
            // fibonachi_button
            // 
            fibonachi_button.Location = new Point(152, 303);
            fibonachi_button.Margin = new Padding(3, 4, 3, 4);
            fibonachi_button.Name = "fibonachi_button";
            fibonachi_button.Size = new Size(107, 57);
            fibonachi_button.TabIndex = 11;
            fibonachi_button.Text = "fibonachi";
            fibonachi_button.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(498, 476);
            Controls.Add(fibonachi_button);
            Controls.Add(coordinate_down_button);
            Controls.Add(golden_selection_button);
            Controls.Add(dichotomy_button);
            Controls.Add(button1);
            Controls.Add(density_button);
            Controls.Add(DataSelector);
            Controls.Add(Stolp_Knn_Weight_button);
            Controls.Add(Stolp_Knn_Core_Button);
            Controls.Add(Stolp_Knn_button);
            Controls.Add(knn_weight_button);
            Controls.Add(knn_Core_button);
            Controls.Add(knn_button);
            Margin = new Padding(3, 4, 3, 4);
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
        private Button density_button;
        private Button button1;
        private Button dichotomy_button;
        private Button button3;
        private Button golden_selection_button;
        private Button coordinate_down_button;
        private Button fibonachi_button;
    }
}
