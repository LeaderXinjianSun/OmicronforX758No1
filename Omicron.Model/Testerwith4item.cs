using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SxjLibrary;
using System.IO;
using BingLibrary.hjb;
using System.Diagnostics;
using BingLibrary.hjb.Intercepts;

namespace Omicron.Model
{
    public class Testerwith4item
    {
        #region 属性定义
        public string TestPcIP { set; get; } = "192.168.1.100";
        public int TestPcRemotePort { set; get; } = 8000;
        public string[] TesterBracode { set; get; } = new string[4] { "Null", "Null", "Null", "Null" };
        public int TestTimeout { set; get; }
        public int Index { set; get; }
        public int[] PassCount { set; get; } = new int[4];
        public int[] FailCount { set; get; } = new int[4];
        public int[] TestCount { set; get; } = new int[4];
        public double[] Yield { set; get; } = new double[4];

        public int[] PassCount_Nomal { set; get; }= new int[4];
        public int[] FailCount_Nomal { set; get; }= new int[4];
        public int[] TestCount_Nomal { set; get; } = new int[4];
        public double[] Yield_Nomal { set; get; } = new double[4];

        public double[] TestSpan { set; get; } = new double[4] { 0, 0, 0, 0 };
        public TestResult[] testResult { set; get; } = new TestResult[4] { TestResult.Unknow, TestResult.Unknow, TestResult.Unknow, TestResult.Unknow };
        public TestStatus[] testStatus { set; get; } = new TestStatus[4] { TestStatus.PreTest, TestStatus.PreTest, TestStatus.PreTest, TestStatus.PreTest };
        private bool[] TestActionSwitch = new bool[4];
        private short[] StepFlag = new short[4];
        #endregion
        #region 功能函数
        public async void Start1()
        {
            Stopwatch sw = new Stopwatch();
            int mResult = -2;
            Func<Task> startTask = () =>
            {
                return Task.Run(async () =>
                {
                    //开始动作
                    TestActionSwitch[0] = true;
                    StepFlag[0] = 0;
                    sw.Start();
                    testResult[0] = TestResult.Unknow;
                    testStatus[0] = TestStatus.Testing;
                    while (StepFlag[0] != 4 && mResult != 2)
                    {
                        TestSpan[0] = Math.Round(sw.Elapsed.TotalSeconds, 2);
                        await Task.Delay(50);
                    }
                });
            };
            Task taskDelay = Task.Delay(TestTimeout);
            var completeTask = await Task.WhenAny(startTask(), taskDelay);
            if (completeTask == taskDelay)
            {
                //超时退出
                mResult = 2;
            }
        }
        /// <扫描周期>
        /// 100ms
        /// </扫描周期>
        private async void RunLoop()
        {
            int[] prePassCount = new int[4] { 0, 0, 0, 0 };
            int[] preFailCount = new int[4] { 0, 0, 0, 0 };
            int[] pc = new int[4] { 0, 0, 0, 0 };
            int[] fc = new int[4] { 0, 0, 0, 0 };
            while (true)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (TestActionSwitch[i])
                    {
                        switch (StepFlag[i])
                        {
                            case 0://读PrePass；读PreNg
                                break;
                            case 1://写条码
                                break;
                            case 2://写按钮
                                break;
                            case 3://读Pass；读Ng
                                break;
                            case 4://完成
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        StepFlag[i] = -1;

                    }
                }
                await Task.Delay(100);
            }
        }
        #endregion
    }
}
