using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CSharpUtility.Algorithms
{
    class Algorithms
    {
        /// <summary>
        /// Difficulty: Medium - 30 Mins
        /// arr = [1, 2, 3, 7, 5]
        /// sum = 12 result = [2, 4]
        /// Sliding Window Problem
        /// Alternative Solution Nick White: https://www.youtube.com/watch?v=XFPHg5KjHoo
        /// </summary>
        public int[] FindLongestSubarrayBySum(int[] src, int sum)
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
        public bool IsPalindrome(string src = "abcba")
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

        /// <summary>
        /// Two Sum Problem
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int[] TwoSum(int[] nums, int target)
        {
            int[] result = new int[2];
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int diff = 0;

            for (int i = 0; i < nums.Length; ++i)
            {
                diff = target - nums[i];
                int index = 0;
                if (dict.TryGetValue(diff, out index))
                {
                    result[0] = i;
                    result[1] = index;
                }

                // check to prevent exception cause by adding same key to dictionary
                if (!dict.ContainsKey(nums[i]))
                {
                    // Add number with its index
                    dict.Add(nums[i], i);
                }
            }

            return result;
        }

        /// <summary>
        /// Find First Unique Character in a Given String
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int FirstUniqueChar(string s)
        {
            // Length of English Alphabet is 26
            int[] lettersArr = new int[26];

            for (int i = 0; i < s.Length; ++i)
            {
                // index for each letters can be found by extracting implicit integer value of 
                // first charcter of 'a' according to ASCII table values.
                ++lettersArr[Convert.ToInt32(s[i] - 'a')];
            }

            for (int i = 0; i < s.Length; ++i)
            {
                if (lettersArr[Convert.ToInt32(s[i] - 'a')] == 1)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 58. Length of Last Word (Easy)
        /// </summary>
        /// <description>
        /// Given a string s consists of some words separated by spaces, return the length of the last word in the string. If the last word does not exist, return 0.
        /// A word is a maximal substring consisting of non-space characters only.
        /// Example 1:
        /// Input: s = "Hello World"
        /// Output: 5
        /// Example 2:
        /// Input: s = " "
        /// Output: 0
        /// Constraints:
        /// 1 <= s.length <= 104
        /// s consists of only English letters and spaces ' '.
        /// </description>
        /// <param name="s"></param>
        /// <returns></returns>
        public int LengthOfLastWord(string s)
        {
            // If input trim length is 0 then input consist of whitespace only. Return 0
            if(s.Trim().Length == 0)
                return 0;

            List<string> list = new List<string>();
            list.AddRange(s.Split(" ").ToList());
            list.RemoveAll(x => string.IsNullOrWhiteSpace(x));
            return list.Last().Length;
        }

        // TODO
        #region Graphs

        public void BFS(int src)
        {
            // O(V + E) time
            // O(V) space (for visited array)
            Queue<int> queue = new Queue<int>();
            bool[] visited = new bool[5];
            int[][] adjacent = new int[5][];

            visited[src] = true;
            queue.Enqueue(src);
            int current;
            while (queue.Any())
            {
                current = queue.Dequeue();
                Console.WriteLine(current);
                int[] currentAdjacents = adjacent[current];
                foreach (int adj in currentAdjacents)
                {
                    if (visited[adj] == false)
                    {
                        visited[adj] = true;
                        queue.Enqueue(adj);
                    }
                }
            }
        }

        public int V;
        public int[] adjacents;
        public void DFS()
        {
            // visited array for all vertices
            bool[] visited = new bool[V];

            for (int i = 0; i < V; i++)
            {
                if (visited[i] == false)
                {
                    DFSUtil(i, visited);
                }
            }

        }

        public void DFSUtil(int v, bool[] visited)
        {
            visited[v] = true;
            Console.WriteLine(v);

            foreach (var adj in adjacents)
            {
                if (visited[adj] == false)
                {
                    DFSUtil(adj, visited);
                }
            }
        }

        #endregion
    }

}
