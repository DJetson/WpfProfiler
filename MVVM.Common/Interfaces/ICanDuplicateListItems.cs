using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Common.Interfaces
{
    public interface IDuplicateCollectionItems 
    {
        void DuplicateCollectionItem(IEnumerable<object> collection, object item);
    }
}
