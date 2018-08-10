using AutoMapper;
using eSalesBog.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static eSalesBog.Models.SalesViewModels;

namespace eSalesBog.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductViewModel, ProductDto>().ForMember(m => m.ID, opt => opt.Ignore());
            CreateMap<ProductDto, ProductViewModel>();

            CreateMap<ConsultantViewModel, ConsultantDto>().ForMember(m => m.ID, opt => opt.Ignore());
            CreateMap<ConsultantDto, ConsultantViewModel>();

            CreateMap<SalesViewModel, SalesDto>().ForMember(m => m.ID, opt => opt.Ignore());
            CreateMap<SalesDto, SalesViewModel>();
        }
    }
}