using System.Collections.Generic;

namespace NoteTag
{
 
    //TODO: Look into using observables on the tags or contents to allow for search cache updating
    //TODO: override to string to recursively build the original not string less the tags of the nested nodes
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
                    _content = value.Trim();
                }
            }
        }

        public string Tag { get; set; }
        public List<NoteNode> Children { get; private set; }

        //Note this is null for the top of a tree.
        public NoteNode Parent { get; set; }
    }
}