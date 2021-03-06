﻿using System;
using System.IO;
using System.Text;

namespace NoteTag
{
    public class NoteParser
    {
        private const string header = "<NoteFile>";
        private const string endTag = @"</>";

        /// <summary>
        ///     Contains the contents of the file passed to the constructor to parse without locking the file.
        /// </summary>
        private readonly string _noteContents;

        /// <summary>
        ///     Takes a string representing the contents of a note file and puts it into a cache for later processing.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="isPath"></param>
        public NoteParser(string input)
        {
            _noteContents = input;
        }

        /// <summary>
        ///     Initializes a new instance of the NoteParser class. Takes a path to an NTF file and reads its contents
        ///     into a cache for later processing.
        /// </summary>
        /// <param name="file"></param>
        /*   public NoteParser(FileInfo file)
           {
               using (var reader = new StreamReader(file.FullName))
               {
                   _noteContents = reader.ReadToEnd();
               }

               if (!_noteContents.StartsWith(header))
               {
                   var builder = new StringBuilder();
                   builder.Append(header);
                   builder.Append(_noteContents);
                   builder.Append(endTag);
                   _noteContents = builder.ToString();
               }
           }
         * */

        /// <summary>
        ///     Parses the file contents and provides a tree structure of the input file.
        /// </summary>
        /// <returns>A <see cref="NoteTree" /> containg the contents of the ntf file.</returns>
        public NoteTree GetTree()
        {
            var stream = new MemoryStream();
            NoteNode notes = null;
            using (var writer = new StreamWriter(stream))
            {
                writer.AutoFlush = true;
                writer.Write(_noteContents);
                stream.Position = 0;

                using (var reader = new StreamReader(stream))
                {
                    //advance stream to beginning tag
                    while (reader.Read() != '<' && !reader.EndOfStream) ;

                    notes = ReadTag(reader, null);
                }
            }
            return new NoteTree(notes);
        }

        private NoteNode ReadTag(StreamReader stream, NoteNode parent)
        {
            var node = GetNodeTagFromStream(stream, parent);
            return GetNodeContentFromStream(stream, node);
        }

        private NoteNode GetNodeContentFromStream(StreamReader stream, NoteNode node)
        {
            var builder = new StringBuilder();
            var count = 0;
            while (!stream.EndOfStream)
            {
                //read the tag until ">" then read in content until end tag "</>" or "<" and recruse
                var newchar = stream.Read();

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

                        throw new BadFormedTagFileException("Bad Formed tag");
                    }
                    builder.Append("{" + count + "}");
                    count++;
                    ReadTag(stream, node);
                }
            }
            return node;
        }

        private static NoteNode GetNodeTagFromStream(StreamReader stream, NoteNode parent)
        {
            var node = new NoteNode(null, null, parent);

            var builder = new StringBuilder();

            while (!stream.EndOfStream)
            {
                var newChar = stream.Read();

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
            return node;
        }
    }

    //TODO provide the location of the error so we can boil it up to the UI for the user to fix
    public class BadFormedTagFileException : Exception
    {
        public BadFormedTagFileException(string msg)
            : base(msg)
        {

        }
    }
}