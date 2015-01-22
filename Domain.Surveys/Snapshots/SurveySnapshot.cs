using Core.Shared.Domain.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Surveys.Snapshots
{
    [Serializable]
    public class SurveySnapshot : SnapshotBase
    {

        public string Title { get; internal set; }
        public string Description { get; internal set; }
        public bool IsActive { get; internal set; }

        public int EventVersion { get; set; }

        public SurveySnapshot(Guid id, int version, string title,string description, bool isActive)
        {
            this.Id = id;
            this.Version = version;
            this.EventVersion = version;

            this.Title = title;
            this.Description = description;
            this.IsActive = isActive;
            
        }

    }
}
