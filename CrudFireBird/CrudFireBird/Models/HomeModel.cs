using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CrudFireBird.Models
{
    public class HomeModel
    {
        public int CODIGO { get; set; }


        [Required(ErrorMessage = "Digite um nome!")]
        public string NOME { get; set; }


        public int IDADE { get; set; }

        public List<HomeModel> ListarTabela(DataTable dt)
        {
            List<HomeModel> lista = new List<HomeModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HomeModel cadastro = new HomeModel()
                { 
                    CODIGO = int.Parse(dt.Rows[i]["CODIGO"].ToString()),
                    NOME = dt.Rows[i]["NOME"].ToString(),
                    IDADE = int.Parse(dt.Rows[i]["IDADE"].ToString())

                };

                lista.Add(cadastro);
            }

            return lista;

        }

    }
}
