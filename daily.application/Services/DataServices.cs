using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using daily.application.Models;
using daily.application.Service;

namespace daily.application.Service
{
    public class DataServices : IDataServices
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
