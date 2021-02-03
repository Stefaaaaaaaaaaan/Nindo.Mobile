using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Nindo.Common.Common
{
    public class RangeObservableCollection<T> : ObservableCollection<T>
        where T : notnull
    {
        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
                base.Add(item);
        }
    }
}