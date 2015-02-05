using System;
using System.Collections.Generic;

namespace NoteTag
{
    public class NoteTree
    {
        private NoteNode _root;

        public NoteTree(NoteNode root)
        {
            _root = root;
        }

        public bool SearchTag(string tag)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NoteNode> GetNotesByTag(string tag)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetTags()
        {
            throw new NotImplementedException();
        }
    }
}