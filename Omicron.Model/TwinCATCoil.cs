using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omicron.Model
{
    public class TwinCATCoil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mName">MAIN.Value,注意MAIN必须大写</param>
        /// <param name="mType">数据类型，INT类型对应UInt16</param>
        /// <param name="mMode">Read和Notice模式</param>
        public TwinCATCoil(string mName,Type mType,Mode mMode)
        {
            CoilName = mName;
            CoilType = mType;
            CoilMode = mMode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mName">MAIN.Value,注意MAIN必须大写</param>
        /// <param name="mType">数据类型，INT类型对应UInt16</param>
        /// <param name="mGroup">如果是数组必须指定长度</param>
        /// <param name="mMode">Read和Notice模式</param>
        public TwinCATCoil(string mName, Type mType,int mGroup, Mode mMode)
        {
            CoilName = mName;
            CoilType = mType;
            CoilGroup = mGroup;
            CoilMode = mMode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mName">MAIN.Value,注意MAIN必须大写</param>
        /// <param name="mType">数据类型，INT类型对应UInt16</param>
        /// <param name="mGroup">如果是数组必须指定长度</param>
        /// <param name="mMode">Read和Notice模式</param>
        /// <param name="mCycleTime">Notice模式下循环周期/ms，默认100ms，需要更新很快可以用1ms</param>
        public TwinCATCoil(string mName, Type mType,int mGroup, Mode mMode, int mCycleTime)
        {
            CoilName = mName;
            CoilType = mType;
            CoilMode = mMode;
            CoilGroup = mGroup;
            CycleTime = mCycleTime;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mName">MAIN.Value,注意MAIN必须大写</param>
        /// <param name="mType">数据类型，INT类型对应UInt16</param>
        /// <param name="mMode">Read和Notice模式</param>
        /// <param name="mCycleTime">Notice模式下循环周期/ms，默认100ms，需要更新很快可以用1ms</param>
        public TwinCATCoil(string mName, Type mType, Mode mMode,int mCycleTime)
        {
            CoilName = mName;
            CoilType = mType;
            CoilMode = mMode;
            CycleTime = mCycleTime;
        }
        public int CoilHandle { get; set; }
        public int CoilNoticeHandle { get; set; }
        public string CoilName
        {
            get;
            set;
        }
        public Type CoilType
        {
            get;
            set;
        }
        public int CoilGroup { get; set; }
        public Mode CoilMode
        {
            get;
            set;
        }
        public enum Mode
        {
            Read,
            Notice,
        }
        public int CycleTime = 100;
        private object mCoilValue;
        public object CoilValue
        {
            get { return mCoilValue; }
            set
            {
                if (mCoilValue == value)
                    return;
                mCoilValue = value;
                if (CoilChanged != null)
                    CoilChanged(this, null);
            }
        }
        public void UpdateCoilValue(object NewValue)
        {
            if (mCoilValue == NewValue)
                return;
            mCoilValue = NewValue;
            if (CoilUpdate != null)
                CoilUpdate(this, null);
        }
        public void UpdateCoilValueEx()
        {
            if (CoilUpdate != null)
                CoilUpdate(this, null);
           
        }
        public event EventHandler CoilChanged;
        public event EventHandler CoilUpdate;
       
    }
}
