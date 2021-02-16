using Application.Services.BudgetPeriods;
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
            CreateMap<BudgetYear, CreateBudgetYearCommand>().ReverseMap();
            CreateMap<BudgetYear, UpdateBudgetYearCommand>().ReverseMap();
            CreateMap<BudgetYear, DeleteBudgetYearCommand>().ReverseMap();

            CreateMap<BudgetPeriod, BudgetPeriodDto>().ReverseMap();
            CreateMap<BudgetPeriod, CreateBudgetPeriodCommand>().ReverseMap();
            CreateMap<BudgetPeriod, UpdateBudgetPeriodCommand>().ReverseMap();
            CreateMap<BudgetPeriod, DeleteBudgetPeriodCommand>().ReverseMap();
        }
    }
}
