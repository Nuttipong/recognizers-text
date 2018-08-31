using System.Collections.Generic;

namespace RecognizersTextApp.Model
{
    public class SpecModel
    {
        public string Input { get; set; }
        public IEnumerable<object> Results { get; set; }
        public string Lang { get; set; }
        public string Method { get; set; }
    }
}
