using System;

namespace HW._09.Task2
{
    class Program
    {
        static void Main(string[] args)
        {           
            var films = CreateFilmsArray();

            Console.WriteLine("Films:");
            foreach  (Film film in films)
            {
                film.Play();
            }
            Console.WriteLine();

            var songs = CreateSongsArray();

            Console.WriteLine("Songs:");
            foreach (Song song in songs)
            {
                song.Play();
            }
            Console.WriteLine();

            var computerPrograms = CreateComputerProgramsArray();

            Console.WriteLine("Computer programs:");
            foreach (ComputerProgram computerProgram in computerPrograms)
            {
                computerProgram.Print();
            }
        }

        public static Film [] CreateFilmsArray()
        {
            Film[] films = new Film[3];
            films[0] = new Film(1, "Forrest Gump", "drama", "Robert Lee Zemeckis", "Thomas Jeffrey Hanks", "Robin Gayle Wright");
            films[1] = new Film(2, "Julie & Julia", "drama, comedy", "Nora Ephron", "Stanley Tucci", "Meryl Streep");
            films[2] = new Film(3, "The Shawshank Redemption", "drama", "Frank Darabont", "Tim Robbins");
            
            return films;
        }

        public static Song [] CreateSongsArray()
        {
            Song[] songs = new Song[2];
            songs[0] = new Song(1, "Don't You Worry 'Bout A Thing", "soundtracks", "Tori Kelly", 4 * 60 + 1);
            songs[1] = new Song(2, "Zenit", "electro-folk", "ONUKA", 4 * 60 + 31);

            return songs;
        } 

        public static ComputerProgram [] CreateComputerProgramsArray()
        {
            ComputerProgram[] computerPrograms = new ComputerProgram[2];
            computerPrograms[0] = new ComputerProgram( 1, "Slack", "corporate messenger", 93.1);
            computerPrograms[1] = new ComputerProgram( 2, "Lightshot", "screeshot", 2.65);

            return computerPrograms;
        }
    }
}
