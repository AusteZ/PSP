namespace PSP.Models.DTOs
{
    public class ServiceCreate
    {
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public float EuroCost { get; set; }
        public int MinutesLength { get; set; }
    }
}
