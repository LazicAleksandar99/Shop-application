﻿using Backend.Helpers;
using Shopping.Api.Interfaces.IRepositories;
using Shopping.Api.Models;

namespace Shopping.Api.Data.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DataContext _data;

        public ArticleRepository(DataContext data)
        {
            _data = data;
        }

        public async Task<bool> Create(Article newArticle)
        {
            _data.Articles.Add(newArticle);
            await _data.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Article newArticle)
        {
            var existingArticle = await _data.Articles.FindAsync(newArticle.Id);

            if(existingArticle != null && existingArticle.UserId == newArticle.UserId) 
            {

                _data.Articles.Update(newArticle);
                await _data.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete (int id, int sellerId)
        {
            var existingArticle = await _data.Articles.FindAsync(id);

            if (existingArticle != null && existingArticle.UserId == sellerId)
            {

                _data.Articles.RemoveRange(existingArticle);
                await _data.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}