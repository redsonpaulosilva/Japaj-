using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Linq;

class MainClass{
    public static bool primeiro_acesso = true;
    private static produto produto = produto.Singleton;
    private static cliente cliente = cliente.Singleton;
    private static pedido pedido = pedido.Singleton;
    
    private static Cliente clienteLogin = null;
    private static Pedido clientePedido = null;
    
    public static void Main(){     
        Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

        try{
            produto.Abrir();
            cliente.Abrir();
            pedido.Abrir();
        }
        catch(Exception){
            Console.WriteLine("Um dos arquivos para salvar as informações ainda será criado!");
        }

        Console.WriteLine("");
        Console.WriteLine("Bem vindo ao Japaja!");
        Console.WriteLine("");
        int operacao = -1;
        int perfil = 0;
        while (operacao != 0){
            try{
                if (perfil == 0){
                    operacao = -1;
                    perfil = MenUsuario();
                    if (perfil == 0){
                        produto.Salvar();
                        cliente.Salvar();
                        pedido.Salvar();
                        break;
                    }

                    else if (perfil != 0 && perfil != 1 && perfil != 2){
                        perfil = 0;
                        Console.WriteLine("A opção informada não é válida!");
                    }
                }

                if (perfil == 1){
                    operacao = MenuFuncionario();
                    switch(operacao){
                        case 0: operacao = 0; break;
                        case  1: ProdutoListar(); break;
                        case  2: ProdutoInserir(); break;
                        case  3: ProdutoAtualizar(); break;
                        case  4: ProdutoExcluir(); break;
                        case  5: ClienteListar(); break;
                        case  6: ClienteInserir(); break;
                        case  7: ClienteAtualizar(); break;
                        case  8: ClienteExcluir(); break;
                        case  9: PedidoListar(); break;
                        case 99: perfil = 0; break;
                        default:
                            Console.WriteLine("A opção informada não é válida!"); break;
                    }
                }
            
                //Perfil do cliente sem login feito
                if (perfil == 2 && clienteLogin == null){
                    operacao = MenuClienteLogin();
                    switch(operacao){
                        case  1:  ClienteLogin(); primeiro_acesso = true; break;
                        case  99: perfil = 0; break;
                    }
                }

                //Perfil do cliente com login feito
                if (perfil == 2 && clienteLogin != null){
                    if (primeiro_acesso){ 
                        Console.WriteLine("");
                        Console.WriteLine("Bem vindo(a), " + clienteLogin.nome + "!");
                        primeiro_acesso = false;
                    }

                    operacao = MenuClienteLogout();
                    switch(operacao){
                        case 0: perfil = 0; break;
                        case  1: ClienteProdutoListar(); break;
                        case  2: ClienteProdutoInserir(); break;
                        case  3: ClienteCarrinhoVisualizar(); break;
                        case  4: ClienteCarrinhoComprar(); break;
                        case  5: ClienteCarrinhoEsvaziar(); break;
                        case  6: ClientePedidoListar(); break;

                        case 99: ClienteLogout(); break;
                    }
                }
            }
        
            catch (Exception){
                Console.WriteLine("A opção informada não é válida!");
                Console.WriteLine("");
            }
        }
    }

    
    public static int MenUsuario(){
        Console.WriteLine("----------- Opções Disponíveis -----------");
        Console.WriteLine("01 - Entrar como Funcionario");
        Console.WriteLine("02 - Entrar como Cliente");
        Console.WriteLine("00 - Sair");
        Console.WriteLine("");
        Console.Write("Informe a operação: ");
        int operacao = int.Parse(Console.ReadLine());
        Console.WriteLine("");
        return operacao;
    }

    ////////////////////////////// VENDEDOR /////////////////////////////

