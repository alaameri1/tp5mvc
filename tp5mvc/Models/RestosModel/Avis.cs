namespace tp5mvc.Models.RestosModel
{
    public class Avis
    {
        public int CodeAvis { get; set; }
        public String NomPersonne { get; set; } = string.Empty;
        public int Note { get; set; }
        public String Commentaire { get; set; } = string.Empty;
        public int NumResto { get; set; }
        public  Restaurant? LeResto {get;set;}

    }
}
