using Core.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Surveys.Commands
{
    [Serializable]
    public class ChangeSurvey : Command
    {

        public string Title { get; internal set; }
        public string Description { get; internal set; }
        public bool IsActive { get; internal set; }

        public ChangeSurvey(Guid aggregateId, int aggregateVersion, string title, string description, bool isActive)
            : base(aggregateId, aggregateVersion)
        {
            this.Title = title;
            this.Description = description;
            this.IsActive = isActive;
        }


    }
}
