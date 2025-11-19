namespace school_analytics
{
    partial class data_grade_add
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
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label1;
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.grade_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.student_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subject_full_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teacher_short_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grade_value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            label4 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(352, 37);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(52, 13);
            label4.TabIndex = 107;
            label4.Text = "Предмет";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(352, 91);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(44, 13);
            label2.TabIndex = 103;
            label2.Text = "Оцінка:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(352, 61);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(48, 13);
            label5.TabIndex = 122;
            label5.Text = "Вчитель";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(373, 8);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(182, 13);
            label1.TabIndex = 125;
            label1.Text = "Андрусенко Антон Олександрович";
            label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(406, 34);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(166, 21);
            this.comboBox2.TabIndex = 108;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(406, 91);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(85, 20);
            this.textBox2.TabIndex = 105;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(252)))), ((int)(((byte)(222)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(252)))), ((int)(((byte)(208)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(392, 155);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 26);
            this.button1.TabIndex = 121;
            this.button1.Text = "Зберегти";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(252)))), ((int)(((byte)(222)))));
            this.buttonBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(248)))), ((int)(((byte)(206)))));
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonBack.Location = new System.Drawing.Point(392, 219);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(150, 26);
            this.buttonBack.TabIndex = 120;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = false;
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(252)))), ((int)(((byte)(222)))));
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDelete.Location = new System.Drawing.Point(392, 187);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(150, 26);
            this.buttonDelete.TabIndex = 119;
            this.buttonDelete.Text = "Видалити";
            this.buttonDelete.UseVisualStyleBackColor = false;
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(252)))), ((int)(((byte)(222)))));
            this.buttonAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(252)))), ((int)(((byte)(208)))));
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonAdd.Location = new System.Drawing.Point(392, 122);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(150, 26);
            this.buttonAdd.TabIndex = 118;
            this.buttonAdd.Text = "Додати";
            this.buttonAdd.UseVisualStyleBackColor = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(406, 61);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(166, 21);
            this.comboBox1.TabIndex = 123;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(251)))), ((int)(((byte)(233)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.grade_id,
            this.student_id,
            this.subject_full_name,
            this.teacher_short_name,
            this.grade_value});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(334, 246);
            this.dataGridView1.TabIndex = 124;
            // 
            // grade_id
            // 
            this.grade_id.DataPropertyName = "grade_id";
            this.grade_id.HeaderText = "grade_id";
            this.grade_id.Name = "grade_id";
            this.grade_id.ReadOnly = true;
            this.grade_id.Visible = false;
            this.grade_id.Width = 50;
            // 
            // student_id
            // 
            this.student_id.DataPropertyName = "student_id";
            this.student_id.HeaderText = "student_id";
            this.student_id.Name = "student_id";
            this.student_id.ReadOnly = true;
            this.student_id.Visible = false;
            // 
            // subject_full_name
            // 
            this.subject_full_name.DataPropertyName = "subject_full_name";
            this.subject_full_name.HeaderText = "Назва предмета";
            this.subject_full_name.Name = "subject_full_name";
            this.subject_full_name.ReadOnly = true;
            this.subject_full_name.Width = 125;
            // 
            // teacher_short_name
            // 
            this.teacher_short_name.DataPropertyName = "teacher_short_name";
            this.teacher_short_name.HeaderText = "Вчитель";
            this.teacher_short_name.Name = "teacher_short_name";
            this.teacher_short_name.ReadOnly = true;
            // 
            // grade_value
            // 
            this.grade_value.DataPropertyName = "grade_value";
            this.grade_value.HeaderText = "Оцінка";
            this.grade_value.Name = "grade_value";
            this.grade_value.ReadOnly = true;
            this.grade_value.Width = 50;
            // 
            // data_grade_add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(251)))), ((int)(((byte)(233)))));
            this.ClientSize = new System.Drawing.Size(586, 270);
            this.Controls.Add(label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(label2);
            this.Name = "data_grade_add";
            this.Text = "Оцінки";
            this.Load += new System.EventHandler(this.data_grade_add_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn grade_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn student_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn subject_full_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn teacher_short_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn grade_value;
    }
}