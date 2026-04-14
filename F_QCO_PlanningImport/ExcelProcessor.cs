using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace F_QCO_PlanningImport
{
    public class ExcelProcessor
    {
        private IWorkbook _workbook = null;

        /// <summary>
        /// file path
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// Number of sheets
        /// </summary>
        public int SheetCount { get; private set; }

        public ExcelProcessor(string filePath)
        {
            this.FilePath = filePath;
            this.InitWorkbook();
        }

        private void InitWorkbook()
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(this.FilePath, FileMode.Open);
                string extension = Path.GetExtension(this.FilePath);
                switch (extension)
                {
                    case ".xls":
                        this._workbook = new HSSFWorkbook(fileStream);
                        break;
                    case ".xlsx":
                        this._workbook = new XSSFWorkbook(fileStream);
                        break;
                    default:
                        throw new Exception($"not support*{extension}");
                }
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }

            this.SheetCount = this._workbook.NumberOfSheets;
        }

        /// <summary>
        /// Get the data of the specified sheet
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public List<object[]> GetSheetData(string sheetName)
        {
            List<object[]> result = new List<object[]>();
            ISheet sheet = this._workbook.GetSheet(sheetName);
            if (sheet != null)
            {
                IRow row = null;
                ICell cell = null;
                int colCount = 0;
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    row = sheet.GetRow(i);
                    if (row == null)
                    {
                        if (colCount != 0)
                        {
                            result.Add(new object[colCount]);
                        }
                        continue;
                    }

                    if (colCount == 0) colCount = row.LastCellNum;

                    object[] temp = new object[colCount];

                    for (int j = 0; j < colCount; j++)
                    {
                        cell = row.GetCell(j);
                        if (cell == null)
                        {
                            temp[j] = null;
                            continue;
                        }
                        switch (cell.CellType)
                        {
                            case CellType.Boolean:
                                temp[j] = cell.BooleanCellValue.ToString();
                                break;
                            case CellType.Numeric:
                                if (DateUtil.IsCellDateFormatted(cell))
                                {
                                    temp[j] = cell.DateCellValue.ToString();
                                }
                                else
                                {
                                    temp[j] = cell.NumericCellValue.ToString();
                                }
                                break;
                            case CellType.String:
                                temp[j] = cell.StringCellValue;
                                break;
                            default:
                                temp[j] = string.Empty;
                                break;
                        }
                    }

                    result.Add(temp);
                }
            }

            return result;
        }

        /// <summary>
        ///  Get the data of the specified sheet
        /// </summary>
        /// <param name="sheetNo"></param>
        /// <returns></returns>
        public IList<object[]> GetSheetData(int sheetNo)
        {
            string sheetName = this._workbook.GetSheetName(sheetNo);

            if (string.IsNullOrEmpty(sheetName))
            {
                return new List<object[]>();
            }

            return this.GetSheetData(sheetName);
        }

        /// <summary>
        /// Get the picture of the specified sheet
        /// </summary>
        /// <param name="sheetNo"></param>
        /// <returns></returns>
        public IList<PictureInfo> GetPictures(int sheetNo)
        {
            string sheetName = this._workbook.GetSheetName(sheetNo);

            if (string.IsNullOrEmpty(sheetName))
            {
                return new List<PictureInfo>();
            }

            return this.GetPictures(sheetName);
        }

        /// <summary>
        /// Get the picture of the specified sheet
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public IList<PictureInfo> GetPictures(string sheetName)
        {
            ISheet sheet = this._workbook.GetSheet(sheetName);
            IDrawing a = sheet.DrawingPatriarch;
            IList<PictureInfo> result = new List<PictureInfo>();

            if (sheet == null)
            {
                throw new Exception($"named{sheetName}The sheet does not exist");
            }
            if (sheet is XSSFSheet)
            {
                List<POIXMLDocumentPart> documentParts = (sheet as XSSFSheet).GetRelations();
                foreach (var part in documentParts)
                {
                    if (part is XSSFDrawing)
                    {
                        List<XSSFShape> shapes = (part as XSSFDrawing).GetShapes();
                        foreach (var shap in shapes)
                        {
                            if (shap is XSSFPicture)
                            {
                                XSSFPicture picture = shap as XSSFPicture;
                                IClientAnchor anchor = picture.ClientAnchor;
                                result.Add(new PictureInfo(anchor.Row1, anchor.Row1, anchor.Col1, anchor.Col1, picture.PictureData.Data));
                            }
                        }
                    }
                }
            }
            else if (sheet is HSSFSheet)
            {
                HSSFShapeContainer shanpContainer = sheet.DrawingPatriarch as HSSFShapeContainer;
                if (shanpContainer != null)
                {
                    foreach (HSSFShape shape in shanpContainer.Children ?? new List<HSSFShape>())
                    {
                        if (shape is HSSFPicture && shape.Anchor is HSSFClientAnchor)
                        {
                            HSSFPicture picture = shape as HSSFPicture;
                            HSSFClientAnchor anchor = shape.Anchor as HSSFClientAnchor;
                            result.Add(new PictureInfo(anchor.Row1, anchor.Row1, anchor.Col1, anchor.Col1, picture.PictureData.Data));
                        }
                    }
                }
            }

            return result;
        }

        public void SetCellValue(string data, int row, int col, int sheetNo)
        {
            string sheetName = this._workbook.GetSheetName(sheetNo);
            this.SetCellValue(data, row, col, sheetName);
        }

        /// <summary>
        /// Add data to the specified cell of the specified sheet
        /// </summary>
        /// <param name="data"></param>
        /// <param name="row">行号</param>
        /// <param name="col">列号</param>
        /// <param name="sheetName"></param>
        public void SetCellValue(string data, int row, int col, string sheetName)
        {
            ISheet sheet = this._workbook.GetSheet(sheetName);
            if (sheet == null)
            {
                throw new Exception($"named{sheetName}The sheet does not exist");
            }

            IRow rowModel = sheet.GetRow(row);
            if (rowModel == null)
            {
                rowModel = sheet.CreateRow(row);
            }
            ICell cell = rowModel.GetCell(col);
            if (cell == null)
            {
                cell = rowModel.CreateCell(col);
            }
            cell.SetCellValue(data);
        }

        /// <summary>
        ///  delete row
        /// </summary>
        /// <param name="row"></param>
        /// <param name="sheetNo"></param>
        public void RemoveRow(int row, int sheetNo)
        {
            string sheetName = this._workbook.GetSheetName(sheetNo);
            this.RemoveRow(row, sheetName);
        }

        /// <summary>
        /// delete row
        /// </summary>
        /// <param name="row"></param>
        /// <param name="sheetName"></param>
        public void RemoveRow(int row, string sheetName)
        {
            ISheet sheet = this._workbook.GetSheet(sheetName);
            if (sheet == null)
            {
                throw new Exception($"named{sheetName}The sheet does not exist");
            }
            IRow rowModel = sheet.GetRow(row);
            if (rowModel == null)
            {
                throw new Exception($"The row does not exist in sheet");
            }
            sheet.RemoveRow(rowModel);
        }

        /// <summary>
        /// save
        /// </summary>
        public void Save()
        {
            using (FileStream fs = File.Create(this.FilePath))
            {
                try
                {
                    this._workbook.Write(fs);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// delete a row of images
        /// </summary>
        /// <param name="row"></param>
        /// <param name="sheetNo"></param>
        public void RemovePictureInRow(int row, int sheetNo)
        {
            this.RemovePictureInCell(row, -1, sheetNo);
        }

        /// <summary>
        /// delete a row of images
        /// </summary>
        /// <param name="row"></param>
        /// <param name="sheetName"></param>
        public void RemovePictureInRow(int row, string sheetName)
        {
            this.RemovePictureInCell(row, -1, sheetName);
        }

        /// <summary>
        /// delete a picture of a cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="sheetNo"></param>
        public void RemovePictureInCell(int row, int col, int sheetNo)
        {
            string sheetName = this._workbook.GetSheetName(sheetNo);
            this.RemovePictureInCell(row, col, sheetName);
        }

        /// <summary>
        /// delete a picture of a cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="sheetName"></param>
        public void RemovePictureInCell(int row, int col, string sheetName)
        {
            ISheet sheet = this._workbook.GetSheet(sheetName);
            if (sheet == null)
            {
                throw new Exception($"named{sheetName}The sheet does not exist");
            }

            if (sheet is HSSFSheet)
            {
                HSSFShapeContainer shanpContainer = sheet.DrawingPatriarch as HSSFShapeContainer;
                List<HSSFShape> indexes = new List<HSSFShape>();
                if (shanpContainer != null)
                {
                    foreach (HSSFShape shape in shanpContainer.Children ?? new List<HSSFShape>())
                    {
                        if (shape is HSSFPicture && shape.Anchor is HSSFClientAnchor)
                        {
                            HSSFClientAnchor anchor = shape.Anchor as HSSFClientAnchor;
                            if (anchor.Row1 == row && (anchor.Col1 == col || col < 0))
                            {
                                indexes.Add(shape);
                            }
                        }
                    }

                    foreach (HSSFShape shap in indexes)
                    {
                        shanpContainer.RemoveShape(shap);
                    }
                }
            }
            else if (sheet is XSSFSheet)
            {

            }
        }

        /// <summary>
        /// Add image to cell
        /// </summary>
        /// <param name="col1"></param>
        /// <param name="row1"></param>
        /// <param name="col2"></param>
        /// <param name="row2"></param>
        /// <param name="dx1"></param>
        /// <param name="dy1"></param>
        /// <param name="dx2"></param>
        /// <param name="dy2"></param>
        /// <param name="picture"></param>
        /// <param name="sheetNo"></param>
        public void InsertPictureInCell(int col1, int row1, int col2, int row2, int dx1, int dy1, int dx2, int dy2, byte[] picture, int sheetNo)
        {
            string sheetName = this._workbook.GetSheetName(sheetNo);
            this.InsertPictureInCell(col1, row1, col2, row2, dx1, dy1, dx2, dy2, picture, sheetName);
        }


        /// <summary>
        /// Add image to cell
        /// </summary>
        /// <param name="col1"></param>
        /// <param name="row1"></param>
        /// <param name="col2"></param>
        /// <param name="row2"></param>
        /// <param name="dx1"></param>
        /// <param name="dy1"></param>
        /// <param name="dx2"></param>
        /// <param name="dy2"></param>
        /// <param name="picture"></param>
        /// <param name="sheetName"></param>
        public void InsertPictureInCell(int col1, int row1, int col2, int row2, int dx1, int dy1, int dx2, int dy2, byte[] picture, string sheetName)
        {
            int pictureIdx1 = this._workbook.AddPicture(picture, PictureType.PNG);

            ISheet sheet = this._workbook.GetSheet(sheetName);
            IDrawing patriarch = sheet.CreateDrawingPatriarch();

            HSSFClientAnchor anchor = new HSSFClientAnchor(dx1, dy1, dx2, dy2, col1, row1, col2, row2);
            anchor.AnchorType = AnchorType.MoveDontResize;
            IPicture pict1 = patriarch.CreatePicture(anchor, pictureIdx1);
        }

        /// <summary>
        /// set cell height
        /// </summary>
        /// <param name="col"></param>
        /// <param name="width"></param>
        /// <param name="sheetNo"></param>
        public void SetCellWidth(int col, int width, int sheetNo)
        {
            string sheetName = this._workbook.GetSheetName(sheetNo);
            this.SetCellWidth(col, width, sheetName);
        }

        /// <summary>
        /// set cell height
        /// </summary>
        /// <param name="col"></param>
        /// <param name="width"></param>
        /// <param name="sheetName"></param>
        public void SetCellWidth(int col, int width, string sheetName)
        {
            ISheet sheet = this._workbook.GetSheet(sheetName);
            sheet.SetColumnWidth(col, width / 9 * 256);
        }

        /// <summary>
        /// set row height
        /// </summary>
        /// <param name="row"></param>
        /// <param name="height"></param>
        /// <param name="sheetNo"></param>
        public void SetRowHeight(int row, int height, int sheetNo)
        {
            string sheetName = this._workbook.GetSheetName(sheetNo);
            this.SetRowHeight(row, height, sheetName);
        }

        /// <summary>
        /// set row height
        /// </summary>
        /// <param name="row"></param>
        /// <param name="height"></param>
        /// <param name="sheetName"></param>
        public void SetRowHeight(int row, int height, string sheetName)
        {
            ISheet sheet = this._workbook.GetSheet(sheetName);
            sheet.GetRow(row).HeightInPoints = height * 0.6F;
        }

        public struct PictureInfo
        {
            public int Row1 { get; set; }
            public int Row2 { get; set; }
            public int Col1 { get; set; }
            public int Col2 { get; set; }
            public byte[] Picture { get; set; }

            public PictureInfo(int row1, int row2, int col1, int col2, byte[] picture)
            {
                this.Row1 = row1;
                this.Row2 = row2;
                this.Col1 = col1;
                this.Col2 = col2;
                this.Picture = picture;
            }
        }
    }
}
