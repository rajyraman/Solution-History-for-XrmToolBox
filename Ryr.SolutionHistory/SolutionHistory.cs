using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Ryr.SolutionHistory.Forms;
using Tanguy.WinForm.Utilities.DelegatesHelpers;
using XrmToolBox;
using XrmToolBox.Attributes;

[assembly: BackgroundColor("")]
[assembly: PrimaryFontColor("")]
[assembly: SecondaryFontColor("Gray")]
[assembly: SmallImageBase64("iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAAHYYAAB2GAV2iE4EAAAHbSURBVFhH7ZfLL0NBFMa/Wy2VSFjZ2ggL/4NEYqESjVe7Ymtvx8LbgtgIEomyYqVC6MaCiI3/gbC18xbaetR33GnSiNvOXJVL0l/ypTMnNzNn5nxzklr+yUwGHmJhzNsEfExB0vBMPv54SimBf5iAvBknucAsgXfqkbp3UIoyxMK4Zh94pV6A5DRQ4bdDXxlIALEjDqrtuQ56N8CTW9w0M+O8ubDSAYyGOUjacx30boALxiLA+TVw9aRigjQTQa2QfAOWQkDNCCdVdqwQ2gnMdwIXN8BCm4o5MHwIzBxwELTnhTAyYVn2xGTvFOjbBno3VUARzFOi7zBKoLxMDUi4EdjoBraiKuAS7RIs9wBzJ0B7PfDMFyEnzZ72VplOLqipFhjc5aDYJUjRYJEmYLEdWKXToxwnmYhsLnNRjLoz7AVmjSiH5jo6XiXzE4xLEGIJ5OSfflSmzK7gpgRGz/DyAZhtVTEHJo6B8X0OfuMZpumDLIkzoH+HvoirgEuMEnjPuauOBmC9C4izQ+Yi5TFBuwRrfO/i+Ps8LpfNp1rYL4Y4KWorli/SlLis0NdSpkpKGbQQeiWQxcqpgPrNJ4PNBX0PyKK6MsB1IyoWpQT+QALyrDyUFfD07znwAQL3rzTVqdqIAAAAAElFTkSuQmCC")]
[assembly: BigImageBase64("iVBORw0KGgoAAAANSUhEUgAAAFAAAABQCAYAAACOEfKtAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAAHYYAAB2GAV2iE4EAAAOJSURBVHhe7ZxdSBVBFMf/93opkTKjD7r2kBVFgQUiUdEHUSIFET7aBxVGSVKCIFaGJZYofYA+9CiRlJU99KlCiBVEiEVl2Ydp9tCTL1oUFJrZGXdgo5doz96543Z+cHD2MNxhf3fcPbOz3FBS5egoBM+EcFwEcgiTQqVRwmOE6Y/AQAQyEYFMRCATEchEBDIRgUxEIBMRyEQEMhGBTEQgExHIRAQyEYFMRCATEchEBDKJn0C1EzNE8Z0RwxRxJoTyOGwqqRG/ALvXAHNSgJGfTvpfmBgBbnUDj3vpIMnJxQPzAtVon4DarUDhcifFYf0F4F4XNSY5x6Yx+y+s5A0Cdbv8kadoo8/akkkNmtFjn28YcwL1zLu+H8jLcFJ+cTOXLgerqREHiWYEqpP6DLQWAjmLnJTfnM+hWZ1Fja8UBiWaEfgNuJEPbJinj2NE7SZg5ypqGLw7m7mJ0Kx4UwJEk4G+ASAhBl/b8AiQmQpUPQRKmyiR6ORjjTGBfaV083gOVDbqnN/QWbw9STOdSpvDd+g4iALrXwCXKN4d1HkfCe2lMU4Bja/NCjRbxhDDHopmmzEu8G+c66AZ1AocbXOj+C7Vex90B8uwTmBTD5UkdK2s73Sj7hnQ2a87WIZ1Apu3A/3FwMciNwYPAUUrdAfLsE7geMO4wEjAvjKjZUxDF1DxAChfRyn1LNAnUqhkKblMY1SYL2OMCrzyilYJzcCCKDBEKwe/SIwA3e9pjGMBF6gK6YaXdLIHdN5H/ptC2s+ZZwPWXdI3NwCpZ4G0Gjem08yqadcdLMM6gdnzgW1LgNx0N3YsBdJn6g6WYZ1A9aj/TDZQneVGzUYgK8bPEr0SsKrMPMYFTkjQjYBgvg5soTpwltSB/8YfK5Gytf6vRI5cpTGCvhJRhfRFih55Iu2dH/JEOrZMqabZtI+i4LfIAwrUTpuFWCewfQ/wpBx4WuZGxwnnumkj1glcPMPZ382IurFsNhCdrDtYhnUCxxvGBUoh7QVdxoy9mXBN5/yG7u6BfzNh7lSqAQeAiPrFC59RG/YLpwGnHwEltykRKIG06shfCaSl6OMYEaYvpqUXuK9e+6XlnQnMCFSopZuJIlqJMyRPYU5gQJEyhokIZCICmYhAJiKQiQhkIgKZiEAmIpCJCGQiApmIQCYikIkIZCICmYhAJiKQiQhkEh77fQEJzxFKrpI9Ee8AvwAB9nnvqlh01wAAAABJRU5ErkJggg==")]
namespace Ryr.SolutionHistory
{
    public partial class SolutionHistory : PluginBase
    {
        public SolutionHistory()
        {
            InitializeComponent();
        }

