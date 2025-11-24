namespace school_analytics
{
    partial class data_class
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
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.subject_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.class_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.class_year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.class_curriculum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teacher_short_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboBoxCurriculum = new System.Windows.Forms.ComboBox();
            this.comboBoxTeacher = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(252)))), ((int)(((byte)(222)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(252)))), ((int)(((byte)(208)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(537, 133);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 26);
            this.button1.TabIndex = 61;
            this.button1.Text = "Зберегти";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(252)))), ((int)(((byte)(222)))));
            this.buttonBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(248)))), ((int)(((byte)(206)))));
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonBack.Location = new System.Drawing.Point(537, 197);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(150, 26);
            this.buttonBack.TabIndex = 54;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = false;
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(252)))), ((int)(((byte)(222)))));
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDelete.Location = new System.Drawing.Point(537, 165);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(150, 26);
            this.buttonDelete.TabIndex = 53;
            this.buttonDelete.Text = "Видалити";
            this.buttonDelete.UseVisualStyleBackColor = false;
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(252)))), ((int)(((byte)(222)))));
            this.buttonAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(252)))), ((int)(((byte)(208)))));
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonAdd.Location = new System.Drawing.Point(537, 100);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(150, 26);
            this.buttonAdd.TabIndex = 52;
            this.buttonAdd.Text = "Додати";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(251)))), ((int)(((byte)(233)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.subject_id,
            this.class_name,
            this.class_year,
            this.class_curriculum,
            this.teacher_short_name});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(420, 224);
            this.dataGridView1.TabIndex = 51;
            // 
            // subject_id
            // 
            this.subject_id.DataPropertyName = "class_id";
            this.subject_id.HeaderText = "class_id";
            this.subject_id.Name = "subject_id";
            this.subject_id.ReadOnly = true;
            this.subject_id.Visible = false;
            this.subject_id.Width = 50;
            // 
            // class_name
            // 
            this.class_name.DataPropertyName = "class_name";
            this.class_name.HeaderText = "Клас";
            this.class_name.Name = "class_name";
            this.class_name.ReadOnly = true;
            this.class_name.Width = 50;
            // 
            // class_year
            // 
            this.class_year.DataPropertyName = "class_year";
            this.class_year.HeaderText = "Рік";
            this.class_year.Name = "class_year";
            this.class_year.ReadOnly = true;
            this.class_year.Width = 60;
            // 
            // class_curriculum
            // 
            this.class_curriculum.DataPropertyName = "class_curriculum";
            this.class_curriculum.HeaderText = "Навчальна програма";
            this.class_curriculum.Name = "class_curriculum";
            this.class_curriculum.ReadOnly = true;
            this.class_curriculum.Width = 150;
            // 
            // teacher_short_name
            // 
            this.teacher_short_name.DataPropertyName = "teacher_short_name";
            this.teacher_short_name.HeaderText = "Класний керівник";
            this.teacher_short_name.Name = "teacher_short_name";
            this.teacher_short_name.ReadOnly = true;
            // 
            // comboBoxCurriculum
            // 
            this.comboBoxCurriculum.FormattingEnabled = true;
            this.comboBoxCurriculum.Location = new System.Drawing.Point(559, 69);
            this.comboBoxCurriculum.Name = "comboBoxCurriculum";
            this.comboBoxCurriculum.Size = new System.Drawing.Size(198, 21);
            this.comboBoxCurriculum.TabIndex = 105;
            // 
            // comboBoxTeacher
            // 
            this.comboBoxTeacher.FormattingEnabled = true;
            this.comboBoxTeacher.Location = new System.Drawing.Point(559, 42);
            this.comboBoxTeacher.Name = "comboBoxTeacher";
            this.comboBoxTeacher.Size = new System.Drawing.Size(198, 21);
            this.comboBoxTeacher.TabIndex = 104;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(439, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 103;
            this.label4.Text = "Класній керівник";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(439, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 102;
            this.label3.Text = "Навчальна програма";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(598, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 101;
            this.label2.Text = "Рік";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(439, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 100;
            this.label1.Text = "Клас";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(626, 16);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(76, 20);
            this.textBox2.TabIndex = 99;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(477, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(76, 20);
            this.textBox1.TabIndex = 98;
            // 
            // data_class
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(251)))), ((int)(((byte)(233)))));
            this.ClientSize = new System.Drawing.Size(772, 246);
            this.Controls.Add(this.comboBoxCurriculum);
            this.Controls.Add(this.comboBoxTeacher);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.dataGridView1);
            this.Name = "data_class";
            this.Text = "Клас";
            this.Load += new System.EventHandler(this.data_class_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox comboBoxCurriculum;
        private System.Windows.Forms.ComboBox comboBoxTeacher;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn subject_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn class_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn class_year;
        private System.Windows.Forms.DataGridViewTextBoxColumn class_curriculum;
        private System.Windows.Forms.DataGridViewTextBoxColumn teacher_short_name;
    }
}