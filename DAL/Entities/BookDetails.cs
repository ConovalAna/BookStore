using System.ComponentModel.DataAnnotations;

namespace BookStore.DAL.Entities
{
    public class BookDetails
    {
        [Key]
        public int BookID { get; set; }
        public string SBN { get; set; }
        public string LongDescription { get; set; }
        public DateTime Date { get; set; }
        public string Edition { get; set; }
        
        public virtual Book? Book { get; set; }
    }
}
