﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Surveys.ViewModels
{
    public class NewSurveyViewModel
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

    }
}
