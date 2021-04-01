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
            public NewSkates_hire(int ID, string Time, string Size, string Count, string Type, string Employees)
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
      
        #endregion
    }
}

