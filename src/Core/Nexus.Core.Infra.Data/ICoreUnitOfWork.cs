using Goal.Domain;
using Nexus.Core.Domain.Accounts.Aggregates;

namespace Nexus.Core.Infra.Data;

public interface ICoreUnitOfWork : IUnitOfWork
{
    IAccountRepository Accounts { get; }
    IFinancialInstitutionRepository FinancialInstitutions { get; }
    //ITransactionCategoryRepository TransactionCategories { get; }
    //ITransactionPaymentMethodRepository TransactionPaymentMethods { get; }
    //ITransactionRepository Transactions { get; }
    //ITransactionSubCategoryRepository TransactionSubCategories { get; }
}
