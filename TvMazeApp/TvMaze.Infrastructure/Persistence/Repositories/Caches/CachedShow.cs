using Microsoft.Extensions.Caching.Memory;
using TvMaze.Application.Interfaces.Shows;
using TvMaze.Domain.Entities;

namespace TvMaze.Infrastructure.Persistence.Repositories.Caches
{
    public class CachedShow : IShowRepository
    {
        private readonly ShowRepository _decoratedShowRepository;
        private readonly IMemoryCache _memoryCache;
        public CachedShow(ShowRepository showRepository, IMemoryCache memoryCache)
        {
            _decoratedShowRepository=showRepository;
            _memoryCache=memoryCache;
        }

        public async Task<int> AddAsync(Show entity, CancellationToken cancellationToken = default)
        {
            var showId = await _decoratedShowRepository.AddAsync(entity, cancellationToken);

            string key = $"Show_{showId}";
            _memoryCache.Set(key, entity);

            return showId;
        }

        public async Task AddRangeAsync(IEnumerable<Show> entities, CancellationToken cancellationToken = default)
        {
            await _decoratedShowRepository.AddRangeAsync(entities, cancellationToken);

            foreach (var show in entities)
            {
                string key = $"Show_{show.Id}";
                _memoryCache.Set(key, show);
            }
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            bool result = await _decoratedShowRepository.DeleteAsync(id, cancellationToken);

            if (result)
            {
                string key = $"Show_{id}";
                _memoryCache.Remove(key);
            }

            return result;
        }

        public async Task<List<Show>> ShowPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _decoratedShowRepository.ShowPageAsync(pageNumber, pageSize, cancellationToken);
        }

        public async Task<Show> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            string key = $"Show_{id}";

            return await _memoryCache.GetOrCreateAsync(key, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return await _decoratedShowRepository.GetByIdAsync(id, cancellationToken);
            });
        }

        public async Task<List<Show>> ShowFilterByNameAsync(string name, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var shows = await _decoratedShowRepository.ShowFilterByNameAsync(name, pageNumber, pageSize, cancellationToken);
            return shows;
        }

        public async Task<bool> UpdateAsync(Show entity, CancellationToken cancellationToken = default)
        {
            bool updated = await _decoratedShowRepository.UpdateAsync(entity, cancellationToken);

            if (updated)
            {
                string key = $"Show_{entity.Id}";
                _memoryCache.Set(key, entity);
            }

            return updated;
        }
    }
}

