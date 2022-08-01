using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.ViewModels
{
    public class MajorIndexViewModel
    {
        private readonly IMajorIndexService _majorIndexService;

        public MajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            _majorIndexService = majorIndexService;
        }

        public MajorIndex DowJones { get; set; }
        public MajorIndex Nasdaq { get; set; }
        public MajorIndex SP500 { get; set; }

        public static MajorIndexViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexViewModel majorIndexViewModel = new MajorIndexViewModel(majorIndexService);
            majorIndexViewModel.LoadMajorIndexes();
            return majorIndexViewModel;
        }

        private async Task LoadMajorIndexes()
        {
            DowJones = await _majorIndexService.GetMajorIndex(MajorIndexType.DowJones);
            Nasdaq = await _majorIndexService.GetMajorIndex(MajorIndexType.Nasdaq);
            SP500 = await _majorIndexService.GetMajorIndex(MajorIndexType.SP500);
        }
    }
}
