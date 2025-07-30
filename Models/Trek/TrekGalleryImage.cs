namespace UserRoles.Models.Trek
{
    public class TrekGalleryImage
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public string Caption { get; set; }
        public int TrekPackageId { get; set; }
    }
}
