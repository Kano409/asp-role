﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class CreateStudentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Enrolled { get; set; }

        public IList<SelectListItem> Courses { get; set; }
    }
}
