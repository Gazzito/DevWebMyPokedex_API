using System;  // Importa o namespace para funcionalidades básicas do sistema
using System.Collections.Generic;  // Importa o namespace para coleções genéricas
using System.Linq;  // Importa o namespace para funcionalidades de consultas LINQ
using System.Threading.Tasks;  // Importa o namespace para funcionalidades assíncronas

namespace MyPokedexAPI.Models  // Define o namespace para os modelos da aplicação
{
    // Define a enumeração Rarity que representa diferentes níveis de raridade
    public enum Rarity
    {
        Bronze,  // Representa a raridade Bronze
        Silver,  // Representa a raridade Prata
        Gold,    // Representa a raridade Ouro
        Platinum,// Representa a raridade Platina
        Diamond  // Representa a raridade Diamante
    }
}
