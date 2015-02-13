using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallSimulator.Common.Core
{
    public class ItemOutsideTableException: Exception
    {
        public ItemOutsideTableException(TableItem item)
        {
            this.TableItem = item;
        }

        public TableItem TableItem { get; private set; }
    }
}
