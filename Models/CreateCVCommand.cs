using System;
using System.Collections.Generic;
using System.Linq;
using CVProject.Models;

namespace CVProject.Models
{
    public class CreateCVCommand : EditCV
    {
        public string ProfilePicture = "";
        public CV ToCV()
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

            return new CV
            {

                FName = fname,
                LName = lname,
                BDate = Bdate,
                Nationality = Nationality,
                Gender = gender,
                Email = email,
                Skills = skills,
                Mark = grade,
                ProfilePicture = ProfilePicture
            };
        }

    }

}
