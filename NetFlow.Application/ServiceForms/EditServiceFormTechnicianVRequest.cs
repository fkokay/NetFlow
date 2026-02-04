namespace NetFlow.Application.ServiceForms
{
    public class EditServiceFormTechnicianVRequest
    {
        public int ServiceFormId { get; set; }
        public bool IsTechnicianAssigned { get; set; }
        public int CreatedBy { get; set; }
    }

}
