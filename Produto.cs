using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Excel_Ler
{
    public class Produto
    {
         public int Codigo { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        
        private const string PATH = "Database/produto.csv";

        /// <summary>
        /// Cria um diretório ou arquivo ou os dois, se não existirem
        /// </summary>
        public Produto(){
            if(!File.Exists(PATH)){
                Directory.CreateDirectory("Database");
                File.Create(PATH).Close();
            }
        }

        /// <summary>
        /// Cadastra produtos
        /// </summary>
        /// <param name="prod">serve para registrar um produto</param>
        public void Cadastrar(Produto prod){
            var linha = new string[] { PrepararLinha(prod) };
            File.AppendAllLines(PATH, linha);
        }
        
        /// <summary>
        /// Lê os produtos
        /// </summary>
        /// <returns>retorna os produtos espaçados</returns>
        public List<Produto> Ler(){
            // Criamos uma lista que servirá como nosso retorno
            List<Produto> produtos = new List<Produto>();;

            // Lemos o arquivo e transformamos em um array de linhas
            // [0] = codigo 1;nome = Fender; preco 5500;
            // [1] = codigo 2;nome = Gibson; preco 7500;
            string[] linhas  =  System.IO.File.ReadAllLines(PATH); 

            foreach(string linha in linhas){
                // Separamos os dados de cada linha com o Split()
                // [0] = codigo = 1
                // [1] = nome = Fender
                // [2] = preco = 5500
                string[] dado = linha.Split(";");

                // Criamos instâncias de produtos para serem colocados na lista 
                Produto prod = new Produto();
                prod.Codigo  = Int32.Parse(Separar(dado[0]));
                prod.Nome    = Separar(dado[1]);
                prod.Preco   = float.Parse( Separar(dado[2]));

                produtos.Add(prod);
            }
            

            return produtos;
        }

        // public void Buscar(string _nome, int _cod){
        //     Ler().Find(x => x.Codigo == _cod).Nome = _nome;
        // }

        /// <summary>
        /// Serve para separar os itens por colunas
        /// </summary>
        /// <param name="_coluna">serve para separar os itens do .csv</param>
        /// <returns>valor de indice [1] da coluna</returns>
        private string Separar(string _coluna){
            return _coluna.Split("=")[1];
        }
        // 1; Celular; 600;

        /// <summary>
        /// Prepara as linhas
        /// </summary>
        /// <param name="p">itens colocados no console</param>
        /// <returns>itens</returns>
        private string PrepararLinha(Produto p){
          return   $"Codigo = {p.Codigo}; Nome = {p.Nome}; Preço = {p.Preco}";
        }
    }
}