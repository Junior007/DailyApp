using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf.learning.IoC
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Application <--> Domain
            CreateMap<wpf.learning.domain.Models.DataDetail, wpf.learning.application.Models.DataDetail>().ReverseMap();
        }
    }
}
