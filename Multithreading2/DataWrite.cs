using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading2
{
    public class DataWrite
    {
        public delegate void UpdateUI(int step, int taskid);//声明一个更新主线程的委托
        public UpdateUI UpdateUIDelegate;

        public delegate void AccomplishTask();//声明一个在完成任务时通知主线程的委托
        public AccomplishTask TaskCallBack;

        public void Write(object lineCount)
        {
            int[] temp = (int[])lineCount;
            StreamWriter writeIO = new StreamWriter("text" + temp [1]+ ".txt", false, Encoding.GetEncoding("gb2312"));
            string head = "编号,省,市";
            writeIO.Write(head);

            for (int i = 0; i < temp[0]; i++)
            {
                writeIO.WriteLine(i.ToString() + ",湖南,衡阳");
                //写入一条数据，调用更新主线程ui状态的委托
                UpdateUIDelegate(1, temp[1]);
            }
            //任务完成时通知主线程作出相应的处理
            TaskCallBack();
            writeIO.Close();
        }
    }
}
