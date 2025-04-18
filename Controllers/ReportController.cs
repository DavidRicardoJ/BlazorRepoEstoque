﻿using AspNetCore.Reporting;
using BlazorRepoEstoque.Models;
using BlazorRepoEstoque.Services;
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
        private readonly ListSharedService listSharedService;

        public ReportController(IWebHostEnvironment webHostEnvironment, ListSharedService listSharedService)
        {
            this.webHostEnvironment = webHostEnvironment;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            this.listSharedService = listSharedService;
        }

        [HttpGet]
        [Route("PrintReport")]
        public IActionResult PrintReport()
        {
            ReportData reportData = new();

            var dt = new DataTable();
            dt = ObjForDataTable(listSharedService.GetListReposicao());

            string mimetype = "";
            int extension = 1;
            var path = $"{webHostEnvironment.WebRootPath}\\report\\ReportReposicaoEstoque.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Farmacia", listSharedService.Farmacia);

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