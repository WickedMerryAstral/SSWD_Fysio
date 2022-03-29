namespace SSWD_Fysio.Models
{
    public class HomeViewModel
    {
        public PractitionerBar practitionerBar { get; set; }
        public HomeViewModel() {
            practitionerBar = new PractitionerBar();
        }
    }
}
