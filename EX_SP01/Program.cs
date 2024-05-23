using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using EX_SP01;

internal class Program
{
    private static void Main(string[] args)
    {
        int opc = 0;
        do
        {
            Console.Clear();
            Console.WriteLine(">>>>>>>>>>> Menu  Principal <<<<<<<<<<<");
            Console.WriteLine(">>> 1 - Chamar uma SP (sem retorno) <<<");
            Console.WriteLine(">>> 2 - Chamar uma SP (com retorno) <<<");
            Console.WriteLine(">>> 0 - Sair <<<");
            Console.Write(">>> Opção desejada:   <<<\b\b\b\b\b");
            try
            {
                opc = Convert.ToInt32(Console.ReadLine());
                switch (opc)
                {
                    case 1:
                        Chamar_SP();
                        break;
                    case 2:
                        Chamar_SP2();
                        break;
                }
            }
            catch (Exception e)
            {
                // Console.WriteLine("Informe um valor válido");
            }
        } while (opc != 0);
        Console.WriteLine(">>>>>>>>>>>>>>>  F I M  <<<<<<<<<<<<<<<");
    }
    private static void Chamar_SP()
    {
        #region Conexao com o Banco
        Conexao_Banco conn = new Conexao_Banco();
        SqlConnection conexaosql = new SqlConnection(conn.Caminho());
        conexaosql.Open();
        #endregion

        #region Inserir_Contato
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[INSERIR_CONTATO]", conexaosql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar)).Value = "Nome do Contato";
            cmd.Parameters.Add(new SqlParameter("@Telefone", SqlDbType.VarChar)).Value = "16999999999";
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar)).Value = "email@email.com";
            var returnValue = cmd.ExecuteNonQuery(); //Retorna a linha que foi executada

            Console.WriteLine("\n\nRetorno da SP:\n" + returnValue.ToString());
            Console.WriteLine("Contato Inserido!");
            Console.ReadKey();
        }
        catch (Exception e)
        {
            //Caso queria controlar o erro
            Console.WriteLine("\nMensagem da Exception que aconteceu:");
            Console.WriteLine(e.ToString());
            Console.ReadKey();
        }
        finally
        {
            conexaosql.Close();
        }
        #endregion
    }


    private static void Chamar_SP2()
    {
        #region Conexao com o Banco
        Conexao_Banco conn = new Conexao_Banco();
        SqlConnection conexaosql = new SqlConnection(conn.Caminho());
        conexaosql.Open();
        #endregion

        #region Consultar_Contato
        try
        {
            SqlCommand cmd = new SqlCommand("[dbo].[CONSULTAR_CONTATO]", conexaosql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar)).Value = "Matheus";
            var returnValue = cmd.ExecuteReader(); //Retorna a linha que foi executada
            if (!returnValue.HasRows)
            {
                Console.WriteLine("Contato não localizado");
                Console.ReadKey();
                //throw new Exception("Não há registros"); //Poderia retornar uma Exception
            }
            else
            {
                Console.WriteLine("\n\nContato Localizado:");
                while (returnValue.Read())
                {
                    int id = int.Parse(returnValue["id"].ToString());
                    string nome = returnValue["nome"].ToString();
                    string telefone = returnValue["telefone"].ToString();
                    string email = returnValue["email"].ToString();
                    //Poderia ter a classe e instanciar o contato aqui com os valores
                    Console.WriteLine("\nDados do Contato Localizado:");
                    Console.WriteLine($"ID: {id}, Nome: {nome}, Telefone: {telefone}, Email: {email}");
                    Console.WriteLine("-x-x-x-x-x-x-x-x-");
                    Console.ReadKey();
                }
                Console.WriteLine("Consulta Executada!");
                Console.ReadKey();
            }
        }
        catch (Exception e)
        {
            //Caso queria controlar o erro
            Console.WriteLine("\nMensagem da Exception que aconteceu:");
            Console.WriteLine(e.ToString());
            Console.ReadKey();
        }
        finally
        {
            conexaosql.Close();
        }
        #endregion
    }
}
