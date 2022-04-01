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

        public IEnumerable<Operation> GetOperations()
        {
            return _dbContext.Operations;
        }

        public IEnumerable<Operation> GetOperationsbyPage(int records, int pageNo)
        {
            int skipRecordsNo = (pageNo - 1) * records;
            return _dbContext.Operations.OrderBy(x => x.Id).Skip(skipRecordsNo).Take(records);
        }

        public Operation GetOperation(int id)
        {
            return _dbContext.Operations.FirstOrDefault(x => x.Id == id);
        }

        public void AddOperation(Operation operation)
        {
            operation.CreatedDate = DateTime.Now;
            _dbContext.Operations.Add(operation);
        }

        public void UpdateOperation(Operation operation)
        {
            var operationToUpdate = _dbContext.Operations.FirstOrDefault(x => x.Id == operation.Id);
            operationToUpdate.Name = operation.Name;
            operationToUpdate.Description = operation.Description;
            operationToUpdate.Value = operation.Value;
            operation.CategoryId = operation.CategoryId;
        }

        public void RemoveOperation(int id)
        {
            var operationToRemove = _dbContext.Operations.FirstOrDefault(x => x.Id == id);
            _dbContext.Operations.Remove(operationToRemove);
        }
    }
}
