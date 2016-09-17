namespace PruebaJson
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.jsonButton = new System.Windows.Forms.Button();
            this.grabarButton = new System.Windows.Forms.Button();
            this.salirButton = new System.Windows.Forms.Button();
            this.jsonTextBox = new System.Windows.Forms.TextBox();
            this.DesdeLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.desdeDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.hastaDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.hastaDateTimePicker);
            this.panel1.Controls.Add(this.desdeDateTimePicker);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.DesdeLabel);
            this.panel1.Controls.Add(this.jsonButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(601, 66);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.jsonTextBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 66);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(601, 263);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.salirButton);
            this.panel3.Controls.Add(this.grabarButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 329);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(601, 31);
            this.panel3.TabIndex = 2;
            // 
            // jsonButton
            // 
            this.jsonButton.Location = new System.Drawing.Point(235, 26);
            this.jsonButton.Name = "jsonButton";
            this.jsonButton.Size = new System.Drawing.Size(75, 21);
            this.jsonButton.TabIndex = 0;
            this.jsonButton.Text = "JSON";
            this.jsonButton.UseVisualStyleBackColor = true;
            this.jsonButton.Click += new System.EventHandler(this.jsonButton_Click);
            // 
            // grabarButton
            // 
            this.grabarButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.grabarButton.Location = new System.Drawing.Point(0, 0);
            this.grabarButton.Name = "grabarButton";
            this.grabarButton.Size = new System.Drawing.Size(75, 29);
            this.grabarButton.TabIndex = 0;
            this.grabarButton.Text = "GRABAR";
            this.grabarButton.UseVisualStyleBackColor = true;
            this.grabarButton.Click += new System.EventHandler(this.grabarButton_Click);
            // 
            // salirButton
            // 
            this.salirButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.salirButton.Location = new System.Drawing.Point(524, 0);
            this.salirButton.Name = "salirButton";
            this.salirButton.Size = new System.Drawing.Size(75, 29);
            this.salirButton.TabIndex = 1;
            this.salirButton.Text = "SALIR";
            this.salirButton.UseVisualStyleBackColor = true;
            this.salirButton.Click += new System.EventHandler(this.salirButton_Click);
            // 
            // jsonTextBox
            // 
            this.jsonTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jsonTextBox.Location = new System.Drawing.Point(0, 0);
            this.jsonTextBox.Multiline = true;
            this.jsonTextBox.Name = "jsonTextBox";
            this.jsonTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.jsonTextBox.Size = new System.Drawing.Size(599, 261);
            this.jsonTextBox.TabIndex = 0;
            // 
            // DesdeLabel
            // 
            this.DesdeLabel.AutoSize = true;
            this.DesdeLabel.Location = new System.Drawing.Point(10, 11);
            this.DesdeLabel.Name = "DesdeLabel";
            this.DesdeLabel.Size = new System.Drawing.Size(38, 13);
            this.DesdeLabel.TabIndex = 1;
            this.DesdeLabel.Text = "Desde";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hasta";
            // 
            // desdeDateTimePicker
            // 
            this.desdeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.desdeDateTimePicker.Location = new System.Drawing.Point(13, 28);
            this.desdeDateTimePicker.Name = "desdeDateTimePicker";
            this.desdeDateTimePicker.Size = new System.Drawing.Size(99, 20);
            this.desdeDateTimePicker.TabIndex = 3;
            // 
            // hastaDateTimePicker
            // 
            this.hastaDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.hastaDateTimePicker.Location = new System.Drawing.Point(118, 28);
            this.hastaDateTimePicker.Name = "hastaDateTimePicker";
            this.hastaDateTimePicker.Size = new System.Drawing.Size(99, 20);
            this.hastaDateTimePicker.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 360);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GENERAR JSON";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker hastaDateTimePicker;
        private System.Windows.Forms.DateTimePicker desdeDateTimePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label DesdeLabel;
        private System.Windows.Forms.Button jsonButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox jsonTextBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button salirButton;
        private System.Windows.Forms.Button grabarButton;
    }
}

