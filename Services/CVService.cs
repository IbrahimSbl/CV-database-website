using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVProject.Models;

namespace CVProject.Services
{
    public class CVService
    {

        readonly AppDbContext _context;
        readonly ILogger _logger;
        
        
        public CVService(AppDbContext context, ILoggerFactory factory)
        {
            _context = context;
            _logger = factory.CreateLogger<CVService>();
        }

        public async Task<List<CVSummaryViewModel>> GetCVs()
        {
            return await _context.CVs
                .Where(x => !x.IsDeleted)
                .Select(x => new CVSummaryViewModel
                {
                    CVId = x.CVId,
                    FName = x.FName,
                    LName = x.LName,
                    Gender = x.Gender,
                    Email = x.Email,
                    Mark = x.Mark,
                    ProfilePicture = x.ProfilePicture
                })
                .ToListAsync();
        }

        public async Task<int> CreateCV(CreateCVCommand cmd)
        {/*
            CV cv = new CV
            {
                FName = cmd.FName,
                LName = cmd.LName,
                BDate = cmd.BDate,
                Nationality = cmd.Nationality,
                Gender = cmd.Gender,
                Skills = cmd.Skills,
                Email = cmd.Email,
                ProfilePicture = cmd.ProfilePicture,
                Mark = cmd.Mark
            };
            _context.Add(cv);
            await _context.SaveChangesAsync();
            return cv.CVId;*/
            var cv = cmd.ToCV();
            _context.Add(cv);
            await _context.SaveChangesAsync();
            return cv.CVId;
        }
        public async Task<CVDetailViewModel> GetRecipeDetail(int id)
        {
            return await _context.CVs
                .Where(x => x.CVId == id)
                .Where(x => !x.IsDeleted)
                .Select(x => new CVDetailViewModel
                {
                    Id = x.CVId,
                    FName = x.FName,
                    LName = x.LName,
                    BDate = x.BDate,
                    Nationality = x.Nationality,
                    Gender = x.Gender,
                    skills = x.Skills,
                    Email = x.Email,
                    ProfilePicture = x.ProfilePicture,
                    Mark = x.Mark
                }).SingleOrDefaultAsync();
           
        }
        public static bool[] toBool(string str)
        {
            string[] arr = str.Split("_");
            bool[] result = new bool[6];

            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] == "java")
                {
                    result[0] = true;
                }else if (arr[i] == "C")
                {
                    result[1] = true;
                }
                else if (arr[i] == "C")
                {
                    result[1] = true;
                }
                else if (arr[i] == "C++")
                {
                    result[2] = true;
                }
                else if (arr[i] == "C#")
                {
                    result[3] = true;
                }
                else if (arr[i] == "python")
                {
                    result[4] = true;
                }
                else if (arr[i] == "javaScript")
                {
                    result[5] = true;
                }
                
            }
            for(int i =0;i< result.Length;i++)
            {
                if (result[i] != true)
                    result[i] = false;
            }
            return result;
        }
        public async Task<UpdateCVCommand> GetCVForUpdate(int CVId)
        {
            return await _context.CVs
                .Where(X => X.CVId == CVId)
                .Where(x =>!x.IsDeleted)
                .Select(x => new UpdateCVCommand
                {
                    Id = x.CVId,
                    fname = x.FName,
                    lname = x.LName,
                    email = x.Email,
                    Bdate = x.BDate,
                    gender = x.Gender,
                    Nationality = x.Nationality,
                    programing = toBool(x.Skills)


                }).SingleOrDefaultAsync();
        }

        /// <summary>
        /// Updateds an existing CV
        public async Task UpdateRecipe(UpdateCVCommand cmd)
        {
            var cv = await _context.CVs.FindAsync(cmd.Id);
            if (cv == null) { throw new Exception("Unable to find the recipe"); }
            if (cv.IsDeleted) { throw new Exception("Unable to update a deleted recipe"); }

            cmd.UpdateCV(cv);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCV(int Id)
        {
            var cv = await _context.CVs.FindAsync(Id);
            if (cv is null) { throw new Exception("Unable to find recipe"); }

            cv.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

    }
}
