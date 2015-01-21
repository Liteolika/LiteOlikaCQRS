using Core.Shared.EventHandlers;
using Domain.Surveys.Events;
using Domain.Surveys.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices.Eventhandlers
{
    public class SurveyEventHandlers :
        IEventHandler<SurveyCreated>,
        IEventHandler<SurveyTitleChanged>,
        IEventHandler<SurveyDescriptionChanged>,
        IEventHandler<SurveyIsActiveChanged>,
        IEventHandler<SurveyActiveFromChanged>,
        IEventHandler<SurveyActiveToChanged>
    {

        private readonly ISurveyViewModelStore _repository;

        public SurveyEventHandlers(ISurveyViewModelStore repository)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            _repository = repository;
        }

        public void Handle(SurveyCreated e)
        {
            SurveyDetailModel dto = new SurveyDetailModel()
            {
                Id = e.AggregateId,
                Title = e.Title,
                Description = e.Description,
                Version = e.aggregateVersion,
                IsActive = e.IsActive,
                ActiveFrom = e.ActiveFrom,
                ActiveTo = e.ActiveTo
            };


            _repository.SurveyDetailModels.Add(dto);
            _repository.Save();
        }

        public void Handle(SurveyTitleChanged e)
        {
            var dto = _repository.SurveyDetailModels.GetById(e.AggregateId);
            dto.Title = e.Title;
            dto.Version = e.aggregateVersion;
            _repository.Save();
        }

        public void Handle(SurveyDescriptionChanged e)
        {
            var dto = _repository.SurveyDetailModels.GetById(e.AggregateId);
            dto.Description = e.Description;
            dto.Version = e.aggregateVersion;
            _repository.Save();
        }

        public void Handle(SurveyIsActiveChanged e)
        {
            var dto = _repository.SurveyDetailModels.GetById(e.AggregateId);
            dto.IsActive = e.IsActive;
            dto.Version = e.aggregateVersion;
            _repository.Save();
        }

        public void Handle(SurveyActiveFromChanged e)
        {
            var dto = _repository.SurveyDetailModels.GetById(e.AggregateId);
            dto.ActiveFrom = e.ActiveFrom;
            dto.Version = e.aggregateVersion;
            _repository.Save();
        }

        public void Handle(SurveyActiveToChanged e)
        {
            var dto = _repository.SurveyDetailModels.GetById(e.AggregateId);
            dto.ActiveTo = e.ActiveTo;
            dto.Version = e.aggregateVersion;
            _repository.Save();
        }
    }
}
