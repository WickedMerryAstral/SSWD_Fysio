using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainServices
{
    public interface ICommentRepository
    {
        public Comment FindCommentById(int id);
        public int AddComment(Comment comment);
        public int DeleteComment(int id);
        public List<Comment> GetCommentsByFileId(int patientFileId);
    }
}
