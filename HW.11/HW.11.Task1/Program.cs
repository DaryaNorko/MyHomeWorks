using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace HW._11.Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Song> songs = CreateSongsCollection();

            Console.WriteLine("Please, input the title of the song.");
            Song userSong = new Song(Console.ReadLine());

            Console.WriteLine("Please, input the duration of the song in minutes.");
            userSong.Minutes = double.Parse(Console.ReadLine());

            Console.WriteLine("Please, input the year the song was released.");
            userSong.AlbumYear = int.Parse(Console.ReadLine());

            Console.WriteLine("Select the genre of the song from the list.");

            for (int i = 0; i <= 4; i++)
            {
                Console.WriteLine("{0} - {1}", i, (Genre)i);
            }

            string inputUserGenre = Console.ReadLine();

            FindUserGenre(userSong, inputUserGenre);
            songs.Add(userSong);

            var song = GetSongData(userSong);

            string json = JsonConvert.SerializeObject(song);
            Console.WriteLine(json);

            GetJsonMicrosoft(userSong);

            Console.WriteLine("If you want to search by the genre of the song, please input genre");
            string userGenre = Console.ReadLine();

            userSong.GetSongsByGenre(songs, userGenre);
        }
               
        public static List<Song> CreateSongsCollection()
        {
            List<Song> songs = new()
            {
                new Song("Hypnotic", 4.42, "Zella Day", 2008, GenreFlags.Rock | GenreFlags.Pop),
                new Song("Coming Back To Life", 3.01, "Andrea Perry", 2012, GenreFlags.Pop),
                new Song("Zenit", 4.47, "ONUKA", 2015, GenreFlags.Folk),
                new Song("Ты запой мне ту песню", 3.55, "Г. Свиридов", 1955, GenreFlags.Classic),
                new Song("В танце", 3.12, "IOWA", 2018, GenreFlags.Pop | GenreFlags.Rock | GenreFlags.Jazz)
            };
            return songs;
        }
        public static void GetJsonMicrosoft(Song userSong)
        {
            var song = GetSongData(userSong);

            string json = JsonSerializer.Serialize(song);

            Console.WriteLine(json);
        }
        public static void FindUserGenre(Song userSong, string inputUserGenre)
        {
            Genre genre = (Genre)Enum.Parse(typeof(Genre), inputUserGenre, true);
         
            switch (genre)
            {
                case Genre.Classic:
                    userSong.Genre = Genre.Classic;
                    break;
                case Genre.Rock:
                    userSong.Genre = Genre.Rock;
                    break;
                case Genre.Jazz:
                    userSong.Genre = Genre.Jazz;
                    break;
                case Genre.Folk:
                    userSong.Genre = Genre.Folk;
                    break;
                case Genre.Pop:
                    userSong.Genre = Genre.Pop;
                    break;
                default:
                    userSong.Genre = default;
                    break;
            }
        }
        public static object GetSongData(Song userSong)
        {
            var song = new
            {
                Title = userSong.Title,
                Minutes = userSong.Minutes,
                AlbumYear = userSong.AlbumYear,
                Genre = userSong.Genre.ToString()
            };

            Console.WriteLine($"Title - {song.Title}, Minutes - {song.Minutes}, AlbumYear - {song.AlbumYear}, Genre - {song.Genre}.");

            return song;
        }
    }
}
