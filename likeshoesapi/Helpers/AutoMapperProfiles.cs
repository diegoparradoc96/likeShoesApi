﻿using AutoMapper;
using likeshoesapi.DTOs;
using likeshoesapi.DTOs.Shoe;
using likeshoesapi.Models;

namespace likeshoesapi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserPostDTO, User>();
            CreateMap<UserLoginDTO, UserDTO>();

            // Shoes
            CreateMap<ShoeSectionPostDTO, ShoeSection>()
                .ForMember(
                    shoeSection => shoeSection.ShoeSectionShoeType,
                    options => options.MapFrom(MapShoeSectionShoeType)
                );
            CreateMap<ShoeSection, ShoeSectionDTO>()
                .ForMember(
                    ShoeSectionDTO => ShoeSectionDTO.ShoeTypes,
                    options => options.MapFrom(MapShoeSectionDTOTypes)
                );
            CreateMap<ShoeDTO, Shoe>().ReverseMap();
            CreateMap<ShoeTypeDTO, ShoeType>().ReverseMap();
        }

        private List<ShoeSectionShoeType> MapShoeSectionShoeType(
            ShoeSectionPostDTO shoeSectionPostDTO,
            ShoeSection shoeSection
        )
        {
            var result = new List<ShoeSectionShoeType>();

            if (shoeSectionPostDTO.ShoeTypeIds == null)
            {
                return result;
            }

            foreach (var shoeTypeId in shoeSectionPostDTO.ShoeTypeIds)
            {
                result.Add(new ShoeSectionShoeType() { ShoeTypeId = shoeTypeId, });
            }

            return result;
        }

        private List<ShoeTypeDTO> MapShoeSectionDTOTypes(
            ShoeSection shoeSection,
            ShoeSectionDTO shoeSectionDTO
        )
        {
            var result = new List<ShoeTypeDTO>();

            if (shoeSection.ShoeSectionShoeType == null)
            {
                return result;
            }

            foreach (var shoeSectionShoeType in shoeSection.ShoeSectionShoeType)
            {
                result.Add(
                    new ShoeTypeDTO()
                    {
                        Id = shoeSectionShoeType.ShoeTypeId,
                        TypeName = shoeSectionShoeType.ShoeType.TypeName
                    }
                );
            }

            return result;
        }
    }
}
