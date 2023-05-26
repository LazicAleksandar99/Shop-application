using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.DTO.ArticleDTO;
using Shopping.Api.DTO.UserDTO;
using Shopping.Api.Interfaces.IServices;
using Shopping.Api.Services;

namespace Shopping.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost("create")]
        //Seller
        public async Task<IActionResult> Create(CreateArticleDto newArticle)
        {
            if (!await _articleService.Create(newArticle))
                return BadRequest("User not valid");
            return Ok();
        }

        //URADITI
        //Seller
        [HttpPatch("update")]
        public async Task<IActionResult> Update(UpdateArticleDto updatedArticle)
        {
            if (!await _articleService.Update(updatedArticle))
                return BadRequest("Updated article not valid");
            return Ok();
        }

        //URADITI
        //Seller
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id, int sellerId)
        {
            if (!await _articleService.Delete(id, sellerId))
                return BadRequest("Invalid id-s");
            return Ok();
        }

        //get all articles
        //get only articles for seller
    }
}
