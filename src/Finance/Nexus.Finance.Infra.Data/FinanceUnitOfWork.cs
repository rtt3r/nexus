using Goal.Infra.Data;
using Nexus.Finance.Domain.Accounts.Aggregates;
using Nexus.Finance.Domain.Transactions.Aggregates;

namespace Nexus.Finance.Infra.Data;

internal sealed class FinanceUnitOfWork(
    FinanceDbContext context,
    IAccountRepository accounts,
    IFinancialInstitutionRepository financialInstitutions)
    //ITransactionCategoryRepository transactionCategories,
    //ITransactionPaymentMethodRepository transactionPaymentMethods,
    //ITransactionRepository transactions,
    //ITransactionSubCategoryRepository transactionSubCategories)
    : UnitOfWork(context), IFinanceUnitOfWork
{
    public IAccountRepository Accounts { get; } = accounts;
    public IFinancialInstitutionRepository FinancialInstitutions { get; } = financialInstitutions;
    //public ITransactionCategoryRepository TransactionCategories { get; } = transactionCategories;
    //public ITransactionPaymentMethodRepository TransactionPaymentMethods { get; } = transactionPaymentMethods;
    //public ITransactionRepository Transactions { get; } = transactions;
    //public ITransactionSubCategoryRepository TransactionSubCategories { get; } = transactionSubCategories;
}
