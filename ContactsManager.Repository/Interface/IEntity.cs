using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Repository.Interface
{
    public interface IEntity<Tkey>
    {
        Tkey Id { get; set; }
    }
}
