using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFAEntity.API
{
    public class Skates_hire
    {
        /// <summary>
        /// Коньки_на_прокат
        /// </summary>
        [Key]
        public int ID_skates_hire { get; set; }
        /// <summary>
        /// Размер
        /// </summary>
        [Required]
        public string Size { get; set; }
        /// <summary>
        /// Время
        /// </summary>
        [Required]
        public string Time { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        [Required]
        public string Count { get; set; }
        /// <summary>
        /// Тип
        /// </summary>
        [Required]
        public string Type { get; set; }
        [ForeignKey("Employees")]
        public int ID_employees { get; set; }
        //[Required]
        /// <summary>
        /// Сотрудник
        /// </summary
        public virtual Employees Employees { get; set; }
        public Skates_hire()
        {
        }
        public Skates_hire(string Size, string Time, string Count, string Type, Employees Employees, int ID_skates_hire = 0)
        {
            this.Size = Size;
            this.Time = Time;
            this.Count = Count;
            this.Type = Type;
            //this.Employees = Employees;
            this.ID_employees = Employees.ID_employees;
            this.ID_skates_hire = ID_skates_hire;
        }
        public virtual ICollection<Ticket> Ticket { get; set; }

    }
}
