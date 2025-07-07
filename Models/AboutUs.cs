namespace UserRoles.Models
{
    public class AboutUs
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Mission { get; set; }
        public string Story { get; set; }

        public ICollection<TeamMember> TeamMembers { get; set; }

    }
}
