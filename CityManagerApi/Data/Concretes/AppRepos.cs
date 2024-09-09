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

        public async Task<List<City>> GetCitiesAsync(int userId)
        {
            var cities = await _context
                .Cities
                .Include(c => c.CityImages)
                .Where(c => c.UserId == userId)
                .ToListAsync();
            return cities;
        }

        public async Task<City> GetCityByIdAsync(int cityId)
        {
            var city = await _context
                .Cities
                .Include(c => c.CityImages)
                .FirstOrDefaultAsync(c => c.Id == cityId);
            return city;
        }

        public async Task<CityImage> GetPhotoByCityIdAsync(int cityId)
        {
            var img = await _context.CityImages.Include(c=>c.City).FirstOrDefaultAsync(c => c.Id == cityId);
            return img;
        }

        public async Task<CityImage> GetPhotoByIdAsync(int photoId)
        {
            var img = await _context.CityImages.FirstOrDefaultAsync(c => c.Id==photoId);
            return img;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await Task.Run(async () =>
            {
                return (await _context.SaveChangesAsync()) > 0;
            });
        }
    }
}
