using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteTag;

namespace NoteTagTest.RegressionTests
{
    [TestClass]
    public class BadNoteContentTests
    {
        private const string NOHEADERPATH = @"TestNoteFiles/NoHeader.ntf";

        /// <summary>
        ///     This test tries to parse an NTF that does not have the "NoteFile"
        ///     tag on it surrounding its content.
        /// </summary>
        [TestMethod]
        public void MissingHeaderParse()
        {
            var parser = new NoteParser(new FileInfo(NOHEADERPATH));
            var tree = parser.GetTree();
            var tags = tree.GetTags();

            Assert.IsTrue(tags.Contains("intro"), "Tag \"intro\" is missing from the list of tags");
            Assert.IsTrue(tags.Contains("date"), "Tag \"date\" is missing from the list of tags");
            Assert.IsTrue(tags.Contains("name"), "Tag \"name\" is missing from the list of tags");
            Assert.IsTrue(tags.Contains("context"), "Tag \"context\" is missing from the list of tags");
        }

        /// <summary>
        /// Test that tries to get the class to parse a string that has no note content at all.
        /// </summary>
        [TestMethod]
        public void NoNoteTags()
        {
            var parser = new NoteParser("test123");
            var tree = parser.GetTree();
            var tags = tree.GetTags();
        }
    }
}