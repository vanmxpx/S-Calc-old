using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Xamarin.Forms;
using RPNlib;
using Mono.CSharp;

namespace S_Calc
{
    public partial class MainPage : ContentPage
    {
        RPN_Real r;
        string errorMsg, ExchangeBuffer = string.Empty,tmp=string.Empty;
        List<string> history;
        const int _history_list_max_limit = 100;
        public MainPage()
        {
            InitializeComponent();
            r = new RPN_Real();
            history = new List<string>() { string.Empty};
            undoButton.IsEnabled = false;
            Input.Unfocused += (sender, e) => 
                {
                    Input.Focus();
                };
        }
        void history_list_add(string s)
        {
            history.Add(s);
            if (history.Count > _history_list_max_limit) { history.RemoveAt(0); }
        }
        //Цифры, точка, запятая, операторы
        private void KeyClicked(object sender, EventArgs e)
        {
            Input.Text += ((Button)sender).Text.Replace("√", "√()");
        }
        private void KeyUndoClicked(object sender, EventArgs e)
        {
            try
            {
                history.RemoveAt(history.Count - 1);
                tmp = history[history.Count - 1];
                history.RemoveAt(history.Count - 1);
                Input.Text = tmp;
            }
            catch { return; }
        }
        private void KeyBackspaceClicked(object sender, EventArgs e)
        {
            try
            {
                Input.Text = Input.Text.Remove(Input.Text.Length - 1, 1);
            }
            catch
            {
                return;
            }
        }
        private void KeyCopyClicked(object sender, EventArgs e)
        {
            ExchangeBuffer = Output.Text.Replace(" = ",string.Empty);
        }
        private void KeyPasteClicked(object sender, EventArgs e)
        {
            Input.Text += ExchangeBuffer;
        }
        private void KeyDeleteClicked(object sender, EventArgs e)
        {
            Input.Text = string.Empty;
        }
        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            history_list_add(Input.Text);
            if (history.Count <= 1)
            {
                undoButton.IsEnabled = false;
            }
            else
            {
                undoButton.IsEnabled = true;
            }
            string inp = Input.Text.ReplaceAllIncorrectChars();
            string res = r.ToString(inp, ref errorMsg);
            Output.Text = string.Format($" = {res}");
        }
    }
}
