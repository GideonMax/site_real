using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace site_real
{
    public class IndexedProperty<IndexT,ValueT>
    {
        Action<IndexT, ValueT> SetAction;
        Func<IndexT, ValueT> GetFunc;

        public IndexedProperty(Func<IndexT, ValueT> getFunc, Action<IndexT, ValueT> setAction)
        {
            GetFunc = getFunc;
            SetAction = setAction;
        }

        public ValueT this[IndexT i]
        {
            get
            {
                return GetFunc(i);
            }
            set
            {
                SetAction(i, value);
            }
        }
    }
}