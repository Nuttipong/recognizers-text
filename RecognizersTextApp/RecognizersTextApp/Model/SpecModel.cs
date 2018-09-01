using System.Collections.Generic;

namespace RecognizersTextApp.Model
{
    public class SpecModel
    {
        public string Input { get; set; }
        public IEnumerable<SpecModelResult> Results { get; set; }
        public string Lang { get; set; }
        public string Method { get; set; }
        public string TypeName { get; set; }
    }

    public class SpecModelResult
    {
        public string Text { get; set; }
        public string TypeName { get; set; }
        public object Resolution { get; set; }
        public string Type { get; set; }
    }
}
