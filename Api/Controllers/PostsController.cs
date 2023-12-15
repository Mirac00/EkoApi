using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Services;
using Api.Models.Posts;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public IActionResult Create(CreatePostRequest model)
        {
            var userId = GetUserIdFromToken();
            _postService.Create(model, userId);
            return Ok(new { message = "Post created successfully" });
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var posts = _postService.GetAll();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var post = _postService.GetById(id);
            return Ok(post);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdatePostRequest model)
        {
            var userId = GetUserIdFromToken();
            _postService.Update(id, model, userId);
            return Ok(new { message = "Post updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userId = GetUserIdFromToken();
            _postService.Delete(id, userId);
            return Ok(new { message = "Post deleted successfully" });
        }

        private int GetUserIdFromToken()
        {
            var user = (Api.Entities.User)HttpContext.Items["User"];
            var userId = user?.Id ?? 0; // Użyj właściwości Id lub odpowiedniej właściwości, która przechowuje identyfikator użytkownika
            return userId;
        }
    }
}
