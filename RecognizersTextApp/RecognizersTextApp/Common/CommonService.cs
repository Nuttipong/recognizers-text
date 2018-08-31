using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognizersTextApp.Common
{
    public static class CommonService
    {
        //public IList<SpecModel> GetDateTimeSpec(string lang)
        //{
        //    var modelResults = new List<SpecModel>();
        //    var directorySpecs = Path.Combine("..", "..", "Specs", "DateTime", Languages[lang]);
        //    var specsFiles = Directory.GetFiles(directorySpecs);
        //    foreach (var specsFile in specsFiles)
        //    {
        //        var data = File.ReadAllText(specsFile);
        //        var specs = JsonConvert.DeserializeObject<IList<SpecModel>>(data);
        //        specs.Select(r => {
        //            r.Lang = lang;
        //            r.Method = Path.GetFileNameWithoutExtension(specsFile);
        //            return r;
        //        }).ToList();
        //        modelResults.AddRange(specs);
        //    }
        //    return modelResults;
        //}
    }
}
