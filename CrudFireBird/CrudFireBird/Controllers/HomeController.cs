using CrudFireBird.Data.Banco;
using CrudFireBird.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CrudFireBird.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            Banco conexao = new Banco();
            List<HomeModel> listarDados = new List<HomeModel>();
            if (conexao.bconexao == true)
            {
                var dt = conexao.RetornoTabela("SELECT * FROM CADASTRO ORDER BY NOME");
                HomeModel tratarDados = new HomeModel();
                listarDados = tratarDados.ListarTabela(dt);
            }


            return View(listarDados);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {

            return View();
        }


        [HttpPost]
        public IActionResult Cadastrar(HomeModel dados)
        {
            Banco conexao = new Banco();

            if (ModelState.IsValid)
            {
                string sql = "INSERT INTO CADASTRO(CODIGO, NOME, IDADE) VALUES(@CODIGO, @NOME, @IDADE) ;";
                conexao.InseriDados(sql, dados);
                return RedirectToAction("Index");
            }

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
