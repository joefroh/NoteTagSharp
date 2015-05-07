using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteTag;

namespace NoteTagTest.UnitTests
{
    [TestClass]
    public class NoteTreeTests
    {
        private const string PATH = "TestNoteFiles/SimpleHappyPath.ntf";
        private const string BAREPATH = "TestNoteFiles/BareString.ntf";

        [TestMethod]
        public void ConstructorWithNullTest()
        {
            try
            {
                var tree = new NoteTree(null);
                Assert.Fail("Constructor failed to throw exception for null.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void ConstructorTest()
        {
            var note = new NoteNode("test", "test");
            var tree = new NoteTree(note);

            Assert.IsNotNull(tree);
        }

        [TestMethod]
        public void SearchTagTest()
        {
            var note = new NoteNode("testTag", "content");
            var tree = new NoteTree(note);

            Assert.IsTrue(tree.SearchTag("testTag"));
        }

        [TestMethod]
        public void GetTagsTest()
        {
            var note = new NoteNode("testTag", "content");
            var tree = new NoteTree(note);
            IEnumerable<string> list = tree.GetTags();

            Assert.AreEqual(list.Count(), 1);
            Assert.AreEqual(list.ElementAt(0), "testTag");
        }

        [TestMethod]
        public void GetNotesByTagTest()
        {
            var note = new NoteNode("testTag", "content");
            var tree = new NoteTree(note);

            IEnumerable<NoteNode> result = tree.GetNotesByTag("testTag");

            Assert.AreEqual(result.Count(), 1);
            Assert.AreEqual(result.ElementAt(0), note);
        }

        [TestMethod]
        public void NotesToString()
        {
            var note = new NoteNode("test", "test");
            var tree = new NoteTree(note);
            var output = tree.ToString();

            var parser = new NoteParser(output);
            var tree2 = parser.GetTree();
            var output2 = tree2.ToString();

            Assert.AreEqual(output, output2);
        }

        [TestMethod]
        public void RealTreeToString()
        {
            var parser = new NoteParser(new FileInfo(PATH));
            var tree = parser.GetTree();
            var output = tree.ToString();

            var parser2 = new NoteParser(output);
            var tree2 = parser2.GetTree();
            var result = tree2.ToString();

            Assert.AreEqual(output, result);
        }

        [TestMethod]
        public void BareString()
        {
            var note = new NoteNode("test", "test");
            var result = note.ToBareString();
            Assert.AreEqual("test", result);
        }

        [TestMethod]
        public void BareStringTree()
        {
            const string expected = "This is a test of the BareString method to see if it works";
            var parser = new NoteParser(new FileInfo(BAREPATH));
            var tree = parser.GetTree();
            var result = tree.ToBareString();

            Assert.AreEqual(expected, result);
        }
    }
}