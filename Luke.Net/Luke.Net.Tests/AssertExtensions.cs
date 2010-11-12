using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Luke.Net.Tests
{
    public static class AssertExtensions
    {
        public static void AssertContains<T>(this IEnumerable<T> col, IEnumerable<T> expected)
        {
            CollectionContains(expected, col);
        }

        public static void CollectionContains<T>(IEnumerable<T> expected, IEnumerable<T> col)
        {
            foreach (var item in col)
            {
                if (!expected.Any(x => item.Equals(x)))
                    Assert.Fail(string.Format("{0} does not exist in the expected collection", item));
            }
        }
    }
}
