using CrudFireBird.Controllers;
using CrudFireBird.Models;
using FirebirdSql.Data.FirebirdClient;
using System.Collections.Generic;
using System.Data;

namespace CrudFireBird.Data.Banco
{
    public class Banco
    {
        private static string pUser = "SYSDBA";
        private static string pPassword = "masterkey";
        private static string pDatabase = "localhost:D:\\Ambiente_Desenvolvimento\\Aulas_C#\\Projeto MVC - ASP.NET - Crud Firebird\\CrudFireBird\\CrudFireBird\\Dados\\TESTE02.FDB";
        private static string pDataSource = "localhost";
        private static int pPort = 3050;
        private static int pDialet = 3;
        private static string pCharset = FbCharset.Utf8.ToString();
        

        private FbConnection connection;

        public bool bconexao { get; set; }

        public Banco()
        {
            FbConnectionStringBuilder stringconnection = new FbConnectionStringBuilder()
            {
                Port=pPort,
                UserID=pUser,
                Password=pPassword,
                Database=pDatabase,
                Dialect=pDialet,
                Charset=pCharset
            };

            try
            {
                connection = new FbConnection(stringconnection.ToString());
                connection.Open();
                bconexao = true;
            }
            catch (Exception ex)
            {
                bconexao = false;
            }
            

            
        }

        public DataTable RetornoTabela(string select)
        {
            DataTable tabela = new DataTable();
            FbCommand comando = new FbCommand(select, connection);
            FbDataAdapter fbda = new FbDataAdapter(comando);

            fbda.Fill(tabela);

            return tabela;

        }

        public void InseriDados(string insert, HomeModel dados)
        {

            FbCommand comando = new FbCommand(insert, connection);
            comando.CommandType = CommandType.Text;

            comando.Parameters.Add("@CODIGO", dados.CODIGO);
            comando.Parameters.Add("@NOME", dados.NOME.ToUpper());
            comando.Parameters.Add("@IDADE", dados.IDADE);

            comando.ExecuteNonQuery();
            
        }

        public DataTable RetornoDado(string select, int CODIGO)
        {
            DataTable tabela = new DataTable();
            FbDataAdapter da = new FbDataAdapter(select, connection);
            da.SelectCommand.Parameters.Add("@CODIGO", CODIGO);
            da.Fill(tabela);

            return tabela;
        }

        public void ExcluirDado(string delete, HomeModel dado)
        {
            FbCommand comando = new FbCommand(delete, connection);
            comando.CommandType = CommandType.Text;

            comando.Parameters.Add("@CODIGO", dado.CODIGO);

            comando.ExecuteNonQuery();
        }

    }
}
