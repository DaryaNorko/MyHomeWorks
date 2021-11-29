using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW._11.Task1
{
    public interface ISongService
    {
        public GenreFlags GenreFlags { get; }
        void GetSongsByGenre(List<Song> songs, string userGenre);
    }
}
