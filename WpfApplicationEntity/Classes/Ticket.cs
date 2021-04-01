using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace WFAEntity.API
{
    public class Ticket
    {
        /// <summary>
        /// ID_билет
        /// </summary>
        [Key]
        public int ID_Ticket { get; set; }
        /// <summary>
        /// Стоимость
        /// </summary>
        [Required]
        public string Cost { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        [Required]
        public string Amount { get; set; }
        /// <summary>
        /// Статус
        /// </summary>
        [Required]
        public string Status { get; set; }
        /// <summary>
        /// Другая_услуга
        /// </summary>
        public virtual Other_services other_services { get; set; }
        /// <summary>
        /// Клиент
        /// </summary>
        public virtual Client Client { get; set; }
        /// <summary>
        /// График_МК
        /// </summary>
        public virtual MK_schedule MK_schedule { get; set; }
        /// <summary>
        /// Коньки_НА_Прокат
        /// </summary>
        public virtual Skates_hire Skates_hire { get; set; }

    }
}
