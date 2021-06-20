using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCms.Data.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializerAsync();
    }
}
