using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Logging;
using AceData.Data;
using AceRepository;
using AceUI.ViewModels;

namespace AceUI.Pages.Competencies
{
    public class Page3Model : PageModel
    {
        public const string SerializedCompetencyJSONKey = "_CompetencySerliazed";

        public CompetencyBuilderViewModel Cbvm {get; set;}

        private readonly ILogger<Page1Model> _logger;
        private readonly IUnitOfWork _UOW;        

        public Page3Model(ILogger<Page1Model> logger, IUnitOfWork uow)
        {
            _logger = logger;
            _UOW = uow;            
        }

        public async Task OnGetAsync()
        {

            await HttpContext.Session.LoadAsync();            

            //read serialized object from session variable
            var serializedin = HttpContext.Session.GetString(SerializedCompetencyJSONKey);
            Cbvm = JsonSerializer.Deserialize<CompetencyBuilderViewModel>(serializedin);
            
        }
    }
}
