using l4_web_api.Models;

namespace l4_web_api.ViewModels
{
    public class PrescriptionsViewModel
    {
        public IEnumerable<Prescription> Prescriptions { get; set; }
    }
}