    public static int MenuVendedor(){
        Console.WriteLine("----------- Opções Disponíveis -----------");
        Console.WriteLine("00 - Encerrar Operações");
        Console.WriteLine("01 - Listar Produto");
        Console.WriteLine("02 - Inserir Produto");
        Console.WriteLine("03 - Atualizar Produto");
        Console.WriteLine("04 - Excluir Produto");
        Console.WriteLine("05 - Listar Cliente");
        Console.WriteLine("06 - Inserir Cliente");
        Console.WriteLine("07 - Atualizar Cliente");
        Console.WriteLine("08 - Excluir Cliente");
        Console.WriteLine("09 - Listar Pedidos");
        Console.WriteLine("10 - Voltar");
        Console.WriteLine("");
        ncategoria.Salvar();
        nproduto.Salvar();
        ncliente.Salvar();
        nfeedback.Salvar();
        nvenda.Salvar();
        Console.Write("Informe a operação: ");
        int operacao = int.Parse(Console.ReadLine());
        Console.WriteLine("");
        return operacao;
    }

    ////////////////////////////// CLIENTE /////////////////////////////

    //Login
    public static int MenuClienteLogin(){
        Console.WriteLine("----------- Opções Disponíveis -----------");
        Console.WriteLine("01 - Realizar Login");
        Console.WriteLine("99 - Voltar");
        Console.WriteLine("");
        Console.Write("Informe a operação: ");
        int operacao = int.Parse(Console.ReadLine());
        Console.WriteLine("");
        return operacao;
    }

    //Logout
    public static int MenuClienteLogout(){
        Console.WriteLine("");
        Console.WriteLine("----------- Escolha uma opção -----------");
        Console.WriteLine("00 - Encerrar Operações");
        Console.WriteLine("01 - Visualizar produtos disponíveis");
        Console.WriteLine("02 - Inserir produto no carrinho");
        Console.WriteLine("03 - Visualizar carrinho de compras");
        Console.WriteLine("04 - Confirmar compra");
        Console.WriteLine("05 - Esvaziar carrinho");
        Console.WriteLine("06 - Visualizar minhas compras");
        Console.WriteLine("07 - Enviar feedback");
        Console.WriteLine("99 - Realizar logout");
        Console.WriteLine("");
        ncategoria.Salvar();
        nproduto.Salvar();
        ncliente.Salvar();
        nfeedback.Salvar();
        nvenda.Salvar();
        Console.Write("Informe a operação: ");
        int operacao = int.Parse(Console.ReadLine());
        Console.WriteLine("");
        return operacao;
    }

    ////////////////////////////// PRODUTO /////////////////////////////

    //Método Listar Produto
    public static void ProdutoListar(){
        Console.WriteLine("----------- Listando Produtos -----------");
         List<Produto> l_produtos = nproduto.Listar();
        if (l_produtos.Count == 0){
            Console.WriteLine("Nenhum produto cadastrado.");
            Console.WriteLine("");
            return;
        }

        for (int i = 0; i < l_produtos.Count; i++){
            Console.WriteLine(l_produtos[i]);
            Console.WriteLine("");
        }
    }

    //Método Inserir Produto
    public static void ProdutoInserir(){
        Console.WriteLine("----------- Inserindo Produtos -----------");

        //Descrição do produto
        Console.Write("Informe uma descrição para o produto: ");
        string descricao = Console.ReadLine();

        //Fabricante
        Console.Write("Informe o fabricante do produto: ");
        string fabricante = Console.ReadLine();
        
        //Quantidade de produtos
        Console.Write("Informe o estoque do produto: ");
        int qtd = int.Parse(Console.ReadLine());

        //Valor do produto
        Console.Write("Informe um valor para o produto: ");
        double valor = double.Parse(Console.ReadLine());
        Console.WriteLine("");

        //Instanciar classe de Produto (criando um novo produto)
        Produto produto = new Produto(descricao, qtd, valor);

        //Inserção do produto na lista de produtos 
        nproduto.Inserir(produto);  

        //Mensagem de confirmação
        Console.WriteLine($"Produto {descricao} adicionado com sucesso na categoria {categoria.GetDescricao()}.");
        Console.WriteLine("");
    }

