using ShoppingList.Models.Domain;

namespace ShoppingList.Models.Repository
{
    public interface IGoodsRepository
    {
        Task<IEnumerable<Goods>> GetAllGoodsAsync();
        Task<Goods> GetGoodsAsyncById(Guid id);
        Task<IEnumerable<Goods>> GetGoodsAsyncByName(string name);
        Task<Goods> AddGoodsAsync(Goods goods);
        Task<Goods> DeleteGoodsAsync(Guid id);
        Task<Goods> UpdateGoodsAsync(Guid id, Goods goods);
    }
}
