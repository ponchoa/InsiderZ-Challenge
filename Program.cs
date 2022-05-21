using System;
using System.Collections.Generic;
using System.Numerics;

namespace TechTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Exercice 1: {Test1(26)}");
            int[] array = new int[] {1, 1, 2, 3, 5, 6, 12, 13, 15, 15, 16, 18, 20, 27, 29 };
            Console.WriteLine($"Exercice 2: {Test2(array, 8)}");
            Console.ReadLine();
        }

        #region Exercice 1
        /// <summary>
        /// Returns the number of possible games if you earn 1 or 2 points per round
        /// and you have to achieve an exact number of points.
        /// </summary>
        /// <param name="pointsToAchieve">The exact number of points to earn before the game is over.</param>
        /// <returns>An int that represents the number of possible games.</returns>
        static BigInteger Test1(BigInteger pointsToAchieve)
        {
            // The simplest way to find the number of possible games was to take each possible
            // combination of game with a result of exactly pointsToAchieve, (where order doesn't matter)
            // and for each of these, find the number of permutations (possible order)
            // then add those numbers to get the result.

            // Or, put in another way, we start with the combination where all digits are 1s
            // then we iteratively replace two 1s with one 2, until there are one or no 1.
            // and each time we find the number of permutations using the binomial coefficient formula:
            // n! / k!(n - k)! where n ≥ k ≥ 0.

            BigInteger numberOfDigits = pointsToAchieve;
            BigInteger result = 0;

            for (BigInteger numberOfOnes = numberOfDigits; numberOfOnes >= 0; numberOfOnes -= 2)
            {
                result += BinomialCoefficient(numberOfDigits, numberOfOnes);
                numberOfDigits--;
            }

            return result;
        }

        /// <summary>
        /// Finds the binomial coefficient nCk with n ≥ k ≥ 0.
        /// </summary>
        /// <param name="n">The number of items.</param>
        /// <param name="k">The size of the group.</param>
        /// <returns>The binomial coefficient nCk.</returns>
        static BigInteger BinomialCoefficient(BigInteger n, BigInteger k)
        {
            if (n < k)
                return 0;
            if (k > n - k)
                k = n - k;
            BigInteger res = 1;
            for (int i = 1; i <= k; i++)
            {
                res *= n;
                res /= i;
                n--;
            }
            return res;
        }
        #endregion

        #region Exercice 2
        /// <summary>
        /// Finds whether or not it is possible to get a number by adding two distinct values of an array.
        /// </summary>
        /// <param name="array">The array of int containing the values to check.</param>
        /// <param name="n">The sum to get.</param>
        /// <returns>A boolean that is equal to true if the sum is possible, false otherwise.</returns>
        static bool Test2(int[] array, int n)
        {
            // This method is assuming that the array is not ordered, so that it is scalable.
            // We use a hashset, which doesn't store duplicate values, and is very efficient
            // while not using up much space, and because it doesn't need to preserve order.
            // We traverse the array and for each value, we check if there was a previous
            // value that when added to it gives us the correct sum.
            // We also need to check if the two values are distincts.

            HashSet<int> checkedValues = new HashSet<int>();
            for (int i = 0; i < array.Length; i++)
            {
                if (!checkedValues.Contains(array[i]) && checkedValues.Contains(n - array[i]))
                {
                    return true;
                }
                checkedValues.Add(array[i]);
            }

            return false;
        }
        #endregion
    }
}
