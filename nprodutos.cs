using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Linq;

class Nproduto {
    private Nproduto(){}
    static Nproduto nprod_obj = new Nproduto();
    public static Nproduto Singleton{ get => nprod_obj; }

    private List<Produto> produtos = new List<Produto>();

    //Abrindo um arquivo de dados com as categorias
    public void Abrir(){
        Arquivo <List<Produto>> arquivo_prod = new Arquivo <List<Produto>>();
        produtos = arquivo_prod.Abrir("projeto poo/produtos.xml");
    }

    //Salvando as categorias cadastradas em um arquivo xml
    public void Salvar(){
        Arquivo <List<Produto>> arquivo_prod = new Arquivo <List<Produto>>();
        arquivo_prod.Salvar("projeto poo/produtos.xml", produtos);
    }

    //Método de inserir um produto no vetor de produtos
    public void Inserir(Produto produto){
        int maiorId = 0;
        maiorId = produtos.Max(maiorI => maiorI.GetId());
        produto.SetId(maiorId + 1);        
        produtos.Add(produto); 

    //Listando todos produtos
    public List<Produto> Listar(){
        produtos.Sort();
        return produtos;
    }

    //Listando um produto baseado em seu ID
    public Produto Listar(int id){
        return produtos.FirstOrDefault(prod => prod.GetId() == id);
    }

    //Atualizar Produto
    public void Atualizar(Produto produto){
        Produto atualProd = Listar(produto.GetId());
        if (atualProd == null){ //Não encontrou o ID na lista de categorias 
            return;
        }

        atualProd.SetDescricao(produto.GetDescricao());
        atualProd.SetQtd(produto.GetQtd());
        atualProd.SetValor(produto.GetValor());

    //Excluir Produto
    public void Excluir(Produto produto){
        //Remove a categoria da lista
        if (produto != null){
            produtos.Remove(produto);

}