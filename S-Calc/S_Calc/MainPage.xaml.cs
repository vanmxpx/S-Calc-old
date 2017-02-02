using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading;
using Xamarin.Forms;
using RPNlib;

namespace S_Calc
{
    public partial class MainPage : ContentPage
    {
        Entry Input, Output, Precision;
        Label PrecisionLabel, ColorLabel;
        Picker Pick;
        RPN_Real r;
        string errorMsg;
        Action<string> act;
        //Thread t;

        const string _font_family = "Times New Roman";
        const double _font_size = 25;
        const double _font_size_settings = 16;

        public MainPage()
        {
            InitializeComponent();
            r = new RPN_Real();
            act = new Action<string>((res) =>
            {
                Output.Text = string.Format($" = {res}");
            });


            StackLayout IOLayout = new StackLayout();
            Input = new Entry();
            Input.TextChanged += Input_TextChanged;            
            Input.FontFamily = _font_family;
            Input.FontSize = _font_size;      
            Output = new Entry();
            Output.Placeholder = " = ";
            Output.FontSize = _font_size;
            Output.FontFamily = _font_family;

            Precision = new Entry();
            Precision.Text = "4";
            Precision.TextChanged += Precision_TextChanged;
            Precision.FontFamily = _font_family;
            Precision.FontSize = _font_size;

            PrecisionLabel = new Label();
            PrecisionLabel.Text = "Количество знаков после запятой:";
            PrecisionLabel.FontFamily = _font_family;
            PrecisionLabel.FontSize = _font_size_settings;
            PrecisionLabel.HorizontalOptions = LayoutOptions.Start;
            PrecisionLabel.VerticalOptions = LayoutOptions.End;

            ColorLabel = new Label();
            ColorLabel.Text = "Цвет фона:";
            ColorLabel.FontFamily = _font_family;
            ColorLabel.FontSize = _font_size_settings;
            ColorLabel.HorizontalOptions = LayoutOptions.Start;
            ColorLabel.VerticalOptions = LayoutOptions.End;

            Pick = new Picker
            {
                Title = "Белый"
            };
            Pick.Items.Add("Синий");
            Pick.Items.Add("Белый");
            Pick.Items.Add("Зеленый");

            Pick.SelectedIndexChanged += Pick_SelectedIndexChanged;

            IOLayout.Children.Add(Input);
            IOLayout.Children.Add(Output);
            IOLayout.Children.Add(PrecisionLabel);
            IOLayout.Children.Add(Precision);
            IOLayout.Children.Add(ColorLabel);
            IOLayout.Children.Add(Pick);
            Content = IOLayout;

           
        }

        private void Pick_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Pick.Items[Pick.SelectedIndex])
            {
                case "Белый": BackgroundColor = Color.White; break;
                case "Синий": BackgroundColor = Color.Aqua; break;
                case "Зеленый": BackgroundColor = Color.Lime; break;
            }
        }

        private void Precision_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                RPN_Real.Precision = Convert.ToUInt16(Precision.Text);
                Input_TextChanged(Input, e);
            }
            catch
            {
                return;
            }
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Input.Text.Contains(" "))
            {
                Input.Text = Input.Text.Replace(" ", string.Empty);
            }
            act(r.ToString(Input.Text, ref errorMsg));
            //t = new Thread(_run) { Priority = ThreadPriority.Highest };
            //t.Start(expr);
        }
        
        //void _run(object expr)
        //{
        //    this. Invoke(act, r.ToString(expr as string, ref errorMsg));
        //}
    }
}
