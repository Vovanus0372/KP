using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFAEntity.API
{
    public class Client
    {
        /// <summary>
        /// ID_Клиента
        /// </summary>
        [Key]
        public int ID_Client { get; set; }
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
        /// Телефон
        /// </summary>
        [Required]
        public string Number { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
          public Client() { }
          public Client(string Surname, string Name, string Patronymic, string Address, string Number, int ID_Client = 0)
        {
            this.Surname = Surname;
            this.Name = Name;
            this.Patronymic = Patronymic;
            this.Address = Address;
            this.Number = Number;
            this.ID_Client = ID_Client;
        }
    }
}
