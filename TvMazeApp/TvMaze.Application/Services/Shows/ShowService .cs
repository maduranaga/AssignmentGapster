using CodeFirst.Common.Expections;
using TvMaze.Application.Interfaces.ShowGenres;
using TvMaze.Application.Interfaces.Shows;
using TvMaze.Application.Interfaces.TvMaze;
using TvMaze.Application.Interfaces.UniOfWork;
using TvMaze.Domain.Entities;

namespace TvMaze.Application.Services.Shows
{
    public class ShowService : IShowService
    {
        private readonly ITvMazeApiService _tvMazeApiService;
        private readonly IShowRepository _showRepository;
        private readonly IShowGenreRepository _showGenreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ShowService(
            ITvMazeApiService tvMazeApiService,
            IShowRepository showRepository,
            IShowGenreRepository showGenreRepository,
            IUnitOfWork unitOfWork)
        {
            _tvMazeApiService = tvMazeApiService;
            _showRepository = showRepository;
            _showGenreRepository = showGenreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddShowIfNotExistsAsync(Show show, List<string> showGenre, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var showFromApi = await _tvMazeApiService.GetShowByNameAsync(show.Name).ConfigureAwait(false);
                var existShow = showFromApi.Where(s => s.Name.ToLower()==show.Name.ToLower()).FirstOrDefault();
                if (existShow !=null)
                {
                    throw new BadRequestException("Show is Already Exist");
                }
                int showid = await _showRepository.AddAsync(show, cancellationToken).ConfigureAwait(false);

                if (showGenre != null && showGenre.Any())
                {
                    AddGeneres(show.Id, showGenre, cancellationToken);
                }

                return true;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async void AddGeneres(int id, List<string> showGenres, CancellationToken cancellationToken)
        {
            var addGenere = showGenres.Select(genreName => new ShowGenre
            {
                ShowId = id,
                Genre = genreName
            }).ToList();
            await _showGenreRepository.AddRangeAsync(addGenere, cancellationToken);
        }

        public async Task<bool> UpdateShowAsync(Show show, List<string> showGenres, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                await _showGenreRepository.DeleteByShowIdAsync(show.Id, cancellationToken).ConfigureAwait(false);
                AddGeneres(show.Id, showGenres, cancellationToken);
                bool updated = await _showRepository.UpdateAsync(show, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return updated;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> DeleteShowAsync(int showId, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                await _showGenreRepository.DeleteByShowIdAsync(showId, cancellationToken);
                return await _showRepository.DeleteAsync(showId, cancellationToken);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task<Show?> GetShowByIdAsync(int showId, CancellationToken cancellationToken)
        {
            return await _showRepository.GetByIdAsync(showId, cancellationToken);
        }

        public async Task<List<Show>> GetShowListAsync(int pageSize, int pageNo, CancellationToken cancellationToken)
        {
            return await _showRepository.ShowPageAsync(pageNo, pageSize, cancellationToken);
        }

        public async Task<List<Show>> GetShowListsByNameAync(string name, int pageSize, int pageNo, CancellationToken cancellationToken)
        {
            return await _showRepository.ShowFilterByNameAsync(name, pageNo, pageSize, cancellationToken);
        }
    }
}
