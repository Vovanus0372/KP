using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFAEntity.API
{
    public class MK_schedule
    {
        /// <summary>
        /// Id_График массового катания
        /// </summary>
        [Key]
        public int ID_MK_schedule { get; set; }
        /// <summary>
        /// Дата
        /// </summary>
        [Required]
        public string Date { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        [Required]
        public string Price { get; set; }
        /// <summary>
        /// Время_начала
        /// </summary>
        [Required]
        public string Start_time { get; set; }
        /// <summary>
        /// Время_конца
        /// </summary>
        [Required]
        public string End_time { get; set; }
        //[ForeignKey("Employees")]
        public int ID_employees { get; set; }
        /// <summary>
        /// Сотрудник
        /// </summary>
        public virtual Employees Employees { get; set; }
        //[ForeignKey("Other_services")]
        public int ID_other_services { get; set; }
        /// <summary>
        /// Услуги
        /// </summary>
        //[ForeignKey("ID_other_services")]
        public virtual Other_services Other_services { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
        public MK_schedule()
        {
        }
        public MK_schedule(string Date, string Price, string Start_time, string End_time, Employees Employees, Other_services Other_services, int ID_MK_schedule = 0)
        {
            this.Date = Date;
            this.Price = Price;
            this.Start_time = Start_time;
            this.End_time = End_time;
          //this.Employees = Employees;
            this.ID_employees = Employees.ID_employees;
            this.ID_other_services = Other_services.ID_other_services;
            this.ID_MK_schedule = ID_MK_schedule;
        }
    }
}
 