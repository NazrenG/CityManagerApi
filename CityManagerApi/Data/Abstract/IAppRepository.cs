using CityManagerApi.Entities;

namespace CityManagerApi.Data.Abstract
{
    public interface IAppRepository
    {
        Task AddAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
        Task<bool> SaveAllAsync();

        Task<List<City>> GetCitiesAsync(int userId);
        Task<CityImage> GetPhotoByCityIdAsync(int cityId);
        Task<City> GetCityByIdAsync(int cityId);
        Task<CityImage> GetPhotoByIdAsync(int photoId);

    }
}
