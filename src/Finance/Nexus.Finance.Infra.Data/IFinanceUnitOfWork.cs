using Goal.Domain;
using Nexus.Finance.Domain.Accounts.Aggregates;

namespace Nexus.Finance.Infra.Data;

public interface IFinanceUnitOfWork : IUnitOfWork
{
    IAccountRepository Accounts { get; }
    IFinancialInstitutionRepository FinancialInstitutions { get; }
    //ITransactionCategoryRepository TransactionCategories { get; }
    //ITransactionPaymentMethodRepository TransactionPaymentMethods { get; }
    //ITransactionRepository Transactions { get; }
    //ITransactionSubCategoryRepository TransactionSubCategories { get; }
}
