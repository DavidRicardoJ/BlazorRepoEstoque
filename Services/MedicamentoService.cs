using BlazorRepoEstoque.Data;
using BlazorRepoEstoque.Models;
using BlazorRepoEstoque.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRepoEstoque.Services
{
    public class MedicamentoService : IMedicamentoService
    {
        private readonly AppDbContext _context;

        public MedicamentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Medicamento>> GetAllMedicamentosAsync()
        {
            return await _context.Medicamentos.ToListAsync();
        }

        public async Task<Medicamento> GetMedicamentoByIdAsync(int id)
        {
            return await _context.Medicamentos.FindAsync(id);
        }

        public async Task<List<Medicamento>> GetMedicamentosByNomeAsync(string nome)
        {
            return await _context.Medicamentos
                .Where(m => m.NomeMedicamento.Contains(nome))
                .ToListAsync();
        }

        public async Task AddMedicamentoAsync(Medicamento medicamento)
        {
            _context.Medicamentos.Add(medicamento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMedicamentoAsync(Medicamento medicamento)
        {
            _context.Entry(medicamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMedicamentoAsync(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento != null)
            {
                _context.Medicamentos.Remove(medicamento);
                await _context.SaveChangesAsync();
            }

        }
    }
}
