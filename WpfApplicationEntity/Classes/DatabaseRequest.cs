using System.Collections.Generic;
using System.Linq;

namespace WFAEntity.API
{
    static class DatabaseRequest
    {
        public class NewSkates_hire
        {
            public int ID_skates_hire { get; set; }
            public string Size { get; set; }
            public string Time { get; set; }
            public string Count { get; set; }
            public string Type { get; set; }
            public string Employees { get; set; }
            public NewSkates_hire(int ID_skates_hire, string Time, string Size, string Count, string Type, string Employees)
            {
                this.ID_skates_hire = ID_skates_hire;
                this.Size = Size;
                this.Time = Time;
                this.Count = Count;
                this.Type = Type;
                this.Employees = Employees;
            }
        }
       
        public class NewOther_services
        {
            public int ID_other_services { get; set; }
            public string Name { get; set; }
            public string The_cost { get; set; }
            public string Employees { get; set; }
            public NewOther_services(int ID_other_services, string Name, string The_cost, string Employees)
            {
                this.ID_other_services = ID_other_services;
                this.Name = Name;
                this.The_cost = The_cost;
                this.Employees = Employees;
            }
        }
        public class NewTicket
        {
            public int ID_Ticket { get; set; }
            public string Cost { get; set; }
            public string Amount { get; set; }
            public string Status { get; set; }
            public string Client { get; set; }
            public string MK_schedule { get; set; }
            public string Other_services { get; set; }
            public string Skates_hire { get; set; }
            public NewTicket(int ID_Ticket, string Cost, string Amount, string Status, string Client, string MK_schedule, string Other_services, string Skates_hire)
            {
                this.ID_Ticket = ID_Ticket;
                this.Cost = Cost;
                this.Amount = Amount;
                this.Status = Status;
                this.Client = Client;
                this.MK_schedule = MK_schedule;
                this.Other_services = Other_services;
                this.Skates_hire = Skates_hire;
            }
        }
        public class NewMK_schedule
        {
            public int ID_MK_schedule { get; set; }
            public string Date { get; set; }
            public string Price { get; set; }
            public string Start_time { get; set; }
            public string End_time { get; set; }
            public string Employees { get; set; }
            public string Other_services { get; set; }
            public NewMK_schedule(int ID_MK_schedule, string Date, string Price, string Start_time, string End_time, string Employees, string Other_services)
            {
                this.ID_MK_schedule = ID_MK_schedule;
                this.Date = Date;
                this.Price = Price;
                this.Start_time = Start_time;
                this.End_time = End_time;
                this.Employees = Employees;
                this.Other_services = Other_services;
            }
        }
       
        public static bool IsUser(MyDBContext objectMyDBContext, string login, string password)
        {
            var tmp = (
                from tmpUser in objectMyDBContext.Employees.ToList<Employees>()
                where tmpUser.Login.CompareTo(login) == 0 && tmpUser.Password.CompareTo(password) == 0
                select tmpUser
                      ).ToList();
            if (tmp.Count == 1)
                return true;
            return false;
        }
        
        #region Клиенты
        public static IEnumerable<Client> GetClients(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Client.ToList();
        }
        #endregion
        #region Сотрудники
        public static IEnumerable<Employees> GetEmployees(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Employees.ToList();
        }
        #endregion
        #region Услуги}
        public static IEnumerable<Other_services> GetServices(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Other_services.ToList();
        }
        public static IEnumerable<NewOther_services> GetEmployeesWithServices(MyDBContext objectMyDBContext)
        {
            return (
                from tmpServices in objectMyDBContext.Other_services.ToList<Other_services>()
                from tmpEmployees in objectMyDBContext.Employees.ToList<Employees>()
                where tmpServices.ID_employees == tmpEmployees.ID_employees
                select (
                new NewOther_services(tmpServices.ID_other_services, tmpServices.Name, tmpServices.The_cost, tmpEmployees.Name)
                )
                       ).ToList();
        }
        #endregion
        #region Коньки на прокат
        public static Skates_hire GetSkatesById(MyDBContext objectMyDBContext, int ID)
        {
            return (from tempSkates in objectMyDBContext.Skates_hire.ToList<Skates_hire>()
                    where tempSkates.ID_skates_hire == ID
                    select tempSkates).First();
        }
        public static IEnumerable<Skates_hire> GetSkates(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Skates_hire.ToList();
        }
        public static IEnumerable<NewSkates_hire> GetEmployeesWithTicket(MyDBContext objectMyDBContext)
        {
            return (
                from tmpSkates in objectMyDBContext.Skates_hire.ToList<Skates_hire>()
                from tmpEmployees in objectMyDBContext.Employees.ToList<Employees>()
                where tmpSkates.ID_employees == tmpEmployees.ID_employees
                select (
                new NewSkates_hire(tmpSkates.ID_skates_hire, tmpSkates.Size, tmpSkates.Time, tmpSkates.Count, tmpSkates.Type, tmpEmployees.Name)
                )
                       ).ToList();
        }
        #endregion
        #region График МК
        public static IEnumerable<MK_schedule> GetShedule(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.MK_schedule.ToList();
        }
        public static IEnumerable<NewMK_schedule> GetEmployeesWithSchedule(MyDBContext objectMyDBContext)
        {
            return (
                from tmpSchedule in objectMyDBContext.MK_schedule.ToList<MK_schedule>()
                from tmpEmployees in objectMyDBContext.Employees.ToList<Employees>()
                from tmpServices in objectMyDBContext.Other_services.ToList<Other_services>()
                where tmpSchedule.ID_employees == tmpEmployees.ID_employees
                where tmpSchedule.ID_other_services == tmpServices.ID_other_services
                select (
                new NewMK_schedule(tmpSchedule.ID_MK_schedule, tmpSchedule.Date, tmpSchedule.Price, tmpSchedule.Start_time, tmpSchedule.End_time, tmpEmployees.Name, tmpServices.Name)
                )
                       ).ToList();
        }
        #endregion
        #region Билеты
        public static IEnumerable<Ticket> GetTicket(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Ticket.ToList();
        }

