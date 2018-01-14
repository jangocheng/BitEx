using BitEx.Core;

namespace BitEx.IRepository.langue
{
    public interface ILangContainer
    {
        string GetMessage(Lang lang,string group, string code);
    }
}
