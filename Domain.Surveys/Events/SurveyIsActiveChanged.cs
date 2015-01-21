using Core.Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Surveys.Events
{
    [Serializable]
    public class SurveyIsActiveChanged : Event
    {

        public bool IsActive { get; internal set; }

        public SurveyIsActiveChanged(Guid aggregateId, bool isActive)
        {
            this.AggregateId = aggregateId;
            this.IsActive = isActive;
        }

    }

}
