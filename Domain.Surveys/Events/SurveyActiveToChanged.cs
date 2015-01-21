using Core.Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Surveys.Events
{
    [Serializable]
    public class SurveyActiveToChanged : Event
    {

        public DateTime ActiveTo { get; internal set; }

        public SurveyActiveToChanged(Guid aggregateId, DateTime activeTo)
        {
            this.AggregateId = aggregateId;
            this.ActiveTo = activeTo;
        }

    }
}
