using Application.Models.ModelsOfPost;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public interface IPostService
    {
        Tuple<string, bool> ApprovePost(string iduser, string idPost);
        Tuple<string, Post?> Add(AddPostModel a);
        Tuple<string, Post?> Update( string idUser,EditPostModel e);
        Tuple<string, bool> Remove(string idUser, string idPost);
        Tuple<string, Post?> GetById(string id);
        Tuple<string, List<Post?>> GetPostOfFaculty(string userId, string facultyId);
        Tuple<string, List<Post?>?> GetAll(string userId);
        Tuple<string, List<Post?>?> GetPublic();
    }
    public class PostService:IPostService
    {
        private readonly IUnitOfWork unitOfWork;
        public PostService(UserDbContext context, IMemoryCache cache)
        {
            unitOfWork = new UnitOfWork(context, cache);
        }

        public Tuple<string, bool> ApprovePost(string idUser, string idPost)
        {
            if (idUser.IsNullOrEmpty() || idPost.IsNullOrEmpty()) return new Tuple<string, bool>("parameter is null", false);
            var post = unitOfWork.postRepository.GetById(idPost);
            if (post == null) return new Tuple<string, bool>($"not found with idPost: {idPost}", false);
            if (post.Faculty.IsDeleted == true) return new Tuple<string, bool>("Faculty of this post was deleted", false);
            if (post.Faculty.ListAdmin.Find(a => a.Id == idUser) != null) return new Tuple<string, bool>($"user with id {idUser} is not Manager of {post.Faculty.Name}", false);
            post.IsApproved = true;
            unitOfWork.postRepository.Update(post);
            unitOfWork.SaveChange();
            return new Tuple<string, bool>($"Aapprove post {post.Title} is successful", true);
        }

        public Tuple<string, Post?> Add(AddPostModel a)
        {
            if (a == null) return new Tuple<string, Post?>("parameter is null", null);
            var faculty = unitOfWork.facultyRepository.GetById(a.FacultyId);
            if (faculty == null) return new Tuple<string, Post?>($"not Fauculty with id: {a.FacultyId}", null);
            if (faculty.IsOpen == false) return new Tuple<string, Post?>($"{faculty.Name} is not open!",null);
            var newPost = new Post()
            {
                Id = Guid.NewGuid().ToString(),
                FacultyId = a.FacultyId,
                Title = a.Title,
                DatePost = DateTime.Now,
                IsApproved = false,
                IsChecked = false,
                LinkDocument = a.LinkDocument,
                AvatarPost = a.AvatarPost,
            };
            unitOfWork.postRepository.Add(newPost);
            unitOfWork.SaveChange();
            return new Tuple<string, Post?>("Add Successful", newPost);
        }

        public Tuple<string, List<Post?>?> GetAll(string userId)
        {
            if (userId.IsNullOrEmpty()) return new Tuple<string, List<Post?>?> ("userId not null", null);
            var user = unitOfWork.userRepository.GetById(userId);
            if (user == null) return new Tuple<string, List<Post?>?>($"not found with userId: {userId}", null);
            if (user.Role.Name != "ADMIN") return new Tuple<string, List<Post?>?>("you cant do this", null);
            var allPost = unitOfWork.postRepository.GetAll();
            if (allPost.IsNullOrEmpty()) return new Tuple<string, List<Post?>?>("no post", null);
            return new Tuple<string, List<Post?>?>("", allPost);
        }

        public Tuple<string, Post?> GetById(string id)
        {
            if (id.IsNullOrEmpty()) return new Tuple<string, Post?>($"parameter is null", null);
            var post = unitOfWork.postRepository.GetById(id);
            if (post == null) return new Tuple<string, Post?>($"not found with id: {id}", null);
            return new Tuple<string, Post?>("", post);
        }

        public Tuple<string, List<Post?>?> GetPublic()
        {
            var postPublic = unitOfWork.postRepository.Find(p => p.IsApproved == true);
            if (postPublic.IsNullOrEmpty()) return new Tuple<string, List<Post?>?>("no public post", null);
            return new Tuple<string, List<Post?>?>("", postPublic);
        }

        public Tuple<string, bool> Remove(string idUser, string idPost)
        {
            if (idUser.IsNullOrEmpty() || idPost.IsNullOrEmpty()) return new Tuple<string, bool>("parameter is null", false);
            var post = unitOfWork.postRepository.GetById(idPost);
            if (post == null) return new Tuple<string, bool>($"not found with idPost: {idPost}", false);
            if (post.Faculty.IsDeleted == true) return new Tuple<string, bool>("Faculty of this post was deleted", false);
            if (post.Faculty.ListAdmin.Find(a => a.Id == idUser) != null) return new Tuple<string, bool>($"user with id {idUser} is not Admin", false);
            unitOfWork.postRepository.Remove(post);
            unitOfWork.SaveChange();
            return new Tuple<string, bool>($"Aapprove post {post.Title} is successful", true);
        }

        public Tuple<string, Post?> Update(string idUser, EditPostModel e)
        {
            if (idUser.IsNullOrEmpty() || e == null) return new Tuple<string, Post?>("parameter is null", null);
            var post = unitOfWork.postRepository.GetById(e.Id);
            if (post == null) return new Tuple<string, Post?>($"not found post with id: {e.Id}", null);
            if (post.Faculty.ListAdmin.Find(a => a.Id == idUser) == null) return new Tuple<string, Post?>("you cant update this post", null);
            post.Title = e.Title;
            unitOfWork.postRepository.Update(post);
            unitOfWork.SaveChange();
            return new Tuple<string, Post?>("update successful", post);
        }

        public Tuple<string, List<Post?>?> GetPostOfFaculty(string userId, string facultyId)
        {
            if (userId.IsNullOrEmpty() || facultyId.IsNullOrEmpty()) return new Tuple<string, List<Post?>?>("parameter is null", null);
            var user = unitOfWork.userRepository.GetById(userId);
            var faculty = unitOfWork.facultyRepository.GetById(facultyId);
            if (!faculty.ListAdmin.Contains(user)) return new Tuple<string, List<Post?>?>("you cant do it", null);
            return new Tuple<string, List<Post?>?>("", faculty.ListPost);
        }
    }
}
