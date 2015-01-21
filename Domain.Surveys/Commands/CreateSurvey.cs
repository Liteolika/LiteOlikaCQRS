using Core.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Surveys.Commands
{
    [Serializable]
    public class CreateSurvey : Command
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public CreateSurvey(Guid aggregateId, int aggregateVersion, string title, string description, bool isActive)
            : base(aggregateId, aggregateVersion)
        {
            this.Title = title;
            this.Description = description;
            this.IsActive = isActive;
        }

    }
}
