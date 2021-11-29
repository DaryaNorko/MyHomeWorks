using System;

namespace HW._11.Task1
{
    public enum Genre
    {
        Classic = 0,
        Rock = 1,
        Jazz = 2,
        Folk = 3,
        Pop = 4
    }

    [Flags]
    public enum GenreFlags
    {
        None = 0,
        Classic = 1,
        Rock = 2,
        Jazz = 4,
        Folk = 8,
        Pop = 16
    }
}
