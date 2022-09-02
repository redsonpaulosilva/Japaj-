using System;

public class Produto : IComparable<Produto> {
    //Atributos
    private int id;
    private int qtd;
    private string descricao;
    private double valor;

    //Propriedades e construtor sem parâmetros públicos necessários para a serialização
    public int Id {get => id; set => id = value;}
    public int Qtd {get => qtd; set => qtd = value;}
    public string Descricao {get => descricao; set => descricao = value;}
    public double Valor {get => valor; set => valor = value;}
    public Produto(){ }
    
    //Construtor 1
    public Produto(int id, string descricao, int qtd, double valor){
        this.id = id;
        this.descricao = descricao;
        this.qtd = qtd > 0 ? qtd : 0;
        this.valor = valor > 0 ? valor : 0;
    }

    //Construtor 2
    public Produto(int id, string descricao, string fabricante, int qtd, double valor, Categoria categoria) : this(id, descricao, fabricante, qtd, valor){
        this.categoria = categoria;
        this.categoriaId = categoria.GetId();
    }

    //Construtor 3
    public Produto(string descricao, string fabricante, int qtd, double valor, Categoria categoria){
        this.descricao = descricao;
        this.fabricante = fabricante;
        this.qtd = qtd > 0 ? qtd : 0;
        this.valor = valor > 0 ? valor : 0;
        this.categoria = categoria;
        this.categoriaId = categoria.GetId();
    }

    //Sets
    public void SetId(int id){
        this.id = id;
    }

    public void SetDescricao(string descricao){
        this.descricao = descricao;
    }

    public void SetQtd(int qtd){
        this.qtd = qtd > 0 ? qtd : 0;
    }

    public void SetValor(double valor){
        this.valor = valor > 0 ? valor : 0;
    }

    //Gets
    public int GetId(){
        return id;
    }

    public string GetDescricao(){
        return descricao;
    }
    
    public string GetFabricante(){
        return fabricante;
    }
    
    public int GetQtd(){
        return qtd;
    }

    public double GetValor(){
        return valor;
    }
 
    //Formatação 
    public override string ToString(){
        if (descricao == null){
            return "- " + fabricante + $"(Id: {id})" + "\n" + "  " + "Estoque: " + qtd + "\n" + "  " + "Valor: " + valor.ToString("R$ 0.00");
        }

}