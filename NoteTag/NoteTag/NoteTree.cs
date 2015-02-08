using System;
using System.Collections.Generic;

namespace NoteTag
{
    public class NoteTree
    {
        private readonly NoteNode _root;

        /// <summary>
        /// Initializes a NoteTree given a root node.
        /// </summary>
        /// <param name="root">The NoteNode rooting the tree.</param>
        public NoteTree(NoteNode root)
        {
            if (null == root)
            {
                throw new ArgumentNullException("root");
            }

            _root = root;
        }

        /// <summary>
        /// Searches for the existance of a NoteNode with a given tag.
        /// </summary>
        /// <param name="tag">The tag of interest.</param>
        /// <returns>True if a tag is on any of the NoteNodes in the tree.</returns>
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

        /// <summary>
        /// Provides a list of all found Notes with the given tag.
        /// </summary>
        /// <param name="tag">The tag of interest.</param>
        /// <returns>A list containing all the NoteNodes or an empty list.</returns>
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

        /// <summary>
        /// Gets the list of all tags found in the NoteTree.
        /// </summary>
        /// <returns>The list of all tags in the tree.</returns>
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