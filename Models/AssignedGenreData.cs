using BookStore.DAL.Entities;

namespace BookStore.Models
{
    public class AssignedGenreData
    {
        public Genres Genre { get; set; }
        public bool Assigned { get; set; }
    }
}
