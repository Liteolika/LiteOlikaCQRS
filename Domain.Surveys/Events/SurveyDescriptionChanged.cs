using Core.Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Surveys.Events
{
    [Serializable]
    public class SurveyDescriptionChanged : Event
    {

        public string Description { get; internal set; }

        public SurveyDescriptionChanged(Guid aggregateId, string description)
        {
            this.AggregateId = aggregateId;
            this.Description = description;
        }

    }
}
