namespace LifeInsurance.Domain.Entities
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Identification { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool? Drives { get; set; }
        public bool? UsesGlasses { get; set; }
        public bool? IsDiabetic { get; set; }
        public string? OtherDiseases { get; set; }
    }
}
