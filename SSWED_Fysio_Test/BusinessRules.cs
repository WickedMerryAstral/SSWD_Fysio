using Core.Domain;
using Core.DomainServices;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Infrastructure.EF;
using Moq.EntityFrameworkCore;
using System.Globalization;
using SSWD_Fysio.Models;

namespace SSWED_Fysio_Test
{
    public class BusinessRules
    {
        // Business Regels

        // BR_1. Het maximaal aantal afspraken per week wordt
        // niet overschreden bij het boeken van een afspraak.

        [Fact]
        public void CannotScheduleAppointmentsOverLimit()
        {
            // Arrange

            var mockDb = new Mock<FysioDBContext>();
            IList<TreatmentPlan> plans = new List<TreatmentPlan>();
            mockDb.Setup(x => x.treatmentPlans).ReturnsDbSet(plans);

            TreatmentPlan plan = new TreatmentPlan();
            plan.treatmentPlanId = 1;
            plan.treatments = new List<Treatment>();
            plan.weeklySessions = 2;

            Treatment treatment_1 = new Treatment();
            treatment_1.treatmentDate = DateTime.ParseExact("2022-04-07", "yyyy-MM-dd", CultureInfo.InvariantCulture);

            Treatment treatment_2 = new Treatment();
            treatment_2.treatmentDate = DateTime.ParseExact("2022-04-07", "yyyy-MM-dd", CultureInfo.InvariantCulture);

            plan.treatments.Add(treatment_1);
            plan.treatments.Add(treatment_2);
            plans.Add(plan);

            EFTreatmentPlanRepository repository = new EFTreatmentPlanRepository(mockDb.Object);

            // Act

            DateTime checkDate = DateTime.ParseExact("2022-04-07", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            bool hasReachedLimit = repository.HasReachedWeeklyLimit(checkDate, 1);

            // Assert

            Assert.True(hasReachedLimit);
        }

        // BR_2. Afspraken kunnen alleen worden gemaakt op beschikbare momenten van de
        // hoofdbehandelaar.Hierbij moet rekening gehouden worden met de algemene
        // beschikbaarheid en de reeds gemaakte afspraken.

        [Fact]
        public void CannotScheduleAppointmentWhenPractitionerDoesntWork()
        {
            // Arrange

            var mockDb = new Mock<FysioDBContext>();

            IList<TreatmentPlan> plans = new List<TreatmentPlan>();
            mockDb.Setup(x => x.treatmentPlans).ReturnsDbSet(plans);

            IList<Practitioner> practitioners = new List<Practitioner>();
            mockDb.Setup(x => x.practitioners).ReturnsDbSet(practitioners);

            Practitioner practitioner = new Practitioner();
            practitioner.practitionerId = 1;

            practitioner.availableMON = true;
            practitioner.availableTUE = true;
            practitioner.availableWED = true;
            practitioner.availableTHU = false;
            practitioner.availableFRI = false;
            practitioner.availableSAT = false;
            practitioner.availableSUN = false;

            practitioners.Add(practitioner);

            EFTreatmentPlanRepository repository = new EFTreatmentPlanRepository(mockDb.Object);

            DateTime fridayTreatment = DateTime.ParseExact("2022-04-15", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime wednesdayTreatment = DateTime.ParseExact("2022-04-20", "yyyy-MM-dd", CultureInfo.InvariantCulture);

            // Act

            bool friday = repository.IsPractitionerWorkingOnDay(1, fridayTreatment);
            bool wednesday = repository.IsPractitionerWorkingOnDay(1, wednesdayTreatment);

            // Assert

            Assert.False(friday);
            Assert.True(wednesday);
        }

        [Fact]
        public void CannotScheduleAppointmentWhenPractitionerIsBusy()
        {
            // Arrange

            var mockDb = new Mock<FysioDBContext>();

            IList<TreatmentPlan> plans = new List<TreatmentPlan>();
            mockDb.Setup(x => x.treatmentPlans).ReturnsDbSet(plans);

            IList<Treatment> treatments = new List<Treatment>();
            mockDb.Setup(x => x.treatments).ReturnsDbSet(treatments);

            IList<Practitioner> practitioners = new List<Practitioner>();
            mockDb.Setup(x => x.practitioners).ReturnsDbSet(practitioners);

            Practitioner practitioner = new Practitioner();
            practitioner.practitionerId = 1;
            practitioners.Add(practitioner);

            TreatmentPlan plan = new TreatmentPlan();
            plan.treatmentPlanId = 1;
            plan.sessionDuration = 60;

            Treatment treatment_1 = new Treatment();
            treatment_1.treatmentDate = DateTime.ParseExact("2022-04-19 09:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            treatment_1.treatmentPlanId = 1;
            treatment_1.practitionerId = 1;

            Treatment treatment_2 = new Treatment();
            treatment_2.treatmentDate = DateTime.ParseExact("2022-04-19 09:00", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            treatment_2.treatmentPlanId = 1;
            treatment_2.practitionerId = 1;

            treatments.Add(treatment_1);
            plans.Add(plan);

            EFTreatmentPlanRepository repository = new EFTreatmentPlanRepository(mockDb.Object);

            // Act

            bool isAvailable = repository.IsPractitionerAvailable(1, 
                treatment_2.treatmentDate, treatment_2.treatmentDate.AddMinutes(plan.sessionDuration));

            // Assert

            Assert.False(isAvailable);
        }

        // BR_3. Een behandeling kan niet in worden gevoerd als de patiënt nog niet 
        // in de praktijk is geregistreerd of nadat de behandeling is beëindigd.

        [Fact]
        public void CannotScheduleAppointmentsOutsideOfTreatmentPlan()
        {
            // Arrange

            var mockDb = new Mock<FysioDBContext>();

            IList<TreatmentPlan> plans = new List<TreatmentPlan>();
            mockDb.Setup(x => x.treatmentPlans).ReturnsDbSet(plans);

            IList<PatientFile> files = new List<PatientFile>();
            mockDb.Setup(x => x.patientFiles).ReturnsDbSet(files);

            PatientFile file = new PatientFile();
            file.patientFileId = 1;
            file.entryDate = DateTime.ParseExact("2022-04-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            file.dischargeDate = DateTime.ParseExact("2022-04-30", "yyyy-MM-dd", CultureInfo.InvariantCulture);

            files.Add(file);

            DateTime treatment_fitting = DateTime.ParseExact("2022-04-15", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime treatment_after_end = DateTime.ParseExact("2022-06-15", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime treatment_before_start = DateTime.ParseExact("2022-03-15", "yyyy-MM-dd", CultureInfo.InvariantCulture);

            EFPatientFileRepository repository = new EFPatientFileRepository(mockDb.Object);

            // Act

            bool fitting = repository.IsWithinTreatmentPeriod(1, treatment_fitting);
            bool after_end = repository.IsWithinTreatmentPeriod(1, treatment_after_end);
            bool before_start = repository.IsWithinTreatmentPeriod(1, treatment_before_start);

            // Assert

            Assert.True(fitting);
            Assert.False(before_start);
            Assert.False(after_end);
        }

        // BR_4 Bij een aantal behandelingen is een toelichting verplicht.
        // Dit is geregeld door casten van JSON arrays naar VektisTreatment objecten.
        // Gebeurt tijdens ophalen van codes.

        [Fact]
        public void TreatmentsSpecifyMandatoryExplanation()
        {
            // Arrange
            VektisTreatment vt_1 = new VektisTreatment("101", "Genezen onder rug", "Eerst dit, dan dat", true);
            VektisTreatment vt_2 = new VektisTreatment("101", "Genezen onder been", "Eerst dat, dan dit", false);

            // Assert
            Assert.True(vt_1.hasMandatoryExplanation);
            Assert.False(vt_2.hasMandatoryExplanation);
        }

        // BR_5 De leeftijd van een patiënt is >= 16

        [Fact]
        public void PatientsMustBeOver16()
        {
            // Arrange

            Patient patient = new Patient();
            
            DateTime birthday_15 = DateTime.ParseExact("2007-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime birthday_22 = DateTime.ParseExact("2000-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);

            // Act

            bool not_old_enough = patient.IsPatientOver16(birthday_15);
            bool old_enough = patient.IsPatientOver16(birthday_22);

            // Assert

            Assert.False(not_old_enough);
            Assert.True(old_enough);
        }

        // BR_6 Een afspraak kan niet door een patiënt worden geannuleerd minder van 24 uur
        // voorafgaand aan de afspraak.

        [Fact]
        public void AppointmentsMustBeCancelledADayInAdvance()
        {
            // Arrange

            var mockDb = new Mock<FysioDBContext>();
            IList<Treatment> treatments = new List<Treatment>();
            mockDb.Setup(x => x.treatments).ReturnsDbSet(treatments);

            Treatment treatment_can_cancel = new Treatment();
            treatment_can_cancel.treatmentId = 1;
            treatment_can_cancel.treatmentDate = DateTime.Now.AddDays(10);

            Treatment treatment_cannot_cancel = new Treatment();
            treatment_cannot_cancel.treatmentId = 2;
            treatment_cannot_cancel.treatmentDate = DateTime.Now.AddHours(12);

            treatments.Add(treatment_can_cancel);
            treatments.Add(treatment_cannot_cancel);

            EFTreatmentRepository repository = new EFTreatmentRepository(mockDb.Object);

            // Act

            bool can_cancel = repository.CanPatientCancel(1);
            bool cannot_cancel = repository.CanPatientCancel(2);

            // Assert

            Assert.True(can_cancel);
            Assert.False(cannot_cancel);
        }
    }
}