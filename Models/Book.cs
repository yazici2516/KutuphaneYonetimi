using System.ComponentModel.DataAnnotations;

namespace KutuphaneYonetim.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kitap adı boş bırakılamaz")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Yazar adı boş bırakılamaz")]
        public string Author { get; set; }

        [Range(1, 2100, ErrorMessage = "Geçerli bir yıl giriniz")]
        public int PublishYear { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
