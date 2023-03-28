using OfficeOpenXml;
using TelegramBotForKubG.dbutils;

namespace TelegramBotForKubG.res
{
    internal class Excel
    {
        public static async Task GetCodeFromExcel(string facultyName) 
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Открываем файл Excel
            FileInfo file = new FileInfo(@$"{facultyName}.xlsx");
            try 
            {
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    // Получаем доступ к листу
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    // Получаем значение из первой ячейки первой строки
                    for (int i = 1; i < 10; i++)
                    {
                        if (worksheet.Cells[i, 1].Value != null)
                        {
                            string cellValue = worksheet.Cells[i, 1].Value.ToString();
                            if (cellValue != null)
                            {
                                await DataBaseMethods.AddCodes(facultyName, cellValue);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}