using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Dtos
{ 
    /// <summary>
    ///  Veritabanı nesnemi doğrudan kullanıcıya döndürmek yerine bir dto oluşturup sadece erişmesini istediğim alanları gönderebilrim. Dto bu yüzden hazırlanır.
    /// </summary>
    public class CreatedBrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