    //Método Atualizar Produto
    public static void ProdutoAtualizar(){
        Console.WriteLine("----------- Atualizando Produto -----------");
        ProdutoListar();

        //Id
        Console.Write("Informe o Id do produto que deseja atualizar: "); 
        int id = int.Parse(Console.ReadLine());

        //Descrição do produto
        Console.Write("Informe uma descrição para o produto: ");
        string descricao = Console.ReadLine();

        //Fabricante
        Console.Write("Informe o fabricante do produto: ");
        string fabricante = Console.ReadLine();
        
        //Quantidade de produtos
        Console.Write("Informe o estoque do produto: ");
        int qtd = int.Parse(Console.ReadLine());

        //Valor do produto
        Console.Write("Informe um valor para o produto: ");
        double valor = double.Parse(Console.ReadLine());
        Console.WriteLine("");
        
        //Instanciar classe de Produto (criando um novo produto)
        Produto produto = new Produto(id, descricao, fabricante, qtd, valor);

        //Atualização do produto na lista de produtos 
        nproduto.Atualizar(produto);  

        //Mensagem de confirmação
        Console.WriteLine($"Produto {descricao} atualizado com sucesso!");
        Console.WriteLine("");
    }

    //Método Excluir Produto
    public static void ProdutoExcluir(){
        Console.WriteLine("----------- Excluindo Produto -----------");
        ProdutoListar();
        
        //Id
        Console.Write("Informe o Id do produto que deseja excluir: "); 
        int id = int.Parse(Console.ReadLine());

        //Procurando o produto pelo id
        Produto produto = nproduto.Listar(id);

        //Mensagem de confirmação
        Console.WriteLine("");
        Console.WriteLine($"Produto {produto.GetDescricao()} excluído com sucesso!");
        Console.WriteLine("");

        //Exclui o produto
        nproduto.Excluir(produto);
    }

    /////////////////////////////// CLIENTE ///////////////////////////////

//---------------------------- AÇÃO DO VENDEDOR ----------------------------

    //Método Listar Clientes
    public static void ClienteListar(){
        Console.WriteLine("----------- Listando Clientes -----------");
        List<Cliente> l_clientes = ncliente.Listar();
        if (l_clientes.Count == 0){
            Console.WriteLine("Nenhum cliente cadastrado.");
            Console.WriteLine("");
            return;
        }

        for (int i = 0; i < l_clientes.Count; i++){
            Console.WriteLine(l_clientes[i]);
            Console.WriteLine("");
        }
    }

    //Método Inserir Cliente
    public static void ClienteInserir(){
        Console.WriteLine("----------- Inserindo Clientes -----------");
                
        //Nome do cliente
        Console.Write("Informe o nome do cliente: ");
        string Nome = Console.ReadLine();

        //Data de nascimento do cliente
        Console.Write("Informe a data de nascimento do cliente (dd/mm/aaaa): ");
        DateTime Nascimento = DateTime.Parse(Console.ReadLine());

        //Contato do cliente
        Console.Write("Informe o contato do cliente: ");
        string Contato = Console.ReadLine();

        //Endereço do cliente
        Console.Write("Informe a rua do cliente: ");
        string Endereco = Console.ReadLine();

        //Estado do cliente
        Console.Write("Informe o estado do cliente (XX): ");
        string Estado = Console.ReadLine();
        Console.WriteLine("");
            
        //Instanciar classe de Cliente (criando um novo cliente)
        Cliente cliente = new Cliente{nome = Nome,contato = Contato, endereco = Endereco};

        //Inserção de cliente na lista de clientes 
        ncliente.Inserir(cliente);

        //Mensagem de confirmação da operação
        Console.WriteLine($"Cliente {Nome} cadastrado com sucesso!");
        Console.WriteLine("");
    }

