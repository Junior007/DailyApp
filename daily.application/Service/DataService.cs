using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using daily.learning.application.Models;

namespace daily.learning.application.Service
{
    public class DataService : IDataService
    {
        public IEnumerable<DataDetail> GetDataDetails()
        {
            List<DataDetail> dataDetails = new List<DataDetail>();
            for (int i = 0; i < 10; i++)
                dataDetails.Add(new DataDetail() { Id = i, Description = $"Descripión del elemento {i}", Name = $"Nombre del elemento {i}" });

            return dataDetails;
        }


    }
}
