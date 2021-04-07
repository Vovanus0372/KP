using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFAEntity.API
{
    public class Employees
    {
        /// <summary>
        /// ID_Сотрудника
        /// </summary>
        [Key]
        public int ID_employees { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        public string Surname { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        [Required]
        public string Patronymic { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        [Required]
        public string Address { get; set; }
        /// <summary>
        /// Дата_рождения
        /// </summary>
        [Required]
        public string Date { get; set; }
        /// <summary>
        /// Должность
        /// </summary>
        [Required]
        public string Position { get; set; }
        /// <summary>
        /// Логин
        /// </summary>
        [Required]
        public string Login { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// Номер_телефона
        /// </summary>
        [Required]
        public string Telephone { get; set; }
        public virtual ICollection<Other_services> Other_services { get; set; }
        public virtual ICollection<Skates_hire> Skates_hire { get; set; }
        public virtual ICollection<MK_schedule> MK_schedule { get; set; }
        public Employees() { }
        public Employees(string Surname,string Name, string Patronymic,string Address,string Date ,string Position,string Login ,string Password, string Telephone, int ID_employees = 0)
        {
            this.Surname = Surname;
            this.Name = Name;
            this.Patronymic = Patronymic;
            this.Address = Address;
            this.Date = Date;
            this.Position = Position;
            this.Login = Login;
            this.Password = Password;
            this.Telephone = Telephone;
            this.ID_employees = ID_employees;
            
        }

    }
}