    //Método Atualizar Cliente
    public static void ClienteAtualizar(){
        Console.WriteLine("----------- Atualizando Cliente -----------");
        ClienteListar();
        
        //Id do Cliente
        Console.Write("Informe o Id do cliente que deseja atualizar: ");
        int Id = int.Parse(Console.ReadLine());
        
        //Nome do cliente
        Console.Write("Informe novamente o nome do cliente: ");
        string Nome = Console.ReadLine();

        //Contato do cliente
        Console.Write("Informe novamente o contato do cliente: ");
        string Contato = Console.ReadLine();

        //Endereço do cliente
        Console.Write("Informe novamente a rua do cliente: ");
        string Endereco = Console.ReadLine();

        //Instanciar classe de Cliente (criando um novo cliente)
        Cliente cliente = new Cliente{id = Id, nome = Nome,, contato = Contato, endereco = Endereco};

        //Atualização do cliente na lista de clientes 
        ncliente.Atualizar(cliente);

        //Mensagem de confirmação
        Console.WriteLine($"Cliente {Nome} atualizado com sucesso!");
        Console.WriteLine("");
    }

    //Método Excluir Cliente
    public static void ClienteExcluir(){
        Console.WriteLine("----------- Excluindo Cliente -----------");
        ClienteListar();
        
        //Id do Cliente
        Console.Write("Informe o Id do cliente que deseja excluir: "); 
        int Id = int.Parse(Console.ReadLine());

        //Procurando o cliente
        Cliente cliente = cliente.Listar(Id);

        //Mensagem de confirmação
        Console.WriteLine($"Cliente {cliente.nome} excluído com sucesso!");
        Console.WriteLine("");

        //Exclui a categoria
        ncliente.Excluir(cliente);
    }

    //Listando vendas
    public static void PedidoListar(){
        Console.WriteLine("----------- Visualizando Pedidos -----------");
        //Lista todas as pedidos de um cliente
        List<Pedido> pedidos = pedido.Listar();
        if(pedidos.Count == 0){ //Se o cliente nunca realizou uma venda
            Console.WriteLine("Nenhum pedido cadastrado.");
            Console.WriteLine("");
            return;
        }
        
        foreach(Pedido v in pedidos){ //Mostra todos os pedidos realizados
            Console.Write(v);
            foreach (pedidoproduto produto in npedido.ProdutoListar(v)){ //Mostra todos os produtos de cada pedido
                Console.WriteLine("\n" + produto);
            }
            Console.WriteLine("");
        }

        var resposta1 = pedidos.Select(v => new{
            Cliente = v.cliente.nome,
            Total = v.Produtos.Sum(vi => vi.Qtd * vi.Valor)
        });

        Console.WriteLine(" ----------- Resumo do valor total gasto por cliente: -----------");
        var resposta2 = resposta1.GroupBy(produto => produto.Cliente,
        (chave, produtos) => new{
            Cliente = chave,
            Total = produtos.Sum(produtos => produto.Total)
        });

        double valor_total = 0;
        foreach(var produto in resposta2){
            valor_total += produto.Total;
            Console.WriteLine(produto);
        }
        
        Console.WriteLine($"\nValor total de todos os pedidos: R$ {valor_total:0.00}\n");
    }    
----------------------- AÇÃO DO CLIENTE ------------------------------

    //Login do Cliente
    public static void ClienteLogin(){
        Console.WriteLine("----------- Realizando Login -----------");
        ClienteListar();
        Console.Write("Informe o Id do cliente para logar: ");
        int Id = int.Parse(Console.ReadLine());
        Console.WriteLine("");
        clienteLogin = ncliente.Listar(Id);
        clientePedido = pedido.ListarCarrinho(clienteLogin); //Abre o carrinho ao fazer login
    }

