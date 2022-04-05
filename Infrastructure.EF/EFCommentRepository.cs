using Core.Domain;
using Core.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF
{
    public class EFCommentRepository : ICommentRepository
    {
        private FysioDBContext context;
        public EFCommentRepository(FysioDBContext db)
        {
            this.context = db;
        }

        public int AddComment(Comment comment)
        {
            context.comments.Add(comment);
            context.SaveChanges();
            return comment.commentId;
        }
        public Comment FindCommentById(int id)
        {
            return context.comments.Where(c => c.commentId == id).FirstOrDefault();
        }

        public int DeleteComment(int id)
        {
            context.Remove(FindCommentById(id));
            context.SaveChanges();
            return id;
        }

        public List<Comment> GetCommentsByFileId(int patientFileId)
        {
            return context.comments.Where(c => c.patientFileId == patientFileId).ToList();
        }
    }
}
