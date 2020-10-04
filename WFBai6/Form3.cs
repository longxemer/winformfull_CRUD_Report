using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace WFBai6
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
             ReportDocument rpt = new ReportDocument();
            //cry.Load(@"E:\C#\WFBai6\WFBai6\CrystalReport3.rpt");
            //crystalReportViewer1.ReportSource = cry;
            //crystalReportViewer1.Refresh();
            //cry.RecordSelectionFormula = "{Sach.gia} = 22000";
            //float s = float.Parse(textBox1.Text);
            //eportDocument cry = new ReportDocument();
            //cry.Load(@"E:\C#\WFBai6\WFBai6\CrystalReport3.rpt");
            //crystalReportViewer1.ReportSource = cry;
            //crystalReportViewer1.Refresh();
            //cry.RecordSelectionFormula = "{Sach.gia} =  18000";

            //ParameterFieldDefinition pfd = rpt.Data

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
    }
}
