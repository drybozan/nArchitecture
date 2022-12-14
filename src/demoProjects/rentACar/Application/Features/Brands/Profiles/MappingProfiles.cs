using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using Application.Features.Brands.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Profiles
{
    public class MappingProfiles : Profile
    {
        /// <summary>
        /// Mapleme işlemi yapılır bu classta. kaynak hedefe dönüştürülür AutoMapper sayesinde.
        /// Veritabanı nesnem oluşturduğum model ve dtolar ile maplenir.
        /// </summary>
        public MappingProfiles()
        {
            CreateMap<Brand,CreatedBrandDto>().ReverseMap();
            CreateMap<Brand,CreateBrandCommand>().ReverseMap();
            CreateMap<IPaginate<Brand>, BrandListModel>().ReverseMap();
            CreateMap<Brand,BrandListDto>().ReverseMap();
            CreateMap<Brand, BrandGetByIdDto>().ReverseMap();
        }
    }
}
