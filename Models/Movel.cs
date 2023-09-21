using System.ComponentModel.DataAnnotations;

namespace InduMovel.Models
{
    public class Movel
    {
        public int MovelId{get;set;}

        [Required(ErrorMessage ="Informe o nome do Móvel")]
        [Display(Name ="Móvel")]
        [MinLength(3,ErrorMessage ="Móvel deve ter no minimo {1} caracteres")]
        [MaxLength(30, ErrorMessage ="Móvel dever ter no maximo {1} caracteres")]
        public string Nome{get;set;}

        [Required(ErrorMessage ="Informe o nome da Descrição")]
        [Display(Name ="Descrição")]
        [MinLength(3,ErrorMessage ="Descrição deve ter no minimo {1} caracteres")]
        [MaxLength(200, ErrorMessage ="Descrição dever ter no maximo {1} caracteres")]
        public string Descricao{get;set;}

        [Display(Name ="Cor")]
        [MaxLength(20, ErrorMessage ="Cor deve ter no maximo {1} caracteres")]
        public string Cor{get;set;}

        [Display(Name ="Caminho da Imagem")]
        public string ImagemUrl{get;set;}

        [Display(Name ="Caminho da Imagem pequena")]
        public string ImagemPequena{get;set;}

        [Required(ErrorMessage ="Informe o valor")]
        [Display(Name ="Valor")]
        [Range(1,99999.99, ErrorMessage ="O valor de estar entre {1} e {2}")]
        public double Valor{get;set;}

        [Display(Name ="Em linha de Produção")]
        public bool EmProducao{get;set;}

        [Display(Name = "Ativo")]
        public bool Ativo{get;set;}

        [Required(ErrorMessage ="Informe a Categoria")]
        public int CategoriaId{get;set;}
        public virtual Categoria Categoria{get;set;}
    }

}