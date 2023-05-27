using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.Data.Repositories;
using Shopping.Api.DTO.ArticleDTO;
using Shopping.Api.DTO.OrderDTO;
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

        public async Task<List<GetAllArticlesDto>> GetAllArticles()
        {
            var result = await _articleRepository.GetAllArticles();
            var returnValue = _mapper.Map<List<GetAllArticlesDto>>(result);
            return returnValue;
        }
        public async Task<List<GetSellerArticlesDto>> GetSellerArticles(int id)
        {
            if (!await _userRepository.DoesUserExist(id))
                return null;


            var result = await _articleRepository.GetSellerArticles(id);
            var history = _mapper.Map<List<GetSellerArticlesDto>>(result);
            return history;
        }

    }
}
