using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace Sync
{
    public partial class f_principal : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public f_principal()
        {
            InitializeComponent();
            barButtonItem1.PerformClick();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            foreach (Form form in Application.OpenForms)
            {
                if (form is f_sync)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }
            }

            Form nf = new f_sync();
            nf.MdiParent = this;
            nf.Show();
            this.Cursor = Cursors.Default;
        }
    }
}