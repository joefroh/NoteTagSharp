using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteTag;

namespace NoteTagTest.RegressionTests
{
    [TestClass]
    public class BadNoteFileTests
    {
        private const string NOHEADERPATH = @"TestNoteFiles/NoHeader.ntf";

        /// <summary>
        ///     This test tries to parse an NTF that does not have the "NoteFile" tag on it surrounding its contents.
        /// </summary>
        [TestMethod]
        public void MissingHeaderParse()
        {
            var parser = new NoteParser(NOHEADERPATH);
            NoteTree tree = parser.GetTree();
            var tags = tree.GetTags();

            Assert.IsTrue(tags.Contains("intro"), "Tag \"intro\" is missing from the list of tags");
            Assert.IsTrue(tags.Contains("date"), "Tag \"date\" is missing from the list of tags");
            Assert.IsTrue(tags.Contains("name"), "Tag \"name\" is missing from the list of tags");
            Assert.IsTrue(tags.Contains("context"), "Tag \"context\" is missing from the list of tags");
        }
    }
}