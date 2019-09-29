namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.PortAuswahlBox = new System.Windows.Forms.ComboBox();
            this.Titel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TemperaturSensorOutput2 = new System.Windows.Forms.Label();
            this.DrucksensorOutput2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DrucksensorOutput1 = new System.Windows.Forms.Label();
            this.TemperaturSensorOutput1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(41, 345);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(332, 59);
            this.button1.TabIndex = 0;
            this.button1.Text = "Applikation starten";
            this.button1.UseCompatibleTextRendering = true;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PortAuswahlBox
            // 
            this.PortAuswahlBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortAuswahlBox.FormattingEnabled = true;
            this.PortAuswahlBox.Location = new System.Drawing.Point(41, 195);
            this.PortAuswahlBox.Name = "PortAuswahlBox";
            this.PortAuswahlBox.Size = new System.Drawing.Size(332, 46);
            this.PortAuswahlBox.TabIndex = 1;
            this.PortAuswahlBox.Text = "Bitte Port auswählen";
            this.PortAuswahlBox.SelectedIndexChanged += new System.EventHandler(this.PortAuswahlBox_SelectedIndexChanged);
            this.PortAuswahlBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PortAuswahlBox_MouseClick);
            // 
            // Titel
            // 
            this.Titel.AutoSize = true;
            this.Titel.Font = new System.Drawing.Font("Century Schoolbook", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Titel.Location = new System.Drawing.Point(-10, 21);
            this.Titel.Name = "Titel";
            this.Titel.Size = new System.Drawing.Size(411, 55);
            this.Titel.TabIndex = 2;
            this.Titel.Text = "Arduino Manager";
            this.Titel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Titel.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.Titel);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.PortAuswahlBox);
            this.panel1.Location = new System.Drawing.Point(138, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(413, 437);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(118)))), ((int)(((byte)(177)))));
            this.panel2.Controls.Add(this.TemperaturSensorOutput1);
            this.panel2.Controls.Add(this.DrucksensorOutput1);
            this.panel2.Controls.Add(this.TemperaturSensorOutput2);
            this.panel2.Controls.Add(this.DrucksensorOutput2);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1930, 940);
            this.panel2.TabIndex = 4;
            this.panel2.Visible = false;
            // 
            // TemperaturSensorOutput2
            // 
            this.TemperaturSensorOutput2.AutoSize = true;
            this.TemperaturSensorOutput2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TemperaturSensorOutput2.Location = new System.Drawing.Point(1392, 428);
            this.TemperaturSensorOutput2.Name = "TemperaturSensorOutput2";
            this.TemperaturSensorOutput2.Size = new System.Drawing.Size(421, 39);
            this.TemperaturSensorOutput2.TabIndex = 6;
            this.TemperaturSensorOutput2.Text = "// Temperatursensor Daten";
            // 
            // DrucksensorOutput2
            // 
            this.DrucksensorOutput2.AutoSize = true;
            this.DrucksensorOutput2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrucksensorOutput2.Location = new System.Drawing.Point(1392, 340);
            this.DrucksensorOutput2.Name = "DrucksensorOutput2";
            this.DrucksensorOutput2.Size = new System.Drawing.Size(335, 39);
            this.DrucksensorOutput2.TabIndex = 5;
            this.DrucksensorOutput2.Text = "// Drucksensor Daten";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(23, 22);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(79, 62);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(207, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1535, 69);
            this.label1.TabIndex = 0;
            this.label1.Text = "Live-Messdaten Live aus der Adsorptionskältemaschine";
            // 
            // DrucksensorOutput1
            // 
            this.DrucksensorOutput1.AutoSize = true;
            this.DrucksensorOutput1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrucksensorOutput1.Location = new System.Drawing.Point(349, 320);
            this.DrucksensorOutput1.Name = "DrucksensorOutput1";
            this.DrucksensorOutput1.Size = new System.Drawing.Size(335, 39);
            this.DrucksensorOutput1.TabIndex = 7;
            this.DrucksensorOutput1.Text = "// Drucksensor Daten";
            // 
            // TemperaturSensorOutput1
            // 
            this.TemperaturSensorOutput1.AutoSize = true;
            this.TemperaturSensorOutput1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TemperaturSensorOutput1.Location = new System.Drawing.Point(283, 441);
            this.TemperaturSensorOutput1.Name = "TemperaturSensorOutput1";
            this.TemperaturSensorOutput1.Size = new System.Drawing.Size(421, 39);
            this.TemperaturSensorOutput1.TabIndex = 8;
            this.TemperaturSensorOutput1.Text = "// Temperatursensor Daten";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1930, 940);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "ArduinoCommunication";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox PortAuswahlBox;
        private System.Windows.Forms.Label Titel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label DrucksensorOutput2;
        private System.Windows.Forms.Label TemperaturSensorOutput2;
        private System.Windows.Forms.Label TemperaturSensorOutput1;
        private System.Windows.Forms.Label DrucksensorOutput1;
    }
}

