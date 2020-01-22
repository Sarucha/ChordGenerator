﻿using System;
using System.Collections.Generic;

namespace ChordGenerator
{
    /// <summary>
    /// Class holding settings changes, like note dictionary or volume
    /// </summary>
    public class Settings
    {
        public float volume { get; private set; } = 0.5f;
        public PlayType HowToPlay { get; private set; } = PlayType.AllAtTheSameTime;
        public float TimeToPlaySingleNote { get; private set; } = 0.33f;
        public float TimeToPlayChord { get; private set; } = 2f;

        public List<MusicalNote> MusicalNotes;

        public enum Type
        {
            NoteTime,
            ChordTime,
            Volume,
            DefaultTypeOfPlay
        }

        public enum PlayType
        {
            AllAtTheSameTime,
            OneAfterAnother
        }

        public void Change(Type type, float value)
        {
            switch (type)
            {
                case Type.NoteTime:
                    this.TimeToPlaySingleNote = value;
                    break;

                case Type.ChordTime:
                    this.TimeToPlayChord = value;
                    break;

                case Type.Volume:
                    this.volume = value;
                    break;

                case Type.DefaultTypeOfPlay:
                    this.HowToPlay = (PlayType)(int)value;
                    break;
            }
        }

        /// <summary>
        /// Generates the MusicalNote Array from ContentDefinitions;
        /// Does it once and saves is to array. Uses the frequency for starting point
        /// </summary>
        private void GenerateMusicalNoteArray(float stratingFrequency)
        {
            if (MusicalNotes == null)
            {
                MusicalNotes = new List<MusicalNote>();
            }
            else MusicalNotes.Clear();

            float temp = stratingFrequency;
            int rank = 0;

            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j < noteNameContent.Length; j++)
                {
                    if (noteNameContent[j].Length == 1)
                    {
                        AddToMusicalNotes(
                            new MusicalNote($"{noteNameContent[j]}{i}", temp, rank++));
                        temp = temp * 1.05946f;
                    }
                    else
                    {
                        if (noteNameContent[j][1] == '#')
                        {
                            AddToMusicalNotes(
                                new MusicalNote($"{noteNameContent[j]}{i}", temp, rank));
                            AddToMusicalNotes(
                                new MusicalNote($"{noteNameContent[j + 1]}{i}", temp, rank++));
                            temp = temp * 1.05946f;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Generates the MusicalNote array from given note name and frequency.
        /// </summary>
        /// <throws>ArgumentException</throws>
        public MusicalNote[] GenerateMusicalNoteArray(string note, float frequency)
        {
            if (MusicalNotes == null)
            {
                GenerateMusicalNoteArray(440);
            }
            try
            {
                var s = MusicalNotes.Find(x => x.Name == note).Frequency;
                var b = MusicalNotes[0].Frequency;
                var d = s / b;
                frequency /= d;
                if (!MusicalNote.IsValidFrequency(frequency))
                    throw new ArgumentException();
                GenerateMusicalNoteArray(frequency);
            }
            catch (ArgumentException e)
            {
                MusicalNotes.Clear();

                throw new ArgumentException(e.Message);
            }
            finally { }

            return MusicalNotes.ToArray();
        }

        private void AddToMusicalNotes(MusicalNote note)
        {
            if (MusicalNotes == null)
            {
                MusicalNotes = new List<MusicalNote>();
            }
            MusicalNotes.Add(note);
        }

        /// <summary>
        /// Opens file stream and search for config file
        /// </summary>
        public Settings(string fileAdress)
        {

        }

        public Settings
            (
            float volume = 0.5f,
            PlayType defaultPlayType = PlayType.AllAtTheSameTime,
            float defaultTimeToPlaySingleNote = 0.33f,
            float defaultTimeToPlayChord = 2f
            )
        {
            this.volume = volume;
            this.HowToPlay = defaultPlayType;
            this.TimeToPlaySingleNote = defaultTimeToPlaySingleNote;
            this.TimeToPlayChord = defaultTimeToPlayChord;
            GenerateMusicalNoteArray(16.35f);
        }

        public Settings(Settings settings)
        {
            volume = settings.volume;
            HowToPlay = settings.HowToPlay;
            TimeToPlaySingleNote = settings.TimeToPlaySingleNote;
            TimeToPlayChord = settings.TimeToPlayChord;
        }

        public Settings()
        {
            GenerateMusicalNoteArray(16.35f);
        }
        
        private string[] noteNameContent =
        {
            "C", "C#", "Db", "D", "D#",
            "Eb", "E", "F", "F#", "Gb",
            "G", "G#", "Ab", "A", "A#",
            "Bb", "B"
        };
    }
}