using Core.Shared.ViewModels;
using Domain.Surveys.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Database.EntityFramework
{
    public class SurveyViewModelStore : DbContextBase, ISurveyViewModelStore
    {

        public SurveyViewModelStore(string connectionStringOrName)
            : base(connectionStringOrName)
        {
            this._surveyDetailRepository = new SurveyViewModelRepository<SurveyDetailModel>(this._SurveyDetailModels);
        }

        public DbSet<SurveyDetailModel> _SurveyDetailModels { get; set; }

        private readonly SurveyViewModelRepository<SurveyDetailModel> _surveyDetailRepository;

        public IViewModelRepository<SurveyDetailModel> SurveyDetailModels
        {
            get { return _surveyDetailRepository; }
        }

        public void Save()
        {
            SaveChanges();
        }
    }
}
