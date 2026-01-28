namespace NetFlow.Application.Personnels
{
    public class TerminatePersonnelRequest
    {
        public int Id { get; set; }
        public DateTime? TerminationDate { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
