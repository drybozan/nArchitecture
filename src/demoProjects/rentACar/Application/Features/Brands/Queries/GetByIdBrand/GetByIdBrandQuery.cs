using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetByIdBrand
{
    public class GetByIdBrandQuery:IRequest<BrandGetByIdDto>
    {
        // quryler için id ile yapılacak bu alanı burda tanımlıyorum.
        public int Id { get; set; }
        public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, BrandGetByIdDto>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;
            private readonly BrandBusinessRules _brandBusinessRules;

            public GetByIdBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;
            }

            public async Task<BrandGetByIdDto> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
            {
                // requsten gelen id yi dbde sorgula varsa entity getir
               Brand? brand =  await _brandRepository.GetAsync(b=>b.Id==request.Id);

                // iş kurallarına uygun mu istenen data ?
               _brandBusinessRules.BrandShouldExistWhenRequested(brand);

                //entity 'i dto'ya maple
               BrandGetByIdDto brandGetByIdDto = _mapper.Map<BrandGetByIdDto>(brand);
               return brandGetByIdDto;
            }
        }
    }
}
