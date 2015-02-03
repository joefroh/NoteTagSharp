using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTag
{
    public class NoteParser
    {
        /// <summary>
        /// Contains the contents of the file passed to the constructor to parse without locking the file.
        /// </summary>
        private string fileContents;

        public NoteParser(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                fileContents = reader.ReadToEnd();
            }

        }

        /// <summary>
        /// Parses the file contents.
        /// </summary>
        private void Parse()
        {
            var stream = new MemoryStream();
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(fileContents);
                stream.Position = 0;

                using (StreamReader reader = new StreamReader(stream))
                {
                    //advance stream to beginning tag
                    while (reader.Read() != '<') ;

                    ReadTag(stream, null);
                }
            }
        }

        private NoteNode ReadTag(StreamReader stream, NoteNode parent)
        {
            var node = new NoteNode(null, null, parent);

            StringBuilder builder = new StringBuilder();

            while (!stream.EndOfStream)
            {
                var newChar = stream.Read();

                if (newChar != ">")
                {
                    builder.Append(newChar);
                }
                else
                {
                    break;
                }
            }

            node.Tag = builder.ToString();
            builder.Clear();

            while (!stream.EndOfStream)
            {
                //read the tag until ">" then read in content until end tag "</>" or "<" and recruse
                var newchar = stream.Read();

                if (newchar != "<")
                {
                    builder.Append = newchar;
                }
                else
                {
                    if (stream.Peek() == "/")
                    {
                        stream.Read();
                        if (stream.Peek() == ">")
                        {
                            stream.Read();
                            node.Content = builder.ToString();
                            return node;
                        }
                        else
                        {
                            throw new Exception("Bad Formed tag");
                        }
                    }
                    else
                    {
                        ReadTag(stream, node);
                    }
                }
            }
        }
    }
}
