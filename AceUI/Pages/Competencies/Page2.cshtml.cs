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
    public class Page2Model : PageModel
    {

        public const string SerializedCompetencyJSONKey = "_CompetencySerliazed";

        [BindProperty]
        [Display(Name="Competency Name")]
        public string CompetencyName {get; set;}

        [BindProperty]
        [Display(Name="Competency Description")]        
        public string CompetencyDescription {get; set;}

        [BindProperty]
        public IEnumerable<Disposition> DispositionList {get; set;}   

        [BindProperty]
        public List<DispositionSelectorModel> DispositionDisplayList  {get; set;}

        public CompetencyBuilderViewModel Cbvm {get; set;}

        private readonly ILogger<Page1Model> _logger;
        private readonly IUnitOfWork _UOW;        

        public Page2Model(ILogger<Page1Model> logger, IUnitOfWork uow)
        {
            _logger = logger;
            _UOW = uow;
        }

        public async Task OnGetAsync()
        {

            _logger.LogInformation("IN ON GET ASYNC");

            await HttpContext.Session.LoadAsync();            

            //read from session variable
            var serializedin = HttpContext.Session.GetString(SerializedCompetencyJSONKey);
            Cbvm = JsonSerializer.Deserialize<CompetencyBuilderViewModel>(serializedin);              

            //read into bound properties
            CompetencyName = Cbvm.CompetencyName;
            CompetencyDescription = Cbvm.CompetencyDescription;

            //get dispositions
            var repository = _UOW.GetRepositoryAsync<Disposition>();
            IEnumerable<Disposition> dlist = await repository.GetListAsync();
            DispositionDisplayList = GetDispositionDisplayList(dlist.ToList());
            _logger.Log(LogLevel.Information, "Dispositions Retrieved and Converted");
        }

        public async Task<IActionResult>  OnPostAsync(){

            _logger.LogInformation("IN ON POST ASYNC");

            await HttpContext.Session.LoadAsync();

            List<int> temp = new List<int>();

            foreach(DispositionSelectorModel dsm in DispositionDisplayList)
            {
                if(dsm.Selected){
                    temp.Add(dsm.Id);
                    _logger.LogInformation($"Added: {dsm.Id.ToString()} to the indicies");
                    _logger.LogInformation(dsm.Name);
                }
            }            

            if(string.IsNullOrEmpty(HttpContext.Session.GetString(SerializedCompetencyJSONKey)))
            {

                Cbvm = new CompetencyBuilderViewModel();

                Cbvm.CompetencyName = CompetencyName;
                Cbvm.CompetencyDescription = CompetencyDescription;
                Cbvm.DispositionIndicies = temp.ToArray();                

                var serialized = JsonSerializer.Serialize(Cbvm);
                HttpContext.Session.SetString(SerializedCompetencyJSONKey, serialized);

            }else {
                //read from session variable
                var serializedin = HttpContext.Session.GetString(SerializedCompetencyJSONKey);
                Cbvm = JsonSerializer.Deserialize<CompetencyBuilderViewModel>(serializedin);

                //read from session variable
                Cbvm.CompetencyName = CompetencyName;
                _logger.LogInformation($"IN POST PAGE 2, Session out: {Cbvm.CompetencyName}");
                Cbvm.CompetencyDescription = CompetencyDescription;
                _logger.LogInformation($"IN POST PAGE 2, Session out: {Cbvm.CompetencyDescription}");
                Cbvm.DispositionIndicies = temp.ToArray();                

                var serializedout = JsonSerializer.Serialize(Cbvm);
                _logger.LogInformation($"IN POST PAGE 2, Serialized out: {serializedout.ToString()}");
                HttpContext.Session.SetString(SerializedCompetencyJSONKey, serializedout);                
            }

            return RedirectToPage("/Competencies/Page3");

        }

        /// <summary>
        /// Convert to a list that we can use in the page.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<DispositionSelectorModel> GetDispositionDisplayList(List<Disposition> list){
            List<DispositionSelectorModel> outlist = new List<DispositionSelectorModel>();

            foreach(Disposition d in list){
                DispositionSelectorModel dsm = new DispositionSelectorModel();
                dsm.Id = d.Id;
                dsm.Name = d.Name;
                dsm.Description = d.Description;
                dsm.Discipline = d.Discipline;
                dsm.Category = d.Category;
                dsm.Selected = false;
                outlist.Add(dsm);
            }

            return outlist;
        }        
    }
}

