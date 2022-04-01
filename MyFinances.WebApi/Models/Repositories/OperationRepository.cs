using Microsoft.EntityFrameworkCore;
using MyFinances.WebApi.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinances.WebApi.Models.Repositories
{
    public class OperationRepository
    {
        private readonly MyFinancesContext _dbContext;
        public OperationRepository(MyFinancesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Operation>> GetOperations()
        {
            return await _dbContext.Operations.ToListAsync();
        }

        public async Task<IEnumerable<Operation>> GetOperationsbyPage(int records, int pageNo)
        {
            int skipRecordsNo = (pageNo - 1) * records;
            return await _dbContext.Operations.OrderBy(x => x.Id).Skip(skipRecordsNo).Take(records).ToListAsync();
        }

        public async Task<Operation> GetOperation(int id)
        {
            return await _dbContext.Operations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddOperation(Operation operation)
        {
            operation.CreatedDate = DateTime.Now;
            await _dbContext.Operations.AddAsync(operation);
        }

        public async Task UpdateOperation(Operation operation)
        {
            var operationToUpdate = await _dbContext.Operations.FirstOrDefaultAsync(x => x.Id == operation.Id);
            operationToUpdate.Name = operation.Name;
            operationToUpdate.Description = operation.Description;
            operationToUpdate.Value = operation.Value;
            operationToUpdate.CategoryId = operation.CategoryId;
        }

        public async Task RemoveOperation(int id)
        {
            var operationToRemove = await _dbContext.Operations.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Operations.Remove(operationToRemove);
        }
    }
}
