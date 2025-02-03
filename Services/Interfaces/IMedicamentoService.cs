using BlazorRepoEstoque.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorRepoEstoque.Services.Interfaces
{
    public interface IMedicamentoService
    {
        Task<List<Medicamento>> GetAllMedicamentosAsync();
        Task<Medicamento> GetMedicamentoByIdAsync(int id);
        Task<List<Medicamento>> GetMedicamentosByNomeAsync(string nome);
        Task AddMedicamentoAsync(Medicamento medicamento);
        Task UpdateMedicamentoAsync(Medicamento medicamento);
        Task DeleteMedicamentoAsync(int id);
    }
}
