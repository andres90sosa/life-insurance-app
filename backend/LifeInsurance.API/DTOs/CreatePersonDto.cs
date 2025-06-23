namespace LifeInsurance.API.DTOs
{
    public class CreatePersonDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Identification { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public bool? Drives { get; set; }
        public bool? UsesGlasses { get; set; }
        public bool? IsDiabetic { get; set; }
        public string? OtherDiseases { get; set; }
    }
}
