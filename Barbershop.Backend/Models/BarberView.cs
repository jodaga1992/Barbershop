namespace Barbershop.Backend.Models
{
    using Domain2;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class BarberView : Barber
    {
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}