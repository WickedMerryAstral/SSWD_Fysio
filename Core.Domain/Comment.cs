using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Comment
    {
        // Identifiers
        [Key]
        public int commentId { get; set; }

        [Required]
        public int practitionerId { get; set; }

        [Required]
        public int patientFileId { get; set; }

        // Info
        public DateTime postDate { get; set; }

        [Required]
        public bool visible { get; set; }

        [Required]
        public string content { get; set; }

        public Comment(int practitionerId, int patientFileId, bool visible, string content)
        {
            this.practitionerId = practitionerId;
            this.patientFileId = patientFileId;
            this.postDate = DateTime.Now;
            this.visible = visible;
            this.content = content;
        }

        public Comment()
        {
        }
    }
}
