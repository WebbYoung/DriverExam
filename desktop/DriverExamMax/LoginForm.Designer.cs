namespace DriverExam
{
	partial class LoginForm
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label3 = new System.Windows.Forms.Label();
			this.RememberMe = new System.Windows.Forms.CheckBox();
			this.IAgree = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.UserPwd = new System.Windows.Forms.TextBox();
			this.UserName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.linkLabel2);
			this.groupBox1.Controls.Add(this.linkLabel1);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.RememberMe);
			this.groupBox1.Controls.Add(this.IAgree);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Controls.Add(this.button3);
			this.groupBox1.Controls.Add(this.UserPwd);
			this.groupBox1.Controls.Add(this.UserName);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(46, 38);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(541, 391);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "登录";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(301, 292);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(26, 18);
			this.label4.TabIndex = 9;
			this.label4.Text = "是";
			// 
			// linkLabel2
			// 
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.Location = new System.Drawing.Point(330, 292);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(116, 18);
			this.linkLabel2.TabIndex = 8;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "《隐私政策》";
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(161, 292);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(152, 18);
			this.linkLabel1.TabIndex = 7;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "《用户使用协议》";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(161, 341);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(260, 18);
			this.label3.TabIndex = 6;
			this.label3.Text = "不是自己的电脑不要勾选此选项";
			// 
			// RememberMe
			// 
			this.RememberMe.AutoSize = true;
			this.RememberMe.Location = new System.Drawing.Point(67, 340);
			this.RememberMe.Name = "RememberMe";
			this.RememberMe.Size = new System.Drawing.Size(88, 22);
			this.RememberMe.TabIndex = 5;
			this.RememberMe.Text = "记住我";
			this.RememberMe.UseVisualStyleBackColor = true;
			// 
			// IAgree
			// 
			this.IAgree.AutoSize = true;
			this.IAgree.Location = new System.Drawing.Point(67, 288);
			this.IAgree.Name = "IAgree";
			this.IAgree.Size = new System.Drawing.Size(88, 22);
			this.IAgree.TabIndex = 4;
			this.IAgree.Text = "我同意";
			this.IAgree.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(121, 190);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 64);
			this.button1.TabIndex = 1;
			this.button1.Text = "注册";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.register_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(237, 190);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 64);
			this.button2.TabIndex = 2;
			this.button2.Text = "登录";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.login_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(353, 190);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 64);
			this.button3.TabIndex = 3;
			this.button3.Text = "退出";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.exit_Click);
			// 
			// UserPwd
			// 
			this.UserPwd.Location = new System.Drawing.Point(151, 136);
			this.UserPwd.Name = "UserPwd";
			this.UserPwd.PasswordChar = '*';
			this.UserPwd.Size = new System.Drawing.Size(324, 28);
			this.UserPwd.TabIndex = 3;
			// 
			// UserName
			// 
			this.UserName.Location = new System.Drawing.Point(151, 63);
			this.UserName.Name = "UserName";
			this.UserName.Size = new System.Drawing.Size(324, 28);
			this.UserName.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(64, 139);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 18);
			this.label2.TabIndex = 1;
			this.label2.Text = "密码";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(64, 73);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "用户名";
			// 
			// LoginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(638, 444);
			this.Controls.Add(this.groupBox1);
			this.Name = "LoginForm";
			this.Text = "欢迎使用驾考宝典";
			this.Load += new System.EventHandler(this.LoginForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox UserPwd;
		private System.Windows.Forms.TextBox UserName;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox RememberMe;
		private System.Windows.Forms.CheckBox IAgree;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
	}
}