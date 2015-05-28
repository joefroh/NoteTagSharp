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
            var textBox = (RichEditBox)sender;
            string data = "";
            textBox.Document.GetText(new TextGetOptions(), out data);
            if (data.Trim().Any())
            {
                switch (data.Trim().Last())
                {
                    case '<':
                        enteringTag = true;
                        break;
                    case '>':
                        var chunks = data.Trim().Split();
                        tagText = chunks.Last().Replace("<", "").Replace(">", "");
                        if (enteringTag)
                        {
                            enteringTag = false;
                            var result = data.Trim() + "</>";
                            textBox.Document.SetText(new TextSetOptions(), result);
                        }
                        break;
                }

            }
        }
    }
}