        private void RetrieveCurrentSolutions()
        {
            fromDateTimePicker.Value = toDateTimePicker.Value.AddMonths(-3);
            solutionsListBox.Items.Clear();
            var solutionFetchXml = @"
                    <fetch version=""1.0"" output-format=""xml-platform"" mapping=""logical"" distinct=""false"">
                      <entity name=""solution"" >
                        <attribute name=""friendlyname"" />
                        <attribute name=""uniquename"" />
                        <attribute name=""solutionid"" />
                        <attribute name=""isvisible"" />
                        <filter>
                          <condition attribute=""isvisible"" operator=""eq"" value=""1"" />
                        </filter>
                      </entity>
                    </fetch>";
            var solutions = Service.RetrieveMultiple(new FetchExpression(solutionFetchXml)).Entities;
            foreach (var solution in solutions)
            {
                solutionsListBox.Items.Add(solution.GetAttributeValue<string>("uniquename"));
            }
            Enumerable.Range(0, solutions.Count)
                .ToList().ForEach(x => solutionsListBox.SetSelected(x, true));
            solutionsListBox.TopIndex = 0;
        }
        private void RetrieveSolutionHistory()
        {
            WorkAsync("Loading solution history..", 
                (w, e) =>
                {
                    var solutionFilter = new StringBuilder();
                    foreach (var s in solutionsListBox.SelectedItems)
                    {
                        solutionFilter.AppendFormat("<value>{0}</value>",s);
                    }
                    var importJobsFetchXml = string.Format(
                        @"<fetch version=""1.0"" output-format=""xml-platform"" mapping=""logical"" distinct=""false"">
                          <entity name=""importjob"">
                            <attribute name=""importjobid"" />
                            <attribute name=""solutionname"" />
                            <attribute name=""startedon"" />
                            <attribute name=""completedon"" />
                            <attribute name=""createdby"" />
                            <attribute name=""data"" />
                            <attribute name=""progress"" />
                            <order attribute=""startedon"" descending=""true"" />
                            <filter type=""and"">
                              <condition attribute=""name"" operator=""eq"" value=""Customizations"" />
                              <condition attribute=""completedon"" operator=""not-null"" />
                              <condition attribute=""solutionname"" operator=""in"" >{0}</condition>
                              <condition attribute=""startedon"" operator=""on-or-after"" value=""{1}"" />
                              <condition attribute=""completedon"" operator=""on-or-before"" value=""{2}"" />
                            </filter>
                          </entity>
                        </fetch>", solutionFilter.ToString(), 
                                 fromDateTimePicker.Value.ToString("s"),
                                 toDateTimePicker.Value.ToString("s"));
                    e.Result = Service.RetrieveMultiple(new FetchExpression(importJobsFetchXml)).Entities;
                },
                e =>
                {
                    ListViewDelegates.ClearItems(lvSolutionImports);
                    ListViewDelegates.ClearItems(lvSolutionComponents);
                    ListViewDelegates.ClearItems(lvSolutionComponentDetail);
                    foreach (var importJob in (ICollection<Entity>)e.Result)
                    {
                        var listItem = new ListViewItem
                        {
                            Text = importJob.GetAttributeValue<DateTime>("startedon").ToString("dd-MMM-yyyy HH:mm"),
                            Tag = importJob.GetAttributeValue<string>("data")
                        };
                        listItem.SubItems.Add(importJob.GetAttributeValue<DateTime>("completedon").ToString("dd-MMM-yyyy HH:mm"));
                        var parsedComponentXml = XElement.Parse(importJob.GetAttributeValue<string>("data"));
                        var solutionManifest = parsedComponentXml.Elements("solutionManifests").Elements("solutionManifest").ToList();
                        var upgradeInformation = parsedComponentXml.Elements("upgradeSolutionPackageInformation").ToList();

                        var name =
                            solutionManifest.Elements("LocalizedNames")
                                .Elements("LocalizedName")
                                .Attributes("description")
                                .Select(att => att.Value)
                                .FirstOrDefault() ?? importJob.GetAttributeValue<string>("solutionname");

                        listItem.SubItems.Add(name);
                        listItem.SubItems.Add(solutionManifest.Elements("Version").Select(el => " " + el.Value).FirstOrDefault() ?? string.Empty);
                        listItem.SubItems.Add((solutionManifest.Elements("Managed").Select(cv => cv.Value).FirstOrDefault() ?? string.Empty) == "0"
                            ? "Unmanaged"
                            : "Managed");
                        listItem.SubItems.Add(importJob.GetAttributeValue<EntityReference>("createdby").Name);
                        listItem.SubItems.Add(upgradeInformation.Elements("currentVersion").Select(cv => cv.Value).FirstOrDefault() ?? string.Empty);
                        listItem.SubItems.Add(upgradeInformation.Elements("fileVersion").Select(fv => fv.Value).FirstOrDefault() ?? string.Empty);
                        listItem.SubItems.Add(solutionManifest.Elements("Publisher").Elements("LocalizedNames").Elements("LocalizedName").Attributes("description").Select(att => att.Value).FirstOrDefault() ?? solutionManifest.Elements("Publisher").Elements("UniqueName").Select(un => un.Value).FirstOrDefault() ?? string.Empty);
                        listItem.SubItems.Add(importJob.GetAttributeValue<double>("progress").ToString("0.##"));
                        listItem.SubItems.Add(importJob.GetAttributeValue<Guid>("importjobid").ToString());
                        ListViewDelegates.AddItem(lvSolutionImports, listItem);
                    }
                    lvSolutionImports.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                },
                e =>
                {

                });
        }

