using System.Linq;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace NoteTagApp
{
    public class NoteEditBox : TextBox
    {
        private bool _enteringTag;
        private bool _inTag;

        /// <summary>
        /// Constructor. Rigs up all the event handlers as that are done internally as well for cleanliness.
        /// </summary>
        public NoteEditBox()
        {
            this.AcceptsReturn = true;

            TextChanged += OnTextEntryChanged;
        }

        protected override void OnKeyUp(KeyRoutedEventArgs e)
        {

            if (e.Key == VirtualKey.Enter && _inTag)
            {
                MoveCurserToEndTag();
                _inTag = false;

            }
            else
            {

                base.OnKeyUp(e);
            }
        }

        protected override void OnKeyDown(KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter && _inTag)
            {

            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        private void MoveCurserToEndTag()
        {
            var cursor = this.SelectionStart;

            while (this.Text.Substring(cursor - 3, 3) != "</>")
            {
                cursor++;
            }

            this.SelectionStart = cursor;
            this.SelectionLength = 0;
        }

        //TODO: Fix the tag completion logic, its shit right now
        /// <summary>
        ///     Method that is called when the composer text box is edited in any way.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextEntryChanged(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;

            var data = "";
            data = textBox.Text;
            if (data.Trim().Any())
            {
                if (!_enteringTag && data[textBox.SelectionStart - 1] == '>')
                {
                    _enteringTag = true;
                    var start = textBox.SelectionStart;
                    var length = textBox.SelectionLength;

                    textBox.Text = data.Insert(textBox.SelectionStart, "</>");
                    textBox.SelectionStart = start;
                    textBox.SelectionLength = length;
                    _inTag = true;
                }
                else
                {
                    //This fixes an issue where the end tag was being continually added due to the addition of it
                    //also triggeringthe event.
                    _enteringTag = false;
                }
            }
        }
    }
}