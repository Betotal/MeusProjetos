using System;
using System.Linq;

namespace ERPModular.Shared.Infrastructure.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Formata um nome para exibição curta (máximo 2 palavras importantes).
    /// Ignora preposições (de, da, do, das, dos).
    /// Exemplo: "Pedro de Alcantara de Oliveira Bó" -> "Pedro Bó"
    /// </summary>
    public static string ToShortName(this string? name)
    {
        if (string.IsNullOrWhiteSpace(name)) return string.Empty;

        var prepositions = new[] { "de", "da", "do", "das", "dos" };
        
        var words = name.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Where(w => !prepositions.Contains(w.ToLower()))
                        .ToList();

        if (words.Count == 0) return name; // Fallback para casos estranhos
        if (words.Count == 1) return words[0];

        // Retorna a primeira e a última palavra processada
        return $"{words[0]} {words[words.Count - 1]}";
    }
}
