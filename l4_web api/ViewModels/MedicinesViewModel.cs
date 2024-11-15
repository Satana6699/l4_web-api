using l4_web_api.Models;
using System.Collections.Generic;

namespace l4_web_api.ViewModels
{
    public class MedicinesViewModel
    {
        public IEnumerable<Medicine> Medicines { get; set; }
    }

}
