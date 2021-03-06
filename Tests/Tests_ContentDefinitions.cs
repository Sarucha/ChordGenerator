using ChordGenerator;
using ChordGenerator.Model;
using NUnit.Framework;
using System;

namespace Tests
{
    public class Tests_ContentDefinitions
    {
        /// <summary>
        /// Test names only
        /// </summary>
        /// <param name="a"></param>
        [TestCase("C#3")]
        [TestCase("C3")]
        [TestCase("Cb3")]
        [TestCase("C0")]
        [TestCase("C9")]
        [TestCase("Cb0")]
        public void MusicalNote_CheckNoteNameValid(string a)
        {
            Assert.AreEqual(true, Note.IsValidName(a));
        }

        /// <summary>
        /// Test names only
        /// </summary>
        /// <param name="a"></param>
        [TestCase("C3b")]
        [TestCase("CC")]
        [TestCase("C")]
        [TestCase("CCCC")]
        [TestCase("K3")]
        [TestCase("3C")]
        public void MusicalNote_CheckNoteNameUnvalid(string a)
        {
            MusicalNote? obj = null;

            try
            {
                obj = new MusicalNote(a, 440, 0);
            }
            catch (ArgumentException e)
            { }

            Assert.AreEqual(null, obj);
        }

        [TestCase(16)]
        [TestCase(20000)]
        [TestCase(616)]
        public void MusicalNote_CheckFrequencyValid(float a)
        {
            Assert.AreEqual(a, new MusicalNote("A4", a, 0).Frequency);
        }

        [TestCase(-16)]
        [TestCase(20002)]
        public void MusicalNote_CheckFrequencyUnvalid(float a)
        {
            MusicalNote? obj = null;

            try
            {
                obj = new MusicalNote("A4", a, 0);
            }
            catch (ArgumentException e)
            { }

            Assert.AreEqual(null, obj);
        }

        [TestCase("A4:440")]
        public void MusicalNote_FromStringValid(string a)
        {
            MusicalNote? obj = null;

            try
            {
                obj = new MusicalNote(a, 0);
            }
            catch (ArgumentException e)
            { }

            Assert.AreEqual("A4: 440", obj.ToString());
        }

        [TestCase("aa4 440")]
        [TestCase("aa4440")]
        public void MusicalNote_FromStringUnvalid(string a)
        {
            MusicalNote? obj = null;

            try
            {
                obj = new MusicalNote(a, 0);
            }
            catch (ArgumentException e)
            { }

            Assert.AreEqual(null, obj);
        }
    }
}