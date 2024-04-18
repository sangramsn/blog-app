namespace BlogApp.Models.ViewModel
{
    public class UpdateBlogDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Authorname { get; set; }
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