    //Logout do Cliente
    public static void ClienteLogout(){
        Console.WriteLine("----------- Realizando Logout -----------");
        if(clientePedido != null){
            pedido.Inserir(clientePedido, true); //Guarda a compra ainda em andamento.
        }
        clienteLogin = null;
        clientePedido = null;
    }

    //Listando Produtos
    public static void ClienteProdutoListar(){
        Console.WriteLine("----------- Visualizando Produtos -----------");
        //Lista os produtos cadastrados no siste
        ProdutoListar();
    }

    //Inserir Produto
    public static void ClienteProdutoInserir(){
        Console.WriteLine("----------- Inserindo Produto -----------");
        //Lista os produtos já cadastrados
        ProdutoListar();
        
        //Id do Produto
        Console.Write("Informe o Id do produto que deseja comprar: ");
        int Id = int.Parse(Console.ReadLine());
        
        //Quantidade do produto específico
        Console.Write("Informe quantas unidades do produto: ");
        int Qtd = int.Parse(Console.ReadLine());
        Console.WriteLine("");
        
        //Procurando o produto pelo id
        Produto produto = nproduto.Listar(Id);

        //Verificação se o produto informado realmente existe
        if (produto != null){ 
            if(clientePedido == null) { //Se o carrinho ainda não existe
                clientePedido = new Pedido(DateTime.Now, clienteLogin);
            }
            //Insere o produto no carrinho
            pedido.ProdutoInserir(clienteVenda, Qtd, produto);
        }
        Console.WriteLine($"Produto {produto.GetDescricao()} inserido no carrinho com sucesso!");
        Console.WriteLine("");
    }

    //Visualizando Carrinho
    public static void ClienteCarrinhoVisualizar(){
        Console.WriteLine("----------- Visualizando Carrinho -----------");
        //Verifica se existe um carrinho de compra
        if(clientePedido == null){
            Console.WriteLine("O carrinho está vazio!");
            return;
        }

        //Lista os produtos inseridos do carrinho
        List<PedidoProduto> produtos = pedido.ProdutoListar(clientePedido);
        foreach(PedidoProduto produto in itens){
            Console.WriteLine(produto);
            Console.WriteLine("");
        }
    }
    
    //Finalizando uma compra
    public static void ClienteCarrinhoComprar(){
        Console.WriteLine("----------- Finalizando Compra -----------");
        if(clientePedido == null){ //
            Console.WriteLine("O carrinho está vazio!"); //Não posso finalizar a compra com o carrinho vazio.
            return;
        }
        //Armazena a compra do cliente
        pedido.Inserir(clientePedido, false);
        
        //Disponibiliza um novo carrinho para o cliente
        clientePedido = null;

        Console.WriteLine("Compra finalizada com sucesso! =)");
    }

    //Esvaziando um carrinho
    public static void ClienteCarrinhoEsvaziar(){
        Console.WriteLine("----------- Esvaziando Carrinho -----------");
        //Verificando se existe um carrinho para esvaziar
        if(clientePedido != null){
            pedido.ItensExcluir(clientePedido);
            clientePedido = null;
            Console.WriteLine("Carrinho esvaziado com sucesso!");
        }
        else{
            Console.WriteLine("O carrinho está vazio!");
        }
    }

    public static void ClientePedidoListar(){
        Console.WriteLine("----------- Visualizando Compras -----------");
        //Armazena todas as compras de um cliente
        List<Pedido> pedido = Pedido.Listar(clienteLogin);
        
        if(pedidos.Count == 0){ //Se o cliente nunca realizou uma compra
            Console.WriteLine("Nenhuma compra cadastrada.");
            return;
        }
        
        foreach(Pedido v in pedidos){ //Mostra todas as vendas de um cliente
            Console.Write(v);
            
            foreach (PedidoProduto produto in pedido.ProdutoListar(v)){ //Mostra todos os itens de cada venda
                Console.WriteLine("\n" + produto);
            }
            Console.WriteLine("");
        }
    } 
  }

}