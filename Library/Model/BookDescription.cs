namespace Library.Bl.Model
{
    public class BookDescription
    {
        public BookDescription(string title, string author)
        {
            Title = title;
            Author = author;
        }

        public string Title { get; }
        public string Author { get; }
    }
}
