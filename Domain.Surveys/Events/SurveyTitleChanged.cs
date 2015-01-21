using Core.Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Surveys.Events
{
    [Serializable]
    public class SurveyTitleChanged : Event
    {

        public string Title { get; internal set; }

        public SurveyTitleChanged(Guid aggregateId, string title)
        {
            this.AggregateId = aggregateId;
            this.Title = title;
        }

    }
}
