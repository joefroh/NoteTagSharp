﻿using System.Linq;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace NoteTagApp
{
    public class NoteEditBox : TextBox
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

            if (e.Key == VirtualKey.Enter && _inTagMode)
            {
                MoveCurserToEndTag();
                _inTagMode = false;

            }
            else
            {

                base.OnKeyUp(e);
            }
        }

        protected override void OnKeyDown(KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter && _inTagMode)
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
            if (!_skipEvent && data.Trim().Any())
            {
                if (data[textBox.SelectionStart - 1] == '<')
                {
                    _enteringTagMode = true;
                    _skipEvent = true;
                    var start = textBox.SelectionStart;
                    textBox.Text = textBox.Text.Insert(textBox.SelectionStart, ">");
                    textBox.SelectionStart = start;
                }
            }
        }
    }
}