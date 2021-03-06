﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;
using System.Threading;
using System.Text.RegularExpressions;

namespace Stock
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (e.GetPosition((IInputElement)sender).Y < title.Height.Value)
                {
                    this.DragMove();
                }
            }
        }

        private void Min_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id;
            if (int.TryParse(StockID.Text, out id) == false)
            {
                MessageBox.Show("错误的股票编号输入\n请输入纯数字编号");
                return;
            }
            StockInfo dlg = new StockInfo();
            dlg.StockID = StockID.Text;
            dlg.colora.Color = this.colora.Color;
            dlg.colorb.Color = this.colorb.Color;
            dlg.colorc.Color = this.colorc.Color;
            dlg.colord.Color = this.colord.Color;
            dlg.colore.Color = this.colore.Color;
            dlg.Show();
        }

        private void StockID_GotFocus(object sender, RoutedEventArgs e)
        {
            if (StockID.Text == "搜索:请输入股票编号")
            {
                StockID.Text = "";
            }
        }

        private void StockID_LostFocus(object sender, RoutedEventArgs e)
        {
            if (StockID.Text == "")
            {
                StockID.Text = "搜索:请输入股票编号";
            }
        }

        private void OpenExcle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择文件";
            openFileDialog.Filter = "xls,xlsx文件|*.xls;*.xlsx";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = "xls";
            if (openFileDialog.ShowDialog() == true)
            {
                MessageBox.Show(openFileDialog.FileName);
            }
            else
            {
                return;
            }
        }

        private void DealList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DealList dlg = new DealList();
            dlg.colora.Color = this.colora.Color;
            dlg.colorb.Color = this.colorb.Color;
            dlg.colorc.Color = this.colorc.Color;
            dlg.colord.Color = this.colord.Color;
            dlg.colore.Color = this.colore.Color;
            dlg.Show();
        }

        private void Structure_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Structure dlg = new Structure();
            dlg.colora.Color = this.colora.Color;
            dlg.colorb.Color = this.colorb.Color;
            dlg.colorc.Color = this.colorc.Color;
            dlg.colord.Color = this.colord.Color;
            dlg.colore.Color = this.colore.Color;
            dlg.Show();
        }

        private void Yield_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Yield dlg = new Yield();
            dlg.colora.Color = this.colora.Color;
            dlg.colorb.Color = this.colorb.Color;
            dlg.colorc.Color = this.colorc.Color;
            dlg.colord.Color = this.colord.Color;
            dlg.colore.Color = this.colore.Color;
            dlg.Show();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InfoShowTimer = new Timer(ShowBoxCheck, null, 0, 2 * 1000);
            InfoShowTimer.Change(-1, 0);
            dlg = new InfoShow();
            total.Text = "100000.00";
            now.Text = "5000.00";
            total.IsEnabled = false;
            //now.IsEnabled = false;
            StockStateBox box1 = new StockStateBox();
            box1.Margin = new Thickness(5, 5, 0, 0);
            box1.StockName.Text = "工商\r\n银行";
            box1.UEvent += new EventHandler(uEvent);
            StockCanvas.Children.Add(box1);
            StockStateBox box2 = new StockStateBox();
            box2.Margin = new Thickness(5, 10 + box1.Margin.Top + box1.Height, 0, 0);
            box2.StockName.Text = "伊利\r\n股份";
            box2.UEvent += new EventHandler(uEvent);
            StockCanvas.Children.Add(box2);
            StockStateBox box3 = new StockStateBox();
            box3.Margin = new Thickness(5, 10 + box2.Margin.Top + box2.Height, 0, 0);
            box3.StockName.Text = "工商\r\n银行";
            box3.UEvent += new EventHandler(uEvent);
            StockCanvas.Children.Add(box3);
            StockStateBox box4 = new StockStateBox();
            box4.Margin = new Thickness(5, 10 + box3.Margin.Top + box3.Height, 0, 0);
            box4.StockName.Text = "北京\r\n银行";
            box4.UEvent += new EventHandler(uEvent);
            StockCanvas.Children.Add(box4);
            StockStateBox box5 = new StockStateBox();
            box5.Margin = new Thickness(5, 10 + box4.Margin.Top + box4.Height, 0, 0);
            box5.StockName.Text = "以岭\r\n药业";
            box5.UEvent += new EventHandler(uEvent);
            StockCanvas.Children.Add(box5);
            StockStateBox box6 = new StockStateBox();
            box6.Margin = new Thickness(5, 10 + box5.Margin.Top + box5.Height, 0, 0);
            box6.StockName.Text = "上海\r\n家化";
            box6.UEvent += new EventHandler(uEvent);
            StockCanvas.Children.Add(box6);
        }

        private void uEvent(object sender, EventArgs e)
        {
            //do something(including send message to other user controls)
            StockInfo dlg = new StockInfo();
            dlg.StockID = Regex.Replace(((StockStateBox)sender).StockName.Text, @"[\r\n]", "");
            dlg.colora.Color = this.colora.Color;
            dlg.colorb.Color = this.colorb.Color;
            dlg.colorc.Color = this.colorc.Color;
            dlg.colord.Color = this.colord.Color;
            dlg.colore.Color = this.colore.Color;
            dlg.Show();
        }

        private Timer InfoShowTimer;
        private bool flag;
        private InfoShow dlg;
        private delegate void DelegateShowInfoBox();
        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            InfoShowTimer.Change(-1, 0);
            InfoShowTimer = new Timer(ShowBoxCheck, null, 1000, 1000);
            flag = true;
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            dlg.Hide();
            flag = false;
        }
        private void ShowBoxCheck(object obj)
        {
            InfoShowTimer.Change(-1, 0);
            if (flag == true)
            {
                Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new DelegateShowInfoBox(ShowInfoBox));
            }
        }
        private void ShowInfoBox()
        {
            dlg.WindowStartupLocation = WindowStartupLocation.Manual;
            dlg.Left = this.Left + this.ActualWidth + 5;
            dlg.Top = this.Top;
            dlg.Show();
        }

        private double mark;
        private void now_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (now.IsReadOnly == true)
            {
                mark = Convert.ToDouble(now.Text.ToString());
                now.IsReadOnly = false;
            }
        }

        private void now_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (now.IsReadOnly == false)
            {
                now.IsReadOnly = true;
                double o;
                if (Double.TryParse(now.Text.ToString(), out o))
                {
                    double t = Convert.ToDouble(total.Text.ToString()) + o - mark;
                    if (t >= 1000000000)
                    {
                        now.Text = String.Format("{0:F}", mark);
                        return;
                    }
                    total.Text = String.Format("{0:F}", t);
                    now.Text = String.Format("{0:F}", o);
                }
                else
                {
                    now.Text = String.Format("{0:F}", mark);
                }
            }
        }
        private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            state.Focus();
        }

        private void Color_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Colors dlg = new Colors();
            dlg.colora.Color = this.colora.Color;
            dlg.colorb.Color = this.colorb.Color;
            dlg.colorc.Color = this.colorc.Color;
            dlg.colord.Color = this.colord.Color;
            dlg.colore.Color = this.colore.Color;
            dlg.Owner = this;
            dlg.Show();
        }

        private void StockCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            int d = e.Delta;
            if (d > 0)
            {
                if (((StockStateBox)StockCanvas.Children[0]).Margin.Top >= 5)
                    return;
                foreach (StockStateBox ui in StockCanvas.Children)
                {
                    ui.Margin = new Thickness(5, ui.Margin.Top + 20 * d / 120, 0, 0);
                }
            }
            if (d < 0)
            {
                StockStateBox ssb = (StockStateBox)StockCanvas.Children[StockCanvas.Children.Count - 1];
                if (ssb.Margin.Top + ssb.Height <= StockCanvas.Height) 
                    return;
                foreach (StockStateBox ui in StockCanvas.Children)
                {
                    ui.Margin = new Thickness(5, ui.Margin.Top + 20 * d / 120, 0, 0);
                }
            }
        }
    }
}
