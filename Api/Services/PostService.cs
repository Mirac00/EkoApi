using System.Collections.Generic;
using Api.Entities;
using Api.Helpers;
using Api.Models.Posts;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public interface IPostService
    {
        void Create(CreatePostRequest model, int userId);
        IEnumerable<Post> GetAll();
        Post GetById(int id);
        void Update(int id, UpdatePostRequest model, int userId);
        void Delete(int id, int userId);
        IEnumerable<Post> GetAllWithUser();
    }

    public class PostService : IPostService
    {
        private readonly DataContext _context;

        public PostService(DataContext context)
        {
            _context = context;
        }

        public void Create(CreatePostRequest model, int userId)
        {
            var post = new Post
            {
                Title = model.Title,
                Content = model.Content,
                UserId = userId
            };

            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public IEnumerable<Post> GetAllWithUser()
        {
            return _context.Posts.Include(p => p.User);
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts;
        }

        public Post GetById(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
                throw new KeyNotFoundException("Post not found");

            return post;
        }

        public void Update(int id, UpdatePostRequest model, int userId)
        {
            var post = GetPost(id);

            // Ensure that the user updating the post is the post owner
            if (post.UserId != userId)
                throw new UnauthorizedAccessException("You are not authorized to update this post");

            post.Title = model.Title;
            post.Content = model.Content;

            _context.Posts.Update(post);
            _context.SaveChanges();
        }

        public void Delete(int id, int userId)
        {
            var post = GetPost(id);

            // Ensure that the user deleting the post is the post owner
            if (post.UserId != userId)
                throw new UnauthorizedAccessException("You are not authorized to delete this post");

            _context.Posts.Remove(post);
            _context.SaveChanges();
        }

        private Post GetPost(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
                throw new KeyNotFoundException("Post not found");

            return post;
        }
    }
}
