using System.ComponentModel.DataAnnotations;

namespace Logic.Dtos
{
    public class UpdateAthleteDto
    {
        [MaxLength(100, ErrorMessage = "Name is too long")]
        public virtual string Name { get; set; }
    }
}
