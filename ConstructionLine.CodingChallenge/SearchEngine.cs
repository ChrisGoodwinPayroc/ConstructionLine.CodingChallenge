using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly HashSet<Shirt> _dataSet;

        public SearchEngine(List<Shirt> shirts)
        {
            if (shirts != null)
            {
                _dataSet = new HashSet<Shirt>(shirts);
            }
        }


        /// <summary>
        /// Shirt Search Engine
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SearchResults Search(SearchOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (_dataSet == null)
                throw new InvalidOperationException("Cannot search as the DataSet is null");

            SearchResults result = new SearchResults();

            IEnumerable<Shirt> searchQuery = _dataSet.Where(shirt => (options.Colors.Any() &&
                                            options.Colors.Select(y => y.Id).Contains(shirt.Color.Id)) &&
                                            (options.Sizes.Any() &&
                                            options.Sizes.Select(y => y.Id).Contains(shirt.Size.Id)));

            result.Shirts = searchQuery.ToList();

            result.ColorCounts = DetermineColorCount(result.Shirts);
            result.SizeCounts = DetermineSizeCount(result.Shirts);

            return result;
        }

        /// <summary>
        /// Determines the Color Count
        /// </summary>
        /// <param name="searchResults"></param>
        /// <returns></returns>
        private List<ColorCount> DetermineColorCount(List<Shirt> searchResults)
        {
            List<ColorCount> result = new List<ColorCount>();

            Color.All.ForEach((item) =>
            {
                var colorCountItem = new ColorCount
                {
                    Color = item
                };

                if (searchResults.Any(x => x.Color.Id == item.Id))
                {
                    colorCountItem.Count = searchResults.Count(x => x.Color.Id == item.Id);
                };

                result.Add(colorCountItem);
            });

            return result;
        }

        /// <summary>
        /// Determines the Size Count
        /// </summary>
        /// <param name="searchResults"></param>
        /// <returns></returns>
        private List<SizeCount> DetermineSizeCount(List<Shirt> searchResults)
        {
            List<SizeCount> result = new List<SizeCount>();

            Size.All.ForEach((item) =>
            {
                var sizeCountItem = new SizeCount
                {
                    Size = item
                };

                if (searchResults.Any(x => x.Size.Id == item.Id))
                {
                    sizeCountItem.Count = searchResults.Count(x => x.Size.Id == item.Id);
                };

                result.Add(sizeCountItem);
            });

            return result;
        }
    }
}