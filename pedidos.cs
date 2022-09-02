using System;
using System.Collections.Generic;
//Reiniciando net caiu
public class Pedido{
    private int id;
    private DateTime data;
    private bool carrinho;
    public Cliente cliente;
    private int clienteId;
    private List<PedidoProduto> produtos = new List<PedidoProduto>();

    public int Id { get => id; set => id = value; }
    public DateTime Data { get => data; set => data = value; }
    public bool Carrinho { get => carrinho; set => carrinho = value; }
    public int ClienteId { get => clienteId; set => clienteId = value; }
    public List<PedidoProduto> Produtos { get => produtos; set => produtos = value; }
    public Pedido(){ }
    
    //Construtor
    public Pedido(DateTime data, Cliente cliente){
        this.data = data;
        this.carrinho = true;
        this.cliente = cliente;
        this.clienteId = cliente.id;
    }

    public void SetId(int id){
        this.id = id;
    }

    public void SetData(DateTime data){
        this.data = data;
    }
    
    public void SetCarrinho(bool carrinho){
        this.carrinho = carrinho;
    }

    public void SetCliente(Cliente cliente){
        this.cliente = cliente;
        this.clienteId = cliente.id;
    }

    public int GetId(){
        return id;
    }

    public DateTime GetData(){
        return data;
    }
    
    public bool GetCarrinho(){
        return carrinho;
    }

    public Cliente GetCliente(){
        return cliente;
    }

    public List<PedidoProduto> ProdutoListar() {
        //Retorna Lista
        return produtos;
    }
        
    //Verifica se um produto já existe no carrinho
    private PedidoProduto ProdutoContar(Produto produto){
        foreach(PedidoProduto produto in produtos){
            if(produto.GetProduto() == produto){
                return produto;
            }
        } 
        return null;
    }
    
    //Inserção de produto no carrinho
    public void ProdutoInserir(int qtd, Produto produto){
        PedidoProduto produto = ProdutoContar(produto);
        if(produto == null){
            //Se o produto a ser adicionado ainda não está no carrinho
            produto = new PedidoProduto(qtd, produto);
                produtos.Add(produto);
        }
        else{
            //Se o produto a ser adicionado já existe no carrinho
            produto.SetQtd(produto.GetQtd() + qtd);
        }
    }

    public void ProdutosExcluir(){
        //Remove todos os produtos do carrinho
        produtos.Clear();
    }

    public override string ToString(){
        //Ainda está comprando
        if(carrinho){ 
            return data.ToString("dd/MM/yyyy") + "\n" + "Carrinho " + id + " de " + cliente.nome + ":";
            // 10/02/2022
            // Carrinho 1 de Fulanin
        }

        //Já finalizou a compra
        else{ 
            return data.ToString("dd/MM/yyyy") + "\n" + "Compra " + id + " de " + cliente.nome + ":";
            // 10/02/2022
            // Compra 1 de Fulanin
        }
    }
    
    /*
    public void ProdutoExcluir(){
        foreach(Produto produto in produtos){  
    }
    */
}