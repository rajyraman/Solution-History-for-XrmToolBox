using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Ryr.SolutionHistory.Forms;
using Tanguy.WinForm.Utilities.DelegatesHelpers;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Windows.Documents;
using System.Diagnostics;

namespace Ryr.SolutionHistory
{
    public partial class SolutionHistory : PluginControlBase, IGitHubPlugin
    {
        public string RepositoryName => "Solution-History-for-XrmToolBox";

        public string UserName => "rajyraman";

        public SolutionHistory()
        {
            InitializeComponent();
        }

        private void RetrieveCurrentSolutions()
        {
            fromDateTimePicker.Value = toDateTimePicker.Value.AddDays(-7);
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
            if (solutionsListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "No solutions selected.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            WorkAsync(new WorkAsyncInfo("Loading solution history..", (e) => {
                var solutionFilter = new StringBuilder();
                var solutions = new StringBuilder();
                foreach (var s in solutionsListBox.SelectedItems)
                {
                    solutionFilter.AppendFormat("<value>{0}</value>", s);
                }
                foreach (var s in solutionsListBox.Items)
                {
                    solutions.AppendFormat("<value>{0}</value>", s);
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
                              <filter type=""or"">
                                  <condition attribute=""solutionname"" operator=""in"" >{0}</condition>
                                  {3}
                              </filter>
                              <condition attribute=""startedon"" operator=""on-or-after"" value=""{1}"" />
                              <condition attribute=""completedon"" operator=""on-or-before"" value=""{2}"" />
                            </filter>
                          </entity>
                        </fetch>", solutionFilter.ToString(),
                             fromDateTimePicker.Value.ToString("s"),
                             toDateTimePicker.Value.ToString("s"),
                             includeDeletedSolutionsCheckBox.Checked ? 
                                $@"<condition attribute=""solutionname"" operator=""not-in"" >{solutions}</condition>"
                                 : "");
                e.Result = Service.RetrieveMultiple(new FetchExpression(importJobsFetchXml)).Entities;
            }) { PostWorkCallBack = (completedargs) => {

                ListViewDelegates.ClearItems(lvSolutionImports);
                ListViewDelegates.ClearItems(lvSolutionComponents);
                ListViewDelegates.ClearItems(lvSolutionComponentDetail);
                foreach (var importJob in (ICollection<Entity>)completedargs.Result)
                {
                    var solutionXml = importJob.GetAttributeValue<string>("data");
                    var listItem = new ListViewItem
                    {
                        Text = importJob.GetAttributeValue<DateTime>("startedon").ToLocalTime().ToString("dd-MMM-yyyy HH:mm"),
                        Tag = solutionXml
                    };
                    listItem.SubItems.Add(importJob.GetAttributeValue<DateTime>("completedon").ToLocalTime().ToString("dd-MMM-yyyy HH:mm"));
                    var parsedComponentXml = XElement.Parse(solutionXml);
                    var isSmartDiffApplied = bool.Parse(parsedComponentXml.Element("SmartDiffApplied")?.Value ?? "false") ? "✔" : "";
                    var solutionManifest = parsedComponentXml
                        .Elements(SolutionElement.SolutionManifests)
                        .Elements(SolutionElement.SolutionManifest).ToList();
                    var upgradeInformation = parsedComponentXml.Elements(SolutionElement.UpgradeSolutionPackageInformation).ToList();

                    var name =
                        solutionManifest
                            .Elements(SolutionElement.LocalizedNames)
                            .Elements(SolutionElement.LocalizedName)
                            .Attributes(SolutionComponent.Description)
                            .Select(att => att.Value)
                            .FirstOrDefault() ?? importJob.GetAttributeValue<string>("solutionname");

                    listItem.SubItems.Add(name);
                    listItem.SubItems.Add(solutionManifest.Elements(SolutionElement.Version).Select(el => " " + el.Value).FirstOrDefault() ?? string.Empty);
                    listItem.SubItems.Add((solutionManifest.Elements(SolutionElement.Managed).Select(cv => cv.Value).FirstOrDefault() ?? string.Empty) == "0"
                        ? "Unmanaged"
                        : "Managed");
                    listItem.SubItems.Add(isSmartDiffApplied);
                    listItem.SubItems.Add(importJob.GetAttributeValue<EntityReference>("createdby").Name);
                    listItem.SubItems.Add(upgradeInformation.Elements(SolutionElement.CurrentVersion).Select(cv => cv.Value).FirstOrDefault() ?? string.Empty);
                    listItem.SubItems.Add(upgradeInformation.Elements(SolutionElement.FileVersion).Select(fv => fv.Value).FirstOrDefault() ?? string.Empty);
                    listItem.SubItems.Add(solutionManifest
                        .Elements(SolutionElement.Publisher)
                        .Elements(SolutionElement.LocalizedNames)
                        .Elements(SolutionElement.LocalizedName)
                        .Attributes(SolutionComponent.Description)
                        .Select(att => att.Value).FirstOrDefault() ?? 
                        solutionManifest
                        .Elements(SolutionElement.Publisher)
                        .Elements(SolutionElement.UniqueName)
                        .Select(un => un.Value).FirstOrDefault() ?? 
                        string.Empty);
                    listItem.SubItems.Add(importJob.GetAttributeValue<double>("progress").ToString("0.##"));
                    listItem.SubItems.Add(importJob.GetAttributeValue<Guid>("importjobid").ToString());
                    ListViewDelegates.AddItem(lvSolutionImports, listItem);
                }
                lvSolutionImports.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            } });
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
                WorkAsync(new WorkAsyncInfo("Parsing solution XML...", ParseSolutionXml)
                {
                    PostWorkCallBack = LoadParsedSolutionXmlToGrid, 
                    AsyncArgument = importDataXml
                });
            }
            else
            {
                tsbExportSolutionLog.Enabled = false;
            }

        }

        private void LoadParsedSolutionXmlToGrid(RunWorkerCompletedEventArgs args)
        {
            var itemsToTrack = (IEnumerable<ImportResult>)args.Result;
            var importResults = itemsToTrack as IList<ImportResult> ?? itemsToTrack.ToList();
            var groupNodes = importResults
                .GroupBy(e => e.Name)
                .Select(x => new
                {
                    Key = x.Key.ToString().ToLower(),
                    Count = x.Count(),
                    State = x.Any(y => y.Result == ComponentResult.Failure) ?
                        ComponentResult.Failure : x.Any(y => y.Result == ComponentResult.Error) ?
                            ComponentResult.Error : x.Any(y => y.Result == ComponentResult.Warning) ?
                                ComponentResult.Warning : ComponentResult.Success
                });
            ListViewDelegates.ClearItems(lvSolutionComponents);
            foreach (var groupNode in groupNodes)
            {
                var listItem = new ListViewItem
                {
                    Text = groupNode.Key,
                    Tag = importResults.Where(x=>x.Name.Equals(groupNode.Key, StringComparison.CurrentCultureIgnoreCase)).ToList()
                };
                switch (groupNode.State)
                {
                    case ComponentResult.Failure:
                    case ComponentResult.Error:
                        listItem.ForeColor = Color.Red;
                        break;
                    case ComponentResult.Warning:
                        listItem.ForeColor = Color.Orange;
                        break;
                }
                listItem.SubItems.Add(groupNode.Count.ToString());
                ListViewDelegates.AddItem(lvSolutionComponents, listItem);
            }
            lvSolutionComponents.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void ParseSolutionXml(DoWorkEventArgs args)
        {
            ListViewDelegates.ClearItems(lvSolutionComponents);
            ListViewDelegates.ClearItems(lvSolutionComponentDetail);
            var parsedSolutionXml = XElement.Parse(args.Argument.ToString());
            var solutionElements = parsedSolutionXml.Descendants("result").ToList().Select(x => {
                var a = x.Ancestors().First();
                return new ImportResult
                {
                    Name = a.Name.ToString(),
                    Id = x.Attribute(SolutionComponent.Id)?.Value,
                    LocalizedName = a.Attribute(SolutionComponent.LocalizedName)?.Value,
                    Description = a.Attribute(SolutionComponent.Description)?.Value,
                    Processed = a.Attribute(SolutionComponent.Processed)?.Value,
                    Result = x.Attribute(SolutionComponent.Result)?.Value,
                    ErrorCode = x.Attribute(SolutionComponent.ErrorCode)?.Value,
                    ErrorText = x.Attribute(SolutionComponent.ErrorText)?.Value,
                    DateTimeTicks = new DateTime(Convert.ToInt64(x.Attribute(SolutionComponent.DateTimeTicks)?.Value), DateTimeKind.Utc)
                                    .ToLocalTime().ToString()
                };
            });	
            args.Result = solutionElements;
        }

        private void lvSolutionComponents_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewDelegates.ClearItems(lvSolutionComponentDetail);;
            if (lvSolutionComponents.SelectedItems.Count == 0) return;

            var itemsToTrack = (List<ImportResult>)lvSolutionComponents.SelectedItems[0].Tag;
            foreach (var itemToTrack in itemsToTrack)
            {
                var listItem = new ListViewItem
                {
                    Text = string.Empty,
                    Tag = itemToTrack.Id
                };
                listItem.SubItems.Add(itemToTrack.DateTimeTicks);
                listItem.SubItems.Add(string.IsNullOrEmpty(itemToTrack.LocalizedName) ? itemToTrack.Id : itemToTrack.LocalizedName);
                listItem.SubItems.Add(itemToTrack.Description);
                switch (itemToTrack.Result)
                {
                    case ComponentResult.Error:
                    case ComponentResult.Failure:
                        listItem.ImageIndex = 1;
                        break;
                    case ComponentResult.Warning:
                        listItem.ImageIndex = 2;
                        break;
                    default:
                        listItem.ImageIndex = 0;
                        break;
                }
                listItem.SubItems.Add(itemToTrack.ErrorText);
                ListViewDelegates.AddItem(lvSolutionComponentDetail, listItem);
            }
            lvSolutionComponentDetail.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void tsbExportSolutionLog_Click(object sender, EventArgs e)
        {
            if (lvSolutionImports.SelectedItems.Count > 0)
            {
                var importJobId = new Guid(lvSolutionImports.SelectedItems[0].SubItems[11].Text);
                WorkAsync(new WorkAsyncInfo("Save Solution Import Log..", ExecuteExportLogRequest)
                {
                    PostWorkCallBack = ProcessExportLogResponse, 
                    AsyncArgument = importJobId
                });
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
                FileName =
                    $"{lvSolutionImports.SelectedItems[0].SubItems[2].Text}-{DateTime.Parse(lvSolutionImports.SelectedItems[0].SubItems[0].Text):yyyyMMdd}.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dialog.FileName, args.Result.ToString());
                MessageBox.Show(this, "Successfully saved the import log file. This XML file can be opened in Microsoft Excel.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(dialog.FileName);
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

    struct ComponentResult
    {
        public const string Warning = "warning";
        public const string Error = "error";
        public const string Failure = "failure";
        public const string Success = "success";
    }

    struct SolutionComponent
    {
        public const string Name = "name";
        public const string LocalizedName = "LocalizedName";
        public const string LocalizedNames = "LocalizedNames";
        public const string Id = "id";
        public const string Result = "result";
        public const string Description = "Description";
        public const string ErrorCode = "errorcode";
        public const string ErrorText = "errortext";
        public const string Processed = "processed";
        public const string DateTimeTicks = "datetimeticks";
    }

    struct SolutionElement
    {
        public const string SolutionManifests = "solutionManifests";
        public const string SolutionManifest = "solutionManifest";
        public const string UpgradeSolutionPackageInformation = "upgradeSolutionPackageInformation";
        public const string LocalizedName = "LocalizedName";
        public const string LocalizedNames = "LocalizedNames";
        public const string Version = "Version";
        public const string Managed = "Managed";
        public const string CurrentVersion = "currentVersion";
        public const string FileVersion = "fileVersion";
        public const string Publisher = "Publisher";
        public const string UniqueName = "UniqueName";
    }

    class ImportResult
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string LocalizedName { get; set; }
        public string Description { get; set; }
        public string Processed { get; set; }
        public string Result { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorText { get; set; }
        public string DateTimeTicks { get; set; }
    }
}
