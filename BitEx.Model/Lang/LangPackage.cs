
using System.Collections.Generic;

namespace BitEx.Model.Lang
{
    public class LangPackage
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string Group { get; set; }
        public List<LangText> TextList { get; set; }
    }
}
