namespace SpellChecking.Algorithms;

public class DamerauLevenshteinDistance
{
    public static int DamerauLevenshtein(string valid, string candidate)
    {
        int validLen = valid.Length;
        int candidateLen = candidate.Length;
        int[,] sizeMatrix = new int[validLen + 1, candidateLen + 1];

        if (validLen == 0) return candidateLen;
        if (candidateLen == 0) return validLen;

        // Initialize the top and left sides of the matrix with incremental distances.
        for (int i = 0; i <= validLen; sizeMatrix[i, 0] = i++) { }
        for (int j = 0; j <= candidateLen; sizeMatrix[0, j] = j++) { }

        for (int i = 1; i <= validLen; i++)
        {
            for (int j = 1; j <= candidateLen; j++)
            {
                // Cost of substitution
                int cost = (candidate[j - 1] == valid[i - 1]) ? 0 : 1;

                // Calculate minimum of delete, insert, substitute
                sizeMatrix[i, j] = Math.Min(Math.Min(sizeMatrix[i - 1, j] + 1, sizeMatrix[i, j - 1] + 1), sizeMatrix[i - 1, j - 1] + cost);

                // Check if transpositions can be applied
                if (i > 1 && j > 1 && valid[i - 1] == candidate[j - 2] && valid[i - 2] == candidate[j - 1])
                {
                    sizeMatrix[i, j] = Math.Min(sizeMatrix[i, j], sizeMatrix[i - 2, j - 2] + cost);
                }
            }
        }

        return sizeMatrix[validLen, candidateLen];
    }
}
