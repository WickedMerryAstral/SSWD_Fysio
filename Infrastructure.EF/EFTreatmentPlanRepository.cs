using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;
using Core.DomainServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF
{
    public class EFTreatmentPlanRepository : ITreatmentPlanRepository
    {
        private FysioDBContext context;
        public EFTreatmentPlanRepository(FysioDBContext db)
        {
            this.context = db;
        }
        public int AddTreatmentPlan(TreatmentPlan treatmentPlan)
        {
            throw new NotImplementedException();
        }

        public int DeleteTreatmentPlanById(int id)
        {
            throw new NotImplementedException();
        }

        public TreatmentPlan FindTreatmentPlanById(int id)
        {
            return context.treatmentPlans.Include(tp => tp.treatments).Where(tp => tp.treatmentPlanId == id).FirstOrDefault();
        }

        public bool HasReachedWeeklyLimit(DateTime date, int treatmentPlanId)
        {
            var currentCulture = CultureInfo.CurrentCulture;
            bool output = false;
            int count = 0;

            // Getting the week number of the new date.
            int weekNo = currentCulture.Calendar.GetWeekOfYear(
                date,
                currentCulture.DateTimeFormat.CalendarWeekRule,
                currentCulture.DateTimeFormat.FirstDayOfWeek);

            // Checking every treatment within the treatment plan of a patientfile
            TreatmentPlan plan = FindTreatmentPlanById(treatmentPlanId);
            foreach (Treatment t in plan.treatments) {
                int treatmentWeekCount = currentCulture.Calendar.GetWeekOfYear(
                t.treatmentDate,
                currentCulture.DateTimeFormat.CalendarWeekRule,
                currentCulture.DateTimeFormat.FirstDayOfWeek);

                if (weekNo == treatmentWeekCount && t.treatmentDate.Year == date.Year) {
                    count++;
                }
            }

            // Checking to see if the limit has been surpassed.
            if (count >= plan.weeklySessions) {
                output = true;
            }

            return output;
        }

        public bool IsPractitionerWorkingOnDay(int practitionerId, DateTime date)
        {
            // Checking if the practitioner works on the day of the treatment.
            Practitioner p = context.practitioners.Where(p => p.practitionerId == practitionerId).FirstOrDefault();
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return p.availableMON;
                case DayOfWeek.Tuesday:
                    return p.availableTUE;
                case DayOfWeek.Wednesday:
                    return p.availableWED;
                case DayOfWeek.Thursday:
                    return p.availableTHU;
                case DayOfWeek.Friday:
                    return p.availableFRI;
                case DayOfWeek.Saturday:
                    return p.availableSAT;
                case DayOfWeek.Sunday:
                    return p.availableSUN;

                default:
                    return false;
            }
        }

        public bool IsPractitionerAvailable(int practitionerId, DateTime start, DateTime end)
        {
            // Finding all treatments on this specific day, for this specific practitioner.
            List<Treatment> treatments = context.treatments.ToList();
            treatments = treatments.Where(t => t.treatmentDate.Date == start.Date).ToList();
            treatments = treatments.Where(t => t.treatmentDate.Year == start.Year).ToList();
            treatments = treatments.Where(t => t.practitionerId == practitionerId).ToList();

            // Checking if the treatment falls under any other treatment.
            foreach (Treatment tr in treatments)
            {
                if (start >= tr.treatmentDate || start <= tr.treatmentEndDate)
                {
                    return false;
                }
                if (end >= tr.treatmentDate || end <= tr.treatmentEndDate)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
