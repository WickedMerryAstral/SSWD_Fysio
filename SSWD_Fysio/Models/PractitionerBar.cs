namespace SSWD_Fysio.Models
{
    public class PractitionerBar
    {
        public int amountOfAppointments { get; set; }
        public bool isPractitioner { get; set; }

        public PractitionerBar() {
            isPractitioner = false;
            amountOfAppointments = -400;
        }
    }
}
