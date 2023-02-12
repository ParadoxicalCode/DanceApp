using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace SQLite.Data
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string Title { get; set; }

        [ForeignKey("Country")]
        public int? CountryId { get; set; }
        public virtual Country? Country { get; set; }
    }
}
