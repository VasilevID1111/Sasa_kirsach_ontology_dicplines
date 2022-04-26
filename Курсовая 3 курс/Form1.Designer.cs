
namespace Курсовая_3_курс
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
            this.cbТипЗапроса = new System.Windows.Forms.ComboBox();
            this.buttonДисциплины = new System.Windows.Forms.Button();
            this.buttonЗнания = new System.Windows.Forms.Button();
            this.buttonВыполнить = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxФраза = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbТипЗапроса
            // 
            this.cbТипЗапроса.FormattingEnabled = true;
            this.cbТипЗапроса.Location = new System.Drawing.Point(168, 12);
            this.cbТипЗапроса.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbТипЗапроса.Name = "cbТипЗапроса";
            this.cbТипЗапроса.Size = new System.Drawing.Size(428, 28);
            this.cbТипЗапроса.TabIndex = 0;
            this.cbТипЗапроса.SelectedIndexChanged += new System.EventHandler(this.cbТипЗапроса_SelectedIndexChanged);
            // 
            // buttonДисциплины
            // 
            this.buttonДисциплины.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.buttonДисциплины.Location = new System.Drawing.Point(11, 262);
            this.buttonДисциплины.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonДисциплины.Name = "buttonДисциплины";
            this.buttonДисциплины.Size = new System.Drawing.Size(129, 55);
            this.buttonДисциплины.TabIndex = 1;
            this.buttonДисциплины.Text = "Дисциплины";
            this.buttonДисциплины.UseVisualStyleBackColor = false;
            this.buttonДисциплины.Click += new System.EventHandler(this.buttonДисциплины_Click);
            // 
            // buttonЗнания
            // 
            this.buttonЗнания.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.buttonЗнания.Location = new System.Drawing.Point(11, 336);
            this.buttonЗнания.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonЗнания.Name = "buttonЗнания";
            this.buttonЗнания.Size = new System.Drawing.Size(129, 55);
            this.buttonЗнания.TabIndex = 1;
            this.buttonЗнания.Text = "Знания";
            this.buttonЗнания.UseVisualStyleBackColor = false;
            this.buttonЗнания.Click += new System.EventHandler(this.buttonЗнания_Click);
            // 
            // buttonВыполнить
            // 
            this.buttonВыполнить.BackColor = System.Drawing.Color.MistyRose;
            this.buttonВыполнить.Location = new System.Drawing.Point(168, 665);
            this.buttonВыполнить.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonВыполнить.Name = "buttonВыполнить";
            this.buttonВыполнить.Size = new System.Drawing.Size(909, 55);
            this.buttonВыполнить.TabIndex = 3;
            this.buttonВыполнить.Text = "Выполнить запрос";
            this.buttonВыполнить.UseVisualStyleBackColor = false;
            this.buttonВыполнить.Click += new System.EventHandler(this.buttonВыполнить_Click);
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(168, 62);
            this.listView1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(428, 579);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 600;
            // 
            // listView2
            // 
            this.listView2.CheckBoxes = true;
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(631, 62);
            this.listView2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(446, 579);
            this.listView2.TabIndex = 4;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.List;
            this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 600;
            // 
            // textBoxФраза
            // 
            this.textBoxФраза.Location = new System.Drawing.Point(631, 14);
            this.textBoxФраза.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBoxФраза.Name = "textBoxФраза";
            this.textBoxФраза.Size = new System.Drawing.Size(446, 26);
            this.textBoxФраза.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1121, 749);
            this.Controls.Add(this.textBoxФраза);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.buttonВыполнить);
            this.Controls.Add(this.buttonЗнания);
            this.Controls.Add(this.buttonДисциплины);
            this.Controls.Add(this.cbТипЗапроса);
            this.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Компетенции и дисциплины";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbТипЗапроса;
        private System.Windows.Forms.Button buttonДисциплины;
        private System.Windows.Forms.Button buttonЗнания;
        private System.Windows.Forms.Button buttonВыполнить;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox textBoxФраза;
    }
}

