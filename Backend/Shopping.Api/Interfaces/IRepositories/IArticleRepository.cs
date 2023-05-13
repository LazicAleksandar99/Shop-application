using Shopping.Api.Models;

namespace Shopping.Api.Interfaces.IRepositories
{
    public interface IArticleRepository
    {
        public Task<bool> Create(Article newArticle);
        public Task<bool> Update(Article newArticle);
        public Task<bool> Delete(int id, int sellerId);

    }
}
