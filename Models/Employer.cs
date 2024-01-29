using System.ComponentModel.DataAnnotations;

namespace CourseProjectBlazor.Models
{
    public class Employer
    {
        public int id { get; set; }
        public string? Фамилия { get; set; }
        public string? Имя { get; set; }
        public string? Отчество { get; set; }
        public string? Организация { get; set; }

        [DataType(DataType.Date)]
        public DateTime Дата_Основания { get; set; }
        public string? Вакансия { get; set; }
        public string? Телефон { get; set; }
        public string? Email { get; set; }
        public string? Фото { get; set; }
       
    }
}
