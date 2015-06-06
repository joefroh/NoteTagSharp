using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoteTag
{

    //TODO: Look into using observables on the tags or contents to allow for search cache updating
    public class NoteNode
    {
        private string _content;

        public NoteNode(string tag, string content, NoteNode parent)
        {
            Tag = tag;
            Content = content;
            Parent = parent;
            Children = new List<NoteNode>();

            if (parent != null)
            {
                parent.Children.Add(this);
            }
        }

        public NoteNode(string tag, string content)
            : this(tag, content, null)
        {
        }

        public string Content
        {
            get { return _content; }
            set
            {
                if (value != null)
                {
                    _content = value.Replace("\r\n",""); //trim here removed logical spaces so i did a replace for line feeds instead
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("<{0}>", Tag));
            builder.AppendLine();
            var children = Children.Select((c) => c.ToString()).ToArray();
            builder.Append(string.Format(Content, children));
            builder.AppendLine();
            builder.Append("</>");
            
            return builder.ToString();
        }

        public string ToBareString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(Content, Children.Select((c) => c.ToBareString()).ToArray());
            return builder.ToString();
        }

        public string Tag { get; set; }
        public List<NoteNode> Children { get; private set; }

        //Note this is null for the top of a tree.
        public NoteNode Parent { get; set; }
    }
}