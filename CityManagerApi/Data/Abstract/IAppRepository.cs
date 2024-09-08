using CityManagerApi.Entities;

namespace CityManagerApi.Data.Abstract
{
    public interface IAppRepository
    {
        Task<List<City>> GetCityByUserIdAsync(int userId);
        Task<CityImage> GetCityImageByIdAsync(int id);
        Task<CityImage> GetCityImageByCityIdAsync(int cityId);
        Task<City> GetCityByIdAsync(int cityId);

        Task AddAsync<T>(T entity) where T: class;
        Task DeleteAsync<T>(T entity)where T: class;
        Task<bool> SaveChangesAsync();

    }
}
