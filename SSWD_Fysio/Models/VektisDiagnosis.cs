namespace SSWD_Fysio.Models
{
    public class VektisDiagnosis
    {
        public int id { get; set; }
        public string code { get; set; }
        public string location { get; set; }
        public string pathology { get; set; }
        public VektisDiagnosis(string diagnosisCode, string bodyLocation, string pathology)
        {
            this.code = diagnosisCode;
            this.location = bodyLocation;
            this.pathology = pathology;
        }
    }
}
