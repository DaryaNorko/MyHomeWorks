using System;
using System.Collections.Generic;

namespace HW._11.Task1
{
    public class Song : ISongService
    {
        public string Title { get; set; }
        public double Minutes { get; set; }
        public string Composer { get; set; }
        public int AlbumYear { get; set; }
        public Genre Genre { get; set; }
        public GenreFlags GenreFlags { get; }

        public Song(string title)
        {
            Title = title;
        }
        public Song(string title, double minutes, string composer, int albumYear, GenreFlags genre)
        {
            Title = title;
            Minutes = minutes;
            Composer = composer;
            AlbumYear = albumYear;
            GenreFlags = genre;
        }
        public override string ToString()
        {
            return $"Title - {Title}, Minutes - {Minutes}, AlbumYear - {AlbumYear}, Genre - {GenreFlags.ToString()}.";
        }

        public void GetSongsByGenre(List<Song> songs, string userGenre)
        {
            GenreFlags genre = (GenreFlags)Enum.Parse((typeof(GenreFlags)), userGenre, true);

            List<Song> userGenreSongs = songs.FindAll(song => song.GenreFlags == genre);

            if (userGenreSongs.Count == 0)
                Console.WriteLine("No results");
            else
            {
                foreach (Song item in userGenreSongs)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }
    }
}
