using Core.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Surveys.ViewModels
{
    public interface ISurveyViewModelStore : IViewModelStore
    {

        IViewModelRepository<SurveyDetailModel> SurveyDetailModels { get; }

        
    }
}
