using ERPModular.Shared.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace ERPModular.Shared.Infrastructure.Services;

/// <summary>
/// Implementação de depuração que loga os e-mails no console em vez de enviá-los de verdade.
/// </summary>
public class DebugEmailSender : IEmailSender
{
    private readonly ILogger<DebugEmailSender> _logger;

    public DebugEmailSender(ILogger<DebugEmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var output = $"""
            
            ========== [DEBUG EMAIL SENDER] ==========
            DESTINO: {email}
            ASSUNTO: {subject}
            MENNSAGEM:
            ------------------------------------------
            {htmlMessage}
            ------------------------------------------
            ==========================================
            
            """;

        _logger.LogInformation(output);
        
        // Também imprime no Console padrão para facilitar a visualização no terminal
        Console.WriteLine(output);

        return Task.CompletedTask;
    }
}
