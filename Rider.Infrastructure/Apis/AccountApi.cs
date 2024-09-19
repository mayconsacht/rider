using Microsoft.AspNetCore.Http.HttpResults;
using Rider.CrossCutting.DTO.Account;

namespace Rider.Infrastructure.Apis;

public static class AccountApi
{
    public static RouteGroupBuilder MapAccountApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/accounts");
        
        api.MapGet("{accountId:guid}", GetAccountAsync);
        api.MapPost("/", CreateAccountAsync);

        return api;
    }

    public static async Task<Results<Ok<AccountDto>, NotFound>> GetAccountAsync(
        Guid selectionId,
        [AsParameters] AccountServices services)
    {
        // Soon to be a direct query
        var account = await services.GetAccount.Execute(selectionId);

        if (account is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(account);
    }
    
    public static async Task<Results<Created<Guid?>, BadRequest<string>>> CreateAccountAsync(
        AccountNoIdDto request,
        [AsParameters] AccountServices services)
    {
        var failedMessage = "Create account failed";
    
        var result = await services.Signup.Execute(request);
        if (result == Guid.Empty)
        {
            services.Logger.LogWarning(failedMessage);
            return TypedResults.BadRequest(failedMessage);
        }
        services.Logger.LogInformation("Create account succeeded");

        return TypedResults.Created($"/api/accounts/{result}", result);
    }

}