using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteTag;

namespace NoteTagTest.UnitTests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void HappyParse()
        {
            var parser = new NoteParser(new FileInfo("TestNoteFiles/SimpleHappyPath.ntf"));
            var result = parser.GetTree();

            Assert.IsNotNull(result);
        }
    }

   
}