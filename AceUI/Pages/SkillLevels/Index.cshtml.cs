using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using AceData.Data;
using AceRepository;

namespace AceUI.Pages.SkillLevels
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IEnumerable<SkillLevel> SkillLevelList {get; set;}

        private readonly ILogger<IndexModel> _logger;
        private readonly IUnitOfWork _UOW;

        public IndexModel(ILogger<IndexModel> logger, IUnitOfWork uow)
        {
            _logger = logger;
            _UOW = uow;            
        }

        public async Task OnGetAsync()
        {
            //var repository = _UOW.GetRepository<Disposition>();
            var repository = _UOW.GetRepositoryAsync<SkillLevel>();
            _logger.Log(LogLevel.Information, "Knowledge Elements Retrieved");
            _logger.Log(LogLevel.Information, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            SkillLevelList = await repository.GetListAsync();
        }
    }
}
