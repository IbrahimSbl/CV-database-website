namespace CVProject.Models
{
    public class UpdateCVCommand:UpdateCV
    {
       
        public void UpdateCV(CV cv)
        {
            string skills = "";
            int grade = 0;
            string lang = "";
            for (int i = 0; i < programing.Length; i++)
            {
                if (programing[i] == true)
                {
                    if (i == 0)
                        lang = "java";
                    else if (i == 1)
                        lang = "C";
                    else if (i == 2)
                        lang = "C++";
                    else if (i == 3)
                        lang = "C#";
                    else if (i == 4)
                        lang = "python";
                    else if (i == 5)
                        lang = "javaScript";
                    skills += lang + "_";
                    grade += 10;
                }
            }
            if (gender == "male")
                grade += 5;
            else
                grade += 10;
            cv.FName = fname;
            cv.LName = lname;
            cv.Email = email;
            cv.BDate = Bdate;
            cv.Gender = gender;
            cv.Nationality = Nationality;
            cv.Skills = skills;
            cv.Mark = grade;
        }
    }
}
