using MyFinances.WebApi.Models.Domains;
using MyFinances.WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MyFinances.WebApi.Models.Converters
{
    static class OperationConverter
    {
        public static OperationDto ToDto(this Operation operation)
        {
            return new OperationDto
            {
                Id = operation.Id,
                Name = operation.Name,
                Description = operation.Description,
                Value = operation.Value,
                CreatedDate = operation.CreatedDate,
                CategoryId = operation.CategoryId
            };
        }

        public static IEnumerable<OperationDto> ToDtos(this IEnumerable<Operation> operations)
        {
            if (operations == null)
                return Enumerable.Empty<OperationDto>();

            return operations.Select(x => x.ToDto());
        }

        public static Operation ToDao(this OperationDto operationDto)
        {
            return new Operation
            {
                Id = operationDto.Id,
                Name = operationDto.Name,
                Description = operationDto.Description,
                Value = operationDto.Value,
                CreatedDate = operationDto.CreatedDate,
                CategoryId = operationDto.CategoryId
            };
        }
    }
}
