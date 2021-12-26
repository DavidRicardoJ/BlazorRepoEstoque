using BlazorRepoEstoque.Models;
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
       
        private ReposicaoEstoque repo;
        private List<ReposicaoEstoque> listRepo = new();


       

        public async Task<List<ReposicaoEstoque>> ReadExcel(MemoryStream stream2)
        {
            List<string> rowList = new List<string>();
            ISheet sheet;
            //using (var stream = new FileStream("Test.xlsx", FileMode.Open))
            using (stream2)
            {
                stream2.Position = 0;
                NPOI.XSSF.UserModel.XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream2);
                sheet = xssWorkbook.GetSheetAt(0);
                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;


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
                                if (j == 0) repo.CodigoMV = row.GetCell(j).ToString();
                                if (j == 1) repo.Medicamento = row.GetCell(j).ToString();
                                if (j == 2) repo.Unidade = row.GetCell(j).ToString();
                                if (j == 3) repo.UltimoMovimento = DateTime.Parse(row.GetCell(j).ToString());
                                if (j == 4) repo.ConsumoTotal = float.Parse(row.GetCell(j).ToString());
                                if (j == 6) repo.EstoqueAtual = float.Parse(row.GetCell(j).ToString());
                                if (j == 7) repo.DiasDeEstoque = int.Parse(row.GetCell(j).ToString());
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
    }
}
