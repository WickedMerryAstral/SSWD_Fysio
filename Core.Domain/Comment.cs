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
        public int practitionerId { get; set; }
        public int patientFileId { get; set; }

        // Info
        public DateTime postDate { get; set; }
        public bool visible { get; set; }
        public string content { get; set; }

        public Comment(int practitionerId, int patientFileId, DateTime postDate, bool visible, string content)
        {
            this.practitionerId = practitionerId;
            this.patientFileId = patientFileId;
            this.postDate = postDate;
            this.visible = visible;
            this.content = content;
        }

        public Comment()
        {
        }
    }
}
