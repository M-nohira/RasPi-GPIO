using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace RasPi_GPIO
{
    public class pwm
    {
        const string pwmDir = "/sys/class/pwm";

        int _chipNum;
        private int frequency;
        public int Frequency
        {
            get { return frequency; }
            set
            {
                if (frequency == value) return;

                frequency = ChengeFrequency() ? value : frequency;
            }
        }
        private float duty;
        public float Duty
        {
            get { return duty; }
            set
            {
                if (duty == value) return;

            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="chipNum">pwm chipNumber</param>
        /// <param name="frequecy">frequeny</param>
        /// <param name="duty">duty ratio</param>
        public pwm(int chipNum, int frequecy, float duty)
        {
            _chipNum = chipNum;

        }

        private bool ChengeFrequency()
        {
            try
            {
                string path = $"{pwmDir}/pwm{_chipNum}/period";
                int period = GetPeriod(Frequency);
                int duty = (int)(period * Duty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        private bool ChengeDuty()
        {
            try
            {
                string path = $"{pwmDir}/pwm{_chipNum}/duty";
                int duty = (int)(Frequency * Duty);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return false;
        }

        static int GetPeriod(int frequency)
        {
            return (1000000000 / frequency);
        }

        /// <summary>
        /// 指定したファイルに書き込む
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="msg">書き込む内容</param>
        static private void PinWrite(string path, string msg)
        {
            File.WriteAllText(path, msg);
        }

    }
}
