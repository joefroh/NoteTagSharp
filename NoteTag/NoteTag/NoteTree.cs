using System;
using System.Collections.Generic;

namespace NoteTag
{
    public class NoteTree
    {
        private readonly NoteNode _root;

        public NoteTree(NoteNode root)
        {
            if (null == root)
            {
                throw new ArgumentNullException("root");
            }

            _root = root;
        }

        public bool SearchTag(string tag)
        {
            return RecurseSearchTag(_root, tag);
        }

        private bool RecurseSearchTag(NoteNode node, string tag)
        {
            if (node.Tag == tag)
            {
                return true;
            }

            foreach (var noteNode in node.Children)
            {
                if (RecurseSearchTag(noteNode, tag))
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<NoteNode> GetNotesByTag(string tag)
        {
            return RecurseGetNotesByTag(new List<NoteNode>(), _root);
        }

        private IEnumerable<NoteNode> RecurseGetNotesByTag(List<NoteNode> list, NoteNode node)
        {
            if (!list.Contains(node))
            {
                list.Add(node);
            }

            foreach (var noteNode in node.Children)
            {
                RecurseGetNotesByTag(list, noteNode);
            }

            return list;
        }

        public IEnumerable<string> GetTags()
        {
            var list = new List<string>();
            return RecurseGetTags(list, _root);
        }

        private IEnumerable<string> RecurseGetTags(List<string> list, NoteNode node)
        {
            if (!list.Contains(node.Tag))
            {
                list.Add(node.Tag);
            }

            foreach (var noteNode in node.Children)
            {
                RecurseGetTags(list, noteNode);
            }
            return list;
        }
    }
}