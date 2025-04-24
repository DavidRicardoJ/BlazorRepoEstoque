using BlazorRepoEstoque.Models;
using BlazorRepoEstoque.Services.Interfaces;
using BlazorRepoEstoque.Shared.SharedState;
using Microsoft.AspNetCore.Components.Forms;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRepoEstoque.Services
{
    public class ImportExcelService
    {
        private readonly ManagerStateAppService _managerStateAppService;
        private readonly IFiltrosServices _filtrosServices;
        private readonly IGroupService _grupoService;

        public ImportExcelService(ManagerStateAppService managerStateAppService, IFiltrosServices filtrosServices, IGroupService grupoService)
        {
            _managerStateAppService = managerStateAppService;
            _filtrosServices = filtrosServices;
            _grupoService = grupoService;
        }

        private ReposicaoEstoque repo;
        private List<ReposicaoEstoque> listRepo = new();



        public int _codigoEstoque { get; set; }
        public string _farmacia { get; set; }
        public string _periodoImportacaoExcel { get; set; }

        public async Task<List<ReposicaoEstoque>> ReadExcel(MemoryStream stream2)
        {
            try
            {
                List<string> rowList = new List<string>();
                ISheet sheet;
                //using (var stream = new FileStream("Test.xlsx", FileMode.Open))
                await using (stream2)
                {
                    stream2.Position = 0;
                    XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream2);
                    sheet = xssWorkbook.GetSheetAt(0);
                    IRow farmaciaOrigem = sheet.GetRow(0);
                    IRow periodoImportado = sheet.GetRow(1);
                    if (!string.IsNullOrEmpty(farmaciaOrigem.GetCell(1).ToString()))
                    {
                        _managerStateAppService.GrupoEstoque.EstoqueDestino = int.Parse(farmaciaOrigem.GetCell(1).ToString());
                        _managerStateAppService.GrupoEstoque.FarmaciaDestino= farmaciaOrigem.GetCell(2).ToString();
                        _managerStateAppService.periodoImportacaoExcel = periodoImportado.GetCell(4).ToString();
                    }

                    IRow headerRow = sheet.GetRow(3);
                    int cellCount = headerRow.LastCellNum;
                    int k = 0;
                    if (cellCount == 16) k = 1;

                    for (int i = (sheet.FirstRowNum + 4); i <= sheet.LastRowNum; i++) //+3
                    {
                        repo = new();
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                            {
                                if (!string.IsNullOrEmpty(row.GetCell(j).ToString()) && !string.IsNullOrWhiteSpace(row.GetCell(j).ToString()))
                                {
                                    if (j == 0 + k) repo.CodigoMV = row.GetCell(j).ToString();
                                    if (j == 1 + k) repo.Medicamento = row.GetCell(j).ToString();
                                    if (j == 2 + k) repo.Unidade = row.GetCell(j).ToString();
                                    if (j == 3 + k) repo.UltimoMovimento = DateTime.ParseExact(row.GetCell(j).ToString().Replace(".", "/"), "dd/MM/yyyy", null);
                                    if (j == 5 + k) repo.ConsumoTotal = float.Parse(row.GetCell(j).ToString());
                                    if (j == 6 + k) repo.EstoqueAtual = float.Parse(row.GetCell(j).ToString());
                                    if (j == 7 + k) repo.DiasDeEstoque = int.Parse(row.GetCell(j).ToString());
                                    if (j == 13 + k) repo.Especie = row.GetCell(j).ToString();
                                }
                            }
                        }
                        listRepo.Add(repo);
                    }
                }

                for (int i = 0; i < listRepo.Count; i++)
                {
                    listRepo[i].Id = i;
                }

                return listRepo;

            }
            catch (Exception e)
            {
                var x = new ReposicaoEstoque() { Medicamento = e.Message };
                listRepo.Add(x);
                return listRepo;

            }

        }

        public async Task GetFile(InputFileChangeEventArgs e)
        {
            try
            {
                // 1. Ler o arquivo Excel para um MemoryStream
                using var inputStream = e.File.OpenReadStream();
                using var memoryStream = new MemoryStream();
                await inputStream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                // Armazenar o stream no serviço de estado
                _managerStateAppService.stream2 = new MemoryStream(memoryStream.ToArray());

                // 2. Interpretar o conteúdo do Excel
                var listRepo = await ReadExcel(_managerStateAppService.stream2);
                if (listRepo == null)
                {
                    NotificarErroImportacao(true);
                    return;
                }

                NotificarErroImportacao(false);

                // 3. Aplicar filtros e ajustes
                listRepo = _filtrosServices.ReordenarID(listRepo);
                listRepo = listRepo.OrderBy(x => x.Medicamento).ToList();
                listRepo = await AddGrupo(listRepo);

                // 4. Atualizar o estado da aplicação
                _managerStateAppService.SetListImportadaOriginal(listRepo);
                _managerStateAppService.SetListReposicaoComFiltro(_filtrosServices.CloneListaImportada()); 
            }
            catch
            {
                NotificarErroImportacao(true);
            }
        }

        // Método auxiliar para notificar e definir estado de erro
        private void NotificarErroImportacao(bool erro)
        {
            _managerStateAppService.erroImport = erro;
            _managerStateAppService.NotifyChanged();
        }


        private async Task<List<ReposicaoEstoque>> AddGrupo(List<ReposicaoEstoque> list)
        {
            var grupos = await _grupoService.GetAllGrupoProdutos();

            if (grupos is not null)
            {
                foreach (var item in list)
                {
                    var nomeGrupo = grupos.FirstOrDefault(x => x.Produto.IdMedicamento.ToString() == item.CodigoMV);

                    if (nomeGrupo is not null)
                    {
                        item.NomeGrupo = nomeGrupo.Grupo.GroupName;
                    }
                    else
                    {
                        item.NomeGrupo = "PRODUTOS SEM GRUPO";
                    }
                }
            }

            return list;
        }

    }
}
