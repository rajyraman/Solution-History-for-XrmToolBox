using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ryr.SolutionHistory.Forms
{
    public partial class SolutionDialog : Form
    {
        public SolutionDialog(IEnumerable<Tuple<string, string, string, string>> solutionData)
        {
            InitializeComponent();

            PopulateDialog(solutionData);
        }

        private void PopulateDialog(IEnumerable<Tuple<string, string, string, string>> solutionData)
        {
            listView1.Items.Clear();
            foreach (var solutionDetail in solutionData)
            {
                listView1.Items.Add(new ListViewItem(new[]
                {
                    solutionDetail.Item1,
                    solutionDetail.Item2,
                    solutionDetail.Item3,
                    solutionDetail.Item4
                }));
            }
        }
    }
}