        private void tsbSolutionHistory_Click(object sender, EventArgs e)
        {
            tsbExportSolutionLog.Enabled = false;
            ExecuteMethod(RetrieveSolutionHistory);
        }

        private void tsbCloseThisTab_Click(object sender, EventArgs e)
        {
            base.CloseTool();
        }

        private void lvSolutionImports_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSolutionImports.SelectedItems.Count > 0)
            {
                tsbExportSolutionLog.Enabled = true;
                string importDataXml = lvSolutionImports.SelectedItems[0].Tag.ToString();
                WorkAsync("Parsing solution XML...", ParseSolutionXml, LoadParsedSolutionXmlToGrid, importDataXml);
            }
            else
            {
                tsbExportSolutionLog.Enabled = false;
            }

        }

        private void LoadParsedSolutionXmlToGrid(RunWorkerCompletedEventArgs args)
        {
            var itemsToTrack = (List<Tuple<string, string, string, IEnumerable<XElement>>>)args.Result;
            ListViewDelegates.ClearItems(lvSolutionComponents);
            foreach (var itemToTrack in itemsToTrack)
            {
                var listItem = new ListViewItem { Text = itemToTrack.Item1, Tag = itemToTrack.Item4 };
                listItem.SubItems.Add(itemToTrack.Item4.Count().ToString());
                ListViewDelegates.AddItem(lvSolutionComponents, listItem);
                lvSolutionComponents.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        private void ParseSolutionXml(DoWorkEventArgs args)
        {
            ListViewDelegates.ClearItems(lvSolutionComponents);
            ListViewDelegates.ClearItems(lvSolutionComponentDetail);
            var parsedSolutionXml = XElement.Parse(args.Argument.ToString());
            var itemsToTrack = new List<Tuple<string, string, string, IEnumerable<XElement>>>
            {
                new Tuple<string, string, string,IEnumerable<XElement>>("Entities", "entities", "entity",parsedSolutionXml.Elements("entities").Elements("entity")),
                new Tuple<string, string, string,IEnumerable<XElement>>("Web Resources", "webResources", "webResource",parsedSolutionXml.Elements("webResources").Elements("webResource")),
                new Tuple<string, string, string,IEnumerable<XElement>>("Dashboards", "dashboards", "dashboard",parsedSolutionXml.Elements("dashboards").Elements("dashboard")),
                new Tuple<string, string, string,IEnumerable<XElement>>("Security Roles", "securityroles", "securityrole",parsedSolutionXml.Elements("securityroles").Elements("securityrole")),
                new Tuple<string, string, string,IEnumerable<XElement>>("Workflows", "workflows", "workflow",parsedSolutionXml.Elements("workflows").Elements("workflow")),
                new Tuple<string, string, string,IEnumerable<XElement>>("Templates", "templates", "template",parsedSolutionXml.Elements("templates").Elements("template")),
                new Tuple<string, string, string,IEnumerable<XElement>>("Optionsets", "optionSets", "optionSet",parsedSolutionXml.Elements("optionSets").Elements("optionSet")),
                new Tuple<string, string, string,IEnumerable<XElement>>("Plugin Assemblies", "SolutionPluginAssemblies", "SolutionPluginAssembly",parsedSolutionXml.Elements("SolutionPluginAssemblies").Elements("PluginAssembly")),
                new Tuple<string, string, string,IEnumerable<XElement>>("Plugin Assembly Steps", "SdkMessageProcessingSteps", "SdkMessageProcessingStep",parsedSolutionXml.Elements("SdkMessageProcessingSteps").Elements("SdkMessageProcessingStep"))
            };
            args.Result = itemsToTrack;
        }

        private void lvSolutionComponents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSolutionComponents.SelectedItems.Count > 0)
            {
                WorkAsync("Parsing component XML...", ParseComponentXml, LoadParsedComponentXmlToGrid, lvSolutionComponents.SelectedItems[0].Tag);
            }
        }

        private void LoadParsedComponentXmlToGrid(RunWorkerCompletedEventArgs args)
        {
            var itemsToTrack = (List<Tuple<string, string, IEnumerable<XElement>>>)args.Result;
            ListViewDelegates.ClearItems(lvSolutionComponentDetail);
            foreach (var itemToTrack in itemsToTrack)
            {
                var listItem = new ListViewItem { Text = string.Empty, Tag = itemToTrack.Item3};
                listItem.SubItems.Add(itemToTrack.Item1);
                listItem.SubItems.Add(itemToTrack.Item2);
                var importStatuses = itemToTrack.Item3;
                if (importStatuses.Any(x => x.Attribute("result").Value == "error"))
                {
                    listItem.ImageIndex = 1;
                }
                else
                if (importStatuses.Any(x => x.Attribute("result").Value == "warning"))
                {
                    listItem.ImageIndex = 2;
                }
                else
                {
                    listItem.ImageIndex = 0;
                }
                ListViewDelegates.AddItem(lvSolutionComponentDetail, listItem);
            }
        }

        private void ParseComponentXml(DoWorkEventArgs args)
        {
            ListViewDelegates.ClearItems(lvSolutionComponentDetail);
            var parsedComponentXml = (IEnumerable<XElement>)args.Argument;
            var itemsToTrack = parsedComponentXml
                .Select(xElement => new Tuple<string, string, IEnumerable<XElement>>(
                    xElement.Attribute("LocalizedName") != null ? xElement.Attribute("LocalizedName").Value : xElement.Attribute("name").Value,
                    xElement.Attribute("id").Value, xElement.Elements("result")))
                .ToList();
            args.Result = itemsToTrack;
        }

        private void lvSolutionComponentDetail_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvSolutionComponentDetail.SelectedItems.Count == 0)
                return;

            ListViewItem item = lvSolutionComponentDetail.SelectedItems[0];
            var errorsAndWarnings = (IEnumerable<XElement>)item.Tag;
            if (!errorsAndWarnings.Any(x => x.Attribute("result").Value == "warning" || x.Attribute("result").Value == "error"))
            {
                return;
            }
            var dialog = new ErrorsAndWarningsDialog(errorsAndWarnings);
            dialog.ShowDialog(this);
        }

        private void tsbExportSolutionLog_Click(object sender, EventArgs e)
        {
            if (lvSolutionImports.SelectedItems.Count > 0)
            {
                var importJobId = new Guid(lvSolutionImports.SelectedItems[0].SubItems[10].Text);
                WorkAsync("Save Solution Import Log..", ExecuteExportLogRequest, ProcessExportLogResponse, importJobId);
            }
        }

        private void ProcessExportLogResponse(RunWorkerCompletedEventArgs args)
        {
            if (lvSolutionImports.SelectedItems.Count == 0)
            {
                return;
            }
            var dialog = new SaveFileDialog
            {
                Filter = "Excel XML Spreadsheet (*.xml)|*.xml",
                FileName = string.Format("{0}-{1}.xml",
                    lvSolutionImports.SelectedItems[0].SubItems[2].Text,
                    DateTime.Parse(lvSolutionImports.SelectedItems[0].SubItems[0].Text).ToString("yyyyMMdd"))
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dialog.FileName, args.Result.ToString());
                MessageBox.Show(this, "Successfully saved the import log file. Open using Excel.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExecuteExportLogRequest(DoWorkEventArgs args)
        {
            var formattedImportJobResultsRequest = new RetrieveFormattedImportJobResultsRequest
            {
                ImportJobId = (Guid) args.Argument
            };
            var formattedImportJobResultsResponse = (RetrieveFormattedImportJobResultsResponse)Service.Execute(formattedImportJobResultsRequest);
            args.Result = formattedImportJobResultsResponse.FormattedResults;
        }

        private void lvSolutionImports_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvSolutionImports.SelectedItems.Clear();
            lvSolutionImports.Sorting = lvSolutionImports.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvSolutionImports.ListViewItemSorter = new ListViewItemComparer(e.Column, lvSolutionImports.Sorting);
        }

        private void lvSolutionComponentDetail_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvSolutionComponentDetail.SelectedItems.Clear();
            lvSolutionComponentDetail.Sorting = lvSolutionComponentDetail.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvSolutionComponentDetail.ListViewItemSorter = new ListViewItemComparer(e.Column, lvSolutionComponentDetail.Sorting);
        }

        private void SolutionHistory_Load(object sender, EventArgs e)
        {
            ExecuteMethod(RetrieveCurrentSolutions);
        }

        private void tsbRefreshSolutions_Click(object sender, EventArgs e)
        {
            ExecuteMethod(RetrieveCurrentSolutions);
        }
    }
}
