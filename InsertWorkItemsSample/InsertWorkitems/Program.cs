using System.Linq;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace InsertWorkItems
{
    class Program
    {
        static void Main(string[] args)
        {
            var teamProjectPicker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false);
            teamProjectPicker.ShowDialog();

            var teamProjectCollection = teamProjectPicker.SelectedTeamProjectCollection;
            var projectInfo = teamProjectPicker.SelectedProjects.FirstOrDefault();

            if (projectInfo == null) return;

            var workItemStore = teamProjectCollection.GetService<WorkItemStore>();
            var teamProject = workItemStore.Projects[projectInfo.Name];

            var workItemPBI = new WorkItem(teamProject.WorkItemTypes["Product Backlog Item"])
            {
                Title = "Product Backlog Item Demo",
                Description = "Description"
            };

            workItemPBI.Fields[CoreField.AssignedTo].Value
                = teamProjectCollection.AuthorizedIdentity.DisplayName;

            var workItemTestCase = new WorkItem(teamProject.WorkItemTypes["Test Case"])
            {
                Title = "Test Case Demo",
            };

            workItemTestCase.Fields["Assigned to"].Value
                = teamProjectCollection.AuthorizedIdentity.DisplayName;

            workItemTestCase.Save();

            var linkTypeEnd = workItemStore.WorkItemLinkTypes.LinkTypeEnds["Tested By"];
            workItemPBI.WorkItemLinks.Add(new WorkItemLink(linkTypeEnd, workItemTestCase.Id));
            workItemPBI.Save();
        }
    }
}
