namespace CVProject.Models
{
    public class CVDetailViewModel
    {

        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }

        public string BDate { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string skills{ get; set; }
        public string Email { get; set; } = " ";
        public string ProfilePicture { get; set; }
        public int Mark { get; set; }
    }
}
