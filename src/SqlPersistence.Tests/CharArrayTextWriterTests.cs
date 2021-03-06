﻿using System.Linq;
using NUnit.Framework;

public class CharArrayTextWriterTests
{
    [Test]
    public void WritingSingleChar()
    {
        var writer = new CharArrayTextWriter();

        writer.Write('z');

        var written = writer.ToCharSegment();

        CollectionAssert.AreEqual('z'.ToString(), written);
    }

    [Test]
    public void WritingCharArray()
    {
        var writer = new CharArrayTextWriter();

        var chars = new[] { 'a', 'b', 'c', 'd', 'e', 'f' };

        const int offset = 5;
        const int take = 1;

        writer.Write(chars, offset, take);
        var written = writer.ToCharSegment();

        CollectionAssert.AreEqual(chars.Skip(offset).Take(take), written);
    }

    [Test]
    public void WritingString()
    {
        var writer = new CharArrayTextWriter();

        writer.Write("test");

        var written = writer.ToCharSegment();

        CollectionAssert.AreEqual("test", written);
    }

    [Test]
    public void WritingCharsBeyondLimit()
    {
        var writer = new CharArrayTextWriter();

        var s = new string('a', CharArrayTextWriter.InitialSize) + "b";

        writer.Write(s);

        var written = writer.ToCharSegment();

        CollectionAssert.AreEqual(s, written);
    }

    [Test]
    public void WritingCharsMuchBeyondLimit()
    {
        var writer = new CharArrayTextWriter();

        var s = new string('a', CharArrayTextWriter.InitialSize * 8);

        writer.Write(s);

        var written = writer.ToCharSegment();

        CollectionAssert.AreEqual(s, written);
    }

    [Test]
    public void HasOffsetResetWhenReleased()
    {
        var writer = CharArrayTextWriter.Lease();
        writer.Write('a');
        writer.Release();

        Assert.AreEqual(0, writer.Size);
    }
}