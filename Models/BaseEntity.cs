using System.ComponentModel.DataAnnotations;

namespace GamesLibrary.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
