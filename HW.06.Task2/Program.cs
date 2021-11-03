using System;

namespace HW._06.Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            string stringWords = "333 4444 666ds6 55555 4aa4 666sd6 2!";

            Console.WriteLine(findAndRemoveLongestWord(stringWords));
            Console.WriteLine();
            Console.WriteLine(ReplaceWords(stringWords));
            Console.WriteLine();
            Console.WriteLine(LettersCount(stringWords));
            Console.WriteLine();
            Console.WriteLine(SortArray(stringWords));
        }

        static string findAndRemoveLongestWord(string stringWords)
        {
            string[] stringArray = stringWords.Split(' ');
            string longestWord = string.Empty;

            for (int i = 0; i < stringArray.Length; i++)
            {
                string checkString = stringArray[i];
                if (checkString.Length > longestWord.Length)
                    longestWord = stringArray[i];
            }
            for (int i = 0; i < stringArray.Length; i++)
            {
                string checkString = stringArray[i];
                if (checkString.Length == longestWord.Length && checkString != longestWord)
                    stringArray[i] = string.Empty;
            }

            stringWords = String.Join(' ', stringArray);

            int indexOfLongestWord = stringWords.IndexOf(longestWord);
            stringWords = stringWords.Remove(indexOfLongestWord, longestWord.Length);

            return stringWords;
        }

        static string ReplaceWords(string stringWords)
        {
            string[] stringArray = stringWords.Split(' ');
            string longestWord = string.Empty;

            for (int i = 0; i < stringArray.Length; i++)
            {
                string checkString = stringArray[i];
                if (checkString.Length > longestWord.Length)
                    longestWord = stringArray[i];
            }

            string shortestWord = string.Empty;
            for (int i = 0; i < stringArray.Length; i++)
            {
                string checkString = stringArray[i];
                if (checkString.Length < longestWord.Length)
                    shortestWord = stringArray[i];
            }

            for (int i = 0; i < stringArray.Length; i++)
            {
                string checkString = stringArray[i];
                if (checkString.Length == longestWord.Length && checkString != longestWord)
                    stringArray[i] = string.Empty;
                else if (checkString.Length == shortestWord.Length && checkString != shortestWord)
                    stringArray[i] = string.Empty;
            }
            stringWords = String.Join(' ', stringArray);

            if (longestWord == shortestWord)
            {
                return "All words in a given string are the same length";
            }
            else
            {
                int indexOfLongestWord = stringWords.IndexOf(longestWord);
                int indexOfShortestWord = stringWords.IndexOf(shortestWord);

                if (indexOfLongestWord > indexOfShortestWord)
                {
                    stringWords = stringWords.Replace(longestWord, shortestWord);
                    stringWords = stringWords.Remove(indexOfShortestWord, shortestWord.Length);
                    stringWords = stringWords.Insert(indexOfShortestWord, longestWord);
                }
                else if (indexOfLongestWord < indexOfShortestWord)
                {

                    stringWords = stringWords.Replace(shortestWord, longestWord);
                    stringWords = stringWords.Remove(indexOfLongestWord, longestWord.Length);
                    stringWords = stringWords.Insert(indexOfLongestWord, shortestWord);
                }
            }
            return stringWords;
        }

        static string LettersCount(string stringWords)
        {
            char[] allStringChars = stringWords.ToCharArray();
            int letters = 0;
            int punctuation = 0;
            for (int i = 0; i < allStringChars.Length; i++)
            {
                char checkCh = allStringChars[i];
                if (char.IsLetter(checkCh))
                    letters++;
                else if (char.IsPunctuation(checkCh))
                    punctuation++;
            }
            return $" The number of letters in the text = {letters}. \n The number of punctuation marks in the text = {punctuation}";
        }

        static string SortArray(string stringWords)
        {
            string[] stringArray = stringWords.Split(' ');

            for (int i = 0; i < stringArray.Length; i++)
            {
                for (int y = 0; y < stringArray.Length-1; y++)
                {
                    string str1 = stringArray[y];
                    string str2 = stringArray[y+1];

                    if (str1.Length < str2.Length)
                    {
                        string str3 = stringArray[y];
                        stringArray[y] = stringArray[y+1];
                        stringArray[y+1] = str3;
                    }
                }
                stringWords = String.Join(' ', stringArray);
            }
            return stringWords;
        }
    }
}
