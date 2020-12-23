using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        [Test]
        public void Test()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void Should_Throw_Exception_If_Search_Options_Are_Null()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            SearchOptions searchOptions = null;

            Assert.Throws<ArgumentNullException>(() => searchEngine.Search(searchOptions));
        }

        [Test]
        public void Should_Throw_Exception_If_DataSet_Are_Null()
        {
            List<Shirt> shirts = null;

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            Assert.Throws<InvalidOperationException>(() => searchEngine.Search(searchOptions), "Cannot search as the DataSet is null");

        }

        [Test]
        public void Verify_Shirt_Properties()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Test Shirt", Size.Small, Color.Red),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);

            Assert.That(results.Shirts.Count, Is.EqualTo(1));

            Shirt searchedShirt = results.Shirts.First();

            Assert.That(searchedShirt.Id, Is.EqualTo(shirts.First().Id));
            Assert.That(searchedShirt.Name, Is.EqualTo(shirts.First().Name));

        }

    }
}
