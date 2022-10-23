using Application.Features.Brands.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetListBrand
{
    // IRequest ile  bir istek olduğunu belirttim.Mediator bunu çözmleyecek
    public class GetListBrandQuery:IRequest<BrandListModel>
    {
        // dataları istiyorum. bir sayfada kaç data gelsin, kaçıncı sayfayı istiyorum vs belirtmek için
        public PageRequest PageRequest { get; set; }


        public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, BrandListModel>
        {
            // sorgu için 
            private readonly IBrandRepository _brandRepository;

            // varlıklartımı maplemek için
            private readonly IMapper _mapper;


            public GetListBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
            }

            public async Task<BrandListModel> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
            {
                //index, hangi sayfadayız ; size, o sayfada kaç tane istiyorsun
                IPaginate<Brand> brands = await _brandRepository.GetListAsync(
                    index: request.PageRequest.Page,
                    size:request.PageRequest.PageSize);

                // veritabnın dönen entity'i modele maplemem lazım ki kullanıcıya model gitsin
                BrandListModel mappedBrandListModel = _mapper.Map<BrandListModel>(brands);

                return mappedBrandListModel;
            }
        }
    }
}
