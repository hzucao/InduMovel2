using InduMovel.Models;
namespace InduMovel.Repositories.Interfaces
{
    public interface IMovelRepository
    {
        IEnumerable<Movel> Moveis { get; }
        IEnumerable<Movel> MoveisEmProducao { get; }
        Movel GetMovelById(int movelId);
    }
}