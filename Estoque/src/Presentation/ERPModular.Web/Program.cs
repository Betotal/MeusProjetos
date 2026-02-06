using ERPModular.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ========================================
// CONFIGURAÇÃO DE SERVIÇOS COMPARTILHADOS
// ========================================
// NOTA: A configuração completa de DbContext e serviços será feita na Fase 2
// quando implementarmos o módulo de Identity e as tabelas do schema 'shared'

// TODO (Fase 2): Adicionar DbContext
// TODO (Fase 2): Registrar IExecutionContextAccessor
// TODO (Fase 2): Registrar TenantInterceptor
// TODO (Fase 2): Configurar Identity com JWT

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
