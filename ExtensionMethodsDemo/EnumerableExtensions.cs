using System.Text.RegularExpressions;

namespace ExtensionMethodsDemo
{
    public class EnumerableExtensionsTests
    {
        [TestCase("ASDF-")]
        [TestCase(@".*-")]
        public void GroupsSerialsWithCustomPrefix(string prefix)
        {
            var exampleSerial = @"ASDF-";

            var serials1 = Enumerable.Range(0, 100)
                .Select(x => $"{exampleSerial}{x}");

            var serials2 = Enumerable.Range(200, 50)
                .Select(x => $"{exampleSerial}{x}");

            var serials3 = Enumerable.Range(333, 5)
                .Select(x => $"{exampleSerial}{x}");

            var allSerials = serials2.Union(serials1)
                .Union(serials3)
                .ToList();

            var grouped = GroupSerials(allSerials, prefix,
                group => $"{group.First()}-{group.Last()}",
                serial => $"{serial}-{serial}"
            )
            .ToArray();

            Assert.That(grouped.Count, Is.EqualTo(3)); // should be 3 groups
            Assert.That(grouped[0], Is.EqualTo("ASDF-0-ASDF-99"));

            Assert.That(grouped[1], Is.EqualTo("ASDF-200-ASDF-249"));

            Assert.That(grouped[2], Is.EqualTo("ASDF-333-ASDF-337"));
        }

        private IEnumerable<T> GroupSerials<T>(List<string> serials, string prefix,
          Func<List<string>, T> groupConverter,
          Func<string, T> basicConverter)
        {
            if (string.IsNullOrWhiteSpace(prefix))
            {
                return serials.OrderBy(x => x).Select(x => basicConverter(x));
            }

            try
            {
                // order numerically, whether the prefix is concrete or regex this will work
                return serials.OrderBy(x => int.Parse(Regex.Replace(x, prefix, "")))
                    .GroupBy((list, item) => _existingGroup(list, item, prefix))
                    .Select(x => groupConverter(x));
            }
            catch
            {
                return serials.OrderBy(x => x).Select(x => basicConverter(x));
            }
        }

        private List<List<string>> _existingGroup(List<List<string>> grouper, string serial, string prefix)
        {
            var currentGroup = grouper.Last();
            var currentSerial = Regex.Replace(currentGroup.Last(), prefix, "");
            var newSerial = Regex.Replace(serial, prefix, "");

            if (int.TryParse(currentSerial, out int a) && int.TryParse(newSerial, out int b))
            {
                if (b - a == 1) // is this the next increment?
                {
                    currentGroup.Add(serial); // add to current group
                }
                else
                {
                    grouper.Add(new() // add as new group
                    {
                        serial
                    });
                }
            }

            return grouper;
        }
    }

    public static class EnumerableExtensions
    {
        public static List<List<T>> GroupBy<T>(this IEnumerable<T> items, Func<List<List<T>>, T, List<List<T>>> grouping)
        {
            return items.Aggregate(new List<List<T>>(), (grouper, item) =>
            {
                switch (grouper.Any())
                {
                    case true:
                        grouping(grouper, item);
                        break;

                    case false:
                        grouper.Add(new List<T> { item });
                        break;
                };

                return grouper;
            });
        }
    }
}
