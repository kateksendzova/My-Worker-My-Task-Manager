using System;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyTaskManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            comboBox_Priority.Items.Add("RealTime");
            comboBox_Priority.Items.Add("High");
            comboBox_Priority.Items.Add("AboveNormal");
            comboBox_Priority.Items.Add("Normal");
            comboBox_Priority.Items.Add("BelowNormal");
            comboBox_Priority.Items.Add("Idle");
            comboBox_Priority.SelectedIndex = 3;

            comboBox_Affinity.Items.Add("Усі ядра");
            comboBox_Affinity.Items.Add("Тільки Ядро 1");
            comboBox_Affinity.Items.Add("Тільки Ядро 2");
            comboBox_Affinity.Items.Add("Ядра 1 і 2");
            comboBox_Affinity.SelectedIndex = 0;

            dataGridView_MainTable.CellClick += dataGridView_MainTable_CellContentClick;

            LoadProcesses();
        }


        private void LoadProcesses()
        {

            dataGridView_MainTable.Rows.Clear();
            try
            {
                Process[] processes = Process.GetProcesses();
                foreach (Process p in processes)
                {
                    try
                    {
                        double memoryMb = p.WorkingSet64 / (1024.0 * 1024.0);
                        dataGridView_MainTable.Rows.Add(
                            p.ProcessName,
                            p.Id,
                            memoryMb.ToString("F2") + " МБ",
                            p.Threads.Count
                        );
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження процесів: " + ex.Message);
            }
        }

        private void LoadThreadsForProcess(int pid)
        {

            dataGridView_ThreadsTable.Rows.Clear();
            try
            {
                Process p = Process.GetProcessById(pid);

                foreach (ProcessThread t in p.Threads)
                {
                    string startTime = "Н/Д";
                    try { startTime = t.StartTime.ToString("HH:mm:ss"); } catch { }

                    dataGridView_ThreadsTable.Rows.Add(
                        t.Id,
                        t.ThreadState.ToString(),
                        t.PriorityLevel.ToString(),
                        startTime
                    );
                }
            }
            catch (ArgumentException)
            {
                dataGridView_ThreadsTable.Rows.Add("Н/Д", "Процес завершено", "Н/Д", "Н/Д");
            }
            catch (Exception ex)
            {
                dataGridView_ThreadsTable.Rows.Add("Помилка", "Немає прав доступу (Access Denied)", "Н/Д", "Н/Д");

                MessageBox.Show("Не вдалося отримати потоки процесу: " + ex.Message, "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void button_StartProgram_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("\"D:\\useful files\\university\\2 курс\\1_term\\OS\\Лабораторні\\Лаб 2\\MyWorker\\x64\\Debug\\MyWorker.exe\"");
                LoadProcesses();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не вдалося запустити програму: " + ex.Message);
            }
        }

        private void button_EndProcess_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_Process.Text, out int pid))
            {
                try
                {
                    Process p = Process.GetProcessById(pid);
                    p.Kill();
                    p.WaitForExit();
                    LoadProcesses();
                    MessageBox.Show($"Процес з PID {pid} успішно завершено.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка при завершенні процесу (можливо, потрібні права адміністратора): " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, оберіть процес у таблиці або введіть коректний PID!");
            }
        }



        private void dataGridView_MainTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var cellValue = dataGridView_MainTable.Rows[e.RowIndex].Cells[1].Value;
                if (cellValue != null && int.TryParse(cellValue.ToString(), out int pid))
                {
                    textBox_Process.Text = pid.ToString();
                    LoadThreadsForProcess(pid);
                }
            }
        }

        private void button_ChangePriority_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_Process.Text, out int pid))
            {
                if (comboBox_Priority.SelectedItem == null)
                {
                    MessageBox.Show("Будь ласка, оберіть пріоритет зі списку!");
                    return;
                }

                try
                {
                    Process p = Process.GetProcessById(pid);
                    string selectedPriority = comboBox_Priority.SelectedItem.ToString();

                    switch (selectedPriority)
                    {
                        case "RealTime": p.PriorityClass = ProcessPriorityClass.RealTime; break;
                        case "High": p.PriorityClass = ProcessPriorityClass.High; break;
                        case "AboveNormal": p.PriorityClass = ProcessPriorityClass.AboveNormal; break;
                        case "Normal": p.PriorityClass = ProcessPriorityClass.Normal; break;
                        case "BelowNormal": p.PriorityClass = ProcessPriorityClass.BelowNormal; break;
                        case "Idle": p.PriorityClass = ProcessPriorityClass.Idle; break;
                    }

                    MessageBox.Show($"Пріоритет процесу з PID {pid} успішно змінено на {selectedPriority}.");
                    LoadThreadsForProcess(pid);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка при зміні пріоритету (можливо, потрібні права адміністратора): " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, оберіть процес у таблиці або введіть коректний PID!");
            }
        }

        private void button_Affinity_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_Process.Text, out int pid))
            {
                if (comboBox_Affinity.SelectedItem == null)
                {
                    MessageBox.Show("Будь ласка, оберіть варіант відповідності зі списку!");
                    return;
                }

                try
                {
                    Process p = Process.GetProcessById(pid);
                    string selectedAffinity = comboBox_Affinity.SelectedItem.ToString();

                    int processorCount = Environment.ProcessorCount;

                    if (selectedAffinity.Contains("Усі ядра"))
                    {
                        long allProcessorsMask = (1L << processorCount) - 1L;
                        p.ProcessorAffinity = (IntPtr)allProcessorsMask;
                    }
                    else if (selectedAffinity.Contains("Ядро 1"))
                    {
                        p.ProcessorAffinity = (IntPtr)1; 
                    }
                    else if (selectedAffinity.Contains("Ядро 2"))
                    {
                        p.ProcessorAffinity = (IntPtr)2;
                    }
                    else if (selectedAffinity.Contains("Ядра 1 і 2"))
                    {
                        p.ProcessorAffinity = (IntPtr)3;
                    }

                    MessageBox.Show($"Процесорну відповідність для процесу з PID {pid} успішно змінено на: {selectedAffinity}.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка при зміні процесорної відповідності (можливо, потрібні права адміністратора або процес захищений): " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, оберіть процес у таблиці або введіть коректний PID!");
            }
        }
    }
}