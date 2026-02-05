namespace MedicalEducation.Api.Dtos
{
    public class RotationCreateDto
    {
        public int TraineeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
