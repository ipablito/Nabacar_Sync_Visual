using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using System.IO;
using System.Reflection;


namespace Sync
{
    public partial class f_sync : DevExpress.XtraEditors.XtraForm
    {
        int id_temp_he = 0;
        public f_sync()
        {
            InitializeComponent();
            nuevo();
        }




        public static void ExportarExcelDataTable(DataTable dt, string RutaExcel)
        {
            //const string FIELDSEPARATOR = "\t";
            const string FIELDSEPARATOR = ",";
            const string ROWSEPARATOR = "\n";
            StringBuilder output = new StringBuilder();
            // Escribir encabezados    
            foreach (DataColumn dc in dt.Columns)
            {
                output.Append(dc.ColumnName);
                output.Append(FIELDSEPARATOR);
            }
            output.Append(ROWSEPARATOR);
            
            
            foreach (DataRow item in dt.Rows)
            {
                foreach (object value in item.ItemArray)
                {
                    //output.Append(value.ToString().Replace('\n', ' ').Replace('\r', ' ').Replace('.', ','));
                    output.Append(value.ToString().Replace('\n', ' ').Replace('\r', ' '));
                    output.Append(FIELDSEPARATOR);
                }
                // Escribir una línea de registro        
                output.Append(ROWSEPARATOR);
            }


            // Valor de retorno    
            // output.ToString();
            StreamWriter sw = new StreamWriter(RutaExcel);
            sw.Write(output.ToString());
            sw.Close();
        }




        void nuevo()
        {
            try
            {
                DataSet_consultaTableAdapters.headerTableAdapter ds = new DataSet_consultaTableAdapters.headerTableAdapter();
                DataTable r = ds.GetData_headers();
                gridControl_header.DataSource = r;
                gridView1.Columns["ID"].OptionsColumn.ReadOnly = true;
            }
            catch
            { }
        }

        private void f_sync_Load(object sender, EventArgs e)
        {

        }


        void searchdetails()
        {
            try
            {
                if (gridView1.GetFocusedRowCellValue("ID") != null)
                {
                    this.Cursor = Cursors.WaitCursor;
                    id_temp_he = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
                    DataSet_consultaTableAdapters.detailTableAdapter ds = new DataSet_consultaTableAdapters.detailTableAdapter();
                    DataTable r = ds.GetData_details(id_temp_he);
                    gridControl_detail.DataSource = r;
                    gridControl_detail.Focus();
                    gridView2.Columns["Date"].UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
                    this.Cursor = Cursors.Default;
                }
            }
            catch
            { }
        }




        private void gridControl_header_DoubleClick(object sender, EventArgs e)
        {
            searchdetails();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView2.RowCount != 0)
                {
                    DataTable nt = new DataTable();
                    SaveFileDialog nd = new SaveFileDialog();
                    nd.Filter = "Csv file (.csv)|*.csv";
                    nd.ShowDialog();
                    if(nd.FileName!="False" || nd.FileName!="")
                    {
                        this.Cursor = Cursors.WaitCursor;
            
                        DataSet_consultaTableAdapters.detailTableAdapter ds = new DataSet_consultaTableAdapters.detailTableAdapter();
                        DataTable r = ds.GetData_details(id_temp_he);

                        ExportarExcelDataTable(r, nd.FileName);
                        this.Cursor = Cursors.Default;
                    }
                   
                }
            }
            catch
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            searchdetails();
        }
    }
}