using HHTRQD.Models;
using HHTRQD.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HHTRQD
{
    public partial class Form1 : Form
    {
        List<TChi> listTC;
        List<Nghe> listNganh;

        public Form1(List<TChi> listTC, List<Nghe> listNganh)
        {
            this.listTC = listTC;
            this.listNganh = listNganh;
            InitializeComponent();
            //LoadMaTrix(listTC);
            gvTrongSo.CellFormatting += gvTrongSo_CellFormatting;
            //gvTrongSo.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void gvTrongSo_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;

            if (rowIndex == columnIndex)
            {
                e.CellStyle.BackColor = Color.LightBlue;
            }

        }

        //private void LoadMaTrix(List<TChi> listTC)
        //{
        //    gvTrongSo.Columns.Clear();

        //    for (int i = 0; i < listTC.Count; i++)
        //    {
        //        string columnName = $"Column{i + 1}";
        //        DataGridViewColumn column = new DataGridViewTextBoxColumn
        //        {
        //            Name = columnName,
        //            HeaderText = listTC[i].TenTC,
        //            DataPropertyName = columnName
        //        };
        //        gvTrongSo.Columns.Add(column);
        //    }

        //    gvTrongSo.Rows.Clear();

        //    int rowCount = listTC.Count;
        //    for (int i = 0; i < rowCount; i++)
        //    {
        //        DataGridViewRow row = new DataGridViewRow();
        //        row.CreateCells(gvTrongSo);
        //        for (int j = 0; j < rowCount; j++)
        //        {
        //            if (i == j)
        //            {
        //                row.Cells[j].Value = 1;
        //                row.Cells[j].ReadOnly = true;
        //            }
        //            else
        //            {
        //                row.Cells[j].Value = DBNull.Value;
        //            }
        //        }

        //        gvTrongSo.Rows.Add(row);
        //        gvTrongSo.Rows[i].HeaderCell.Value = listTC[i].TenTC;
        //    }


        //}

        private void gvTrongSo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;

            // Kiểm tra chỉ thực hiện cập nhật giá trị đối diện khi thay đổi giá trị ở các ô phía trên đường chéo
            if (columnIndex > rowIndex)
            {
                var currentCell = gvTrongSo.Rows[rowIndex].Cells[columnIndex];

                if (double.TryParse(currentCell.Value?.ToString(), out double currentValue))
                {
                    var oppositeCell = gvTrongSo.Rows[columnIndex].Cells[rowIndex];
                    oppositeCell.Value = Math.Round(1 / currentValue, 4);
                }
            }

        }

        private void btnTinhAHP_Click(object sender, EventArgs e)
        {
            Check();
        }
        private void Check()
        {
            int n = gvTrongSo.RowCount - 1;
            double[,] matrix = new double[n, n];

            // Lấy giá trị trong DataGridView để cập nhật ma trận so sánh cặp các tiêu chí
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = 1; // Đường chéo chính bằng 1
                    }
                    else
                    {
                        matrix[i, j] = Convert.ToDouble(gvTrongSo.Rows[i].Cells[j].Value);
                        matrix[j, i] = 1 / matrix[i, j]; // Ma trận đối xứng
                    }
                }
            }
            double[] columnSums = TinhTongCacCot(gvTrongSo);
            double[,] matrix4 = LayMaTranChiaChoTongCacCot(gvTrongSo, columnSums);
            double[,] matrix1 = RemoveLastRowFromMatrix(matrix4);
            double[] vectorTS = TinhTrungBinhCongHang(matrix1);

            double[,] matrix2 = ConvertDataGridViewToMatrix(gvTrongSo);
            double[,] matrix3 = NhanMaTranVoiVector(matrix2, vectorTS);
            double[] tonghang = CalculateRowSums(matrix3);
            double[] vectorT = VectorDivision(tonghang, vectorTS);
            double lamda = CalculateAverage(vectorT);
            double CI = CalculateCI(lamda, n);
            double CR = calculateCR(CI);
            if (CR > 0.1)
            {
                MessageBox.Show("Chỉ số nhất quán không hợp lệ");
            }
            else
            {
                double[] NL = loadPAHP();
                double[] LVN = loadLVNhom();
                double[] LG = loadLogic();
                double[] GT = loadGT();
                double[] TD = loadTDLV();
                double[,] GVTMT = GopVectorsThanhMaTran(NL,LVN,LG,GT,TD);
                List<string> danhSachTenTruong = LayDanhSachTenNganh(listNganh);

                // Chuyển danh sách tên trường thành một vector
                string[] vectorTenTruong = danhSachTenTruong.ToArray();
                double[] ketqua = MultiplyMatrixByVector(GVTMT, vectorTS);
                DataTable matranketqua = CreateMatrixWithRank(vectorTenTruong, ketqua);
                gvKetqua.DataSource = matranketqua;
            }
        }
        // 
        private double[] TinhTongCacCot(DataGridView dataGridView)
        {
            int rowCount = dataGridView.RowCount;
            int columnCount = dataGridView.ColumnCount;

            double[] columnSums = new double[columnCount];

            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                double sum = 0;

                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    var cellValue = dataGridView.Rows[rowIndex].Cells[columnIndex].Value;

                    if (cellValue != null && double.TryParse(cellValue.ToString(), out double cellNumber))
                    {
                        sum += cellNumber;
                    }
                }

                columnSums[columnIndex] = sum;
            }

            return columnSums;
        }

        //
        private double[,] LayMaTranChiaChoTongCacCot(DataGridView dataGridView, double[] columnSums)
        {
            int rowCount = dataGridView.RowCount;
            int columnCount = dataGridView.ColumnCount;

            double[,] matrix = new double[rowCount, columnCount];

            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    var cellValue = dataGridView.Rows[rowIndex].Cells[columnIndex].Value;

                    if (cellValue != null && double.TryParse(cellValue.ToString(), out double cellNumber))
                    {
                        // Chia giá trị của ô cho tổng cột tương ứng
                        matrix[rowIndex, columnIndex] = cellNumber / columnSums[columnIndex];
                    }
                    else
                    {
                        matrix[rowIndex, columnIndex] = 0; // Giá trị không hợp lệ, có thể xử lý hoặc bỏ qua tùy theo yêu cầu
                    }
                }
            }

            return matrix;
        }
        // 
        private double[,] RemoveLastRowFromMatrix(double[,] matrix)
        {
            int rowCount = matrix.GetLength(0);
            int columnCount = matrix.GetLength(1);

            if (rowCount < 1)
            {
                throw new ArgumentException("The matrix must have at least one row.");
            }

            double[,] newMatrix = new double[rowCount - 1, columnCount];

            for (int i = 0; i < rowCount - 1; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    newMatrix[i, j] = matrix[i, j];
                }
            }

            return newMatrix;
        }
        private double[] TinhTrungBinhCongHang(double[,] matrix)
        {
            int rowCount = matrix.GetLength(0);
            int columnCount = matrix.GetLength(1);

            double[] rowAverages = new double[rowCount];

            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                double sum = 0;

                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    sum += matrix[rowIndex, columnIndex];
                }

                rowAverages[rowIndex] = sum / columnCount;
            }

            return rowAverages;
        }
        //
        private double[,] ConvertDataGridViewToMatrix(DataGridView dataGridView)
        {
            int rowCount = dataGridView.RowCount;
            int columnCount = dataGridView.ColumnCount;

            double[,] matrix = new double[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    var cellValue = dataGridView.Rows[i].Cells[j].Value;

                    if (cellValue != null && double.TryParse(cellValue.ToString(), out double cellNumber))
                    {
                        matrix[i, j] = cellNumber;
                    }
                    else
                    {
                        // Giá trị không hợp lệ, có thể xử lý hoặc bỏ qua tùy theo yêu cầu
                        matrix[i, j] = 0;
                    }
                }
            }

            return matrix;
        }
        private double[,] NhanMaTranVoiVector(double[,] matrix, double[] vector)
        {
            int rowCount = matrix.GetLength(0);
            int columnCount = matrix.GetLength(1);

            double[,] result = new double[rowCount - 1, columnCount];

            for (int i = 0; i < rowCount - 1; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    result[i, j] = matrix[i, j] * vector[j];
                }
            }

            return result;
        }
        private double[] CalculateRowSums(double[,] matrix)
        {
            int rowCount = matrix.GetLength(0);
            int columnCount = matrix.GetLength(1);

            double[] rowSums = new double[rowCount];

            for (int i = 0; i < rowCount; i++)
            {
                double sum = 0;
                for (int j = 0; j < columnCount; j++)
                {
                    sum += matrix[i, j];
                }
                rowSums[i] = sum;
            }

            return rowSums;
        }
        private double[] VectorDivision(double[] vector1, double[] vector2)
        {
            int length = vector1.Length;

            double[] result = new double[length];

            for (int i = 0; i < length; i++)
            {
                if (vector2[i] != 0)
                {
                    result[i] = vector1[i] / vector2[i];
                }
                else
                {
                    // Xử lý trường hợp chia cho 0 tùy theo yêu cầu
                    // Ví dụ: Gán giá trị NaN, 0 hoặc xử lý báo lỗi
                    result[i] = 0; // Giả sử gán giá trị 0 cho trường hợp chia cho 0
                }
            }

            return result;
        }

        private double CalculateAverage(double[] vector)
        {
            int length = vector.Length;

            if (length == 0)
            {
                return 0; // Trường hợp vector rỗng, trả về giá trị mặc định
            }

            double sum = 0;

            for (int i = 0; i < length; i++)
            {
                sum += vector[i];
            }

            double average = sum / length;

            return average;
        }
        private double CalculateCI(double lambda, int n)
        {
            return (lambda - n) / (n - 1);
        }
        private double calculateCR(double ci)
        {
            return ci/1.12;
        }
        private double[,] CreateMatrixFromNganhAndTieuChi(List<Nghe> listNganh, List<TChi> listTC)
        {
            int n = listNganh.Count;
            double[,] matrix = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                double nganhValue = (double)listNganh[i].TC1; // Thay thế TC1 bằng thuộc tính tương ứng của Nghe
                                                              // Lặp qua danh sách các tiêu chí
                for (int j = 0; j < n; j++)
                {
                    string tieuChiName = listTC[j].TenTC; // Thay thế TenTC bằng thuộc tính tương ứng của TChi
                    double tieuChiValue = 0;

                    // Tìm tiêu chí có tên tương ứng trong listNganh
                    switch (tieuChiName)
                    {
                        case "Năng lực":
                            tieuChiValue = (double)listNganh[i].TC1;
                            break;
                        case "Khả năng làm việc nhóm":
                            tieuChiValue = (double)listNganh[i].TC2;
                            break;
                        case "Tư duy Logic":
                            tieuChiValue = (double)listNganh[i].TC3;
                            break;
                        case "Khả năng giao tiếp":
                            tieuChiValue = (double)listNganh[i].TC4;
                            break;
                        case "Thái độ làm việc":
                            tieuChiValue = (double)listNganh[i].TC5;
                            break;
                        default:
                            break;
                    }

                    if (tieuChiValue != 0)
                    {
                        matrix[i, j] = nganhValue / tieuChiValue;
                    }
                }
            }

            return matrix;
        }
        private List<double[,]> CreateMatricesFromTieuChi(List<TChi> TC)
        {
            int numTieuChi = TC.Count;
            List<double[,]> matrices = new List<double[,]>();

            for (int i = 0; i < numTieuChi; i++)
            {
                double[,] matrix = new double[numTieuChi, numTieuChi];

                for (int j = 0; j < numTieuChi; j++)
                {
                    if (i == j)
                    {
                        matrix[j, j] = 1.0;
                    }
                    else
                    {
                        // Thực hiện logic để tạo giá trị của các phần tử khác đối với mỗi ma trận
                        // Sử dụng TC[i] và TC[j] để truy cập thông tin của các tiêu chí
                        // Ví dụ: matrix[j, i] = giá trị của phần tử tại dòng j, cột i
                        // Ví dụ: matrix[i, j] = giá trị của phần tử tại dòng i, cột j
                    }
                }

                matrices.Add(matrix);
            }

            return matrices;
        }
        private void DisplayMatricesOnDataGridView(List<double[,]> matrices)
        {
            int numMatrices = matrices.Count;
            int numTieuChi = matrices[0].GetLength(0);

            // Xóa các cột và hàng hiện tại trên DataGridView gvKetQua (nếu có)
            gvKetqua.Rows.Clear();
            gvKetqua.Columns.Clear();

            // Tạo cột và hàng tương ứng với số lượng tiêu chí
            for (int i = 0; i < numTieuChi; i++)
            {
                gvKetqua.Columns.Add($"TC{i + 1}", $"TC{i + 1}");
                gvKetqua.Rows.Add();
                gvKetqua.Rows[i].HeaderCell.Value = $"TC{i + 1}";
            }

            // Điền giá trị từng phần tử vào ô tương ứng trong DataGridView gvKetQua
            for (int i = 0; i < numTieuChi; i++)
            {
                for (int j = 0; j < numTieuChi; j++)
                {
                    gvKetqua.Rows[j].Cells[i].Value = matrices[0][j, i];
                }
            }
        }
        private void HienThiMaTranLenDataGridView(DataGridView dataGridView, double[,] matrix)
        {
            int rowCount = matrix.GetLength(0);
            int columnCount = matrix.GetLength(1);

            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            // Thêm các cột vào DataGridView
            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                dataGridView.Columns.Add($"Column{columnIndex + 1}", $"Column {columnIndex + 1}");
            }

            // Thêm các dòng vào DataGridView và gán giá trị
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                dataGridView.Rows.Add();

                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    double value = matrix[rowIndex, columnIndex];
                    dataGridView.Rows[rowIndex].Cells[columnIndex].Value = value;
                }
            }
        }

        // Tính từng tiêu chí
        private void tieuChiAHP()
        {
            HTRQDContext context = new HTRQDContext();
            // Lấy danh sách các tiêu chí từ CSDL
            List<TieuChi> listTieuChi = context.TieuChi.ToList();

            // Thêm các cột vào DataGridView
            gvTrongSo.Columns.Clear();
            gvTrongSo.Rows.Clear();

            foreach (var tc in listTieuChi)
            {
                gvTrongSo.Columns.Add(tc.TenTc, tc.TenTc);
            }

            gvTrongSo.Rows.Add(listTieuChi.Count);

            // Thêm các dòng vào DataGridView
            for (int i = 0; i < listTieuChi.Count; i++)
            {
                gvTrongSo.Rows[i].HeaderCell.Value = listTieuChi[i].TenTc;
                gvTrongSo.Rows[i].HeaderCell.Style.WrapMode = DataGridViewTriState.True;
            }

            // Cho phép người dùng nhập giá trị đánh giá vào các ô bên trong DataGridView
            for (int i = 0; i < listTieuChi.Count; i++)
            {
                for (int j = 0; j < listTieuChi.Count; j++)
                {
                    var cell = gvTrongSo.Rows[j].Cells[i];
                    if (i == j)
                    {
                        cell.Value = 1;
                    }
                    else
                    {
                        cell.Value = "";
                    }
                    cell.ReadOnly = i == j || i < j;
                    cell.Style.BackColor = i == j ? SystemColors.Control : SystemColors.Window;

                    if (i < j)
                    {
                        var oppositeCell1 = gvTrongSo.Rows[i].Cells[j];

                        gvTrongSo.CellValueChanged += (sender, e) =>
                        {
                            int rowIndex = e.RowIndex;
                            int columnIndex = e.ColumnIndex;

                            // Kiểm tra chỉ thực hiện cập nhật giá trị đối diện khi thay đổi giá trị ở các ô phía trên đường chéo
                            if (columnIndex > rowIndex)
                            {
                                var currentCell = gvTrongSo.Rows[rowIndex].Cells[columnIndex];

                                // Kiểm tra giá trị của ô vừa thay đổi
                                if (double.TryParse(currentCell.Value?.ToString(), out double currentValue))
                                {
                                    // Cập nhật giá trị đối diện
                                    var oppositeCell = gvTrongSo.Rows[columnIndex].Cells[rowIndex];
                                    oppositeCell.Value = Math.Round(1 / currentValue, 4);
                                }
                            }

                        };
                    }
                }
            }
        }
        private double[] loadPAHP()
        {
            HTRQDContext context = new HTRQDContext();
            // Lấy danh sách các tiêu chí từ CSDL
            List<Nghe> listTieuChi = listNganh;

            // Thêm các cột vào DataGridView
            gvNangLuc.Columns.Clear();
            gvNangLuc.Rows.Clear();
            gvNangLuc.Columns.Add("empty", "");
            gvNangLuc.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // cột rỗng

            foreach (var tc in listTieuChi)
            {
                gvNangLuc.Columns.Add(tc.TenNN, tc.TenNN);
                gvNangLuc.Columns[gvNangLuc.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            gvNangLuc.Rows.Add(listTieuChi.Count);

            // Thêm các dòng vào DataGridView
            for (int i = 0; i < listTieuChi.Count; i++)
            {
                gvNangLuc.Rows[i].HeaderCell.Value = listTieuChi[i].TenNN;
            }

            // Gắn giá trị tỷ lệ học phí vào ô giao nhau
            for (int i = 1; i <= listTieuChi.Count; i++)
            {
                for (int j = 1; j <= listTieuChi.Count; j++)
                {
                    if (i != j)
                    {
                        var cell = gvNangLuc.Rows[i - 1].Cells[j];

                        var truongA = listTieuChi[i - 1];
                        var truongB = listTieuChi[j - 1];

                        if (truongA.TC1.HasValue && truongA.TC1.Value != 0 && truongB.TC1.HasValue && truongB.TC1.Value != 0)
                        {
                            double tyLeHocPhi = truongA.TC1.Value / truongB.TC1.Value;
                            cell.Value = Math.Round(tyLeHocPhi, 4);
                        }
                        else
                        {
                            cell.Value = DBNull.Value; // Hoặc giá trị mặc định khác tuỳ bạn muốn
                        }
                    }
                    else
                    {
                        var cell = gvNangLuc.Rows[i - 1].Cells[j];
                        cell.Value = 1;
                    }
                }
            }

            // Vô hiệu hóa chỉnh sửa các ô giao nhau
            for (int i = 1; i <= listTieuChi.Count; i++)
            {
                for (int j = 1; j <= listTieuChi.Count; j++)
                {
                    if (i != j)
                    {
                        var cell = gvNangLuc.Rows[i - 1].Cells[j];
                        cell.ReadOnly = true;
                        cell.Style.BackColor = System.Drawing.SystemColors.Control;
                    }
                }
            }
            double[] columnSums = TongCacCot(gvNangLuc);
            double[,] matrix = MaTranDGChiaTong(gvNangLuc, columnSums);
            double[] vector = TinhTrungBinhCongHang(matrix);
            return vector;

        }
        private double[] loadLVNhom()
        {
            HTRQDContext context = new HTRQDContext();
            // Lấy danh sách các tiêu chí từ CSDL
            List<Nghe> listTieuChi = listNganh;

            // Thêm các cột vào DataGridView
            gvLVNhom.Columns.Clear();
            gvLVNhom.Rows.Clear();
            gvLVNhom.Columns.Add("empty", "");
            gvLVNhom.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // cột rỗng

            foreach (var tc in listTieuChi)
            {
                gvLVNhom.Columns.Add(tc.TenNN, tc.TenNN);
                gvLVNhom.Columns[gvLVNhom.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            gvLVNhom.Rows.Add(listTieuChi.Count);

            // Thêm các dòng vào DataGridView
            for (int i = 0; i < listTieuChi.Count; i++)
            {
                gvLVNhom.Rows[i].HeaderCell.Value = listTieuChi[i].TenNN;
            }

            // Gắn giá trị tỷ lệ học phí vào ô giao nhau
            for (int i = 1; i <= listTieuChi.Count; i++)
            {
                for (int j = 1; j <= listTieuChi.Count; j++)
                {
                    if (i != j)
                    {
                        var cell = gvLVNhom.Rows[i - 1].Cells[j];

                        var truongA = listTieuChi[i - 1];
                        var truongB = listTieuChi[j - 1];

                        if (truongA.TC2.HasValue && truongA.TC2.Value != 0 && truongB.TC2.HasValue && truongB.TC2.Value != 0)
                        {
                            double tyLeHocPhi = truongA.TC2.Value / truongB.TC2.Value;
                            cell.Value = Math.Round(tyLeHocPhi, 4);
                        }
                        else
                        {
                            cell.Value = DBNull.Value; // Hoặc giá trị mặc định khác tuỳ bạn muốn
                        }
                    }
                    else
                    {
                        var cell = gvLVNhom.Rows[i - 1].Cells[j];
                        cell.Value = 1;
                    }
                }
            }

            // Vô hiệu hóa chỉnh sửa các ô giao nhau
            for (int i = 1; i <= listTieuChi.Count; i++)
            {
                for (int j = 1; j <= listTieuChi.Count; j++)
                {
                    if (i != j)
                    {
                        var cell = gvLVNhom.Rows[i - 1].Cells[j];
                        cell.ReadOnly = true;
                        cell.Style.BackColor = System.Drawing.SystemColors.Control;
                    }
                }
            }
            double[] columnSums = TongCacCot(gvLVNhom);
            double[,] matrix = MaTranDGChiaTong(gvLVNhom, columnSums);
            double[] vector = TinhTrungBinhCongHang(matrix);
            return vector;

        }
        private double[] loadLogic()
        {
            HTRQDContext context = new HTRQDContext();
            // Lấy danh sách các tiêu chí từ CSDL
            List<Nghe> listTieuChi = listNganh;

            // Thêm các cột vào DataGridView
            gvLogic.Columns.Clear();
            gvLogic.Rows.Clear();
            gvLogic.Columns.Add("empty", "");
            gvLogic.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // cột rỗng

            foreach (var tc in listTieuChi)
            {
                gvLogic.Columns.Add(tc.TenNN, tc.TenNN);
                gvLogic.Columns[gvLogic.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            gvLogic.Rows.Add(listTieuChi.Count);

            // Thêm các dòng vào DataGridView
            for (int i = 0; i < listTieuChi.Count; i++)
            {
                gvLogic.Rows[i].HeaderCell.Value = listTieuChi[i].TenNN;
            }

            // Gắn giá trị tỷ lệ học phí vào ô giao nhau
            for (int i = 1; i <= listTieuChi.Count; i++)
            {
                for (int j = 1; j <= listTieuChi.Count; j++)
                {
                    if (i != j)
                    {
                        var cell = gvLogic.Rows[i - 1].Cells[j];

                        var truongA = listTieuChi[i - 1];
                        var truongB = listTieuChi[j - 1];

                        if (truongA.TC3.HasValue && truongA.TC3.Value != 0 && truongB.TC3.HasValue && truongB.TC3.Value != 0)
                        {
                            double tyLeHocPhi = truongA.TC3.Value / truongB.TC3.Value;
                            cell.Value = Math.Round(tyLeHocPhi, 4);
                        }
                        else
                        {
                            cell.Value = DBNull.Value; // Hoặc giá trị mặc định khác tuỳ bạn muốn
                        }
                    }
                    else
                    {
                        var cell = gvLogic.Rows[i - 1].Cells[j];
                        cell.Value = 1;
                    }
                }
            }

            // Vô hiệu hóa chỉnh sửa các ô giao nhau
            for (int i = 1; i <= listTieuChi.Count; i++)
            {
                for (int j = 1; j <= listTieuChi.Count; j++)
                {
                    if (i != j)
                    {
                        var cell = gvLogic.Rows[i - 1].Cells[j];
                        cell.ReadOnly = true;
                        cell.Style.BackColor = System.Drawing.SystemColors.Control;
                    }
                }
            }
            double[] columnSums = TongCacCot(gvLogic);
            double[,] matrix = MaTranDGChiaTong(gvLogic, columnSums);
            double[] vector = TinhTrungBinhCongHang(matrix);
            return vector;

        }
        private double[] loadGT()
        {
            HTRQDContext context = new HTRQDContext();
            // Lấy danh sách các tiêu chí từ CSDL
            List<Nghe> listTieuChi = listNganh;

            // Thêm các cột vào DataGridView
            gvGiaoTiep.Columns.Clear();
            gvGiaoTiep.Rows.Clear();
            gvGiaoTiep.Columns.Add("empty", "");
            gvGiaoTiep.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // cột rỗng

            foreach (var tc in listTieuChi)
            {
                gvGiaoTiep.Columns.Add(tc.TenNN, tc.TenNN);
                gvGiaoTiep.Columns[gvGiaoTiep.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            gvGiaoTiep.Rows.Add(listTieuChi.Count);

            // Thêm các dòng vào DataGridView
            for (int i = 0; i < listTieuChi.Count; i++)
            {
                gvGiaoTiep.Rows[i].HeaderCell.Value = listTieuChi[i].TenNN;
            }

            // Gắn giá trị tỷ lệ học phí vào ô giao nhau
            for (int i = 1; i <= listTieuChi.Count; i++)
            {
                for (int j = 1; j <= listTieuChi.Count; j++)
                {
                    if (i != j)
                    {
                        var cell = gvGiaoTiep.Rows[i - 1].Cells[j];

                        var truongA = listTieuChi[i - 1];
                        var truongB = listTieuChi[j - 1];

                        if (truongA.TC4.HasValue && truongA.TC4.Value != 0 && truongB.TC4.HasValue && truongB.TC4.Value != 0)
                        {
                            double tyLeHocPhi = truongA.TC4.Value / truongB.TC4.Value;
                            cell.Value = Math.Round(tyLeHocPhi, 4);
                        }
                        else
                        {
                            cell.Value = DBNull.Value; // Hoặc giá trị mặc định khác tuỳ bạn muốn
                        }
                    }
                    else
                    {
                        var cell = gvGiaoTiep.Rows[i - 1].Cells[j];
                        cell.Value = 1;
                    }
                }
            }

            // Vô hiệu hóa chỉnh sửa các ô giao nhau
            for (int i = 1; i <= listTieuChi.Count; i++)
            {
                for (int j = 1; j <= listTieuChi.Count; j++)
                {
                    if (i != j)
                    {
                        var cell = gvGiaoTiep.Rows[i - 1].Cells[j];
                        cell.ReadOnly = true;
                        cell.Style.BackColor = System.Drawing.SystemColors.Control;
                    }
                }
            }
            double[] columnSums = TongCacCot(gvGiaoTiep);
            double[,] matrix = MaTranDGChiaTong(gvGiaoTiep, columnSums);
            double[] vector = TinhTrungBinhCongHang(matrix);
            return vector;

        }
        private double[] loadTDLV()
        {
            HTRQDContext context = new HTRQDContext();
            // Lấy danh sách các tiêu chí từ CSDL
            List<Nghe> listTieuChi = listNganh;

            // Thêm các cột vào DataGridView
            gvThaiDo.Columns.Clear();
            gvThaiDo.Rows.Clear();
            gvThaiDo.Columns.Add("empty", "");
            gvThaiDo.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // cột rỗng

            foreach (var tc in listTieuChi)
            {
                gvThaiDo.Columns.Add(tc.TenNN, tc.TenNN);
                gvThaiDo.Columns[gvThaiDo.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            gvThaiDo.Rows.Add(listTieuChi.Count);

            // Thêm các dòng vào DataGridView
            for (int i = 0; i < listTieuChi.Count; i++)
            {
                gvThaiDo.Rows[i].HeaderCell.Value = listTieuChi[i].TenNN;
            }

            // Gắn giá trị tỷ lệ học phí vào ô giao nhau
            for (int i = 1; i <= listTieuChi.Count; i++)
            {
                for (int j = 1; j <= listTieuChi.Count; j++)
                {
                    if (i != j)
                    {
                        var cell = gvThaiDo.Rows[i - 1].Cells[j];

                        var truongA = listTieuChi[i - 1];
                        var truongB = listTieuChi[j - 1];

                        if (truongA.TC5.HasValue && truongA.TC5.Value != 0 && truongB.TC5.HasValue && truongB.TC5.Value != 0)
                        {
                            double tyLeHocPhi = truongA.TC5.Value / truongB.TC5.Value;
                            cell.Value = Math.Round(tyLeHocPhi, 4);
                        }
                        else
                        {
                            cell.Value = DBNull.Value; // Hoặc giá trị mặc định khác tuỳ bạn muốn
                        }
                    }
                    else
                    {
                        var cell = gvThaiDo.Rows[i - 1].Cells[j];
                        cell.Value = 1;
                    }
                }
            }

            // Vô hiệu hóa chỉnh sửa các ô giao nhau
            for (int i = 1; i <= listTieuChi.Count; i++)
            {
                for (int j = 1; j <= listTieuChi.Count; j++)
                {
                    if (i != j)
                    {
                        var cell = gvThaiDo.Rows[i - 1].Cells[j];
                        cell.ReadOnly = true;
                        cell.Style.BackColor = System.Drawing.SystemColors.Control;
                    }
                }
            }
            double[] columnSums = TongCacCot(gvThaiDo);
            double[,] matrix = MaTranDGChiaTong(gvThaiDo, columnSums);
            double[] vector = TinhTrungBinhCongHang(matrix);
            return vector;

        }
        private double[] TongCacCot(DataGridView dataGridView)
        {
            int rowCount = dataGridView.RowCount;
            int columnCount = dataGridView.ColumnCount;

            double[] columnSums = new double[columnCount - 1]; // Giảm kích thước mảng đi 1 vì loại bỏ cột đầu tiên

            for (int columnIndex = 1; columnIndex < columnCount; columnIndex++) // Bắt đầu từ columnIndex = 1 để loại bỏ cột đầu tiên
            {
                double sum = 0;

                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    var cellValue = dataGridView.Rows[rowIndex].Cells[columnIndex].Value;

                    if (cellValue != null && double.TryParse(cellValue.ToString(), out double cellNumber))
                    {
                        sum += cellNumber;
                    }
                }

                columnSums[columnIndex - 1] = sum; // Giảm chỉ số mảng đi 1 vì loại bỏ cột đầu tiên
            }

            // Loại bỏ các giá trị trống cuối cùng của mảng
            while (columnSums.Length > 0 && columnSums[columnSums.Length - 1] == 0)
            {
                Array.Resize(ref columnSums, columnSums.Length - 1);
            }

            return columnSums;
        }
        private double[,] MaTranDGChiaTong(DataGridView dataGridView, double[] columnSums)
        {
            int rowCount = dataGridView.RowCount - 1; // Loại bỏ hàng cuối cùng
            int columnCount = dataGridView.ColumnCount - 1; // Loại bỏ cột rỗng đầu tiên

            double[,] matrix = new double[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 1; j <= columnCount; j++)
                {
                    var cell = dataGridView.Rows[i].Cells[j];
                    double value = 0;

                    if (cell.Value != null && double.TryParse(cell.Value.ToString(), out value))
                    {
                        matrix[i, j - 1] = Math.Round(value, 4);
                    }
                    else
                    {
                        // Giá trị không hợp lệ, có thể xử lý hoặc bỏ qua tùy theo yêu cầu
                        matrix[i, j - 1] = 0;
                    }
                }
            }

            // Chia ma trận cho mảng columnSums
            double[,] result = MaTranChiaTong(matrix, columnSums);

            return result;
        }
        private double[,] MaTranChiaTong(double[,] matrix, double[] columnSums)
        {
            int rowCount = matrix.GetLength(0);
            int columnCount = matrix.GetLength(1);

            double[,] result = new double[rowCount, columnCount];

            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    result[rowIndex, columnIndex] = matrix[rowIndex, columnIndex] / columnSums[columnIndex];
                }
            }

            return result;
        }
        private double[,] GopVectorsThanhMaTran(double[] vector1, double[] vector2, double[] vector3, double[] vector4, double[] vector5)
        {
            int rowCount = vector1.Length;
            int columnCount = 5; // Số lượng vector

            double[,] matrix = new double[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                matrix[i, 0] = vector1[i];
                matrix[i, 1] = vector2[i];
                matrix[i, 2] = vector3[i];
                matrix[i, 3] = vector4[i];
                matrix[i, 4] = vector5[i];
            }

            return matrix;
        }

        private DataTable CreateMatrixWithRank(string[] vectorTenTruong, double[] ketqua)
        {
            int length = vectorTenTruong.Length;

            // Create DataTable with columns
            DataTable matrixTable = new DataTable();
            matrixTable.Columns.Add("TenTruong", typeof(string));
            matrixTable.Columns.Add("Ketqua", typeof(double));
            matrixTable.Columns.Add("Rank", typeof(int));

            // Populate the DataTable with values
            for (int i = 0; i < length; i++)
            {
                matrixTable.Rows.Add(vectorTenTruong[i], ketqua[i], i + 1);
            }

            // Sort the DataTable based on the Ketqua column in descending order
            DataView sortedView = matrixTable.DefaultView;
            sortedView.Sort = "Ketqua DESC";
            DataTable sortedMatrixTable = sortedView.ToTable();

            // Update the Rank column based on the sorted order
            for (int i = 0; i < length; i++)
            {
                sortedMatrixTable.Rows[i]["Rank"] = i + 1;
            }

            return sortedMatrixTable;
        }
        private double[] MultiplyMatrixByVector(double[,] matrix, double[] vector)
        {
            int rowCount = matrix.GetLength(0);
            int columnCount = matrix.GetLength(1);

            if (columnCount != vector.Length)
            {
                throw new ArgumentException("The number of columns in the matrix must be equal to the length of the vector.");
            }

            double[] result = new double[rowCount];

            for (int i = 0; i < rowCount; i++)
            {
                double sum = 0;

                for (int j = 0; j < columnCount; j++)
                {
                    sum += matrix[i, j] * vector[j];
                }

                result[i] = sum;
            }

            return result;
        }
        private List<string> LayDanhSachTenNganh(List<Nghe> listNganh)
        {
            List<string> danhSachTenNganh = new List<string>();

            foreach (Nghe nganh in listNganh)
            {
                string tenNganh = nganh.TenNN;
                danhSachTenNganh.Add(tenNganh);
            }

            return danhSachTenNganh;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tieuChiAHP();

        }

        private void gvKetqua_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSaveDatabase_Click(object sender, EventArgs e)
        {
            int n = gvTrongSo.RowCount - 1;
            double[,] matrix = new double[n, n];

            // Lấy giá trị trong DataGridView để cập nhật ma trận so sánh cặp các tiêu chí
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = 1; // Đường chéo chính bằng 1
                    }
                    else
                    {
                        matrix[i, j] = Convert.ToDouble(gvTrongSo.Rows[i].Cells[j].Value);
                        matrix[j, i] = 1 / matrix[i, j]; // Ma trận đối xứng
                    }
                }
            }
            double[] columnSums = TinhTongCacCot(gvTrongSo);
            double[,] matrix4 = LayMaTranChiaChoTongCacCot(gvTrongSo, columnSums);
            double[,] matrix1 = RemoveLastRowFromMatrix(matrix4);
            double[] vectorTS = TinhTrungBinhCongHang(matrix1);

            double[,] matrix2 = ConvertDataGridViewToMatrix(gvTrongSo);
            double[,] matrix3 = NhanMaTranVoiVector(matrix2, vectorTS);
            double[] tonghang = CalculateRowSums(matrix3);
            double[] vectorT = VectorDivision(tonghang, vectorTS);
            double lamda = CalculateAverage(vectorT);
            double CI = CalculateCI(lamda, n);
            double CR = calculateCR(CI);
            
                double[] NL = loadPAHP();
                double[] LVN = loadLVNhom();
                double[] LG = loadLogic();
                double[] GT = loadGT();
                double[] TD = loadTDLV();
                double[,] GVTMT = GopVectorsThanhMaTran(NL, LVN, LG, GT, TD);
                List<string> danhSachTenTruong = LayDanhSachTenNganh(listNganh);

                // Chuyển danh sách tên trường thành một vector
                string[] vectorTenTruong = danhSachTenTruong.ToArray();
                double[] ketqua = MultiplyMatrixByVector(GVTMT, vectorTS);
                DataTable matranketqua = CreateMatrixWithRank(vectorTenTruong, ketqua);
                gvKetqua.DataSource = matranketqua;
            
        }
    }


}
