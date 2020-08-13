namespace FSAM.Api.Responses
{
    public class AllocateResponse
    {
        public int BusinessUsage { get; set; }
        public decimal BusinessAmount { get; set; }

        public int EconomyUsage { get; set; }
        public decimal EconomyAmount { get; set; }
    }
}