using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Models;

namespace WebApplication.Data
{
    public class FamilyJSONData : IFamilyData
    {
        private string adultsFile = "adults.json";
        private IList<Adult> adults;

        public FamilyJSONData()
        {
            if (!File.Exists(adultsFile))
            {
                Seed();
                WriteAdultsToFile();
            }
            else
            {
                string content = File.ReadAllText(adultsFile);
                adults = JsonSerializer.Deserialize<List<Adult>>(content);
            }
        }
        
        private void Seed()
        {
            Adult[] ts =
            {
                new Adult
                {
                    Id = 1,
                    FirstName = "Lars",
                    LastName ="Rasmussen",
                    Age = 24,
                    EyeColor = "Blue",
                    HairColor = "Blonde",
                    Height = 182,
                    JobTitle = {JobTitle = "Manager", Salary = 29003},
                    Sex = "M",
                    Weight = 92
                },
                new Adult
                {
                    Id = 2,
                    FirstName = "Maria",
                    LastName ="Jensen",
                    Age = 18,
                    EyeColor = "Brown",
                    HairColor = "Brown",
                    Height = 165,
                    JobTitle = {JobTitle = "Football Coach", Salary = 8904},
                    Sex = "F",
                    Weight = 75
                },
                new Adult
                {
                    Id = 3,
                    FirstName = "Connie",
                    LastName ="Fisker",
                    Age = 45,
                    EyeColor = "Green",
                    HairColor = "Red",
                    Height = 173,
                    JobTitle = {JobTitle = "Consultant", Salary = 24033},
                    Sex = "F",
                    Weight = 83
                },
                new Adult
                {
                    Id = 4,
                    FirstName = "Mikkel",
                    LastName ="Krabbe",
                    Age = 55,
                    EyeColor = "Green",
                    HairColor = "Brown",
                    Height = 186,
                    JobTitle = {JobTitle = "Nurse", Salary = 24203},
                    Sex = "M",
                    Weight = 99
                },
            };
            adults = ts.ToList();
        }
        public async Task<IList<Adult>> GetAdultsAsync()
        {
            List<Adult> tmp = new List<Adult>(adults);
            return tmp;
        }


        public async Task<Adult> AddAdultAsync(Adult adult)
        {
            int max = adults.Max(adult => adult.Id);
            adult.Id = (++max);
            adults.Add(adult);
            WriteAdultsToFile();
            return adult;
        }
        
        public async Task RemoveAdultAsync(int adultId)
        {
            Adult toRemove = adults.First(t => t.Id == adultId);
            adults.Remove(toRemove);
            WriteAdultsToFile();
        }
        
        public void WriteAdultsToFile()
        {
            string adultsAsJson = JsonSerializer.Serialize(adults);
            File.WriteAllText(adultsFile, adultsAsJson);
        }
        
        public async Task<Adult> UpdateAsync(Adult adult)
        {
            Adult toUpdate = adults.First(t => t.Id == adult.Id);
            toUpdate.Age = adult.Age;
            toUpdate.FirstName = adult.FirstName;
            toUpdate.LastName = adult.LastName;
            toUpdate.HairColor = adult.HairColor;
            toUpdate.EyeColor = adult.EyeColor;
            toUpdate.Weight = adult.Weight;
            toUpdate.Height = adult.Height;
            toUpdate.Sex = adult.Sex;
            toUpdate.JobTitle.JobTitle = adult.JobTitle.JobTitle;
            toUpdate.JobTitle.Salary = adult.JobTitle.Salary;
            WriteAdultsToFile();
            return toUpdate;
        }
        
        /*public Person Get(int id)
        {
            return adults.FirstOrDefault(t => t.Id == id);
        }*/
    }
}