using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CourseProjectBlazor.Models;

public class CourseProjectASPnetMVCContext : DbContext
{
    public CourseProjectASPnetMVCContext(DbContextOptions<CourseProjectASPnetMVCContext> options)
        : base(options)
    {
    }

    public DbSet<CourseProjectBlazor.Models.Employer> Employer { get; set; } = default!;

    public DbSet<CourseProjectBlazor.Models.Applicant> Applicant { get; set; } = default!;
    private static readonly Random random = new Random();
    public void GetData()
    {
        if (!Employer.Any())
        {
            var data = EmployerInfo();
            Employer.AddRange(data);
            SaveChanges();
        }
    }
    public void GetData2()
    {
        if (!Applicant.Any())
        {
            var data = ApplicantInfo();
            Applicant.AddRange(data);
            SaveChanges();
        }
    }
    public int Random() 
    {
       return random.Next(0,20);
    }
    public string GetPhoto() 
    {
        int random = Random();
        string sourceFolderPath = "..\\CourseProjectASPnetMVC\\wwwroot\\ga";
        string targetFolderPath = "..\\CourseProjectASPnetMVC\\wwwroot\\img\\employers";
        string[] files = Directory.GetFiles(sourceFolderPath);
        string sourceFile = files[random];
        string targetFile = Path.Combine(targetFolderPath, Path.GetFileName(sourceFile));

        File.Copy(sourceFile, targetFile, true);

        return Path.Combine("img", "employers", Path.GetFileName(sourceFile));
    }
    public List<Employer> EmployerInfo()
    {
        List<string> lines = File.ReadAllLines("..\\CourseProjectASPnetMVC\\wwwroot\\Base\\employers.csv").ToList();
        var Employer = new List<Employer>();

        foreach (var line in lines)
        {
            var photo = GetPhoto();
            string[] spl = line.Split(';');
            var employer = new Employer
            {
                Фамилия = spl[0],
                Имя = spl[1],
                Отчество = spl[2],
                Организация = spl[3],
                Дата_Основания = Convert.ToDateTime(spl[4]),
                Вакансия = spl[5],
                Телефон = spl[6],
                Email = spl[7],
                Фото = photo
            };
            Employer.Add(employer);
        }
        return Employer;
    }
    public string GetPhoto2()
    {
        int random = Random();
        string sourceFolderPath = "..\\CourseProjectASPnetMVC\\wwwroot\\ga";
        string targetFolderPath = "..\\CourseProjectASPnetMVC\\wwwroot\\img\\applicants";
        string[] files = Directory.GetFiles(sourceFolderPath);
        string sourceFile = files[random];
        string targetFile = Path.Combine(targetFolderPath, Path.GetFileName(sourceFile));

        File.Copy(sourceFile, targetFile, true);

        return Path.Combine("img", "applicants", Path.GetFileName(sourceFile));
    }
    public List<Applicant> ApplicantInfo()
    {
        List<string> lines = File.ReadAllLines("..\\CourseProjectASPnetMVC\\wwwroot\\Base\\applicants.csv").ToList();
        var Applicant = new List<Applicant>();

        foreach (var line in lines)
        {
            var photo = GetPhoto2();
            string[] spl = line.Split(';');
            var applicant = new Applicant
            {
                Фамилия = spl[0],
                Имя = spl[1],
                Отчество = spl[2],
                Образование = spl[3],
                Специальность = spl[4],
                Дата_Рождения = Convert.ToDateTime(spl[5]),
                Телефон = spl[6],
                Email = spl[7],
                Фото = photo
            };
            Applicant.Add(applicant);
        }
        return Applicant;
    }
}
