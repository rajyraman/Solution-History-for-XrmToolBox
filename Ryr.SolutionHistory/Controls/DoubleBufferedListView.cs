
namespace Ryr.SolutionHistory.Controls
{
    using System.Windows.Forms;

    /// <summary>
    /// The double buffered list view.
    /// </summary>
    public class DoubleBufferedListView : ListView
    {
        /// <summary>
        /// Called after the control has been added to another container.
        /// </summary>
        protected override void InitLayout()
        {
            this.DoubleBuffered = true;

            base.InitLayout();
        }
    }
}