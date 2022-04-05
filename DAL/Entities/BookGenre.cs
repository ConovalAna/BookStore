namespace BookStore.DAL.Entities
{
    public class BookGenre
    {
        public int BookGenreID { get; set; }
        public int BookID { get; set; }
        public Genres Genre { get; set; }

        public virtual Book Book { get; set; }

    }
}
