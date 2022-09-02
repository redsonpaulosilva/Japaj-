using System;

public class Cliente : IComparable<Cliente> {
    //Propriedades do Cliente
    public int id { get ; set; }
    public string nome { get; set; }
    public string contato { get; set; }
    public string endereco { get; set; }

    
    public int CompareTo(Cliente cliente) {
        return this.nome.CompareTo(cliente.nome);
    }

    public override string ToString(){ 
        return nome + $" (Id: {id})" + "\n" + "Contato: " + contato + "\n" + "Endereço: " + endereco + ",;
    }
}