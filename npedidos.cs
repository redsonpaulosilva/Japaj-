using System;
using System.Collections.Generic;
using System.Linq;

class Npedido {
    private Npedido(){}
    static Npedido npedido_obj = new Npedido();
    public static Npedido Singleton { get => npedido_obj; }

    //Lista com todos os pedidos cadastrados;
    private List<Pedido> pedidos = new List<Pedido>();    

    //Abrindo um arquivo de dados com os pedidos
    public void Abrir(){
        Arquivo <List<Pedido>> arquivo_pedido = new Arquivo <List<Pedido>>();
        pedidos = arquivo_pedido.Abrir("Projeto_POO_14/pedidos.xml");
        //Atualizando dados
        AtualizarCliente();
        AtualizarProduto();
    }

    private void AtualizarCliente(){
        //Percorre a lista de pedidos para procurar quem comprou
        foreach(Pedido pedido in pedidos){
            Cliente cliente = Ncliente.Singleton.Listar(pedido.ClienteId);
            if (cliente != null){
                pedido.SetCliente(cliente);
            }
        }
    }

    private void AtualizarProduto(){
        foreach(Pedido pedido in pedidos){ //Percorrendo todo o pedido
            foreach(PedidoItem pedidoI in pedido.ItemListar()){ //Percorrendo cada produto do pedido
                Produto produto = Nproduto.Singleton.Listar(pedidoI.ProdutoId);
                if (produto != null){
                    pedidoI.SetProduto(produto);
                }
            }
        }
    }
    
    //Salvando os pedidos cadastrados em um arquivo xml
    public void Salvar(){
        Arquivo <List<Pedido>> arquivo_pedido = new Arquivo <List<Pedido>>();
        arquivo_pedido.Salvar("projeto poo/pedidos.xml", pedidos);
    }
    
    //Retorna uma lista com todos os pedidos cadastrados.
    public List<Pedido> Listar(){
        return pedidos;
    }

    public List<Pedido> Listar(Cliente cliente){
        //Retorna uma lista contendo as compras já feitas pelo cliente. 
        return pedidos.Where(v => v.GetCliente() == cliente).ToList();
    }

    public Pedido ListarCarrinho(Cliente cliente) {
        foreach(Pedido v in pedidos){
            //Se achei o cliente procurado e ele já finalizou a compra do carrinho.
            if(v.GetCliente() == cliente && v.GetCarrinho()) {
                return v; //Retorno ao pedido.
            }
        }
        return null;
    }

    public void Inserir(Pedido pedido, bool carrinho){
        int maiorId = 0;
        maiorId = pedidos.Max(maiorI => maiorI.GetId());
        pedido.SetId(maiorId + 1);
        
        //Insere o novo pedido em pedidos
        pedidos.Add(pedido);
        
        //Define o atributo carrinho
        pedido.SetCarrinho(carrinho); 
        //Vai ser verdadeiro se ainda for um carrinho;
        //Vai ser falso se o carrinho se tornou uma pedido.
    }

    public List<PedidoItem> ItemListar(Pedido pedido){
        //Retorna os itens do pedido
        return pedido.ItemListar();
    }

    public void ItemInserir(Pedido pedido, int qtd, Produto produto){
        //Inserir um produto em um pedido
        pedido.ItemInserir(qtd, produto);
    }

    public void ItensExcluir(Pedido pedido){
        //Remover todos os itens do pedido
        pedido.ItensExcluir();   
    }
}