using Core.Domain;

namespace SSWD_Fysio.Models
{
    public class HomeViewModel
    {
        public bool noAccount { get; set; }
        public PractitionerBar practitionerBar { get; set; }
        public HomeViewModel() {
            practitionerBar = new PractitionerBar();
            noAccount = false;
        }
    }
}
