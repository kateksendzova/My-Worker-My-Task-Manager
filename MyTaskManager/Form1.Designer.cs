namespace MyTaskManager
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
            dataGridView_MainTable = new DataGridView();
            ColumnName = new DataGridViewTextBoxColumn();
            ColumnPID = new DataGridViewTextBoxColumn();
            ColumnRam = new DataGridViewTextBoxColumn();
            ColumnThreads = new DataGridViewTextBoxColumn();
            label_infoProcess = new Label();
            textBox_Process = new TextBox();
            dataGridView_ThreadsTable = new DataGridView();
            ColumnTID = new DataGridViewTextBoxColumn();
            ColumnState = new DataGridViewTextBoxColumn();
            ColumnPriority = new DataGridViewTextBoxColumn();
            ColumnStartTime = new DataGridViewTextBoxColumn();
            button_EndProcess = new Button();
            button_StartProgram = new Button();
            comboBox_Priority = new ComboBox();
            button_ChangePriority = new Button();
            comboBox_Affinity = new ComboBox();
            button_Affinity = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView_MainTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView_ThreadsTable).BeginInit();
            SuspendLayout();
            // 
            // dataGridView_MainTable
            // 
            dataGridView_MainTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_MainTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_MainTable.Columns.AddRange(new DataGridViewColumn[] { ColumnName, ColumnPID, ColumnRam, ColumnThreads });
            dataGridView_MainTable.Location = new Point(0, 1);
            dataGridView_MainTable.Name = "dataGridView_MainTable";
            dataGridView_MainTable.RowHeadersWidth = 51;
            dataGridView_MainTable.Size = new Size(643, 582);
            dataGridView_MainTable.TabIndex = 0;
            dataGridView_MainTable.CellContentClick += dataGridView_MainTable_CellContentClick;
            // 
            // ColumnName
            // 
            ColumnName.HeaderText = "Ім'я процесу";
            ColumnName.MinimumWidth = 6;
            ColumnName.Name = "ColumnName";
            // 
            // ColumnPID
            // 
            ColumnPID.HeaderText = "PID";
            ColumnPID.MinimumWidth = 6;
            ColumnPID.Name = "ColumnPID";
            // 
            // ColumnRam
            // 
            ColumnRam.HeaderText = "Обсяг пам'яті";
            ColumnRam.MinimumWidth = 6;
            ColumnRam.Name = "ColumnRam";
            // 
            // ColumnThreads
            // 
            ColumnThreads.HeaderText = "Потоки";
            ColumnThreads.MinimumWidth = 6;
            ColumnThreads.Name = "ColumnThreads";
            // 
            // label_infoProcess
            // 
            label_infoProcess.AutoSize = true;
            label_infoProcess.Location = new Point(821, 16);
            label_infoProcess.Name = "label_infoProcess";
            label_infoProcess.Size = new Size(94, 20);
            label_infoProcess.TabIndex = 1;
            label_infoProcess.Text = "PID процесу";
            // 
            // textBox_Process
            // 
            textBox_Process.Location = new Point(918, 12);
            textBox_Process.Name = "textBox_Process";
            textBox_Process.Size = new Size(125, 27);
            textBox_Process.TabIndex = 2;
            // 
            // dataGridView_ThreadsTable
            // 
            dataGridView_ThreadsTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_ThreadsTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_ThreadsTable.Columns.AddRange(new DataGridViewColumn[] { ColumnTID, ColumnState, ColumnPriority, ColumnStartTime });
            dataGridView_ThreadsTable.Location = new Point(671, 56);
            dataGridView_ThreadsTable.Name = "dataGridView_ThreadsTable";
            dataGridView_ThreadsTable.RowHeadersWidth = 51;
            dataGridView_ThreadsTable.Size = new Size(577, 276);
            dataGridView_ThreadsTable.TabIndex = 3;
            // 
            // ColumnTID
            // 
            ColumnTID.HeaderText = "TID";
            ColumnTID.MinimumWidth = 6;
            ColumnTID.Name = "ColumnTID";
            // 
            // ColumnState
            // 
            ColumnState.HeaderText = "Стан";
            ColumnState.MinimumWidth = 6;
            ColumnState.Name = "ColumnState";
            // 
            // ColumnPriority
            // 
            ColumnPriority.HeaderText = "Пріорітет потоку";
            ColumnPriority.MinimumWidth = 6;
            ColumnPriority.Name = "ColumnPriority";
            // 
            // ColumnStartTime
            // 
            ColumnStartTime.HeaderText = "Час запуску";
            ColumnStartTime.MinimumWidth = 6;
            ColumnStartTime.Name = "ColumnStartTime";
            // 
            // button_EndProcess
            // 
            button_EndProcess.BackColor = Color.Thistle;
            button_EndProcess.Location = new Point(804, 389);
            button_EndProcess.Name = "button_EndProcess";
            button_EndProcess.Size = new Size(94, 52);
            button_EndProcess.TabIndex = 4;
            button_EndProcess.Text = "Завершити процес";
            button_EndProcess.UseVisualStyleBackColor = false;
            button_EndProcess.Click += button_EndProcess_Click;
            // 
            // button_StartProgram
            // 
            button_StartProgram.BackColor = Color.LightPink;
            button_StartProgram.Location = new Point(671, 389);
            button_StartProgram.Name = "button_StartProgram";
            button_StartProgram.Size = new Size(109, 52);
            button_StartProgram.TabIndex = 7;
            button_StartProgram.Text = "Запустити програму";
            button_StartProgram.UseVisualStyleBackColor = false;
            button_StartProgram.Click += button_StartProgram_Click;
            // 
            // comboBox_Priority
            // 
            comboBox_Priority.BackColor = Color.PaleTurquoise;
            comboBox_Priority.FormattingEnabled = true;
            comboBox_Priority.Location = new Point(932, 372);
            comboBox_Priority.Name = "comboBox_Priority";
            comboBox_Priority.Size = new Size(150, 28);
            comboBox_Priority.TabIndex = 8;
            // 
            // button_ChangePriority
            // 
            button_ChangePriority.BackColor = Color.PaleTurquoise;
            button_ChangePriority.Location = new Point(957, 406);
            button_ChangePriority.Name = "button_ChangePriority";
            button_ChangePriority.Size = new Size(102, 48);
            button_ChangePriority.TabIndex = 9;
            button_ChangePriority.Text = "Змінити пріорітет";
            button_ChangePriority.UseVisualStyleBackColor = false;
            button_ChangePriority.Click += button_ChangePriority_Click;
            // 
            // comboBox_Affinity
            // 
            comboBox_Affinity.BackColor = Color.LemonChiffon;
            comboBox_Affinity.FormattingEnabled = true;
            comboBox_Affinity.Location = new Point(1098, 372);
            comboBox_Affinity.Name = "comboBox_Affinity";
            comboBox_Affinity.Size = new Size(150, 28);
            comboBox_Affinity.TabIndex = 10;
            // 
            // button_Affinity
            // 
            button_Affinity.BackColor = Color.LemonChiffon;
            button_Affinity.Location = new Point(1123, 406);
            button_Affinity.Name = "button_Affinity";
            button_Affinity.Size = new Size(102, 48);
            button_Affinity.TabIndex = 11;
            button_Affinity.Text = "Вибір ядер";
            button_Affinity.UseVisualStyleBackColor = false;
            button_Affinity.Click += button_Affinity_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1260, 582);
            Controls.Add(button_Affinity);
            Controls.Add(comboBox_Affinity);
            Controls.Add(button_ChangePriority);
            Controls.Add(comboBox_Priority);
            Controls.Add(button_StartProgram);
            Controls.Add(button_EndProcess);
            Controls.Add(dataGridView_ThreadsTable);
            Controls.Add(textBox_Process);
            Controls.Add(label_infoProcess);
            Controls.Add(dataGridView_MainTable);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView_MainTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView_ThreadsTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView_MainTable;
        private DataGridViewTextBoxColumn ColumnName;
        private DataGridViewTextBoxColumn ColumnPID;
        private DataGridViewTextBoxColumn ColumnRam;
        private DataGridViewTextBoxColumn ColumnThreads;
        private Label label_infoProcess;
        private TextBox textBox_Process;
        private DataGridView dataGridView_ThreadsTable;
        private DataGridViewTextBoxColumn ColumnTID;
        private DataGridViewTextBoxColumn ColumnState;
        private DataGridViewTextBoxColumn ColumnPriority;
        private DataGridViewTextBoxColumn ColumnStartTime;
        private Button button_EndProcess;
        private Button button_StartProgram;
        private ComboBox comboBox_Priority;
        private Button button_ChangePriority;
        private ComboBox comboBox_Affinity;
        private Button button_Affinity;
    }
}
