using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFAEntity.API
{
    public class Other_services
    {
        /// <summary>
        /// Id_Другие_услуги
        /// </summary>
        [Key]
        public int ID_other_services { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Стоимость
        /// </summary>
        [Required]
        public string The_cost { get; set; }
        [ForeignKey("Employees")]
        public int ID_employees { get; set; }
        //[Required]
        /// <summary>
        /// Сотрудник
        /// </summary
        public virtual Employees Employees { get; set; }
        /// <summary>
        /// Услуги
        /// </summary>
        public Other_services()
        {
        }
        public Other_services(string Name, string The_cost, Employees Employees, int ID_other_services = 0)
        {
            this.Name = Name;
            this.The_cost = The_cost;
            //this.Employees = Employees;
            this.ID_employees = Employees.ID_employees;
            this.ID_other_services = ID_other_services;
        }
        public virtual ICollection<Ticket> Ticket { get; set; }
        public virtual ICollection<MK_schedule> MK_schedule { get; set; }
    }
}
