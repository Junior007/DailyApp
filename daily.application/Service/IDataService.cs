using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using daily.learning.application.Models;

namespace daily.learning.application.Service
{
    public interface IDataService
    {
        public IEnumerable<DataDetail> GetDataDetails();
    }

}
