namespace school_analytics
{
    partial class data_teacher
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label subject_nameLabel;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.subject_nameTextBox = new System.Windows.Forms.TextBox();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.teacher_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teacher_last_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teacher_first_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teacher_middle_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teacher_experience = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teacher_category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teacher_rank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            subject_nameLabel = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(782, 79);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(70, 13);
            label2.TabIndex = 59;
            label2.Text = "По-батькові:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(782, 50);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(26, 13);
            label1.TabIndex = 57;
            label1.Text = "Ім\'я";
            // 
            // subject_nameLabel
            // 
            subject_nameLabel.AutoSize = true;
            subject_nameLabel.Location = new System.Drawing.Point(782, 24);
            subject_nameLabel.Name = "subject_nameLabel";
            subject_nameLabel.Size = new System.Drawing.Size(51, 13);
            subject_nameLabel.TabIndex = 55;
            subject_nameLabel.Text = "Фамілія:";
            subject_nameLabel.Click += new System.EventHandler(this.subject_nameLabel_Click);
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(782, 111);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(33, 13);
            label3.TabIndex = 63;
            label3.Text = "Стаж";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(782, 138);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(56, 13);
            label4.TabIndex = 65;
            label4.Text = "Категорія";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(782, 164);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(44, 13);
            label5.TabIndex = 67;
            label5.Text = "Звання";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(252)))), ((int)(((byte)(222)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(252)))), ((int)(((byte)(208)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(841, 234);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 26);
            this.button1.TabIndex = 61;
            this.button1.Text = "Зберегти";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(882, 161);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(166, 21);
            this.comboBox1.TabIndex = 60;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(882, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(166, 20);
            this.textBox1.TabIndex = 58;
            // 
            // subject_nameTextBox
            // 
            this.subject_nameTextBox.Location = new System.Drawing.Point(882, 21);
            this.subject_nameTextBox.Name = "subject_nameTextBox";
            this.subject_nameTextBox.Size = new System.Drawing.Size(166, 20);
            this.subject_nameTextBox.TabIndex = 56;
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(252)))), ((int)(((byte)(222)))));
            this.buttonBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(248)))), ((int)(((byte)(206)))));
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonBack.Location = new System.Drawing.Point(841, 298);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(150, 26);
            this.buttonBack.TabIndex = 54;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(252)))), ((int)(((byte)(222)))));
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDelete.Location = new System.Drawing.Point(841, 266);
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
            this.buttonAdd.Location = new System.Drawing.Point(841, 201);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(150, 26);
            this.buttonAdd.TabIndex = 52;
            this.buttonAdd.Text = "Додати";
            this.buttonAdd.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(251)))), ((int)(((byte)(233)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.teacher_id,
            this.teacher_last_name,
            this.teacher_first_name,
            this.teacher_middle_name,
            this.teacher_experience,
            this.teacher_category,
            this.teacher_rank});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(721, 329);
            this.dataGridView1.TabIndex = 51;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(882, 76);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(166, 20);
            this.textBox2.TabIndex = 62;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(882, 108);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(166, 20);
            this.textBox3.TabIndex = 64;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(882, 134);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(166, 21);
            this.comboBox2.TabIndex = 68;
            // 
            // teacher_id
            // 
            this.teacher_id.DataPropertyName = "subject_id";
            this.teacher_id.HeaderText = "teacher_id";
            this.teacher_id.Name = "teacher_id";
            this.teacher_id.ReadOnly = true;
            this.teacher_id.Visible = false;
            this.teacher_id.Width = 50;
            // 
            // teacher_last_name
            // 
            this.teacher_last_name.DataPropertyName = "teacher_last_name";
            this.teacher_last_name.HeaderText = "Фамілія";
            this.teacher_last_name.Name = "teacher_last_name";
            this.teacher_last_name.ReadOnly = true;
            this.teacher_last_name.Width = 125;
            // 
            // teacher_first_name
            // 
            this.teacher_first_name.DataPropertyName = "teacher_first_name";
            this.teacher_first_name.HeaderText = "Ім\'я";
            this.teacher_first_name.Name = "teacher_first_name";
            this.teacher_first_name.ReadOnly = true;
            // 
            // teacher_middle_name
            // 
            this.teacher_middle_name.DataPropertyName = "teacher_middle_name";
            this.teacher_middle_name.HeaderText = "По-батькові";
            this.teacher_middle_name.Name = "teacher_middle_name";
            this.teacher_middle_name.ReadOnly = true;
            this.teacher_middle_name.Width = 125;
            // 
            // teacher_experience
            // 
            this.teacher_experience.DataPropertyName = "teacher_experience";
            this.teacher_experience.HeaderText = "Стаж";
            this.teacher_experience.Name = "teacher_experience";
            this.teacher_experience.ReadOnly = true;
            this.teacher_experience.Width = 40;
            // 
            // teacher_category
            // 
            this.teacher_category.DataPropertyName = "teacher_category";
            this.teacher_category.HeaderText = "Категорія";
            this.teacher_category.Name = "teacher_category";
            this.teacher_category.ReadOnly = true;
            this.teacher_category.Width = 170;
            // 
            // teacher_rank
            // 
            this.teacher_rank.DataPropertyName = "teacher_rank";
            this.teacher_rank.HeaderText = "Звання";
            this.teacher_rank.Name = "teacher_rank";
            this.teacher_rank.ReadOnly = true;
            // 
            // data_teacher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(251)))), ((int)(((byte)(233)))));
            this.ClientSize = new System.Drawing.Size(1080, 350);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(label5);
            this.Controls.Add(label4);
            this.Controls.Add(label3);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(subject_nameLabel);
            this.Controls.Add(this.subject_nameTextBox);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.dataGridView1);
            this.Name = "data_teacher";
            this.Text = "Вчителя";
            this.Load += new System.EventHandler(this.data_teacher_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox subject_nameTextBox;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn teacher_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn teacher_last_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn teacher_first_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn teacher_middle_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn teacher_experience;
        private System.Windows.Forms.DataGridViewTextBoxColumn teacher_category;
        private System.Windows.Forms.DataGridViewTextBoxColumn teacher_rank;
    }
}