using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WFAEntity.API
{
    public class Client
    {
        /// <summary>
        /// ID_Клиента
        /// </summary>
        [Key]
        public int ID_client { get; set; }
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
        public string Adress { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        [Required]
        public string Number { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
