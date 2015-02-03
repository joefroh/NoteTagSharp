using System;
using System.IO;
using System.Text;

namespace NoteTag
{
    public class NoteParser
    {
        /// <summary>
        ///     Contains the contents of the file passed to the constructor to parse without locking the file.
        /// </summary>
        private readonly string fileContents;

        public NoteParser(string path)
        {
            using (var reader = new StreamReader(path))
            {
                fileContents = reader.ReadToEnd();
            }
        }

        /// <summary>
        ///     Parses the file contents.
        /// </summary>
        public NoteNode Parse()
        {
            var stream = new MemoryStream();
            NoteNode result = null;
            using (var writer = new StreamWriter(stream))
            {
                writer.AutoFlush = true;
                writer.Write(fileContents);
                stream.Position = 0;

                using (var reader = new StreamReader(stream))
                {
                    //advance stream to beginning tag
                    while (reader.Read() != '<') ;

                    result = ReadTag(reader, null);
                }
            }
            return result;
        }

        private NoteNode ReadTag(StreamReader stream, NoteNode parent)
        {
            var node = new NoteNode(null, null, parent);

            var builder = new StringBuilder();

            while (!stream.EndOfStream)
            {
                int newChar = stream.Read();

                if (newChar != '>')
                {
                    builder.Append((char)newChar);
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
                int newchar = stream.Read();

                if (newchar != '<')
                {
                    builder.Append((char)newchar);
                }
                else
                {
                    if (stream.Peek() == '/')
                    {
                        stream.Read();
                        if (stream.Peek() == '>')
                        {
                            stream.Read();
                            node.Content = builder.ToString();
                            return node;
                        }

                        throw new Exception("Bad Formed tag");
                    }

                    ReadTag(stream, node);
                }
            }
            return node;
        }
    }
}