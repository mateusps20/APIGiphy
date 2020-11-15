using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TelaDeLogin.Models
{
    [Table("imagens")]
    public class Giphy
    {
        public int Id_Imagem { get; set; }
        public int Id { get; set; }
        public string Url { get; set; }
        public string Descricao { get; set; }

    }
}
