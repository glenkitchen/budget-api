using Application.Services.BudgetYears;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BudgetYear, BudgetYearDto>().ReverseMap();
            CreateMap<BudgetYear, BudgetYearListDto>().ReverseMap();
        }
    }
}
