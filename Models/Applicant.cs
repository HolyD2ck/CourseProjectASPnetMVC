using System.ComponentModel.DataAnnotations;

namespace CourseProjectBlazor.Models
{
    public class Applicant
    {
        public int id { get; set; }
        public string? Фамилия { get; set; }
        public string? Имя { get; set; }
        public string? Отчество { get; set; }
        public string? Образование { get; set; }
        public string? Специальность { get; set; }

        [DataType(DataType.Date)]
        public DateTime Дата_Рождения { get; set; }
        public string? Телефон { get; set; }
        public string? Email { get; set; }
        public string? Фото { get; set; }
    }
}
