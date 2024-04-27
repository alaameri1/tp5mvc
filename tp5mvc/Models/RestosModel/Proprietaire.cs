namespace tp5mvc.Models.RestosModel
{
    public class Proprietaire
    {
        public int Numero { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Gsm { get; set; } = string.Empty;
        public virtual ICollection<Restaurant>? LesRestos { get; set; }

    }
}
