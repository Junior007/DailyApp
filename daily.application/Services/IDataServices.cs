using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using daily.application.Models;

namespace daily.application.Service
{
    public interface IDataServices
    {
        public IEnumerable<DataDetail> GetDataDetails();
    }

}
