using AutoMapper;
using nideriji_export.JsonModel;
using nideriji_export.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nideriji_export.helpers
{
    public  class AutoMapperHelper
    {
        public IMapper Mapper { get; set; }
        public AutoMapperHelper()
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<User, UserDb>().ReverseMap();
                    cfg.CreateMap<Daily, DailyDb>().ReverseMap();
                });
            Mapper = config.CreateMapper();

        }
    }
}
