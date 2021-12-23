using System;
using System.Collections.Generic;
using System.Linq;

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

           string longestWord = stringArray.OrderByDescending(word => word.Length).First();
           stringArray = stringArray.Where(word => word.Length < longestWord.Length).ToArray();
           
           stringWords = String.Join(' ', stringArray);

           return stringWords;
        }
        static string ReplaceWords(string stringWords)
        {
            string[] stringArray = stringWords.Split(' ');

            string longestWord = stringArray.OrderByDescending(word => word.Length).First();
            string shortestWord = stringArray.OrderBy(word => word.Length).First();

            if (longestWord.Length == shortestWord.Length)
                return "All words in a given string are the same length";
            else
            {
                stringArray = stringArray.Where(word => word.Length < longestWord.Length || word.Equals(longestWord)).ToArray();
                stringArray = stringArray.Where(word => word.Length > shortestWord.Length || word.Equals(shortestWord)).ToArray();

                stringWords = String.Join(' ', stringArray);

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

            stringArray = stringArray.OrderByDescending(word => word.Length).ToArray();

            stringWords = String.Join(' ', stringArray);

            return stringWords;
        }
    }
}
