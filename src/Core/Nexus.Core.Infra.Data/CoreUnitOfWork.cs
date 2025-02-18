using Goal.Infra.Data;
using Nexus.Core.Domain.Accounts.Aggregates;
using Nexus.Core.Domain.Transactions.Aggregates;

namespace Nexus.Core.Infra.Data;

internal sealed class CoreUnitOfWork(
    CoreDbContext context,
    IAccountRepository accounts,
    IFinancialInstitutionRepository financialInstitutions)
    //ITransactionCategoryRepository transactionCategories,
    //ITransactionPaymentMethodRepository transactionPaymentMethods,
    //ITransactionRepository transactions,
    //ITransactionSubCategoryRepository transactionSubCategories)
    : UnitOfWork(context), ICoreUnitOfWork
{
    public IAccountRepository Accounts { get; } = accounts;
    public IFinancialInstitutionRepository FinancialInstitutions { get; } = financialInstitutions;
    //public ITransactionCategoryRepository TransactionCategories { get; } = transactionCategories;
    //public ITransactionPaymentMethodRepository TransactionPaymentMethods { get; } = transactionPaymentMethods;
    //public ITransactionRepository Transactions { get; } = transactions;
    //public ITransactionSubCategoryRepository TransactionSubCategories { get; } = transactionSubCategories;
}
