using BlazorRepoEstoque.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BlazorRepoEstoque.Data;
using Microsoft.JSInterop;

namespace BlazorRepoEstoque.Services
{
    public class CreateRobot
    {
        public string GetScriptRobot()
        {
            return BodyRobot(ReportDataReposicao.GetListReposicao(), ReportDataReposicao.GetDadosScript());
        }


        private string BodyRobot(List<ReposicaoEstoque> listReposicao, LoginUsuarioMV login)
        {
            if (listReposicao == null || login == null) return null;


            StringBuilder sb = new();

            sb.AppendLine("{ ");
            sb.AppendLine(" \"id\":\"cc06d863-a9fc-42ea-a527-e0ec8ddfae97\", ");
            sb.AppendLine(" \"version\": \"2.0\", ");
            sb.AppendLine(" \"name\": \"testeMV\", ");
            sb.AppendLine(" \"url\": \"http://srv-soulmvblc01.hbase.local\", ");
            sb.AppendLine(" \"tests\": [{ ");
            sb.AppendLine("  \"id\": \"07699077-e549-4b61-a850-fdc6bd300095\", ");
            sb.AppendLine("  \"name\": \"testeMV01\", ");
            sb.AppendLine("  \"commands\": [{ ");
            sb.AppendLine("  \"id\": \"c1a468ef-986a-4f04-abef-fadead73ff5a\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"open\", ");
            sb.AppendLine("  \"target\": \"/mvautenticador-cas/login?service=http%3A%2F%2Fsrv-soulmvblc01.hbase.local%3A80%2Fsoul-mv%2Fcas\", ");
            sb.AppendLine("  \"targets\": [], ");
            sb.AppendLine("  \"value\": \"\" ");
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"8acc1158-7cc0-4692-b186-73ad8dcc6830\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"setWindowSize\", ");
            sb.AppendLine("  \"target\": \"1936x1056\", ");
            sb.AppendLine("   \"targets\": [], ");
            sb.AppendLine("   \"value\": \"\"  ");
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"630a2c7b-acb5-44c0-aeb5-1030c5735bc6\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"waitForElementPresent\", ");
            sb.AppendLine("  \"target\": \"id=username\", ");
            sb.AppendLine("   \"targets\": [], ");
            sb.AppendLine("   \"value\": \"30000\" ");
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"fdd9eca2-29d5-4a18-8de5-b0f8617a9ac2\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"click\", ");
            sb.AppendLine("  \"target\":  \"id=username\", ");
            sb.AppendLine("  \"targets\": [ ");
            sb.AppendLine("    [\"id=username\", \"id\"], ");
            sb.AppendLine("    [\"css=#username\", \"css:finder\"], ");
            sb.AppendLine("    [\"xpath=//input[@id='username']\", \"xpath:attributes\"], ");
            sb.AppendLine("    [\"xpath=//div[@id='context_login']/section/input[2]\", \"xpath:idRelative\"], ");
            sb.AppendLine("    [\"xpath=//input[2]\", \"xpath:position\"] ");
            sb.AppendLine("   ], ");
            sb.AppendLine("  \"value\": \"\" ");
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"59a9aa9c-2e10-43e4-8890-9a8a6137f62d\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"type\", ");
            sb.AppendLine("  \"target\":  \"id=username\", ");
            sb.AppendLine("  \"targets\": [ ");
            sb.AppendLine("    [\"id=username\", \"id\"], ");
            sb.AppendLine("    [\"css=#username\", \"css:finder\"], ");
            sb.AppendLine("    [\"xpath=//input[@id='username']\", \"xpath:attributes\"], ");
            sb.AppendLine("    [\"xpath=//div[@id='context_login']/section/input[2]\", \"xpath:idRelative\"], ");
            sb.AppendLine("    [\"xpath=//input[2]\", \"xpath:position\"] ");
            sb.AppendLine("   ], ");
            sb.AppendLine("  \"value\": " + $"\"{login.UsuarioMV}" + "\" "); //Usuário MV
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"daaa4426-607b-4687-8374-e9db67818870\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"type\", ");
            sb.AppendLine("  \"target\":  \"id=password\", ");
            sb.AppendLine("  \"targets\": [ ");
            sb.AppendLine("    [\"id=password\", \"id\"], ");
            sb.AppendLine("    [\"css=#password\", \"css:finder\"], ");
            sb.AppendLine("    [\"xpath=//input[@id='password']\", \"xpath:attributes\"], ");
            sb.AppendLine("    [\"xpath=//div[@id='context_login']/section[2]/input[2]\", \"xpath:idRelative\"], ");
            sb.AppendLine("    [\"xpath=//section[2]/input[2]\", \"xpath:position\"] ");
            sb.AppendLine("   ], ");
            sb.AppendLine("  \"value\": " + $"\"{login.Senha}" + "\" "); //Senha Usuário MV
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"8852d0ae-15f1-4538-ac3c-18a8e2cf2bfa\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"click\", ");
            sb.AppendLine("  \"target\":  \"id=companies\",  ");
            sb.AppendLine("  \"targets\": [ ");
            sb.AppendLine("    [\"id=companies\", \"id\"], ");
            sb.AppendLine("    [\"name=company\", \"name\"], ");
            sb.AppendLine("    [\"css=#companies\", \"css:finder\"], ");
            sb.AppendLine("    [\"xpath=//select[@id='companies']\", \"xpath:attributes\"], ");
            sb.AppendLine("    [\"xpath=//div[@id='context_login']/section[3]/select\", \"xpath:idRelative\"], ");
            sb.AppendLine("    [\"xpath=//select\", \"xpath:position\"] ");
            sb.AppendLine("   ], ");
            sb.AppendLine("  \"value\": \"\" ");
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"858d109e-6d1c-455f-96d9-877564569026\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"select\", ");
            sb.AppendLine("  \"target\":  \"id=companies\", ");
            sb.AppendLine("   \"targets\": [], ");
            sb.AppendLine("   \"value\": \"label=FUNFARME-1 HOSPITAL DE BASE SJRIO PRETO\" ");
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"d8bd4cea-fe6d-49d1-90b7-f18500428458\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"click\", ");
            sb.AppendLine("  \"target\":  \"id=companies\", ");
            sb.AppendLine("  \"targets\": [ ");
            sb.AppendLine("    [\"id=companies\", \"id\"], ");
            sb.AppendLine("    [\"name= company\", \"name\"],  ");
            sb.AppendLine("    [\"css=#companies\", \"css:finder\"], ");
            sb.AppendLine("    [\"xpath=//select[@id='companies']\", \"xpath:attributes\"], ");
            sb.AppendLine("    [\"xpath=//div[@id='context_login']/section[3]/select\", \"xpath:idRelative\"], ");
            sb.AppendLine("    [\"xpath=//select\", \"xpath:position\"] ");
            sb.AppendLine("   ], ");
            sb.AppendLine("  \"value\": \"\" ");
            sb.AppendLine("  }, {  ");

            sb.AppendLine("  \"id\": \"a9fd8757-bc2a-4758-afb2-5bf250144f48\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"click\", ");
            sb.AppendLine("  \"target\": \"name=submit\", ");
            sb.AppendLine("  \"targets\": [ ");
            sb.AppendLine("    [\"name=submit\", \"name\"], ");
            sb.AppendLine("    [\"css=.btn-submit:nth-child(8)\", \"css:finder\"], ");
            sb.AppendLine("    [\"xpath=//input[@name='submit']\", \"xpath:attributes\"], ");
            sb.AppendLine("    [\"xpath=//div[@id='context_login']/section[4]/input[7]\", \"xpath:idRelative\"], ");
            sb.AppendLine("    [\"xpath=//input[7]\", \"xpath:position\"] ");
            sb.AppendLine("   ], ");
            sb.AppendLine("  \"value\": \"\" ");
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"1ffd4110-1dc1-45a4-b252-dc957e56387f\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"click\", ");
            sb.AppendLine("  \"target\": \"css=.mv-basico-logistica\", ");
            sb.AppendLine("  \"targets\": [ ");
            sb.AppendLine("     [\"css=.mv-basico-logistica\", \"css:finder\"], ");
            sb.AppendLine("     [\"xpath=//section[@id='workspace-menubar']/ul/li[8]/a/i\", \"xpath:idRelative\"], ");
            sb.AppendLine("     [\"xpath=//li[8]/a/i\", \"xpath:position\"] ");
            sb.AppendLine("    ], ");
            sb.AppendLine("   \"value\": \"\" ");
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"fac11af2-181e-457a-86fe-a31e58145ffb\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"click\", ");
            sb.AppendLine("  \"target\": \"css=.menu-submenu .menu-node:nth-child(2) .menu-node-text\", ");
            sb.AppendLine("  \"targets\": [ ");
            sb.AppendLine("     [\"css=.menu-submenu .menu-node:nth-child(2) .menu-node-text\", \"css:finder\"], ");
            sb.AppendLine("     [\"xpath=//section[@id='workspace-menubar']/ul/li[8]/div/ul/li[2]/a/span\", \"xpath:idRelative\"], ");
            sb.AppendLine("     [\"xpath=//li[8]/div/ul/li[2]/a/span\", \"xpath:position\"], ");
            sb.AppendLine("     [\"xpath=//span[contains(.,'Almoxarifado')]\", \"xpath:innerText\"] ");
            sb.AppendLine("    ],  ");
            sb.AppendLine("   \"value\": \"\" ");
            sb.AppendLine("  }, {  ");

            sb.AppendLine("  \"id\": \"d3daf200-5ed5-455c-92ab-0a7438e7c036\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"click\", ");
            sb.AppendLine("  \"target\": \"css=.menu-node:nth-child(2) .menu-node:nth-child(3) .menu-node-text\", ");
            sb.AppendLine("  \"targets\": [ ");
            sb.AppendLine("     [\"css=.menu-node:nth-child(2) .menu-node:nth-child(3) .menu-node-text\", \"css:finder\"],  ");
            sb.AppendLine("     [\"xpath=//section[@id='workspace-menubar']/ul/li[8]/div/ul/li[2]/div/ul/li[3]/a/span\", \"xpath:idRelative\"], ");
            sb.AppendLine("     [\"xpath=//li[2]/div/ul/li[3]/a/span\", \"xpath:position\"], ");
            sb.AppendLine("     [\"xpath=//span[contains(.,'Solicitações')]\", \"xpath:innerText\"] ");
            sb.AppendLine("    ], ");
            sb.AppendLine("   \"value\": \"\" ");
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"8af7c2bf-dc2f-4d39-a055-f48f2a2d3296\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"click\", ");
            sb.AppendLine("  \"target\": \"css=.menu-node:nth-child(3) .menu-node:nth-child(3) .menu-node-text\", ");
            sb.AppendLine("  \"targets\": [ ");
            sb.AppendLine("     [\"css=.menu-node:nth-child(3) .menu-node:nth-child(3) .menu-node-text\", \"css:finder\"],  ");
            sb.AppendLine("     [\"xpath=//section[@id='workspace-menubar']/ul/li[8]/div/ul/li[2]/div/ul/li[3]/div/ul/li[3]/a/span\", \"xpath:idRelative\"], ");
            sb.AppendLine("     [\"xpath=//li[3]/div/ul/li[3]/a/span\", \"xpath:position\"],  ");
            sb.AppendLine("     [\"xpath=//span[contains(.,'Produtos ao Estoque')]\", \"xpath:innerText\"] ");
            sb.AppendLine("    ], ");
            sb.AppendLine("   \"value\": \"\" ");
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"d2be4a27-556f-485f-8295-e473845e49f5\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"selectFrame\", ");
            sb.AppendLine("  \"target\": \"index=1\", ");
            sb.AppendLine("  \"targets\": [  ");
            sb.AppendLine("     [\"index=1\"] ");
            sb.AppendLine("    ], ");
            sb.AppendLine("   \"value\": \"\" ");
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"fe51fd9b-03ca-4886-a9bf-6a178191e81b\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"waitForElementPresent\", ");
            sb.AppendLine("  \"target\": \"id=rb_TpSolsaiPro_Estoque_btn\", ");
            sb.AppendLine("  \"targets\": [], ");
            sb.AppendLine("   \"value\": \"70000\" ");
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"ebb50ff7-15f4-4556-a86e-a97b856f8d2e\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"click\", ");
            sb.AppendLine("  \"target\":  \"id=rb_TpSolsaiPro_Estoque_btn\", ");
            sb.AppendLine("  \"targets\": [  ");
            sb.AppendLine("     [\"id=rb_TpSolsaiPro_Estoque_btn\", \"id\"], ");
            sb.AppendLine("     [\"css=#rb_TpSolsaiPro_Estoque_btn\", \"css:finder\"],  ");
            sb.AppendLine("     [\"xpath=//button[@id='rb_TpSolsaiPro_Estoque_btn']\", \"xpath:attributes\"], ");
            sb.AppendLine("     [\"xpath=//div[@id='rb_TpSolsaiPro_Estoque_rb_TpSolsaiPro_Estoque']/button\", \"xpath:idRelative\"], ");
            sb.AppendLine("     [\"xpath=//fieldset/div/fieldset/div/div/div[4]/button\", \"xpath:position\"] ");
            sb.AppendLine("    ], ");
            sb.AppendLine("   \"value\": \"\" ");
            sb.AppendLine("  }, {  ");

            sb.AppendLine("  \"id\": \"6aa01e33-6ebf-47d7-bd8e-49bca0d3b0fa\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"type\", ");
            sb.AppendLine("  \"target\":  \"id=inp:cdEstoque\", ");
            sb.AppendLine("  \"targets\": [ ");
            sb.AppendLine("     [\"id=inp:cdEstoque\", \"id\"], ");
            sb.AppendLine("     [\"name=cdEstoque\", \"name\"], ");
            sb.AppendLine("     [\"css=#inp\\\\3A cdEstoque\", \"css:finder\"], "); //v
            sb.AppendLine("     [\"xpath=//input[@id='inp:cdEstoque']\", \"xpath:attributes\"], ");
            sb.AppendLine("     [\"xpath=//div[@id='cdEstoque']/input\", \"xpath:idRelative\"], ");
            sb.AppendLine("     [\"xpath=//div[2]/div[2]/input\", \"xpath:position\"] ");
            sb.AppendLine("    ], ");
            sb.AppendLine("   \"value\": " + $"\"{login.EstoqueOrigem}" + "\" "); // Código estoque de origem
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"a7da1581-9144-41c4-bf09-6dc11e8c6cc1\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"click\", ");
            sb.AppendLine("  \"target\":  \"id=inp:cdEstoqueSolicitante\", ");
            sb.AppendLine("   \"targets\": [], ");
            sb.AppendLine("   \"value\": \"\" ");
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"06590ab6-a10d-4387-bf90-66ea6ce87859\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"type\", ");
            sb.AppendLine("  \"target\":  \"id=inp:cdEstoqueSolicitante\", ");
            sb.AppendLine("  \"targets\": [ ");
            sb.AppendLine("     [\"id=inp:cdEstoqueSolicitante\", \"id\"], ");
            sb.AppendLine("     [\"name=cdEstoqueSolicitante\", \"name\"], ");
            sb.AppendLine("     [\"css=#inp\\\\3A cdEstoqueSolicitante\", \"css:finder\"], ");
            sb.AppendLine("     [\"xpath=//input[@id='inp:cdEstoqueSolicitante']\", \"xpath:attributes\"], ");
            sb.AppendLine("     [\"xpath=//div[@id='cdEstoqueSolicitante']/input\", \"xpath:idRelative\"], ");
            sb.AppendLine("     [\"xpath=//div[4]/input\", \"xpath:position\"] ");
            sb.AppendLine("    ], ");
            sb.AppendLine("   \"value\": " + $"\"{login.EstoqueDestino}" + "\" "); // Código do estoque de Destino
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"dcc0b465-9512-49be-830e-bc2709fd04ad\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"click\", ");
            sb.AppendLine("  \"target\": \"id=inp:dsObs\", ");
            sb.AppendLine("  \"targets\": [  ");
            sb.AppendLine("     [\"id=inp:dsObs\", \"id\"], ");
            sb.AppendLine("     [\"name=dsObs\", \"name\"],  ");
            sb.AppendLine("     [\"css=#inp\\\\3A dsObs\", \"css:finder\"], ");
            sb.AppendLine("     [\"xpath=//input[@id='inp:dsObs']\", \"xpath:attributes\"], ");
            sb.AppendLine("     [\"xpath=//div[@id='dsObs']/input\", \"xpath:idRelative\"], ");
            sb.AppendLine("     [\"xpath=//div[5]/div/input\", \"xpath:position\"] ");
            sb.AppendLine("    ], ");
            sb.AppendLine("   \"value\": \"\" ");
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"82b80083-5405-474c-9016-99029ec7184a\", ");
            sb.AppendLine("  \"comment\": \"\", ");
            sb.AppendLine("  \"command\": \"type\", ");
            sb.AppendLine("  \"target\": \"id=inp:dsObs\", ");
            sb.AppendLine("  \"targets\": [ ");
            sb.AppendLine("     [\"id=inp:dsObs\", \"id\"], ");
            sb.AppendLine("     [\"name=dsObs\", \"name\"], ");
            sb.AppendLine("     [\"css=#inp\\\\3A dsObs\", \"css:finder\"], ");
            sb.AppendLine("     [\"xpath=//input[@id='inp:dsObs']\", \"xpath:attributes\"],  ");
            sb.AppendLine("     [\"xpath=//div[@id='dsObs']/input\", \"xpath:idRelative\"], ");
            sb.AppendLine("     [\"xpath=//div[5]/div/input\", \"xpath:position\"] ");
            sb.AppendLine("     ], ");
            sb.AppendLine($"    \"value\": \"Reposição de Estoque >> {ReportDataReposicao.Farmacia} \" "); // observação
            sb.AppendLine("  }, { ");

            sb.AppendLine("  \"id\": \"9d0ccd6d-169b-4a87-a712-4640f7476060\", ");
            sb.AppendLine("  \"comment\": \"\",   ");
            sb.AppendLine("  \"command\": \"click\", ");
            sb.AppendLine("  \"target\": \"css=.b0\", ");
            sb.AppendLine("  \"targets\": [ ");
            sb.AppendLine("     [\"css=.b0\", \"css:finder\"], ");
            sb.AppendLine("     [\"xpath=//div[@id='grdItsolsaiPro']/div[4]/div[3]/div/div/div\", \"xpath:idRelative\"], ");
            sb.AppendLine("     [\"xpath=//div[3]/div/div/div\", \"xpath:position\"] ");
            sb.AppendLine("    ], ");
            sb.AppendLine("   \"value\": \"\" ");
            sb.AppendLine("  }, {");

            // Adiciona Medicamento 
            int line = 23;
            foreach (var item in listReposicao)
            {              
                sb.AppendLine("  \"id\": \"2d04e76a-6393-4597-8695-ac5b0576773c\", ");
                sb.AppendLine("  \"comment\": \"\", ");
                sb.AppendLine("  \"command\": \"type\", ");
                sb.AppendLine($"  \"target\": \"id=#frames{line}\", ");
                sb.AppendLine("  \"targets\": [ ");
                sb.AppendLine($"     [\"id=#frames{line}\", \"id\"], ");
                sb.AppendLine($"     [\"css=#\\\\#frames{line}\", \"css:finder\"], ");
                sb.AppendLine($"     [\"xpath=//input[@id='#frames{line}']\", \"xpath:attributes\"], ");
                sb.AppendLine("     [\"xpath=//div[@id='grdItsolsaiPro']/div[4]/div[3]/div/div/div/div/input\", \"xpath:idRelative\"], ");
                sb.AppendLine("     [\"xpath=//div[3]/div/div/div/div/input\", \"xpath:position\"] ");
                sb.AppendLine("    ], ");
                sb.AppendLine("    \"value\": " + $"\"{item.CodigoMV}" + "\" "); // Código do medicamento
                sb.AppendLine("  }, { ");

                sb.AppendLine("  \"id\": \"97b69362-4260-4a6c-8e60-ff471c3ee6b8\", ");
                sb.AppendLine("  \"comment\": \"\", ");
                sb.AppendLine("  \"command\": \"sendKeys\", ");
                sb.AppendLine($"  \"target\": \"id=#frames{line}\", ");
                sb.AppendLine("  \"targets\": [ ");
                sb.AppendLine($"     [\"id=#frames{line}\", \"id\"], ");
                sb.AppendLine($"     [\"css=#\\\\#frames{line}\", \"css:finder\"], ");
                sb.AppendLine($"     [\"xpath=//input[@id='#frames{line}']\", \"xpath:attributes\"], ");
                sb.AppendLine("     [\"xpath=//div[@id='grdItsolsaiPro']/div[4]/div[3]/div/div/div/div/input\", \"xpath:idRelative\"],  ");
                sb.AppendLine("     [\"xpath=//div[3]/div/div/div/div/input\", \"xpath:position\"] ");
                sb.AppendLine("    ], ");
                sb.AppendLine("   \"value\": \"${KEY_ENTER}\"  ");
                sb.AppendLine("  }, { ");

                sb.AppendLine("  \"id\": \"022451cf-a43e-4430-a357-f88d5b7b16c2\", ");
                sb.AppendLine("  \"comment\": \"\", ");
                sb.AppendLine("  \"command\": \"click\", ");
                sb.AppendLine($"  \"target\": \"id=frames{line+2}\", ");
                sb.AppendLine("  \"targets\": [ ");
                sb.AppendLine($"     [\"id=frames{line+2}\", \"id\"], ");
                sb.AppendLine($"     [\"css=#\\\\#frames{line+2}\", \"css:finder\"], ");
                sb.AppendLine($"     [\"xpath=//button[@id='frames{line+2}']\", \"xpath:attributes\"], ");
                sb.AppendLine("     [\"xpath=//li[@id='notifications']/ul/li[2]/button\", \"xpath:idRelative\"],  ");
                sb.AppendLine("     [\"xpath=//button[contains(.,'OK')]\", \"xpath:innerText\"] ");
                sb.AppendLine("    ], ");
                sb.AppendLine("   \"value\": \"\"  ");
                sb.AppendLine("  }, { ");

                line++;

                sb.AppendLine("  \"id\": \"55d7c56d-cf5f-49d8-9d1c-ad1ceae799a5\", ");
                sb.AppendLine("  \"comment\":\"\", ");
                sb.AppendLine("  \"command\": \"type\", ");
                sb.AppendLine($"  \"target\": \"id=#frames{line}\", ");
                sb.AppendLine("  \"targets\": [ ");
                sb.AppendLine($"     [\"id=#frames{line}\", \"id\"], ");
                sb.AppendLine($"     [\"css=#\\\\#frames{line}\", \"css:finder\"], ");
                sb.AppendLine($"     [\"xpath=//input[@id='#frames{line}']\", \"xpath:attributes\"], ");
                sb.AppendLine($"     [\"xpath=//div[@id='frames{line}']/input\", \"xpath:idRelative\"], ");
                sb.AppendLine("     [\"xpath=//div[3]/div/div/div[4]/input\", \"xpath:position\"] ");
                sb.AppendLine("    ], ");
                sb.AppendLine("    \"value\": " + $"\"{item.Reposicao}" + "\" "); // Quantidade da Reposição
                sb.AppendLine("  }, { ");

                sb.AppendLine("  \"id\": \"64801d54-9016-4e2a-adbb-31385aa89f93\", ");
                sb.AppendLine("  \"comment\": \"\", ");
                sb.AppendLine("  \"command\": \"sendKeys\", ");
                sb.AppendLine($"  \"target\": \"id=#frames{line}\", ");
                sb.AppendLine("  \"targets\": [ ");
                sb.AppendLine($"     [\"id=#frames{line}\", \"id\"], ");
                sb.AppendLine($"     [\"css=#\\\\#frames{line}\", \"css:finder\"], ");
                sb.AppendLine($"     [\"xpath=//input[@id='#frames{line}']\", \"xpath:attributes\"], ");
                sb.AppendLine($"     [\"xpath=//div[@id='frames{line}']/input\", \"xpath:idRelative\"], ");
                sb.AppendLine("     [\"xpath=//div[3]/div/div/div[4]/input\", \"xpath:position\"] ");
                sb.AppendLine("    ], ");
                sb.AppendLine("   \"value\": \"${KEY_ENTER}\" ");
                sb.AppendLine("  }, { ");

                line++;
            }
            sb.AppendLine("    }] ");
            sb.AppendLine(" }], ");
            sb.AppendLine("  \"suites\": [{ ");
            sb.AppendLine("   \"id\": \"5400b9a0-3c25-46b7-a2fa-2910d348cd81\", ");
            sb.AppendLine("   \"name\": \"Default Suite\", ");
            sb.AppendLine("   \"persistSession\": false, ");
            sb.AppendLine("   \"parallel\": false, ");
            sb.AppendLine("   \"timeout\": 600, ");
            sb.AppendLine("   \"tests\": [\"07699077-e549-4b61-a850-fdc6bd300095\"] ");
            sb.AppendLine(" }], ");
            sb.AppendLine(" \"urls\": [\"http://srv-soulmvblc01.hbase.local/ \"], ");
            sb.AppendLine(" \"plugins\": [] ");
            sb.AppendLine("} ");

            return sb.ToString();
        }
    }
}
