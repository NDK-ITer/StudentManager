using Application.Models.ModelsOfComment;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public interface ICommentService
    {
        Tuple<string, Comment?> Add(string idUser, string idPost ,AddCommentModel a);
        Tuple<string, bool?> Remove(string idUser, string idCmt);
    }
    public class CommentService:ICommentService
    {
        private readonly IUnitOfWork unitOfWork;
        public CommentService(UserDbContext context, IMemoryCache cache)
        {
            unitOfWork = new UnitOfWork(context, cache);
        }

        public Tuple<string, Comment?> Add(string idUser, string idPost, AddCommentModel a)
        {
            if (a == null || idUser.IsNullOrEmpty() || idPost.IsNullOrEmpty()) return new Tuple<string, Comment?>("parameter is null", null);
            var cmt = new Comment()
            {
                Id = Guid.NewGuid().ToString(),
                Content = a.Content,
                UserId = idUser,
                PostId = idPost,
                DateComment = DateTime.Now,
            };
            unitOfWork.commentRepository.Add(cmt);
            unitOfWork.SaveChange();
            return new Tuple<string, Comment?>("add successful", cmt);
        }

        public Tuple<string, bool?> Remove(string idUser,string idCmt)
        {
            if (idCmt.IsNullOrEmpty()) return new Tuple<string, bool?>("parameter is null", false);
            var cmt = unitOfWork.commentRepository.GetById(idCmt);
            if (cmt == null) return new Tuple<string, bool?>("not found cmt!", false);
            if (cmt.Post.Faculty.ListAdmin.Find(a => a.Id == idUser) == null) return new Tuple<string, bool?>("you cant delete this cmt", false);
            unitOfWork.commentRepository.Remove(cmt);
            return new Tuple<string, bool?>("delete successful", true);
        }
    }
}
