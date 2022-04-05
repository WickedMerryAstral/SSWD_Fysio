namespace SSWD_Fysio.Models
{
    public class VektisTreatment
    {
        public int id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string explanation { get; set; }
        public bool hasMandatoryExplanation { get; set; }

        public VektisTreatment(string code, string description, string explanation, bool hasMandatoryExplanation)
        {
            this.code = code;
            this.description = description;
            this.explanation = explanation;
            this.hasMandatoryExplanation = hasMandatoryExplanation;
        }
    }
}
