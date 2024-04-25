using SpellChecking.Algorithms;

namespace SpellChecking
{
    public class SpellChecker
    {
        public SpellChecker(HashSet<string> words)
        {
            dictionaryWords = words;
        }

        public HashSet<string> dictionaryWords = new HashSet<string>();

        public Dictionary<string, double> WeightedCandidatesByJaro(string input, int maxWeights = 5)
        {
            var result = new Dictionary<string, double>();

            foreach (var wordInDictionary in dictionaryWords)
            {
                result.Add(wordInDictionary, JaroDistance.Jaro(wordInDictionary, input));
            }

            return result
                .OrderByDescending(weight => weight.Value)
                .Take(maxWeights)
                .ToDictionary();
        }

        public Dictionary<string, double> WeightedCandidatesByLevenshtein(string input, int maxWeights = 5)
        {
            var result = new Dictionary<string, double>();

            foreach (var wordInDictionary in dictionaryWords)
            {
                result.Add(wordInDictionary, LevenshteinDistance.Levenshtein(wordInDictionary, input));
            }

            return result
                .OrderBy(weight => weight.Value) //ascending since lower is best
                .Take(maxWeights)
                .ToDictionary();
        }

        public Dictionary<string, double> WeightedCandidatesByDamerauLevenshtein(string input, int maxWeights = 5)
        {
            var result = new Dictionary<string, double>();

            foreach (var wordInDictionary in dictionaryWords)
            {
                result.Add(wordInDictionary, DamerauLevenshteinDistance.DamerauLevenshtein(wordInDictionary, input));
            }

            return result
                .OrderBy(weight => weight.Value) //ascending since lower is best
                .Take(maxWeights)
                .ToDictionary();
        }
    }
}
