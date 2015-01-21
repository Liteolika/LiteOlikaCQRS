using Core.Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Surveys.Events
{
    [Serializable]
    public class SurveyActiveFromChanged : Event
    {

        public DateTime ActiveFrom { get; internal set; }

        public SurveyActiveFromChanged(Guid aggregateId, DateTime activeFrom)
        {
            this.AggregateId = aggregateId;
            this.ActiveFrom = activeFrom;
        }

    }
}
