using System;
using System.Windows.Forms;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace CustomCommentCheckinPolicy
{
    [Serializable]
    public class CheckForCommentsPolicy : PolicyBase
    {
        /// <summary>
        /// Shows a configuration dialog box to the user to specify options for the check-in policy.
        /// </summary>
        /// <returns>
        /// True if the dialog box opened; otherwise, false.
        /// </returns>
        /// <param name="policyEditArgs">Arguments for the configuration dialog box.</param>
        public override bool Edit(IPolicyEditArgs policyEditArgs)
        {
            return true;
        }

        /// <summary>
        /// Gets the name of this policy.
        /// </summary>
        /// <returns>
        /// The name of this policy.
        /// </returns>
        public override string Type
        {
            get { return "Custom Comment Policy"; }
        }

        /// <summary>
        /// Gets the description of this kind of policy.
        /// </summary>
        /// <returns>
        /// The description of this kind of policy.
        /// </returns>
        public override string TypeDescription
        {
            get { return "This policy will check the comment for 10 or more characters."; }
        }

        /// <summary>
        /// Gets the description of this policy.
        /// </summary>
        /// <returns>
        /// The description of this policy.
        /// </returns>
        public override string Description
        {
            get { return "The Checkin Comment must contain 10 or more characters"; }
        }

        /// <summary>
        /// Called if the user double-clicks on a policy failure.
        /// </summary>
        /// <param name="failure">The policy failure that causes this event.</param>
        public override void Activate(PolicyFailure failure)
        {
            MessageBox.Show("Please type a comment with 10 or more characters", "Resolving Comment Failure");
        }

        /// <summary>
        /// Gets a flag that describes whether this policy is configurable.
        /// </summary>
        /// <returns>
        /// True if this policy is configurable; otherwise, false.
        /// </returns>
        public override bool CanEdit
        {
            get { return false; }
        }

        /// <summary>
        /// Performs the policy evaluation.
        /// </summary>
        /// <returns>
        /// An array of PolicyFailures that results from the evaluation.
        /// </returns>
        public override PolicyFailure[] Evaluate()
        {
            var comment = PendingCheckin.PendingChanges.Comment.Trim();

            if (comment.Length < 10)
                return new[]
                {
                    new PolicyFailure("Please type a commen with 10 or more characters", this)
                };

            return new PolicyFailure[0];
        }
    }
}