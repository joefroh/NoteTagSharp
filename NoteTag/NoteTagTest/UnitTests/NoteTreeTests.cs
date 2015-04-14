﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteTag;

namespace NoteTagTest.UnitTests
{
    [TestClass]
    public class NoteTreeTests
    {
        private const string PATH = "TestNoteFiles/SimpleHappyPath.ntf";

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
    }
}