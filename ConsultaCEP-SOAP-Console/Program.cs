using System;
using System.Threading.Tasks;
using TestSOAPConsole.Models;

namespace TestSOAPConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Cep cep = new Cep();
            Console.WriteLine("Digite o CEP :");
            cep.Codigo = Console.ReadLine();

            Endereco enderecoEncontrado = GetEndereco(cep);

            Console.WriteLine("Endereço encontrado:");
            Console.WriteLine(enderecoEncontrado.Descricao);
            Console.WriteLine(enderecoEncontrado.Complemento);
            Console.WriteLine(enderecoEncontrado.Bairro);
            Console.WriteLine(enderecoEncontrado.Cidade);
            Console.WriteLine(enderecoEncontrado.UF);
        }

        private static Endereco GetEndereco(Cep cep)
        {
            Endereco enderecoEncontrado = new Endereco();
            var correios = new Correios.AtendeClienteClient();

            var consulta = correios.consultaCEPAsync(cep.Codigo.Replace("-", "")).Result;

            if (consulta != null)
            {
                enderecoEncontrado = new Models.Endereco()
                {
                    Descricao = consulta.@return.end,
                    Complemento = consulta.@return.complemento2,
                    Bairro = consulta.@return.bairro,
                    Cidade = consulta.@return.cidade,
                    UF = consulta.@return.uf
                };
            }
            return enderecoEncontrado;
        }
    }
}