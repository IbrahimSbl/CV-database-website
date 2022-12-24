namespace CVProject.Models
{
    public class CVSummaryViewModel
    {
        public int CVId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public int Mark { get; set; }
        public string ProfilePicture { get; set; }

        public CVSummaryViewModel FromCV(CV cv)
        {
            return new CVSummaryViewModel
            {
                CVId = cv.CVId,
                FName = cv.FName,
                LName = cv.LName,
                Gender = cv.Gender,
                Email = cv.Email,
                Mark = cv.Mark,
                ProfilePicture = cv.ProfilePicture
            };
        }
    }
}
