using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSharpUtility.Algorithms
{
    class Algorithms
    {
        /// <summary>
        /// Medium 30 Mins
        /// arr = [1, 2, 3, 7, 5]
        /// sum = 12 result = [2, 4]
        /// Sliding Window Problem
        /// https://www.youtube.com/watch?v=XFPHg5KjHoo
        /// </summary>
        public int[] findLongestSubarrayBySum(int[] src, int sum)
        {
            int[] result = { -1, -1 };
            int leftIdx = 0;
            int rightIdx = 0;
            int currentSum = 0;
            int maxDiff = 0;
            while (rightIdx <= src.Length)
            {
                if (currentSum == sum && rightIdx - leftIdx > maxDiff)
                {
                    // If current sum is equal to desired sum and gap between head and tail indexes are maximum
                    // then set result array with values of those indexes
                    result[0] = src[leftIdx];
                    result[1] = src[rightIdx];
                    maxDiff = rightIdx - leftIdx;
                }
                else if (currentSum > sum)
                {
                    // Else if: current sum is greater than desired sum
                    // then subtract value of left index from current sum and move index to the right
                    // Like sliding a window
                    currentSum -= src[leftIdx];
                    ++leftIdx;
                }
                else
                {
                    if (rightIdx < src.Length)
                    {
                        // Since outer while loop works for right index which is eqaul to length of source
                        // Hence, we should avoid indexing with that
                        // Defensive
                        currentSum += src[rightIdx];
                    }
                    
                    // increase right index unitl we reach or pass desired sum
                    ++rightIdx;
                }
            }
            return result;
        }

        /// <summary>
        /// To determine whether given string source is palindrome or not
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public bool isPalindrome(string src = "abcba")
        {
            bool result = true;
            int head = 0;
            int tail = src.Length - 1;
            while (head < tail)
            {
                if (src[head] != src[tail])
                {
                    // not palindrome
                    result = false;
                    break;
                }

                ++head;
                --tail;
            }

            return result;
        }
    }
}
