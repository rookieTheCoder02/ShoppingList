using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ShoppingList.Data;
using ShoppingList.Models.Domain;

namespace ShoppingList.Models.Repository
{
    public class GoodsRepository : IGoodsRepository
    {
        private readonly ShoppingListDbContext shoppingListDbContext;

        public GoodsRepository(ShoppingListDbContext shoppingListDbContext)
        {
            this.shoppingListDbContext = shoppingListDbContext;
        }

        public async Task<Goods> AddGoodsAsync(Goods goods)
        {
            goods.Id = Guid.NewGuid();
            await shoppingListDbContext.Goods.AddAsync(goods);
            await shoppingListDbContext.SaveChangesAsync();
            return goods;
        }

        public async Task<Goods> DeleteGoodsAsync(Guid id)
        {
            var request = await shoppingListDbContext.Goods.FirstOrDefaultAsync(x => x.Id == id);
            if(request == null)
            {
                return null;
            }
            shoppingListDbContext.Goods.Remove(request);
            await shoppingListDbContext.SaveChangesAsync();
            return request;
        }

        public async Task<IEnumerable<Goods>> GetAllGoodsAsync()
        {
            return await shoppingListDbContext.Goods.ToListAsync();
        }

        public async Task<Goods> GetGoodsAsyncById(Guid id)
        {
            return await shoppingListDbContext.Goods.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Goods>> GetGoodsAsyncByName(string name)
        {
            return await shoppingListDbContext.Goods.Where(x => x.Name == name).ToListAsync();

        }

        public async Task<Goods> UpdateGoodsAsync(Guid id, Goods goods)
        {
            var request = await shoppingListDbContext.Goods.FirstOrDefaultAsync(x => x.Id == id);
            if(goods.Name == "string" || goods.Price == 0)
            {
                request.Name = request.Name;
                request.Price = request.Price;
                return request;

            }
            request.Name = goods.Name;
            request.Price = goods.Price;
            await shoppingListDbContext.SaveChangesAsync();
            return request;
        }
    }
}
