using CityManagerApi.Data.Abstract;
using CityManagerApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityManagerApi.Data.Concretes
{
    public class AppRepos : IAppRepository
    {
        private readonly CityManagerDbContext _context;

        public AppRepos(CityManagerDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            await Task.Run(() =>
            {
                _context.Remove(entity);
            });
        }

        public async Task<List<City>> GetCityByUserIdAsync(int userId)
        {
            return await _context.Cities.Include(i=>i.Images).Where(c=>c.UserId==userId).ToListAsync();
        }

        public async Task<City> GetCityByIdAsync(int cityId)
        {
           return await _context.Cities
                .Include(i=>i.Images)
                .FirstOrDefaultAsync(c=>c.Equals(cityId));

        }

        public async Task<CityImage> GetCityImageByCityIdAsync(int cityId)
        {
            return await _context.Images.Include(c=>c.City).FirstOrDefaultAsync(p=>p.CityId==cityId);
        }

        public async Task<CityImage> GetCityImageByIdAsync(int id)
        {
            return await _context.Images.Include(c => c.City).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await Task.Run(async () =>
            {
                return (await _context.SaveChangesAsync()) > 0;
            });
        }
    }
}
