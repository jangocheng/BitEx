namespace BitEx.IGrain.Entity
{
    public class ConfirmatonInfo
    {
        public int Required { get; set; }
        public int Safe { get; set; }
        public ConfirmatonInfo(int required, int safe)
        {
            this.Required = required;
            this.Safe = safe;
        }
    }
}
