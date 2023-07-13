using AutoMapper;

using PEtrial_1.Models;

using static PEtrial_1.DTOs.DTO;

namespace PEtrial_1 {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile()
        {
            CreateMap<Director, DirectorDTO>()
                .ForMember(x => x.Gender,
options => options.MapFrom(source => source.Male ? "Male" : "Female"))
                .ForMember(x => x.DobString,
options => options.MapFrom(source => source.Dob.ToString("d/M/yyyy")))
                .ReverseMap();

            CreateMap<Movie, MovieDTO>()
                .ForMember(x => x.ReleaseYear,
options => options.MapFrom(source => source.ReleaseDate.GetValueOrDefault().Year))
                .ForMember(x => x.ProducerName,
options => options.MapFrom(source => source.Producer.Name))
                .ForMember(x => x.DirectorName,
options => options.MapFrom(source => source.Director.FullName))
                .ReverseMap();
            CreateMap<Director, CreateDirectorDto>().ReverseMap();
        }
    }
}
