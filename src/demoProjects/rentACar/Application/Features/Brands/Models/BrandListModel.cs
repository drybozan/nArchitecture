using Application.Features.Brands.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Models
{
    // birden fazla dto veya varlığın alanlarını bir araya getirilerek yeni bir model oluşturuyoruz. joinleme gibi
    // sayfalama işlemi için model kullanılır.
    // geri kalan alanalrı BasePageableModel den alır
    public class BrandListModel:BasePageableModel
    {
        public IList<BrandListDto> Items { get; set; }

    }
}
