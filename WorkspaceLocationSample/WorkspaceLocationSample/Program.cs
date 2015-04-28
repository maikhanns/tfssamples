using System;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace WorkspaceLocationSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var teamProjectPicker = new TeamProjectPicker(TeamProjectPickerMode.NoProject, false);
            teamProjectPicker.ShowDialog();

            var teamProjectCollection = teamProjectPicker.SelectedTeamProjectCollection;
            var versionControlService = teamProjectCollection.GetService<VersionControlServer>();

            var workspaces = versionControlService.QueryWorkspaces(null, null, null);

            foreach (var workspace in workspaces)
                Console.WriteLine("Workspace: {0}\nComputer: {1}\nOwner: {2}\nLocation: {3}\n\n",
                    workspace.Name, workspace.Computer, workspace.OwnerDisplayName, workspace.Location);

            Console.ReadKey();
        }
    }
}
