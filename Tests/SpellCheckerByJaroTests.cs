using SpellChecking;

namespace Tests
{
    public class SpellCheckerByJaroTests
    {
        [Fact]
        public void Top_Weighted_Candidates_Are_Returned_By_Jaro()
        {
            // Arrange
            HashSet<string> dictionaryWords =
                new HashSet<string> { "cykel", "sekel", "skel", "ankel", "bydel" };

            var inputWord = "sykel";

            var speller = new SpellChecker(dictionaryWords);

            // Act
            var result = speller.WeightedCandidatesByJaro(inputWord, 3);

            // Assert
            Assert.Equal(3, result.Count);

            double lastValue = 1;
            foreach (var score in result.Values)
            {
                //we expect the weights (result) are ordered lower to higher score
                //if you change values in dictionaryWords this might blow up
                Assert.True(score < lastValue);

                lastValue = score;
            }
        }

        [Fact]
        public void Top_Weighted_Candidates_Are_Returned_By_Jaro_Full_Word_List()
        {
            // Arrange
            HashSet<string> dictionaryWords =
                LoadDictionaryFromCsv();

            var inputWord = "sikel"; //"resturant";

            var speller = new SpellChecker(dictionaryWords);

            // Act
            var result = speller.WeightedCandidatesByJaro(inputWord, 5);

            // Assert
            Assert.Equal(5, result.Count);

            double lastValue = 1;
            foreach (var score in result.Values)
            {
                //we expect the weights (result) are ordered lower to higher score
                Assert.True(score <= lastValue);

                lastValue = score;
            }
        }


        private HashSet<string> LoadDictionaryFromCsv()
        {
            var fullWordListPath = Path.Combine(
                Environment.CurrentDirectory, "dictionary", "uniquewordlist.csv");

            HashSet<string> data = new();

            using var reader = new StreamReader(fullWordListPath);

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                data.Add(line.Trim());
            }

            return data;
        }
    }
}