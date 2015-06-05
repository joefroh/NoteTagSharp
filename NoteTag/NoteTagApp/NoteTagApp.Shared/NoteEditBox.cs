using System.Diagnostics;
using System.Linq;
using Windows.System;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace NoteTagApp
{
    public class NoteEditBox : RichEditBox
    {
        private bool _enteringTagMode;
        private bool _inTagMode;
        private bool _skipEvent;

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

            if (e.Key == VirtualKey.Enter && (_inTagMode || _enteringTagMode))
            {
                if (_enteringTagMode)
                {
                    MoveCurserToEndTag();
                    _inTagMode = true;
                    _enteringTagMode = false;
                }
                else if (_inTagMode)
                {
                    _inTagMode = false;
                    _enteringTagMode = false;
                    MoveCurserToEndTag();
                }

            }
            else
            {
                base.OnKeyUp(e);
            }
        }


        protected override void OnKeyDown(KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter && (_inTagMode || _enteringTagMode))
            {
            }
            else
            {

                base.OnKeyDown(e);
            }
        }

        private void MoveCurserToEndTag()
        {
            var cursor = this.Document.Selection.StartPosition + 1;
            string text = "";
            this.Document.GetText(TextGetOptions.None, out text);
            

            while (text.Substring(cursor - 1, 1) != ">")
            {
                cursor++;
            }

            this.Document.Selection.StartPosition = cursor;
            this.Document.Selection.EndPosition = cursor;
        }

        //TODO: Fix the tag completion logic, its shit right now
        /// <summary>
        ///     Method that is called when the composer text box is edited in any way.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextEntryChanged(object sender, RoutedEventArgs e)
        {
            var textBox = (RichEditBox)sender;

            var data = "";
            this.Document.GetText(TextGetOptions.None, out data);
            if (!_skipEvent && data.Trim().Any())
            {
                //Debug.WriteLine(data[textBox.SelectionStart - 1]);
                if (data[textBox.Document.Selection.StartPosition - 1] == '<')
                {
                    _enteringTagMode = true;
                    _inTagMode = false;
                    _skipEvent = true;
                    var start = textBox.Document.Selection.StartPosition;
                    data = data.Insert(textBox.Document.Selection.StartPosition, "></>");
                    textBox.Document.SetText(TextSetOptions.None, data);
                    textBox.Document.Selection.StartPosition = start;
                    textBox.Document.Selection.EndPosition = start;
                }
            }
            else
            {
                _skipEvent = false;
            }
        }
    }
}