using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
       //public virtual Other_services other_services { get; set; }
        //       [ForeignKey("Client")]
        public int ID_Client { get; set; }
        /// <summary>
        /// Клиент
        /// </summary>
        /// 
        public virtual Client Client { get; set; }
        //       [ForeignKey("MK_schedule")]
        public int ID_MK_schedule { get; set; }
        public virtual MK_schedule MK_schedule { get; set; }
        /// <summary>
        /// График_МК
        /// </summary>
        /// 
        //        [ForeignKey("Other_services")]
        public int ID_other_services { get; set; }
        public virtual Other_services Other_services { get; set; }
        //       [ForeignKey("Skates_hire")]
        public int ID_skates_hire { get; set; }
        /// <summary>
        /// Коньки_НА_Прокат
        /// </summary>
        public virtual Skates_hire Skates_hire { get; set; }
         public Ticket()
        {
        }
         public Ticket(string Cost, string Amount, string Status, Client Client, MK_schedule MK_schedule, Other_services Other_services, Skates_hire Skates_hire, int ID_Ticket = 0)
        {
            this.Cost = Cost;
            this.Amount = Amount;
            this.Status = Status;
            //this.Client = Client;
            this.ID_Client = Client.ID_Client;
            this.ID_MK_schedule = MK_schedule.ID_MK_schedule;
            this.ID_other_services = Other_services.ID_other_services;
            this.ID_skates_hire = Skates_hire.ID_skates_hire;
            this.ID_Ticket = ID_Ticket;
        }

    }
}
