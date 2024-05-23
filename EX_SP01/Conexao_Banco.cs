using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX_SP01
{
    internal class Conexao_Banco
    {
        string strConexao = "";

        public Conexao_Banco()
        {
            strConexao += "Data Source=localhost;";             //Endereco do Servidor
            strConexao += "Initial Catalog=AgendaTelefone;";    //Nome da Base de Dados
            strConexao += "User Id=sa; Password=Senha@2024!;";  //Usuario e Senha
        }

        public string Caminho()
        {
            return strConexao;
        }
    }
}
