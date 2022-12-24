using System.Threading.Tasks;

namespace CVProject.Models
{
    public class CV
    {
        public int CVId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string BDate { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string Skills { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }

        public int Mark { get; set; } = 0;
        public bool IsDeleted { get; internal set; } = false;
    }
}
