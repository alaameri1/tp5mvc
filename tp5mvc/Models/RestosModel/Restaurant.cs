using System.Drawing.Text;

namespace tp5mvc.Models.RestosModel
{
    public class Restaurant
    {
        public int CodeResto { get; set; }
        public string NomResto { get; set; }=String.Empty;
        public string Specialite{ get; set; } = String.Empty;
        public string Ville { get; set; } = String.Empty;
        public string Tel { get; set; } = String.Empty;
        public int NumProp { get; set; }
        public Proprietaire? LeProprio { get; set; }
        public virtual ICollection<Avis>? LesAvis { get;set;}

    }
}
