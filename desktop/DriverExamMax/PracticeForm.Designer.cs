namespace DriverExam
{
	partial class PracticeForm
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
			Title = new TextBox();
			panel1 = new Panel();
			Analysis = new TextBox();
			label8 = new Label();
			answerLabel = new Label();
			label6 = new Label();
			button1 = new Button();
			button2 = new Button();
			button3 = new Button();
			button4 = new Button();
			label1 = new Label();
			label2 = new Label();
			rightLabel = new Label();
			wrongLabel = new Label();
			CountQuestion = new Label();
			pictureBox1 = new PictureBox();
			backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			optionPanel = new Panel();
			label3 = new Label();
			current = new Label();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			// 
			// Title
			// 
			Title.Enabled = false;
			Title.Location = new Point(53, 133);
			Title.Margin = new Padding(4);
			Title.Multiline = true;
			Title.Name = "Title";
			Title.Size = new Size(477, 272);
			Title.TabIndex = 0;
			// 
			// panel1
			// 
			panel1.Controls.Add(Analysis);
			panel1.Controls.Add(label8);
			panel1.Controls.Add(answerLabel);
			panel1.Controls.Add(label6);
			panel1.Location = new Point(584, 41);
			panel1.Margin = new Padding(4);
			panel1.Name = "panel1";
			panel1.Size = new Size(367, 796);
			panel1.TabIndex = 5;
			panel1.Visible = false;
			// 
			// Analysis
			// 
			Analysis.Enabled = false;
			Analysis.Location = new Point(23, 113);
			Analysis.Margin = new Padding(4);
			Analysis.Multiline = true;
			Analysis.Name = "Analysis";
			Analysis.Size = new Size(321, 660);
			Analysis.TabIndex = 18;
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Location = new Point(35, 64);
			label8.Margin = new Padding(4, 0, 4, 0);
			label8.Name = "label8";
			label8.Size = new Size(82, 24);
			label8.TabIndex = 17;
			label8.Text = "题目解析";
			// 
			// answerLabel
			// 
			answerLabel.AutoSize = true;
			answerLabel.Location = new Point(97, 15);
			answerLabel.Margin = new Padding(4, 0, 4, 0);
			answerLabel.Name = "answerLabel";
			answerLabel.Size = new Size(63, 24);
			answerLabel.TabIndex = 16;
			answerLabel.Text = "label7";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new Point(35, 15);
			label6.Margin = new Padding(4, 0, 4, 0);
			label6.Name = "label6";
			label6.Size = new Size(46, 24);
			label6.TabIndex = 15;
			label6.Text = "答案";
			// 
			// button1
			// 
			button1.Location = new Point(160, 52);
			button1.Margin = new Padding(4);
			button1.Name = "button1";
			button1.Size = new Size(132, 73);
			button1.TabIndex = 6;
			button1.Text = "答题";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// button2
			// 
			button2.Location = new Point(287, 52);
			button2.Margin = new Padding(4);
			button2.Name = "button2";
			button2.Size = new Size(132, 73);
			button2.TabIndex = 7;
			button2.Text = "背题";
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// button3
			// 
			button3.Location = new Point(397, 863);
			button3.Margin = new Padding(4);
			button3.Name = "button3";
			button3.Size = new Size(128, 72);
			button3.TabIndex = 8;
			button3.Text = "上一题";
			button3.UseVisualStyleBackColor = true;
			button3.Click += button3_Click;
			// 
			// button4
			// 
			button4.Location = new Point(532, 863);
			button4.Margin = new Padding(4);
			button4.Name = "button4";
			button4.Size = new Size(128, 72);
			button4.TabIndex = 9;
			button4.Text = "下一题";
			button4.UseVisualStyleBackColor = true;
			button4.Click += button4_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(39, 887);
			label1.Margin = new Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new Size(46, 24);
			label1.TabIndex = 10;
			label1.Text = "答对";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(182, 887);
			label2.Margin = new Padding(4, 0, 4, 0);
			label2.Name = "label2";
			label2.Size = new Size(46, 24);
			label2.TabIndex = 11;
			label2.Text = "答错";
			// 
			// rightLabel
			// 
			rightLabel.AutoSize = true;
			rightLabel.Location = new Point(106, 887);
			rightLabel.Margin = new Padding(4, 0, 4, 0);
			rightLabel.Name = "rightLabel";
			rightLabel.Size = new Size(98, 24);
			rightLabel.TabIndex = 12;
			rightLabel.Text = "rightLabel";
			// 
			// wrongLabel
			// 
			wrongLabel.AutoSize = true;
			wrongLabel.Location = new Point(236, 887);
			wrongLabel.Margin = new Padding(4, 0, 4, 0);
			wrongLabel.Name = "wrongLabel";
			wrongLabel.Size = new Size(65, 24);
			wrongLabel.TabIndex = 13;
			wrongLabel.Text = "wrong";
			// 
			// CountQuestion
			// 
			CountQuestion.AutoSize = true;
			CountQuestion.Location = new Point(853, 899);
			CountQuestion.Margin = new Padding(4, 0, 4, 0);
			CountQuestion.Name = "CountQuestion";
			CountQuestion.Size = new Size(21, 24);
			CountQuestion.TabIndex = 14;
			CountQuestion.Text = "0";
			// 
			// pictureBox1
			// 
			pictureBox1.Location = new Point(240, 415);
			pictureBox1.Margin = new Padding(4);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(286, 181);
			pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBox1.TabIndex = 15;
			pictureBox1.TabStop = false;
			pictureBox1.Click += pictureBox1_Click;
			// 
			// optionPanel
			// 
			optionPanel.Location = new Point(67, 604);
			optionPanel.Margin = new Padding(4);
			optionPanel.Name = "optionPanel";
			optionPanel.Size = new Size(458, 251);
			optionPanel.TabIndex = 16;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(827, 897);
			label3.Name = "label3";
			label3.Size = new Size(18, 24);
			label3.TabIndex = 17;
			label3.Text = "/";
			// 
			// current
			// 
			current.AutoSize = true;
			current.Location = new Point(791, 899);
			current.Name = "current";
			current.Size = new Size(21, 24);
			current.TabIndex = 18;
			current.Text = "0";
			// 
			// PracticeForm
			// 
			AutoScaleDimensions = new SizeF(11F, 24F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1010, 1170);
			Controls.Add(current);
			Controls.Add(label3);
			Controls.Add(optionPanel);
			Controls.Add(pictureBox1);
			Controls.Add(CountQuestion);
			Controls.Add(wrongLabel);
			Controls.Add(rightLabel);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(button4);
			Controls.Add(button3);
			Controls.Add(button2);
			Controls.Add(button1);
			Controls.Add(panel1);
			Controls.Add(Title);
			Margin = new Padding(4);
			Name = "PracticeForm";
			Text = "顺序练习";
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox Title;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.TextBox Analysis;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label answerLabel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label rightLabel;
		private System.Windows.Forms.Label wrongLabel;
		private System.Windows.Forms.Label CountQuestion;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.Panel optionPanel;
		private Label label3;
		private Label current;
	}
}