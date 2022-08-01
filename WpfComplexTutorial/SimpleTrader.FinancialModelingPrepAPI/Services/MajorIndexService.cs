using Newtonsoft.Json;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class MajorIndexService : IMajorIndexService
    {
        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            string uri = "Http/api..." + GetUriSuffix(indexType);
            using(HttpClient httpClient = new HttpClient())
            {
               HttpResponseMessage response = await httpClient.GetAsync(uri);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                MajorIndex majorIndex = JsonConvert.DeserializeObject<MajorIndex>(jsonResponse);
                return majorIndex;

            }
        }

        private string GetUriSuffix(MajorIndexType indexType)
        {
            switch (indexType)
            {
                case MajorIndexType.DowJones:
                    return ".DJI";
                case MajorIndexType.Nasdaq:
                        return ".IXIC";
                case MajorIndexType.SP500:
                        return ".INX";
                default :
                    return ".DJI";
            }
        }
    }
}
