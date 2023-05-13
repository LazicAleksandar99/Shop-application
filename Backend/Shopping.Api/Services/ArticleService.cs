using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.DTO.ArticleDTO;
using Shopping.Api.DTO.UserDTO;
using Shopping.Api.Interfaces.IRepositories;
using Shopping.Api.Interfaces.IServices;
using Shopping.Api.Models;

namespace Shopping.Api.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ArticleService(IArticleRepository articleRepository, IUserRepository userRepository,IMapper mapper) 
        {
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> Create(CreateArticleDto newArticle)
        {
            if (!await _userRepository.DoesSellerExist(newArticle.UserId))
                return false;
            var article = _mapper.Map<Article>(newArticle);
            return await _articleRepository.Create(article);
        }

        public async Task<bool> Update(UpdateArticleDto oldArticle)
        {
            if (!await _userRepository.DoesSellerExist(oldArticle.UserId))
                return false;
            var article = _mapper.Map<Article>(oldArticle);
            return await _articleRepository.Update(article);
        }

        //provjera da li je trenutni artikl u dostavi...
        //uh i gdje ga sve brsiem
        public async Task<bool> Delete(int id, int seller)
        {
            if (!await _userRepository.DoesSellerExist(seller))
                return false;

            return await _articleRepository.Delete(id, seller);
        }

    }
}
