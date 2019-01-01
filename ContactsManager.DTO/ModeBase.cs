using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.DTO
{
    public class ModelBase<T>
    {
        public T Id { get; set; }
    }

    public class MultiLangModelBase<T> : ModelBase<T>
    {
        public string EnName { get; set; }
        public string ArName { get; set; }
    }
}
