using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Ryr.SolutionHistory.Forms
{
    public partial class ErrorsAndWarningsDialog : Form
    {
        public ErrorsAndWarningsDialog(IEnumerable<XElement> errorsAndWarnings)
        {
            InitializeComponent();

            PopulateGrid(errorsAndWarnings);
        }

        private void PopulateGrid(IEnumerable<XElement> errorsAndWarnings)
        {
            listView1.Items.Clear();
            foreach (var errorsAndWarning in errorsAndWarnings)
            {
                if (errorsAndWarning.Attribute("result").Value != "success")
                {
                    listView1.Items.Add(new ListViewItem(new[]
                    {
                        errorsAndWarning.Attribute("result").Value,
                        errorsAndWarning.Attribute("errorcode").Value,
                        errorsAndWarning.Attribute("errortext").Value
                    }));
                }
            }
        }
    }
}
