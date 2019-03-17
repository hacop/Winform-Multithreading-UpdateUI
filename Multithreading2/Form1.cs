using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multithreading2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        delegate void AsynUpdateUI(int step);
        private void button1_Click(object sender, EventArgs e)
        {
            int[] taskCount = { 5, 0 }; //任务量为10000
            this.pgbWrite.Maximum = taskCount[0];
            this.pgbWrite.Value = 0;

            int[] taskCount1 = { 10000, 1 }; //任务量为10000
            this.progressBar1.Maximum = taskCount1[0];
            this.progressBar1.Value = 0;


            DataWrite dataWrite = new DataWrite();//实例化一个写入数据的类
            dataWrite.UpdateUIDelegate += UpdataUIStatus;//绑定更新任务状态的委托
            dataWrite.TaskCallBack += Accomplish;//绑定完成任务要调用的委托

            Thread thread = new Thread(new ParameterizedThreadStart(dataWrite.Write))
            {
                IsBackground = true
            };
            thread.Start(taskCount);

            Thread thread1 = new Thread(new ParameterizedThreadStart(dataWrite.Write))
            {
                IsBackground = true
            };
            thread1.Start(taskCount1);
        }
        //更新UI
        private void UpdataUIStatus(int step, int taskid)
        {
            if (InvokeRequired)
            {
                this.Invoke(new AsynUpdateUI(delegate (int s)
                {
                    if (taskid == 0)
                    {
                        this.pgbWrite.Value += s;
                        this.lblWriteStatus.Text = this.pgbWrite.Value.ToString() + "/" + this.pgbWrite.Maximum.ToString();
                    }
                    else
                    {
                        this.progressBar1.Value += s;
                        this.label1.Text = this.progressBar1.Value.ToString() + "/" + this.progressBar1.Maximum.ToString();
                    }
                }), step);
            }
            else
            {
                if (taskid == 0)
                {
                    this.pgbWrite.Value += step;
                    this.lblWriteStatus.Text = this.pgbWrite.Value.ToString() + "/" + this.pgbWrite.Maximum.ToString();
                }
                else
                {
                    this.progressBar1.Value += step;
                    this.label1.Text = this.progressBar1.Value.ToString() + "/" + this.progressBar1.Maximum.ToString();
                }
            }
        }

        //完成任务时需要调用
        private void Accomplish()
        {
            //还可以进行其他的一些完任务完成之后的逻辑处理
            MessageBox.Show("任务完成");
        }
    }
}
