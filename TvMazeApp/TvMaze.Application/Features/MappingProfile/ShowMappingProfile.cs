using AutoMapper;
using TvMaze.Application.Features.Shows.Commands;
using TvMaze.Domain.Entities;

namespace TvMaze.Application.Features.MappingProfile
{
    public class ShowMappingProfile : Profile
    {
        public ShowMappingProfile()
        {
            CreateMap<ShowUpdateCommand, Show>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Languge))
                .ForMember(dest => dest.Premiered, opt => opt.MapFrom(src => src.Premiered))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary));

            CreateMap<ShowAddCommand, Show>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Languge))
                .ForMember(dest => dest.Premiered, opt => opt.MapFrom(src => src.Premiered))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary));
        }
    }
}
