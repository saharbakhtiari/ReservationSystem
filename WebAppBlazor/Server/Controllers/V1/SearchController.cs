using Application.AdvanceSearch.Queries.SearchRuleVersion;
using Application_Backend.Common;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SearchController : ApiController
    {
        private readonly IStringLocalizer _localizer;
        [HttpPost("searchrule_ef")]
        public async Task<IActionResult> GetFilteredEf([FromBody] SearchRuleVersionQuery dto)
        {
            var output = await Mediator.SendWithUow(dto);
            return Ok(output);
        }
        [HttpPost("searchrule")]
        public async Task<IActionResult> GetFiltered([FromBody] FullTextSearchRuleVersionQuery dto, CancellationToken cancellationToken)
        {
            
            var output = await Mediator.SendWithUow(dto, cancellationToken);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));
            return Ok(output);
        }

        [HttpPost("searchruleExcel")]
        public async Task<IActionResult> GetFilteredExcel([FromBody] FullTextSearchRuleVersionQuery dto)
        {
            var output = await Mediator.SendWithUow(dto);
            var exelOutput = ExportExcel_RuleVersions(output);

            return Ok(exelOutput);
        }
        [NonAction]
        //  private FileStreamResult ExportExcel_RuleVersions(List<SearchRuleVersionDto> items)
        private SearchRuleVersionFileDto ExportExcel_RuleVersions(List<SearchRuleVersionDto> items)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet;
                int totalRows = items.Count;
                InitialExcelFile_RuleVersions(package, items, out worksheet, totalRows);
                var colIndex = 1;
                int i = 0;
                for (int row = 2; row <= totalRows + 1; row++)
                {
                    colIndex = 1;

                    worksheet.Cells[row, colIndex++].Value = items[i].Id;
                    worksheet.Cells[row, colIndex++].Value = items[i].Version;
                    worksheet.Cells[row, colIndex++].Value = items[i].Title;
                    worksheet.Cells[row, colIndex++].Value = items[i].CategoryTitle;
                    worksheet.Cells[row, colIndex++].Value = items[i].TanghihStatus;
                    worksheet.Cells[row, colIndex++].Value = items[i].Description;
                    worksheet.Cells[row, colIndex++].Value = items[i].ApprovalNo;
                    worksheet.Cells[row, colIndex++].Value = items[i].ApprovalDate.ToPersianDate();
                    worksheet.Cells[row, colIndex++].Value = items[i].ApprovalInstitution;
                    worksheet.Cells[row, colIndex++].Value = items[i].PublishLetterNo;
                    worksheet.Cells[row, colIndex++].Value = items[i].PublishDate.ToPersianDate();
                    worksheet.Cells[row, colIndex++].Value = items[i].ExecuteNo;
                    worksheet.Cells[row, colIndex++].Value = items[i].ExecuteDate.ToPersianDate();
                    worksheet.Cells[row, colIndex++].Value = items[i].ExecuteAuthority;
                    worksheet.Cells[row, colIndex++].Value = items[i].NotificationNo;
                    worksheet.Cells[row, colIndex++].Value = items[i].NotificationDate.ToPersianDate();
                    worksheet.Cells[row, colIndex++].Value = items[i].NotificationAuthority;

                    i++;
                }

                // var stream = new MemoryStream(package.GetAsByteArray());
                // return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Rules.xlsx");
                var result = new SearchRuleVersionFileDto();
                result.DataFiles = package.GetAsByteArray();
                result.Name = "Excel.xlsx";
                result.FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return result;
            }
        }
        [NonAction]
        private void InitialExcelFile_RuleVersions(ExcelPackage package, List<SearchRuleVersionDto> items, out ExcelWorksheet worksheet, int totalRows)
        {
            //var color = _optionsApplicationConfiguration.
            worksheet = package.Workbook.Worksheets.Add("قانون ها");
            worksheet.View.RightToLeft = true;
            worksheet.Row(1).Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Row(1).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#f1f9fb"));
            worksheet.Row(1).Style.Font.Bold = true;
            string autoFilterCols = "A1:Q1";
            worksheet.Cells[autoFilterCols].AutoFitColumns(); //worksheet.Dimension.Address
            worksheet.Cells[autoFilterCols].AutoFilter = true;
            worksheet.Cells[autoFilterCols].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[autoFilterCols].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheet.Column(1).Style.Numberformat.Format = "General";
            worksheet.Column(17).Style.Numberformat.Format = "#,##0";
            for (var even = 2; even <= totalRows + 1; even++)
            {
                if (even % 2 != 0)
                {
                    worksheet.Row(even).Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Row(even).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#fffcfc"));
                }
            }
            var colIndex = 1;
            worksheet.Cells[1, colIndex++].Value = "شماره نسخه";
            worksheet.Cells[1, colIndex++].Value = "نسخه";
            worksheet.Cells[1, colIndex++].Value = "عنوان";
            worksheet.Cells[1, colIndex++].Value = "عنوان طبقه بندی";
            worksheet.Cells[1, colIndex++].Value = " وضعیت تنقیحی";
            worksheet.Cells[1, colIndex++].Value = "توضیحات نسخه";
            worksheet.Cells[1, colIndex++].Value = " شماره مصوبه";
            worksheet.Cells[1, colIndex++].Value = " تاریخ تصویب";
            worksheet.Cells[1, colIndex++].Value = "نهاد تصویب کننده";
            worksheet.Cells[1, colIndex++].Value = " شماره نامه انتشار";
            worksheet.Cells[1, colIndex++].Value = "تاریخ انتشار";
            worksheet.Cells[1, colIndex++].Value = "شماره اجرا";
            worksheet.Cells[1, colIndex++].Value = "تاریخ اجرا";
            worksheet.Cells[1, colIndex++].Value = "مرجع اجرا";
            worksheet.Cells[1, colIndex++].Value = " شماره ابلاغ";
            worksheet.Cells[1, colIndex++].Value = " تاریخ ابلاغ";
            worksheet.Cells[1, colIndex++].Value = " مرجع ابلاغ";


        }

    }
}
