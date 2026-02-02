using System;

namespace ERPModular.Core.Interfaces;

/// <summary>
/// Interface responsável por fornecer o ID da empresa (Tenant) atual.
/// No futuro, pegaremos isso do JWT ou da sessão do usuário logado.
/// </summary>
public interface ITenantProvider
{
    Guid GetTenantId();
    Guid GetCompanyId();
    string GetDomain();
}
