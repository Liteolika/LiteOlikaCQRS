using Core.Shared.CommandHandlers;
using Core.Shared.Storage;
using Domain.Surveys;
using Domain.Surveys.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices.CommandHandlers
{
    public class SurveyCommandHandler : 
        ICommandHandler<CreateSurvey>,
        ICommandHandler<ChangeSurvey>
    {

        private readonly IEventRepository<Survey> _repository;

        public SurveyCommandHandler(IEventRepository<Survey> repository)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            _repository = repository;
        }

        public void Execute(CreateSurvey command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var aggregate = new Survey(
                command.AggregateId, 
                command.Title, 
                command.Description, 
                command.IsActive);
            aggregate.AggregateVersion = -1;
            _repository.Save(aggregate, aggregate.AggregateVersion);
        }

        public void Execute(ChangeSurvey command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var aggregate = _repository.GetById(command.AggregateId);

            if (aggregate.Title != command.Title)
                aggregate.ChangeTitle(command.Title);

            if (aggregate.Description != command.Description)
                aggregate.ChangeDescription(command.Description);

            if (aggregate.IsActive != command.IsActive)
                aggregate.ChangeIsActive(command.IsActive);

            _repository.Save(aggregate, command.AggregateVersion);
        }
    }
}
