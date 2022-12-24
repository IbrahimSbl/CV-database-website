using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using CVProject.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using CVProject.Models;

namespace CVProject.Pages
{
    public class SendModel : PageModel
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        [BindProperty]
        public CreateCVCommand Input { get; set; } = new CreateCVCommand();

        private readonly CVService _service;

        public string uniqueFileName = null;
        public SendModel(IWebHostEnvironment hostEnvironment, CVService service)
        {
            webHostEnvironment = hostEnvironment;
            _service = service;
        }
        public CV cv = new CV();
        public string[] skillsList = { "java", "C", "C++", "C#", "python", "javaScript" };
        /*
        [BindProperty]
        public OwnModel Input { get; set; }
        */

        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }



        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            bool valid = true;
            if (Input.sum != Input.x + Input.y)
            {
                ModelState.AddModelError(string.Empty, "You have entered a wrong summation.Please verify your answer again");
                valid = false;
            }
            if (Input.email != null && Input.vemail != null)
            {
                if (Input.email.Equals(Input.vemail))
                {

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please verify your email again");
                    valid = false;
                }
            }

            int day, year, month;
            var dateNow = DateTime.Now;
            string strDate = dateNow.ToString("dd-MM-yyyy");
            if (Input.Bdate != null)
            {
                day = int.Parse(Input.Bdate.Split("-")[2]);
                month = int.Parse(Input.Bdate.Split("-")[1]);
                year = int.Parse(Input.Bdate.Split("-")[0]);
                if (year >= int.Parse(strDate.Split("-")[2]))
                {
                    ModelState.AddModelError(string.Empty, "Enter a valid birth date." + strDate.Split("-")[0] + " " + strDate.Split("-")[1] + " " + strDate.Split("-")[2]);
                    valid = false;
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Empty Date");
                valid = false;
            }
            int check = 0;
            for (int i = 0; i < Input.programing.Length; i++)
                if (Input.programing[i] == true)
                {
                    check = 1;
                    break;
                }
            if (check == 0)
            {
                ModelState.AddModelError(string.Empty, "Choose at least one programing skill.");
                valid = false;
            }




            if (ProfileImage != null)
            {


                //validate if not image
                string type = ProfileImage.FileName.Split(".")[1];
                if (valid == true)//to avoid adding image to the server if model state is unvalid 
                {
                    if (type.Equals("jpg") || type.Equals("png") || type.Equals("JPG") || type.Equals("PNG"))
                    {
                        //here we wont add to path
                        //we will wait till we recieve the id
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Insert an image not a normal file!!!!!!!!!");
                    }
                }



            }


            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                //if model state is valid
                Input.ProfilePicture = ProfileImage.FileName;
                var id = await _service.CreateCV(Input);
                //after we have the id we can add to the image folder
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Input.fname + "_" + id + "." + ProfileImage.FileName.Split(".")[1];//Guid.NewGuid().ToString() + "_" + ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ProfileImage.CopyTo(fileStream);
                }
                //return RedirectToPage("View", new { id = id });

                /*
                 string skills = "";
                 int grade = 0;
                 for (int i = 0; i < Input.programing.Length; i++)
                 {
                     if (Input.programing[i] == true)
                     {
                         skills += skillsList[i] + "_";
                         grade += 10;
                     }
                 }
                 if (Input.gender == "male")
                     grade += 5;
                 else
                     grade += 10;

                 cv.FName = Input.fname;
                 cv.LName = Input.lname;
                 cv.BDate = Input.Bdate;
                 cv.Nationality = Input.Nationality;
                 cv.Gender = Input.gender;
                 cv.Email = Input.email;
                 cv.Skills = skills;
                 cv.Mark = grade;
                 cv.ProfilePicture = uniqueFileName;
                */

            }


            return RedirectToPage("index");
        }
        /*
        public class OwnModel
		{

            [Required]
			[StringLength(30,ErrorMessage ="Maximum length = {1}")]
			[Display(Name = "Your first name")]            
            public string fname { get; set; }
			[Required]
			[StringLength(30, ErrorMessage = "Maximum length = {1}")]
			[Display(Name = "your last name")]
            public string lname { get; set; }

			[Required]
			[DataType(DataType.Date,ErrorMessage = "Non valid date")]
            public string Bdate { get; set; }
			[Required]
            public string Nationality { get; set; }
            [Required]
            public string gender { get; set; } 
			[Required]
            public bool[]  programing { get; set; }
            [Required]
            [EmailAddress(ErrorMessage = "Email must be of the form 'something@else.com'")]
            public string email { get; set; } = " ";
            [Required]
            public string vemail { get; set; } = " ";
			[Required]
			[Range(1,20)]
            public double x { get; set; }
            [Required]
            [Range(20,50)]
            public double y { get; set; }
            [Required]
            public double sum { get; set; }
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
                    ProfilePicture = uniqueFileName
                };
            }

        }*/
    }
}
