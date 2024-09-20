using Account.CrossCutting.DTO.Account;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Account.Infrastructure.Apis;

public static class AccountApi
{
    public static RouteGroupBuilder MapAccountApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/accounts");
        
        api.MapGet("{accountId:guid}", GetAccountAsync);
        api.MapPost("/", CreateAccountAsync);

        return api;
    }

    public static async Task<Results<Ok<AccountDto>, NotFound, BadRequest<string>>> GetAccountAsync(
        Guid accountId,
        [AsParameters] AccountServices services)
    {
        if (accountId == Guid.Empty)
            return TypedResults.BadRequest("Id is not valid.");
        
        var account = await services.GetAccount.Execute(accountId);

        if (account is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(account);
    }
    
    public static async Task<Results<Created<Guid?>, BadRequest<string>>> CreateAccountAsync(
        AccountNoIdDto request,
        [AsParameters] AccountServices services)
    {
        var result = await services.Signup.Execute(request);
        if (result == Guid.Empty)
        {
            return TypedResults.BadRequest("Create account failed");
        }

        return TypedResults.Created($"/api/accounts/{result}", result);
    }
}