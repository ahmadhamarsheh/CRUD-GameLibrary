using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesLibrary.Models
{
    public class Games : BaseEntity
    {
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Cover { get; set; } = string.Empty;
        [ForeignKey("Category")]
        public int category { get; set; }
        public Category Category { get; set; } = default!;

        public ICollection<GameDevice> Devices { get; set; } = new List<GameDevice>();

    }
}
