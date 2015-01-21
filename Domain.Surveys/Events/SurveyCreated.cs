using Core.Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Surveys.Events
{
    [Serializable]
    public class SurveyCreated : Event
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }

        public SurveyCreated(
            Guid aggregateId, 
            string title, 
            string description, 
            bool isActive,
            DateTime activeFrom,
            DateTime activeTo)
        {
            this.AggregateId = aggregateId;
            this.Title = title;
            this.Description = description;
            this.IsActive = isActive;
            this.ActiveFrom = activeFrom;
            this.ActiveTo = activeTo;
        }

    }
}
