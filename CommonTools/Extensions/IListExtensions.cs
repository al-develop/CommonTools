using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTools.Extensions
{
    public static class IListExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items) 
            => new ObservableCollection<T>(items);

        public static void InsertAfter<T>(this IList<T> self, T after, T item)
        {
            var index = self.IndexOf(after);

            if (index == -1)
                return;

            self.Insert(index + 1, item);
        }

        public static IEnumerable<IList<T>> Batch<T>(this IEnumerable<T> items, int batchSize)
        {
            IList<T> bucket = null;
            var count = 0;

            foreach (var item in items)
            {
                if (bucket == null)
                    bucket = new List<T>(batchSize);

                bucket.Add(item);
                count++;

                // The bucket is fully buffered before it's yielded
                if (count != batchSize)
                    continue;

                yield return bucket.ToList();

                bucket = null;
                count = 0;
            }

            // Return the last bucket with all remaining elements
            if (bucket != null && count > 0)
            {
                yield return bucket.Take(count).ToList();
            }
        }

        public static IEnumerable EmptyIfNull(this IEnumerable collection)
        {
            if (collection == null)
                return new List<object>();

            return collection;
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                return new List<T>();

            return collection;
        }

        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
                yield return item;
            }
        }

        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            foreach (T item in items.EmptyIfNull())
            {
                list.Add(item);
            }
        }

        public static void RemoveWhere<T>(this ICollection<T> self, Func<T, bool> filter)
        {
            var itemsToRemove = self.Where(filter).ToList();

            foreach (T item in itemsToRemove)
            {
                self.Remove(item);
            }
        }

        public static bool HasMultipleOf<TItem, TValue>(this IEnumerable<TItem> self, Func<TItem, TValue> selector) 
            => self.Select(selector).Distinct().Count() > 1;

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> self, Func<TSource, TKey> selector) 
            => self.GroupBy(selector).Select(f => f.First());
    }
}