        public static IEnumerable<NewTicket> GetClientWithTicket(MyDBContext objectMyDBContext)
         {
             return (
                    from tmpTicket in objectMyDBContext.Ticket.ToList<Ticket>()
                    from tmpClient in objectMyDBContext.Client.ToList<Client>()
                    from tmpShedule in objectMyDBContext.MK_schedule.ToList<MK_schedule>()
                    from tmpServices in objectMyDBContext.Other_services.ToList<Other_services>()
                    from tmpSkates in objectMyDBContext.Skates_hire.ToList<Skates_hire>()
                    where tmpSkates.ID_skates_hire == tmpSkates.ID_skates_hire
                    where tmpServices.ID_other_services == tmpServices.ID_other_services
                    where tmpShedule.ID_MK_schedule == tmpShedule.ID_MK_schedule
                    where tmpTicket.ID_Client == tmpClient.ID_Client
                    select (
                    new NewTicket(tmpTicket.ID_Ticket, tmpTicket.Cost, tmpTicket.Amount, tmpTicket.Status, tmpClient.Name, tmpShedule.Date, tmpServices.Name, tmpSkates.Size)
                    )
                           ).ToList();
         }
      
        #endregion
        #region Default
       public static void Fill()
        {
            using (WFAEntity.API.MyDBContext objectMyDBContext = new WFAEntity.API.MyDBContext())
            {
                if (objectMyDBContext.Database.Exists() == false)
                {
                    Employees employe1 = new Employees("Владимир", "Савин", "Алексеевич", "Пушкина, 4", "02.15.2003", "Администратор", "KING", "1111", "29541514");
                    objectMyDBContext.Employees.Add(employe1);
                    objectMyDBContext.SaveChanges();
                    Employees employe2 = new Employees("Мишуто", "Максим", "Витальевич", "Блохина, 2", "06.04.2004", "Посетитель", "Maks", "1234", "1445124785");
                    objectMyDBContext.Employees.Add(employe2);
                    objectMyDBContext.SaveChanges();
                    Employees employe3 = new Employees("Абрамович", "Даниил", "Владимирович", "Пушкина, 2", "03.09.2003", "Кассир", "ABR", "1234", "1445124785");
                    objectMyDBContext.Employees.Add(employe3);
                    objectMyDBContext.SaveChanges();
                    Skates_hire skatesHire1 = new Skates_hire("1", "10:00", "1", "м", employe1);
                    objectMyDBContext.Skates_hire.Add(skatesHire1);
                    objectMyDBContext.SaveChanges();
                    Skates_hire skatesHire2 = new Skates_hire("2", "20:00", "2", "м", employe2);
                    objectMyDBContext.Skates_hire.Add(skatesHire2);
                    objectMyDBContext.SaveChanges();
                    Skates_hire skatesHire3 = new Skates_hire("3", "11:00", "3", "м", employe1);
                    objectMyDBContext.Skates_hire.Add(skatesHire3);
                    objectMyDBContext.SaveChanges();
                    Skates_hire skatesHire4 = new Skates_hire("4", "12:00", "4", "м", employe2);
                    objectMyDBContext.Skates_hire.Add(skatesHire4);
                    objectMyDBContext.SaveChanges();
                    Client client1 = new Client("Игорев", "Игорь", "Игоревич", "Блохина 46", "294512486");
                    objectMyDBContext.Client.Add(client1);
                    objectMyDBContext.SaveChanges();
                    Other_services service1 = new Other_services("Коньки на прокат", "10", employe2);
                    objectMyDBContext.Other_services.Add(service1);
                    objectMyDBContext.SaveChanges();
                    MK_schedule MK_schedule1 = new MK_schedule("16.02.2020", "200", "17:00", "18:00", employe2, service1);
                    objectMyDBContext.MK_schedule.Add(MK_schedule1);
                    objectMyDBContext.SaveChanges();
                    Ticket ticket1 = new Ticket("5", "200", "Есть", client1, MK_schedule1, service1, skatesHire2);
                    objectMyDBContext.Ticket.Add(ticket1);
                    objectMyDBContext.SaveChanges();
                }
            }
        }
        #endregion
    }
}

