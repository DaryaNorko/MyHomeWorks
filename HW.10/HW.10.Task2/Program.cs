using HW._10.Task2.Models;
using System;
using System.Collections.Generic;

namespace HW._10.Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Item> items = new();

            items.AddRange(CreateFilmsCollection());
            items.AddRange(CreateSongsCollection());
            items.AddRange(CreateComputerProgramsCollection());

            foreach (var item in items)
            {
                item.Play();
            }
        }
        public static List<Item> CreateFilmsCollection()
        {
            Film film1 = new(1, "Forrest Gump", "drama", 5.7);
            film1.AddSpecificInform("Robert Lee Zemeckis", "Thomas Jeffrey Hanks", "Robin Gayle Wright");

            Film film2 = new(2, "Julie & Julia", "drama, comedy", 7.6);
            film2.AddSpecificInform("Nora Ephron", "Stanley Tucci", "Meryl Streep");

            Film film3 = new(3, "The Shawshank Redemption", "drama", 2.5);
            film3.AddSpecificInform("Frank Darabont", "Tim Robbins");

            List <Item> films = new List<Item>() { film1, film2, film3 };

            return films;
        }
        public static List<Item> CreateSongsCollection()
        {
            Song song1 = new(1, "Don't You Worry 'Bout A Thing", "soundtracks", 2.7);
            song1.AddSpecificInform("Tori Kelly", 4 * 60 + 1);

            Song song2 = new(2, "Zenit", "electro-folk", 3.0);
            song2.AddSpecificInform("ONUKA", 4 * 60 + 31);

            List <Item> songs = new() { song1, song2 };

            return songs;
        }
        public static List<Item> CreateComputerProgramsCollection()
        {
            ComputerProgram computerProgram1 = new(1, "Slack", "corporate messenger", 93.1);
            ComputerProgram computerProgram2 = new(2, "Lightshot", "screeshot", 2.65);

            List<Item> computerPrograms = new() { computerProgram1, computerProgram2 };

            return computerPrograms;
        }
    }
}
