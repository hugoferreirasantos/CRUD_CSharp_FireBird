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

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            Banco conexao = new Banco();
            HomeModel dado = new HomeModel();

            if (conexao.bconexao == true)
            {
                string sql = "SELECT * FROM CADASTRO WHERE CODIGO = @CODIGO";

                var dt = conexao.RetornoDado(sql, (int)id);

                HomeModel tratarDados = new HomeModel();
                dado = tratarDados.RetornoDadoPorCODIGO(dt);

                

            }

            return View(dado);

        }

        [HttpGet]
        public IActionResult Excluir(int ? id)
        {
            Banco conexao = new Banco();
            HomeModel dado = new HomeModel();

            if(conexao.bconexao == true)
            {
                string sql = "SELECT * FROM CADASTRO WHERE CODIGO = @CODIGO";

                var dt = conexao.RetornoDado(sql, (int)id);

                HomeModel tratardado = new HomeModel();
                dado = tratardado.RetornoDadoPorCODIGO(dt);

            }

            return View(dado);
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

        [HttpPost]
        public IActionResult Editar(HomeModel dados)
        {
            Banco conexao = new Banco();

            if (ModelState.IsValid)
            {
                string sql = "UPDATE CADASTRO SET CODIGO = @CODIGO, NOME = @NOME, IDADE = @IDADE WHERE CODIGO = @CODIGO ;";
                conexao.InseriDados(sql, dados);
                return RedirectToAction("Index");
            }

            return View();

        }

        [HttpPost]
        public IActionResult Excluir(HomeModel dado)
        {
            Banco conexao = new Banco();

            if (ModelState.IsValid)
            {
                string sql = "DELETE FROM CADASTRO WHERE CODIGO = @CODIGO";
                conexao.ExcluirDado(sql, dado);
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
