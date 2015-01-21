﻿using Core.Shared.Domain;
using Core.Shared.Domain.Mementos;
using Core.Shared.Events;
using Core.Shared.Storage.Memento;
using Domain.Surveys.Events;
using Domain.Surveys.Mementos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Surveys
{
    public class Survey : AggregateRoot,
        IHandle<SurveyCreated>,
        IHandle<SurveyTitleChanged>,
        IHandle<SurveyDescriptionChanged>,
        IHandle<SurveyIsActiveChanged>,
        IHandle<SurveyActiveFromChanged>,
        IHandle<SurveyActiveToChanged>,
        IOriginator
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }

        public Survey()
        {
            
        }

        public Survey(Guid id, string title, string description, bool isActive, DateTime activeFrom, DateTime activeTo)
        {
            ApplyChange(new SurveyCreated(id, title, description, isActive, activeFrom, activeTo));
        }


        public void Handle(SurveyCreated e)
        {
            this.AggregateId = e.AggregateId;
            this.AggregateVersion = e.aggregateVersion;
            this.Title = e.Title;
            this.Description = e.Description;
            this.IsActive = e.IsActive;
        }

        public void Handle(SurveyTitleChanged e)
        {
            Title = e.Title;
        }

        public void Handle(SurveyDescriptionChanged e)
        {
            Description = e.Description;
        }

        public void Handle(SurveyIsActiveChanged e)
        {
            IsActive = e.IsActive;
        }

        public void Handle(SurveyActiveFromChanged e)
        {
            ActiveFrom = e.ActiveFrom;
        }

        public void Handle(SurveyActiveToChanged e)
        {
            ActiveTo = e.ActiveTo;
        }


        public void ChangeTitle(string title)
        {
            ApplyChange(new SurveyTitleChanged(AggregateId, title));
        }

        public void ChangeDescription(string description)
        {
            ApplyChange(new SurveyDescriptionChanged(AggregateId, description));
        }

        public void ChangeIsActive(bool isActive)
        {
            ApplyChange(new SurveyIsActiveChanged(AggregateId, isActive));
        }

        public void ChangeActiveFrom(DateTime activeFrom)
        {
            ApplyChange(new SurveyActiveFromChanged(AggregateId, activeFrom));
        }

        public void ChangeActiveTo(DateTime activeTo)
        {
            ApplyChange(new SurveyActiveToChanged(AggregateId, activeTo));
        }



        public BaseMemento GetMemento()
        {
            return new SurveyMemento(AggregateId, AggregateVersion, Title, Description, IsActive);
        }

        public void SetMemento(BaseMemento memento)
        {
            this.AggregateId = memento.Id;
            this.AggregateVersion = memento.Version;

            Title = ((SurveyMemento)memento).Title;
            Description = ((SurveyMemento)memento).Description;
            IsActive = ((SurveyMemento)memento).IsActive;
            
        }






    }
}
