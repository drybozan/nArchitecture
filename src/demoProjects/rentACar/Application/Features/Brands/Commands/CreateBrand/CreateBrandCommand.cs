using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public partial class CreateBrandCommand:IRequest<CreatedBrandDto>
    {
        /// <summary>
        /// api create isteğinde bulunduğunda entity nin alanlarıyla (command için gerekli olan alanalrı burda tanıtmak yeterli) işlev görür. 
        /// Bu alan üzerinden handler ile birlikte command işlenir.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// CreateBrandCommandHandler sorgu atar ve gelen cevabı dto ya yükler 
        ///  Apiden gönderilen command Mediator sayesinde buraya gönderilir.
        ///  Mediator, IRequestHandler araclığıyla buraya gelmesi gerektiğini anlar.
        /// </summary>
        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>
        {
            // servis işlemlerimi görecek
            private readonly IBrandRepository _brandRepository;
            //entity ile dtoları mapler
            private readonly IMapper _mapper;
            // bu entity için kurallarım
            private readonly BrandBusinessRules _brandBusinessRules;

            /// <summary>
            /// handler içinde iş kuralları kullanılır.
            /// </summary>
            
            public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;
            }

            public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                // marka ismi tekrar edemez eklendiğinde. İş kralı Rules kısmında tanımlı
                await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);

                Brand mappedBrand = _mapper.Map<Brand>(request); //marka,entity ile eşlenir
                Brand createdBrand = await _brandRepository.AddAsync(mappedBrand); //repository aracılığyla db ye entity eklenir. Artık id si var. db de oluşturulan brand, createdBrand nesnesine atanır. 
                CreatedBrandDto createdBrandDto = _mapper.Map<CreatedBrandDto>(createdBrand);//db den gelen nesneyi dto ya dönüştürüyorum . oluşturulan dto kullanıcıya sunulacak.

                return createdBrandDto; //veritabanı nesnesini asla apiye yollama sadece göndermek istediğim kolonları dto ile gönderiyorum.

            }
        }
    }
}
