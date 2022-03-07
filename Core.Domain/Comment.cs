using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Comment
    {
        // Identifiers
        public string commentId;
        // Author is always a practitioner.
        public string practitionerId;

        // Info
        public DateTime postDate;
        public bool visible;
        public string content;
    }
}
