using AspNetCore.Reporting;
using BlazorRepoEstoque.Data;
using BlazorRepoEstoque.Models;
using BlazorRepoEstoque.Shared.SharedState;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BlazorRepoEstoque.Controllers
{
    [Route("[controller]")]
    [ApiController]


    public class ReportController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ManagerStateAppService _managerStateAppService;

        public ReportController(IWebHostEnvironment webHostEnvironment, ManagerStateAppService managerStateAppService)
        {
            this.webHostEnvironment = webHostEnvironment;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _managerStateAppService = managerStateAppService;
        }

        [HttpPost]
        [Route("PrintReport")]
        public IActionResult PrintReport([FromBody] ReportDataRequestModel dadosRelatorio) // Receba os dados no corpo
        {
            // Agora use os dados recebidos no parametro dadosRelatorio
            var dt = new DataTable();
            dt = ObjForDataTable(dadosRelatorio.ListaReposicao); // Use a lista do parametro

            string mimetype = "";
            int extension = 1;
            var path = $"{webHostEnvironment.WebRootPath}\\report\\ReportReposicaoEstoque.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Farmacia", dadosRelatorio.FarmaciaDestino); // Use a farmacia do parametro

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSetReposicaoEstoque", dt);
            var report = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);

            return File(report.MainStream, "Application/pdf");
        }

        private DataTable ObjForDataTable(List<ReposicaoEstoque> list)
        {
            var dt = new DataTable();
            dt.Columns.Add("CodigoMV");
            dt.Columns.Add("Medicamento");
            dt.Columns.Add("Unidade");
            dt.Columns.Add("Reposicao");

            DataRow dr;
            foreach (var item in list)
            {
                dr = dt.NewRow();
                dr["CodigoMV"] = item.CodigoMV;
                dr["Medicamento"] = item.Medicamento;
                dr["Unidade"] = item.Unidade;
                dr["Reposicao"] = item.Reposicao;

                dt.Rows.Add(dr);
            }
            return dt;
        }
    }


}