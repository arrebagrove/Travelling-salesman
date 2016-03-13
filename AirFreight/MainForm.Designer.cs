namespace AirFreight
{
	partial class MainForm
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
			this.btnInput = new System.Windows.Forms.Button();
			this.textInputFile = new System.Windows.Forms.TextBox();
			this.lblInput = new System.Windows.Forms.Label();
			this.bntCompute = new System.Windows.Forms.Button();
			this.flwPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.lblMinDistance = new System.Windows.Forms.Label();
			this.txtRoute = new System.Windows.Forms.TextBox();
			this.btnStop = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.pbProgress = new System.Windows.Forms.ProgressBar();
			this.lblTravelMap = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnInput
			// 
			this.btnInput.Location = new System.Drawing.Point(575, 10);
			this.btnInput.Name = "btnInput";
			this.btnInput.Size = new System.Drawing.Size(28, 23);
			this.btnInput.TabIndex = 0;
			this.btnInput.Text = " ...";
			this.btnInput.UseVisualStyleBackColor = true;
			this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
			// 
			// textInputFile
			// 
			this.textInputFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textInputFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textInputFile.Location = new System.Drawing.Point(95, 12);
			this.textInputFile.Name = "textInputFile";
			this.textInputFile.Size = new System.Drawing.Size(470, 20);
			this.textInputFile.TabIndex = 4;
			// 
			// lblInput
			// 
			this.lblInput.AutoSize = true;
			this.lblInput.Location = new System.Drawing.Point(12, 15);
			this.lblInput.Name = "lblInput";
			this.lblInput.Size = new System.Drawing.Size(83, 13);
			this.lblInput.TabIndex = 5;
			this.lblInput.Text = "Select Input file:";
			// 
			// bntCompute
			// 
			this.bntCompute.Location = new System.Drawing.Point(416, 38);
			this.bntCompute.Name = "bntCompute";
			this.bntCompute.Size = new System.Drawing.Size(106, 23);
			this.bntCompute.TabIndex = 6;
			this.bntCompute.Text = "Compute Route";
			this.bntCompute.UseVisualStyleBackColor = true;
			this.bntCompute.Click += new System.EventHandler(this.bntCompute_Click);
			// 
			// flwPanel
			// 
			this.flwPanel.Location = new System.Drawing.Point(5, 90);
			this.flwPanel.Name = "flwPanel";
			this.flwPanel.Size = new System.Drawing.Size(410, 317);
			this.flwPanel.TabIndex = 7;
			// 
			// lblMinDistance
			// 
			this.lblMinDistance.AutoSize = true;
			this.lblMinDistance.Location = new System.Drawing.Point(12, 48);
			this.lblMinDistance.Name = "lblMinDistance";
			this.lblMinDistance.Size = new System.Drawing.Size(0, 13);
			this.lblMinDistance.TabIndex = 8;
			// 
			// txtRoute
			// 
			this.txtRoute.Location = new System.Drawing.Point(422, 79);
			this.txtRoute.Multiline = true;
			this.txtRoute.Name = "txtRoute";
			this.txtRoute.ReadOnly = true;
			this.txtRoute.Size = new System.Drawing.Size(181, 328);
			this.txtRoute.TabIndex = 9;
			// 
			// btnStop
			// 
			this.btnStop.Enabled = false;
			this.btnStop.Location = new System.Drawing.Point(528, 38);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 23);
			this.btnStop.TabIndex = 10;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(8, 418);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(71, 13);
			this.lblStatus.TabIndex = 11;
			this.lblStatus.Text = "Processing ...";
			// 
			// pbProgress
			// 
			this.pbProgress.Location = new System.Drawing.Point(82, 413);
			this.pbProgress.Name = "pbProgress";
			this.pbProgress.Size = new System.Drawing.Size(333, 23);
			this.pbProgress.TabIndex = 12;
			// 
			// lblTravelMap
			// 
			this.lblTravelMap.AutoSize = true;
			this.lblTravelMap.Location = new System.Drawing.Point(12, 71);
			this.lblTravelMap.Name = "lblTravelMap";
			this.lblTravelMap.Size = new System.Drawing.Size(0, 13);
			this.lblTravelMap.TabIndex = 13;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(615, 445);
			this.Controls.Add(this.lblTravelMap);
			this.Controls.Add(this.pbProgress);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.txtRoute);
			this.Controls.Add(this.lblMinDistance);
			this.Controls.Add(this.flwPanel);
			this.Controls.Add(this.bntCompute);
			this.Controls.Add(this.lblInput);
			this.Controls.Add(this.textInputFile);
			this.Controls.Add(this.btnInput);
			this.Name = "MainForm";
			this.Text = "AirFreightCompany";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnInput;
		private System.Windows.Forms.TextBox textInputFile;
		private System.Windows.Forms.Label lblInput;
		private System.Windows.Forms.Button bntCompute;
		private System.Windows.Forms.FlowLayoutPanel flwPanel;
		private System.Windows.Forms.Label lblMinDistance;
		private System.Windows.Forms.TextBox txtRoute;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.ProgressBar pbProgress;
		private System.Windows.Forms.Label lblTravelMap;
	}
}

