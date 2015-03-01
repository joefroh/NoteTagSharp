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
            var parser = new NoteParser("TestNoteFiles/SimpleHappyPath.ntf");
            var result = parser.GetTree();

            Assert.IsNotNull(result);
        }
    }

   
}