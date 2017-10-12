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
                if (errorsAndWarning.Attribute(SolutionComponent.Result).Value != ComponentResult.Success)
                {
                    listView1.Items.Add(new ListViewItem(new[]
                    {
                        errorsAndWarning.Attribute(SolutionComponent.Result).Value,
                        errorsAndWarning.Attribute(SolutionComponent.ErrorCode).Value,
                        errorsAndWarning.Attribute(SolutionComponent.ErrorText).Value
                    }));
                }
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }
}
