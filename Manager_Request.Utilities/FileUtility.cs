using Aspose.Cells;
using Aspose.Words.Reporting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Manager_Request.Ultilities
{
    public static class FileUtility
    {
        public static string ReadFile(string url, ref bool isSuccess, ref Exception error)
        {
            try
            {
                string result = File.ReadAllText(url);

                isSuccess = true;

                return result;
            }
            catch (Exception ex)
            {
                error = ex;

                isSuccess = false;

                return string.Empty;
            }
        }

        public static bool WriteFile(string value, string url, ref Exception error)
        {
            try
            {
                string addText = File.ReadAllText(url);
                File.WriteAllText(url, addText + value, Encoding.UTF8);

                return true;
            }
            catch (Exception ex)
            {
                error = ex;
                return false;
            }
        }

        public class UploadFiles
        {
            public UploadFiles(long _size, List<string> _listFileName, bool _success, string _message)
            {
                size = _size;
                listFileName = _listFileName;
                success = _success;
                message = _message;
            }

            public long size { get; set; }
            public List<string> listFileName { get; set; }
            public bool success { get; set; }
            public string message { get; set; }
        }

        public class DownloadFile
        {
            public DownloadFile(System.IO.MemoryStream _content, string _contentType, string _original_fileName)
            {
                content = _content;
                contentType = _contentType;
                original_fileName = _original_fileName;
            }

            public System.IO.MemoryStream content { get; set; }
            public string contentType { get; set; }
            public string original_fileName { get; set; }
        }


    }

    public static class AsposeFile
    {
        /// <summary>
        /// Đọc file template word theo mẫu và xuất file theo định dạng
        /// </summary>
        /// <param name="teamplateName">Tên file (Bao gồm cả đuôi file) </param>
        /// <param name="dataBind">Dữ liệu binding vào template</param>
        /// <param name="saveFormat">Định dạnh file xuất</param>
        /// <returns></returns>
        public static byte[] FromWordExportFileByte(string teamplateName, object dataBind, Aspose.Words.SaveFormat saveFormat)
        {
            if (dataBind != null)
            {
                string urlTemplateFull = $"Resources/Template/{teamplateName}";
                //Create file export
                var path = Path.Combine(Directory.GetCurrentDirectory(), urlTemplateFull);
                var doc = new Aspose.Words.Document(path);
                ReportingEngine engine = new ReportingEngine();
                engine.BuildReport(doc, dataBind, "data");
                MemoryStream stream = new MemoryStream();
                doc.Save(stream, saveFormat);
                //Convert to Bytes
                return stream.ToArray();
            }
            else return null;
        }

        /// <summary>
        /// Đọc file template excel theo mẫu và xuất file theo định dạng
        /// </summary>
        /// <param name="teamplateName">Tên file (Bao gồm cả đuôi file) </param>
        /// <param name="dataBind">Dữ liệu binding vào template</param>
        /// <param name="saveFormat">Định dạnh file xuất</param>
        /// <returns></returns>
        public static byte[] FromExcelExportFileByte(string teamplateName, object dataBind, Aspose.Cells.SaveFormat saveFormat, params CellInput[] cellInput)
        {
            if (dataBind != null)
            {
                string urlTemplateFull = $"Resources/Template/{teamplateName}";

                var path = Path.Combine(Directory.GetCurrentDirectory(), urlTemplateFull);
                //Create a workbookdesigner object
                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Workbook = new Workbook(path);

                designer.Workbook.Worksheets[0].AutoFitColumns();

                designer.SetDataSource("data", dataBind);
                foreach (var input in cellInput)
                {
                    var cell = designer.Workbook.Worksheets[0].Cells[input.Key];
                    cell.PutValue(input.Value);
                }

                //Process the markers.
                designer.Process();

                MemoryStream stream = new MemoryStream();
                designer.Workbook.Save(stream, saveFormat);

                return stream.ToArray();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Đọc file template excel theo mẫu và xuất file theo định dạng
        /// </summary>
        /// <param name="teamplateName">Tên file (Bao gồm cả đuôi file) </param>
        /// <param name="dataBind">Dữ liệu binding vào template</param>
        /// <param name="saveFormat">Định dạnh file xuất</param>
        /// <returns></returns>
        public static byte[] FromExcelExportFileByte(string teamplateName, object dataBind, Aspose.Cells.SaveFormat saveFormat)
        {
            if (dataBind != null)
            {
                string urlTemplateFull = $"Resources/Template/{teamplateName}";

                var path = Path.Combine(Directory.GetCurrentDirectory(), urlTemplateFull);
                //Create a workbookdesigner object
                WorkbookDesigner designer = new WorkbookDesigner();

                designer.Workbook = new Workbook(path);

                designer.Workbook.Worksheets[0].AutoFitColumns();

                designer.SetDataSource("data", dataBind);

                //Process the markers.
                designer.Process();

                MemoryStream stream = new MemoryStream();
                designer.Workbook.Save(stream, saveFormat);

                return stream.ToArray();
            }
            else
            {
                return null;
            }
        }
    }

    public class CellInput
    {
        public string Key { get; set; }
        public object Value { get; set; }
    }
}