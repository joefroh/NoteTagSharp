using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
            this.MainDataEntry.TagsUpdated += UpdateTagList;
        }

        private void UpdateTagList(Dictionary<string, int> tags)
        {
            var builder = new StringBuilder();
            foreach (var tag in tags)
            {
                if (tag.Key.Equals("NoteTag"))
                {
                    continue;
                }
                builder.AppendLine(String.Format("{0}: {1}", tag.Key, tag.Value));
            }
            this.TagListBlock.Text = builder.ToString();
        }
    }
}
