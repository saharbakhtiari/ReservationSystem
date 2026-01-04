using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Extensions
{
    public static partial class StringExtension
    {
        private static readonly HashSet<string> _prepositions = new()
        {
            "از",
            "به",
            "با",
            "بی",
            "در",
            "بر",
            "برای",
            "تا",
            "مگر",
            "جز",
            "الا",
            "الّا",
            "چون",
            "اندر",
            "زی",
            "و",
            "له",
            "که",
            "اما",
            "نیز",
            "هم",
            "یا",
            "ان",
            "نه",
            "همه",
            "پس",
            "بین",
            "غیره",
        };
        public static List<string> GetSimilarWords(this string word)
        {
            var chars = word.ToCharArray();
            HashSet<string> similarWords = new()
            {
                word
            };
            for (int i = 0; i < chars.Length; i++)
            {
                var similarChars = Character.GetSimilarChars(chars[i]);
                if (similarChars != null && similarChars.Count > 1)
                {
                    CreateSimilarWords(similarWords, similarChars, i);
                }
            }
            List<string> result = new();
            foreach (var item in similarWords)
            {
                result.Add(new string(item));
            }
            return result;
        }

        public static List<string> GetSimilarWords(this List<string> words)
        {
            List<string> similars = new List<string>();
            foreach (var word in words)
            {
                word.GetSimilarWords().ForEach(a =>
                {
                    similars.Add(a);
                });
            }
            return similars;
        }
        private static void CreateSimilarWords(HashSet<string> words, List<char> chars, int index)
        {
            HashSet<string> similarWords = new(words);
            foreach (var word in words)
            {
                foreach (var letter in chars)
                {
                    char[] newWord = new char[word.Length];
                    word.ToCharArray().CopyTo(newWord, 0);
                    newWord[index] = letter;
                    similarWords.Add(new string(newWord));
                }
            }
            similarWords.ToList().ForEach(a => { words.Add(a); });
        }

        public static bool NormalContains(this string source, List<string> contents, bool isExactWord)
        {
            if (source.IsNullOrEmpty())
                return false;

            foreach (var item in contents)
            {
                var normalizedItem = item.PersianNormalize();
                var pattern = isExactWord ? @"\b" + normalizedItem + @"\b" : normalizedItem;
                Regex rgx = new Regex(pattern);
                Match match = rgx.Match(source.PersianNormalize());
                if (match.Success) return true;
                // if (item.IsNullOrWhiteSpace().Not() && source.Contains(pattern)) return true;
            }
            return false;
        }

        public static bool AllWordsContains(this string source, List<string> contents, bool isExactWord)
        {
            if (source.IsNullOrEmpty())
                return false;

            var result = true;
            foreach (var item in contents)
            {
                var normalizedItem = item.PersianNormalize();
                var pattern = isExactWord ? @"\b" + normalizedItem + @"\b" : normalizedItem;
                Regex rgx = new Regex(pattern);
                Match match = rgx.Match(source.PersianNormalize());
                if (!match.Success)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public static bool ExceptWordsContains(this string source, List<string> contents, bool isExactWord)
        {
            if (source.IsNullOrEmpty())
                return false;

            var result = true;
            foreach (var item in contents)
            {
                var normalizedItem = item.PersianNormalize();
                var pattern = isExactWord ? @"\b" + normalizedItem + @"\b" : normalizedItem;
                Regex rgx = new Regex(pattern);
                Match match = rgx.Match(source.PersianNormalize());
                if (match.Success)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public static bool ExactContentContains(this string source, string content)
        {
            if (source.IsNullOrEmpty())
                return false;

            var normalizedItem = content.PersianNormalize();
            var pattern = @"\b" + normalizedItem + @"\b";
            Regex rgx = new Regex(pattern);
            Match match = rgx.Match(source.PersianNormalize());
            return match.Success;
        }

        public static string PersianNormalize(this string text)
        {

            var contentArray = text.ToCharArray();
            for (int i = 0; i < contentArray.Length; i++)
            {
                contentArray[i] = Character.
                    
                    
                    GetNormalizedChar(contentArray[i]);
            }
            return new string(contentArray);
        }

        public static List<string> RemoveStopWords(this string text)
        {
            if (text == null) return null;
            var normilized = text.PersianNormalize();
            var sepratedContent = text?.ToSeperateInput(" ");
            return sepratedContent.Except(_prepositions).ToList();
        }
        public static string GetStringBetweenCharacters(string input, char charFrom, char charTo)
        {
            int posFrom = input.IndexOf(charFrom);
            if (posFrom != -1) //if found char
            {
                int posTo = input.IndexOf(charTo, posFrom + 1);
                if (posTo != -1) //if found char
                {
                    return input.Substring(posFrom + 1, posTo - posFrom - 1);
                }
            }

            return string.Empty;
        }
    }

    public static class Character
    {
        private static readonly Dictionary<char, char> _normalizeCharacter = new Dictionary<char, char>()
        {
            ['ء'] = '\0',
            ['آ'] = 'ا',
            ['أ'] = 'ا',
            ['ؤ'] = 'و',
            ['إ'] = 'ا',
            ['ئ'] = 'ی',
            ['ا'] = 'ا',
            ['ب'] = 'ب',
            ['ت'] = 'ت',
            ['ث'] = 'ث',
            ['ج'] = 'ج',
            ['ح'] = 'ح',
            ['خ'] = 'خ',
            ['د'] = 'د',
            ['ذ'] = 'ذ',
            ['ر'] = 'ر',
            ['ز'] = 'ز',
            ['س'] = 'س',
            ['ش'] = 'ش',
            ['ص'] = 'ص',
            ['ض'] = 'ض',
            ['ط'] = 'ط',
            ['ظ'] = 'ظ',
            ['ع'] = 'ع',
            ['غ'] = 'غ',
            ['ف'] = 'ف',
            ['ق'] = 'ق',
            ['ل'] = 'ل',
            ['م'] = 'م',
            ['ن'] = 'ن',
            ['ه'] = 'ه',
            ['و'] = 'و',
            ['پ'] = 'پ',
            ['چ'] = 'چ',
            ['ژ'] = 'ژ',
            ['ک'] = 'ک',
            ['گ'] = 'گ',
            ['ھ'] = 'ه',
            ['ی'] = 'ی',
            ['ة'] = 'ه',
            ['ك'] = 'ک',
            ['ى'] = 'ی',
            ['ي'] = 'ی',
            ['ە'] = 'ه',
            ['۰'] = '0',
            ['۱'] = '1',
            ['۲'] = '2',
            ['۳'] = '3',
            ['۴'] = '4',
            ['۵'] = '5',
            ['۶'] = '6',
            ['۷'] = '7',
            ['۸'] = '8',
            ['۹'] = '9',
            ['0'] = '0',
            ['1'] = '1',
            ['2'] = '2',
            ['3'] = '3',
            ['4'] = '4',
            ['5'] = '5',
            ['6'] = '6',
            ['7'] = '7',
            ['8'] = '8',
            ['9'] = '9',
        };

        public static List<char> GetSimilarChars(char input)
        {
            if (_normalizeCharacter.ContainsKey(input))
            {
                var value = _normalizeCharacter.GetValueOrDefault(input);
                var similars = _normalizeCharacter.Where(a => a.Value == value).ToList();
                HashSet<char> result = new HashSet<char>();
                similars.ForEach(a => { result.Add(a.Key); });
                return result.ToList();
            }
            return null;
        }

        public static char GetNormalizedChar(char character)
        {
            return _normalizeCharacter.ContainsKey(character) ? _normalizeCharacter[character] : character;
        }

        public static string CorrectText(this string text)
        {
            if (text.IsNullOrEmpty())
            {
                return text;
            }
            string retText = text;
            retText = retText.Replace('ي', 'ی');
            retText = retText.Replace("ك", "ک");
            retText = retText.Replace("ۀ", "ه");
            retText = retText.Replace("ة", "ه");
            retText = retText.Replace("هیئت", "هیأت");
            return retText;
        }
        public static string StripHTML(this string str)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }
            return Regex.Replace(str, @"<(.|\n)*?>|(&nbsp;)*", string.Empty);
        }
    }
}
