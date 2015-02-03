using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTag
{
    public class NoteNode
    {
        public string Content { get; set; }
        public string Tag { get; set; }
        public List<NoteNode> Children { get; private set; }

        //Note this is null for the top of a tree.
        public NoteNode Parent { get; set; }

        public NoteNode(string tag, string content, NoteNode parent)
        {
            this.Tag = tag;
            this.Content = content;
            this.Parent = parent;
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

    }
}
