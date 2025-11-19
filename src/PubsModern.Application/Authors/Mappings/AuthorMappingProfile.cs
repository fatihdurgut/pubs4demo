namespace PubsModern.Application.Authors.Mappings;

using AutoMapper;
using PubsModern.Application.Authors.DTOs;
using PubsModern.Domain.Entities;

public class AuthorMappingProfile : Profile
{
    public AuthorMappingProfile()
    {
        CreateMap<Author, AuthorDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.ContractStatus, opt => opt.MapFrom(src => src.ContractStatus.ToString()))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address != null ? src.Address.Street : null))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address != null ? src.Address.City : null))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address != null ? src.Address.State : null))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address != null ? src.Address.PostalCode : null))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address != null ? src.Address.Country : null))
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.BookAuthors.Select(ba => ba.Book)));

        CreateMap<Book, BookSummaryDto>()
            .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN.Value))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount));
    }
}
