namespace FlutterProjectKAMSOFT.Patterns.DSL
{
    public class Encounter
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public DateTime Date { get; set; }
        public AppointmentStatus Status { get; set; } = new Scheduled();
    }

    public class PlanTask
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TaskStatus Status { get; set; }
    }

    public enum TaskStatus
    {
        Scheduled,
        Completed,
        Canceled
    }

    public abstract record AppointmentStatus();
    public record Scheduled() : AppointmentStatus;
    public record Completed() : AppointmentStatus;
    public record Canceled() : AppointmentStatus;

    public interface IAppointmentChecker
    {
        bool Check(PlanTask plan, Appointment appointment);
    }

    public class AppointmentChecker : IAppointmentChecker
    {
        public bool Check(PlanTask plan, Appointment appointment)
        {
            return plan.Date == appointment.Date && plan.Status == TaskStatus.Scheduled;
        }
    }

    public class PaymentProcess
    {
        public PaymentProcess Group() => this;
        public PaymentProcess DiscountIf() => this;
        public PaymentProcess CreateInvoice() => this;
    }

    public class Process
    {
        public static Process<T1, T2> For<T1, T2>() => new Process<T1, T2>();
        public static Process<T1, T2, T3> For<T1, T2, T3>() => new Process<T1, T2, T3>();
    }

    public class Process<T1, T2>
    {
        public Process<T1, T2> WhenPlanAndAppointment(Func<T1, T2, bool> condition) => this;
        public Process<T1, T2> ThenUpdate(Func<T1, T2> action) => this;
        public Process<T1, T2> ThenUpdatePlanAndAppointment(Action<T1, T2> action) => this;
        public Process<T1, T2> Group() => this;
        public void Execute(T1 t1, T2 t2) { }
    }

    public class Process<T1, T2, T3>
    {
        public Process<T1, T2, T3> WhenPlanAndAppointment(Func<T1, T2, bool> condition) => this;
        public Process<T1, T2, T3> ThenUpdatePlanAndAppointment(Action<T1, T2> action) => this;
        public Process<T1, T2, T3> WhenAppointmentAndEncounter(Func<T2, T3, bool> condition) => this;
        public Process<T1, T2, T3> ThenUpdateAppointmentAndEncounter(Action<T2, T3> action) => this;
        public Process<T1, T2, T3> Join() => this;
        public void Execute(T1 t1, T2 t2, T3 t3) { }
    }

    public class Test
    {
        public void Run()
        {
            var appointmentChecker = new AppointmentChecker();

            var encounter = new Encounter
            {
                Id = Guid.NewGuid(),
                PatientId = Guid.NewGuid(),
                Date = DateTime.Now,
                Description = "Patient reported mild headache."
            };

            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = encounter.PatientId,
                Date = DateTime.Now.AddDays(7)
            };

            var planTask = new PlanTask
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now.AddDays(1),
                Status = TaskStatus.Scheduled
            };

            var del = appointmentChecker.Check;
            var result = del.Invoke(planTask, appointment);
            var result2 = del.Invoke(planTask, appointment);

            var process = Process.For<PlanTask, Appointment, Encounter>()
                .WhenPlanAndAppointment((plan, appointment) => plan.Date == appointment.Date && plan.Status == TaskStatus.Scheduled)
                .ThenUpdatePlanAndAppointment((plan, appointment) => appointment.Status = new Completed())
                .WhenAppointmentAndEncounter((appointment, encounter) => appointment.Date < encounter.Date && appointment.Status is Scheduled)
                .ThenUpdateAppointmentAndEncounter((appointment, encounter) => appointment.Status = new Completed());

            process.Execute(planTask, appointment, encounter);

            var process2 = Process.For<PlanTask, Appointment>()
                .WhenPlanAndAppointment((plan, appointment) => plan.Date == appointment.Date && plan.Status == TaskStatus.Scheduled)
                .ThenUpdatePlanAndAppointment((plan, appointment) => plan.Status = TaskStatus.Completed)
                .Group();

            process2.Execute(planTask, appointment);

            var process3 = Process.For<PlanTask, Appointment, Encounter>()
                .Join();

            process3.Execute(planTask, appointment, encounter);

            var process4 = new PaymentProcess()
                .Group()
                .DiscountIf()
                .CreateInvoice();
        }
    }
}
