using Core.Domain;

namespace SSWD_Fysio.Models
{
    public class CommentViewModel
    {
        public Comment comment { get; set; }
        public PractitionerBar practitionerBar { get; set; }
        public CommentViewModel() {
            practitionerBar = new PractitionerBar();
        }
    }
}
