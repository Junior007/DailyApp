using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf.learning.application.Models;

namespace wpf.learning.application.Service
{
    public interface IDataService
    {
        public IEnumerable<DataDetail> GetDataDetails();
    }

}
