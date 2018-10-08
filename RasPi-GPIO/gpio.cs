﻿using System;
using System.IO;

namespace RasPi_GPIO
{
	public class GPIO
	{
		const string gpioDir = "/sys/class/gpio";

		private int _pinNum;
		private string _pinDir;

		/// <summary>
		/// インスタンス作成 -Create Instance-
		/// </summary>
		/// <param name="pinNum">ピン番号 -GPIO PinNumber-</param>
		public GPIO(int pinNum, bool isGPIOPinNum = true)
		{
			_pinNum = pinNum;
			_pinDir = $"{gpioDir}/gpio{pinNum}";
		}

		/// <summary>
		/// ピンを有効化する -Set Pin Into Activate-
		/// </summary>
		public void PinActivate()
		{
			if (CheckPinActivate()) return;

			string gpio_d = $"{gpioDir}/export";
			PinWrite(gpio_d, _pinNum.ToString());

		}

		/// <summary>
		/// ピンの入出力を設定 -Set Pin Direction-
		/// </summary>
		/// <param name="mode">1 = Output,0 = Input</param>
		public void SetPinDirection(int mode)
		{
			string direct_pin = $"{_pinDir}/direction";
			string smode = (mode == 1) ? "In" : "Out";
			PinWrite(direct_pin, smode);
		}

		/// <summary>
		/// ピンの状態を出力
		/// </summary>
		/// <returns>1 = High, 0 = Low</returns>
		public int GetPinValue()
		{
			string value_pin = $"{_pinDir}/value";
			return Convert.ToInt32(File.ReadAllText(value_pin));
		}

		/// <summary>
		/// ピンの状態を設定
		/// </summary>
		/// <param name="value">1=High,0=Low</param>
		public void SetPinValue(int value)
		{
			string value_pin = $"{_pinDir}/value";
			PinWrite(value_pin, value.ToString());
		}

		/// <summary>
		/// ピンが有効化されているかを確認する -Check Pin Was Activated or InActivated-
		/// </summary>
		/// <returns></returns>
		private bool CheckPinActivate()
		{
			if (Directory.Exists(_pinDir)) return true;
			return false;
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
