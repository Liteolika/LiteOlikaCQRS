using Core.ApplicationServices;
using Domain.Surveys.Commands;
using Domain.Surveys.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test.WebApplication.Controllers
{
    public class HomeController : Controller
    {

        private readonly ISurveyViewModelStore _surveyViewModelStore;

        public HomeController(ISurveyViewModelStore surveyViewModelStore)
        {
            if (surveyViewModelStore == null) throw new ArgumentNullException("surveyViewModelStore");
            _surveyViewModelStore = surveyViewModelStore;
        }

        public ActionResult Index()
        {
            var surveys = _surveyViewModelStore.SurveyDetailModels.GetItems();

            return View(surveys);
        }

        [HttpGet]
        public ActionResult AddSurvey()
        {
            return View(new NewSurveyViewModel());
        }

        [HttpPost]
        public ActionResult AddSurvey(NewSurveyViewModel model)
        {
            ServiceLocator.CommandBus.Send(new CreateSurvey(Guid.NewGuid(), 0, model.Title, model.Description, model.IsActive));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditSurvey(Guid surveyId)
        {
            var surveyModel = _surveyViewModelStore.SurveyDetailModels.GetById(surveyId);
            return View(surveyModel);
        }

        [HttpPost]
        public ActionResult UpdateSurvey(SurveyDetailModel model)
        {
            ServiceLocator.CommandBus.Send(
                new ChangeSurvey(model.Id, model.Version, model.Title, model.Description, model.IsActive));

            return RedirectToAction("Index");
        }


    }
}