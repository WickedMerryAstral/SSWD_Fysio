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
                date,
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
    }
}
