namespace ERPModular.Shared.Domain.Interfaces;

/// <summary>
/// Interface para abstração de envio de mensagens do sistema (E-mails, Notificações).
/// </summary>
public interface IEmailSender
{
    /// <summary>
    /// Envia um e-mail de forma assíncrona.
    /// </summary>
    /// <param name="email">Endereço de destino</param>
    /// <param name="subject">Assunto</param>
    /// <param name="htmlMessage">Conteúdo da mensagem (HTML)</param>
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}
