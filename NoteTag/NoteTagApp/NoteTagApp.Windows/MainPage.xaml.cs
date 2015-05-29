using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Text;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NoteTagApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string tagText = "";
        private bool enteringTag = false;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnTextEntryChanged(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            
            string data = "";
            data = textBox.Text;
            if (data.Trim().Any())
            {
                if (!enteringTag && data[textBox.SelectionStart - 1] == '>')
                {
                    enteringTag = true;
                    var start = textBox.SelectionStart;
                    var length = textBox.SelectionLength;

                    textBox.Text = data.Insert(textBox.SelectionStart, "</>");
                    textBox.SelectionStart = start;
                    textBox.SelectionLength = length;

                }
                else
                {
                    enteringTag = false;
                }

            }
           
        }
    }
}
